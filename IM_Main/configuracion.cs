using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using IM_Main.Entidades;

namespace IM_Main
{
    public partial class configuracion : Form
    {
        public Repositorio icls_repositorio = new Repositorio();
        public configuracion()
        {
            InitializeComponent();
        }

        private void configuracion_Load(object sender, EventArgs e)
        {
            string ls_servidor_origen = "";
            string ls_base_origen = "";
            string ls_servidor_destino = "";
            string ls_base_destino = "";
            String ls_tabla_destino = "";

            int li_registros = 0;
            long ll_desde = 0;
            decimal ldec_frecuencia = 0;

            icls_repositorio.getConfiguracion(ref ldec_frecuencia, ref li_registros, ref ll_desde, ref ls_servidor_origen, ref ls_base_origen, ref ls_servidor_destino, ref ls_base_destino, ref ls_tabla_destino);

            txt_server_origen.Text = ls_servidor_origen;
            txt_base_origen.Text = ls_base_origen;

            txt_sever_destino.Text = ls_servidor_destino;
            txt_base_destino.Text = ls_base_destino;

            txt_fecuencia.Text = ldec_frecuencia.ToString();
            txt_registros.Text = li_registros.ToString();
            txt_desde.Text = ll_desde.ToString();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_grabar_Click(object sender, EventArgs e)
        {
            string ls_servidor_origen = "";
            string ls_base_origen = "";
            string ls_servidor_destino = "";
            string ls_base_destino = "";

            int li_registros = 0;
            long ll_desde = 0;
            decimal ldec_frecuencia = 0;


            ls_servidor_origen = txt_server_origen.Text;
            ls_base_origen = txt_base_origen.Text;
            ls_servidor_destino = txt_sever_destino.Text;
            ls_base_destino = txt_base_destino.Text;

            li_registros = Convert.ToInt32(txt_registros.Text);
            ll_desde = Convert.ToInt32(txt_desde.Text);
            ldec_frecuencia = Convert.ToDecimal(txt_fecuencia.Text) ;

            icls_repositorio.setConfiguracion(ldec_frecuencia, li_registros, ll_desde, ls_servidor_origen, ls_base_origen, ls_servidor_destino, ls_base_destino);

        }
    }
}
