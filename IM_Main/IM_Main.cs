using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IM_Main.Entidades;

namespace IM_Main
{
    public partial class IM_Main : Form
    {
        public int gi_registros = 10;
        public int gi_desde = 100;
        public Repositorio l_repositorio = new Repositorio();
        public List<Ticket> lst_tickets;
        public List<TicketDatos> lst_tickets_datos;
        static Timer myTimer = new System.Windows.Forms.Timer();

        public IM_Main()
        {
            InitializeComponent();
        }

        #region Eventos de ventana
        private void IM_Main_Load(object sender, EventArgs e)
        {
            uf_set_ultima_lectura();
        }
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            List<Log_Proceso> lst_log = l_repositorio.procesarLote();

            foreach(Log_Proceso l in lst_log)
            {
                dgv_tickets.Rows.Add(l.fecha.ToString("yyyy/MM/dd HH:mm:ss"), l.detalle);
            }
            uf_set_ultima_lectura();
        }
        #endregion

        #region funciones de ventana
        private void uf_set_ultima_lectura()
        {
            long ll_desde = 0;
            string ls_ultima_lectura = "";

            l_repositorio.getUltimoRegistro(ref ll_desde, ref ls_ultima_lectura);

            lbl_ultimo_reg.Text = ll_desde.ToString();
            lbl_ultima_lectura.Text = ls_ultima_lectura;

        }
        #endregion

        #region definicion de botones
        private void btn_config_Click(object sender, EventArgs e)
        {
            configuracion w_configuracion = new configuracion();
            w_configuracion.Show();

        }
        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            l_repositorio.iniciar();

            btn_iniciar.Enabled = false;
            lblEstado.Text = "Procesando...";

            uf_set_ultima_lectura();
            decimal ldec_frecuenia = l_repositorio.gdec_frecuencia;
            ldec_frecuenia = ldec_frecuenia * 1000;

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = Convert.ToInt32(ldec_frecuenia);    // 1 segundo
            myTimer.Start();

        }
        private void btn_detener_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
            btn_iniciar.Enabled = true;
            lblEstado.Text = "Sincronización detenida";
        }
        #endregion

    }
}
