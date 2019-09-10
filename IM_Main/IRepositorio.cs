using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IM_Main.Entidades;
using System.Data;

namespace IM_Main
{
    public interface IRepositorio
    {
        void iniciar();
        bool Conectar();
        void getConfiguracion(ref decimal ad_frecuecia, ref int ai_registros, ref long al_desde, ref string as_server_origen, ref string as_base_origen, ref string as_server_destino, ref string as_base_destino, ref string as_tabla_destino);
        List<Ticket> getTicketsxLote(long al_desde, int ai_registros);
        List<TicketDatos> GetTicketDatos(long al_id);
        DataTable ProcesarLista(List<Ticket> alst_tickets);
        List<Log_Proceso> procesarLote();
        void setUltimoRegistro(long al_ultimo_registro);
        void getUltimoRegistro(ref long al_ultimo_registro, ref string as_ultima_lectura);
        void setConfiguracion(decimal ad_frecuencia, int ai_registros, long al_desde, string as_server_origen, string as_base_origen, string as_server_destino, string as_base_destino);

    }
}
