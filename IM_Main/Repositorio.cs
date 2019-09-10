using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using IM_Main.Entidades;
using Dapper;

namespace IM_Main
{
    public class Repositorio:IRepositorio
    {
        public bool gbo_conectado = false;
        public string gs_conexion;
        public string gs_conexion_dest;
        public string gs_mensaje;
        public SqlConnection gsqlConnection;
        public SqlConnection gsqlConnection_dest;
        public IDbConnection gDb;
        public IDbConnection gDb_dest;
        public string gs_server_origen;
        public string gs_server_destino;
        public string gs_base_origen;
        public string gs_base_destino;
        public long gl_desde;
        public decimal gdec_frecuencia;
        public int gi_registros;
        public string gs_tabla_destino;
        // ESTO ES UN COMENTARIO
        public Repositorio()
        {
            /*Se llama a la función que recupera la configuración e inicia las varibles globales de la clase*/
            iniciar();
            //gbo_conectado = Conectar();
        }

        public void iniciar()
        {
            string ls_usuario = "infomanager";
            string ls_psw = "Sistec2555";
            String ls_usuario_destino = "sa";
            String ls_clave_destino = "gestion525";

            string ls_cnx = ConfigurationManager.ConnectionStrings["cnx_Sizer"].ToString();
            string ls_cnx_dest = ConfigurationManager.ConnectionStrings["cnx_Destino"].ToString();
            string cnxSizer = "";
            string cnxDestino = "";

            /*Levanto la configuracion del xml*/
            getConfiguracion(ref gdec_frecuencia, ref gi_registros, ref gl_desde, ref gs_server_origen, ref gs_base_origen, ref gs_server_destino, ref gs_base_destino, ref gs_tabla_destino);

            /*Recupera las cadenas de conexión y reeplaza los valores*/
            cnxSizer = string.Format(ls_cnx, ls_usuario, ls_psw, gs_server_origen, gs_base_origen);
            cnxDestino = string.Format(ls_cnx_dest, ls_usuario_destino, ls_clave_destino, gs_server_destino, gs_base_destino);

            // Asigno la cadena de conexion
            gs_conexion = cnxSizer;
            gs_conexion_dest = cnxDestino;

            /*Inicializa los objetos de conexión globales de la clase, en este caso es SQLServer*/
            gsqlConnection = new SqlConnection(gs_conexion);
            gsqlConnection_dest = new SqlConnection(gs_conexion_dest);

            /*Se asigna las cadenas de conexión al objeto de conección que utiliza el Dapper*/
            gDb = gsqlConnection;
            gDb_dest = gsqlConnection_dest;

        }

        public bool Conectar()
        {
            /*Función que realiza una conexión a la base de datos*/
            bool lbo_conectado = false;
            try
            {
                gsqlConnection = new SqlConnection(gs_conexion);
                gsqlConnection.Open();
                gs_mensaje = "";
            }
            catch (Exception ex)
            {
                gs_mensaje = ex.Message;
            }


            if (gsqlConnection.State == ConnectionState.Open)
                lbo_conectado = true;
            else
                lbo_conectado = false;

            return lbo_conectado;
        }

        public void getConfiguracion(ref decimal ad_frecuencia, ref int ai_registros, ref long al_desde, ref string as_server_origen, ref string as_base_origen, ref string as_server_destino, ref string as_base_destino, ref string as_tabla_destino)
        {
            /*Función que recupera los valores del archivo configuración*/
            XDocument xmlConfig = XDocument.Load("Configuracion.xml");

            var origen = from server in xmlConfig.Descendants("Configuracion") select server;

            foreach (XElement e in origen.Elements("ServidorOrigen"))
            {
                as_server_origen = e.Element("server").Value.ToString();
                as_base_origen = e.Element("database").Value.ToString();
            }

            foreach (XElement e in origen.Elements("ServidorDestino"))
            {
                as_server_destino = e.Element("server").Value.ToString();
                as_base_destino = e.Element("database").Value.ToString();
                as_tabla_destino = e.Element("tabla").Value.ToString();
            }

            foreach (XElement e in origen.Elements("ModoLectura"))
            {
                ad_frecuencia = Convert.ToDecimal(e.Element("frecuencia").Value.ToString());
                ai_registros = Convert.ToInt32(e.Element("registros").Value.ToString());
                al_desde = Convert.ToInt32(e.Element("ultimo").Value.ToString());
            }
        }

        public List<Ticket> getTicketsxLote(long al_desde, int ai_registros)
        {
            /*Función que recupera la lista de tickets a procesar*/
            string ls_sql_tickets = @"
                SELECT TOP (@registros) 
	                Id,
	                TicketIdPrefix,
	                TicketId,
	                BatchId,
	                OutletId,
	                FruitCount,
	                PackWeightDecigrams,
	                PackCompleteTime,
	                PrintTime,
	                Status,
	                ModifiedByUser,
	                CreatedByUser,
	                IsTransferred,
	                IsMerged,
	                MergedTicketId,
	                PrinterName,
	                Tag,
	                PrintedId
                FROM Ticket
                WHERE Id >= @desde";
            var parametros = new DynamicParameters();

            parametros.Add("@registros", value: ai_registros);
            parametros.Add("@desde", value: al_desde);

            return gDb.Query<Ticket>(ls_sql_tickets, parametros).ToList();
        }

        public List<TicketDatos> GetTicketDatos(long al_id)
        {
            /*Función que recupera la lista de valores de un ticket*/
            string ls_sql = @"
                SELECT
	                (t.Id) AS Id, 
	                (t.BatchId) AS BatchId, 
	                (t.PackWeightDecigrams) AS PackWeightDecigrams, 
	                (t.PrintTime) AS PrintTime, 
	                (tfn.Name) AS Name, 
	                (tfnv.Value) AS Value
                FROM Ticket AS t
	                INNER JOIN TicketFieldValue AS tfv ON tfv.TicketId = t.id
	                INNER JOIN TicketFieldNameValue AS tfnv ON tfnv.id = tfv.TicketFieldNameValueId
	                INNER JOIN TicketFieldName AS tfn ON tfn.Id = tfnv.TicketFieldNameId
                WHERE  t.id = @id
                ORDER BY t.id";
            var parametros = new DynamicParameters();

            parametros.Add("@id", value: al_id);
            return gDb.Query<TicketDatos>(ls_sql, parametros).ToList();
        }

        public DataTable ProcesarLista(List<Ticket> alst_tickets)
        {
            DataTable lds_tabla = new DataTable();
            DataRow ldw_fila;
            List<TicketDatos> lst_Datos = new List<TicketDatos>();
            string ls_campo = "";
            string ls_sql_insert = "INSERT INTO " + gs_tabla_destino + "{0} VALUES {1};";
            string ls_lista_campos = "";
            string ls_lista_param = "";
            string ls_error = "";

            IEnumerable<XElement> campos = XDocument.Load("Configuracion.xml", LoadOptions.None).Element("Configuracion").Element("TablaDestino").Descendants("campo");
            foreach (XElement campo in campos)
            {
                lds_tabla.Columns.Add(campo.Element("destino").Value.ToString());
                ls_lista_campos += string.Format("{0}, ",campo.Element("destino").Value.ToString());
                ls_lista_param += string.Format("@{0}, ", campo.Element("destino").Value.ToString());
            }

            ls_lista_campos = "(" + ls_lista_campos.Substring(0, ls_lista_campos.Length - 2) + ")";
            ls_lista_param = "(" + ls_lista_param.Substring(0, ls_lista_param.Length - 2) + ")"; ;

            ls_sql_insert = string.Format(ls_sql_insert, ls_lista_campos, ls_lista_param);

            foreach (Ticket t in alst_tickets)
            {
                long ll_id_ticket = t.Id;
                lst_Datos = GetTicketDatos(ll_id_ticket);

                ldw_fila = lds_tabla.NewRow();
                var consulta = (from c in lst_Datos
                                where c.Name.ToLower().Equals(ls_campo.ToLower())
                                select new
                                {
                                    c.Name,
                                    c.Value
                                }).Take(1);

                var param_insert = new DynamicParameters();

                foreach (XElement campo in campos)
                {
                    string ls_destino = "";
                    string ls_origen = "";
                    ls_destino = campo.Element("destino").Value.ToString();
                    ls_origen = campo.Element("origen").Value.ToString();

                    ls_campo = ls_origen;
                    foreach (var x in consulta)
                    {
                        ldw_fila[ls_destino] = x.Value.ToString();
                    }
                    param_insert.Add(string.Format("@{0}", ls_destino), value: ldw_fila[ls_destino].ToString());
                }

                try
                {
                    gDb_dest.Execute(ls_sql_insert, param_insert);
                } 
                catch(Exception ex)
                {
                    ls_error = ex.Message.ToString();
                }

                lds_tabla.Rows.Add(ldw_fila);

            }
            //DataRow ldw_fila;


            return lds_tabla;
        }

        public List<Log_Proceso> procesarLote()
        {
            List<Log_Proceso> lst_log = new List<Log_Proceso>();
            List<Ticket> lst_tickets = new List<Ticket>();
            List<TicketDatos> lst_Datos = new List<TicketDatos>();
            string ls_campo = "";
            string ls_sql_insert = "INSERT INTO " + gs_tabla_destino + "{0} VALUES {1};";
            string ls_lista_campos = "";
            string ls_lista_param = "";
            string ls_error = "";
            string ls_valor = "";
            string ls_fecha = "";
            string ls_detalle = "";
            long ll_ultimo = 0;

            /*Recupero el ultimo registro procesado desde el archivo de configuración*/
            getUltimoRegistro(ref gl_desde, ref ls_fecha);

            ls_detalle = "Inicio de proceso registro:{0}"; 
            lst_log.Add(new Log_Proceso {fecha = DateTime.Now, detalle = string.Format(ls_detalle, gl_desde.ToString())});

            /*Obtengo la lista de tickets a procesar*/
            lst_tickets = getTicketsxLote(gl_desde, gi_registros);

            int li_cantidad = (from item in lst_tickets
                                select Convert.ToInt32(item.Id.ToString())).Count();
            ls_detalle = "Cantidad de registros a procesar:{0}";
            lst_log.Add(new Log_Proceso { fecha = DateTime.Now, detalle = string.Format(ls_detalle, li_cantidad.ToString()) });

            if (li_cantidad > 0)
            {
                ll_ultimo = (from item in lst_tickets
                             select Convert.ToInt32(item.Id.ToString())).Max();
                ll_ultimo++;

                IEnumerable<XElement> campos = XDocument.Load("Configuracion.xml", LoadOptions.None).Element("Configuracion").Element("TablaDestino").Descendants("campo");
                foreach (XElement campo in campos)
                {
                    ls_lista_campos += string.Format("{0}, ", campo.Element("destino").Value.ToString());
                    ls_lista_param += string.Format("@{0}, ", campo.Element("destino").Value.ToString());
                }

                ls_lista_campos = "(" + ls_lista_campos.Substring(0, ls_lista_campos.Length - 2) + ")";
                ls_lista_param = "(" + ls_lista_param.Substring(0, ls_lista_param.Length - 2) + ")"; ;

                ls_sql_insert = string.Format(ls_sql_insert, ls_lista_campos, ls_lista_param);

                foreach (Ticket t in lst_tickets)
                {
                    long ll_id_ticket = t.Id;
                    lst_Datos = GetTicketDatos(ll_id_ticket);

                    //ldw_fila = lds_tabla.NewRow();
                    var consulta = (from c in lst_Datos
                                    where c.Name.ToLower().Equals(ls_campo.ToLower())
                                    select new
                                    {
                                        c.Name,
                                        c.Value
                                    }).Take(1);

                    var param_insert = new DynamicParameters();

                    foreach (XElement campo in campos)
                    {
                        string ls_destino = "";
                        string ls_origen = "";
                        ls_destino = campo.Element("destino").Value.ToString();
                        ls_origen = campo.Element("origen").Value.ToString();

                        ls_campo = ls_origen;
                        ls_valor = "";
                        foreach (var x in consulta)
                        {
                            ls_valor = x.Value.ToString();
                        }
                        param_insert.Add(string.Format("@{0}", ls_destino), value: ls_valor);
                    }
                    try
                    {
                        gDb_dest.Execute(ls_sql_insert, param_insert);
                    }
                    catch (Exception ex)
                    {
                        ls_error = ex.Message.ToString();
                        ls_detalle = "Error:{0}";
                        lst_log.Add(new Log_Proceso { fecha = DateTime.Now, detalle = string.Format(ls_detalle, ls_error) });
                    }

                    //lds_tabla.Rows.Add(ldw_fila);

                }
                ls_detalle = "Fin proceso";
                lst_log.Add(new Log_Proceso { fecha = DateTime.Now, detalle = ls_detalle});

                setUltimoRegistro(ll_ultimo);
            }

            return lst_log;
        }

        public void setUltimoRegistro(long al_ultimo_registro)
        {
            XElement xmlConfiguracion = XElement.Load("Configuracion.xml");
            IEnumerable<XElement> ultima_lectura = (from c in xmlConfiguracion.Elements("ModoLectura").Elements()
                                                    where (c.Name == "ultimo")
                                                    select c).Take(1);
            foreach (XElement x in ultima_lectura)
            {
                x.Value = al_ultimo_registro.ToString();
                x.Attribute("fecha").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            xmlConfiguracion.Save("Configuracion.xml");

        }
        public void getUltimoRegistro(ref long al_ultimo_registro, ref string as_ultima_lectura)
        {
            XElement xmlConfiguracion = XElement.Load("Configuracion.xml");
            IEnumerable<XElement> ultima_lectura = (from c in xmlConfiguracion.Elements("ModoLectura").Elements()
                                                    where (c.Name == "ultimo")
                                                    select c).Take(1);
            foreach (XElement x in ultima_lectura)
            {
                al_ultimo_registro = Convert.ToInt32(x.Value.ToString());
                as_ultima_lectura = x.Attribute("fecha").Value.ToString();
            }

        }
        public void setConfiguracion(decimal ad_frecuencia, int ai_registros, long al_desde, string as_server_origen, string as_base_origen, string as_server_destino, string as_base_destino)
        {
            XDocument xmlConfig = XDocument.Load("Configuracion.xml");

            var origen = from server in xmlConfig.Descendants("Configuracion") select server;

            foreach (XElement e in origen.Elements("ServidorOrigen"))
            {
                e.Element("server").Value = as_server_origen;
                e.Element("database").Value = as_base_origen;
            }

            foreach (XElement e in origen.Elements("ServidorDestino"))
            {
                e.Element("server").Value = as_server_destino;
                e.Element("database").Value = as_base_destino;
            }

            foreach (XElement e in origen.Elements("ModoLectura"))
            {

                e.Element("frecuencia").Value = ad_frecuencia.ToString();
                e.Element("registros").Value = ai_registros.ToString();
                e.Element("ultimo").Value = al_desde.ToString();
            }

            xmlConfig.Save("Configuracion.xml");

        }

    }

}
