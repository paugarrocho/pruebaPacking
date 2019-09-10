namespace IM_Main
{
    partial class IM_Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IM_Main));
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_tickets = new System.Windows.Forms.DataGridView();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_config = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_ultimo_reg = new System.Windows.Forms.Label();
            this.lbl_ultima_lectura = new System.Windows.Forms.Label();
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.btn_detener = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(279, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ultimo registro leido:";
            // 
            // dgv_tickets
            // 
            this.dgv_tickets.AllowUserToAddRows = false;
            this.dgv_tickets.AllowUserToDeleteRows = false;
            this.dgv_tickets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_tickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tickets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fecha,
            this.detalle});
            this.dgv_tickets.Location = new System.Drawing.Point(282, 61);
            this.dgv_tickets.Name = "dgv_tickets";
            this.dgv_tickets.Size = new System.Drawing.Size(618, 368);
            this.dgv_tickets.TabIndex = 5;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.Width = 150;
            // 
            // detalle
            // 
            this.detalle.HeaderText = "Detalle";
            this.detalle.Name = "detalle";
            this.detalle.Width = 400;
            // 
            // btn_config
            // 
            this.btn_config.Image = ((System.Drawing.Image)(resources.GetObject("btn_config.Image")));
            this.btn_config.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_config.Location = new System.Drawing.Point(12, 2);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(115, 41);
            this.btn_config.TabIndex = 13;
            this.btn_config.Text = "Configuración";
            this.btn_config.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_config.UseVisualStyleBackColor = true;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(139, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_ultimo_reg
            // 
            this.lbl_ultimo_reg.AutoSize = true;
            this.lbl_ultimo_reg.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbl_ultimo_reg.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ultimo_reg.ForeColor = System.Drawing.Color.Blue;
            this.lbl_ultimo_reg.Location = new System.Drawing.Point(426, 2);
            this.lbl_ultimo_reg.Name = "lbl_ultimo_reg";
            this.lbl_ultimo_reg.Size = new System.Drawing.Size(15, 18);
            this.lbl_ultimo_reg.TabIndex = 15;
            this.lbl_ultimo_reg.Text = "0";
            // 
            // lbl_ultima_lectura
            // 
            this.lbl_ultima_lectura.AutoSize = true;
            this.lbl_ultima_lectura.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbl_ultima_lectura.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ultima_lectura.ForeColor = System.Drawing.Color.Blue;
            this.lbl_ultima_lectura.Location = new System.Drawing.Point(426, 25);
            this.lbl_ultima_lectura.Name = "lbl_ultima_lectura";
            this.lbl_ultima_lectura.Size = new System.Drawing.Size(21, 18);
            this.lbl_ultima_lectura.TabIndex = 16;
            this.lbl_ultima_lectura.Text = "-.-";
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.Image = ((System.Drawing.Image)(resources.GetObject("btn_iniciar.Image")));
            this.btn_iniciar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_iniciar.Location = new System.Drawing.Point(139, 88);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(115, 47);
            this.btn_iniciar.TabIndex = 17;
            this.btn_iniciar.Text = "Iniciar";
            this.btn_iniciar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_iniciar.UseVisualStyleBackColor = true;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_iniciar_Click);
            // 
            // btn_detener
            // 
            this.btn_detener.Image = ((System.Drawing.Image)(resources.GetObject("btn_detener.Image")));
            this.btn_detener.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_detener.Location = new System.Drawing.Point(139, 139);
            this.btn_detener.Name = "btn_detener";
            this.btn_detener.Size = new System.Drawing.Size(115, 47);
            this.btn_detener.TabIndex = 18;
            this.btn_detener.Text = "Detener";
            this.btn_detener.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_detener.UseVisualStyleBackColor = true;
            this.btn_detener.Click += new System.EventHandler(this.btn_detener_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.Red;
            this.lblEstado.Location = new System.Drawing.Point(9, 416);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 13);
            this.lblEstado.TabIndex = 19;
            // 
            // IM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(908, 450);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btn_detener);
            this.Controls.Add(this.btn_iniciar);
            this.Controls.Add(this.lbl_ultima_lectura);
            this.Controls.Add(this.lbl_ultimo_reg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.dgv_tickets);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IM_Main";
            this.Text = "IM Interfaces";
            this.Load += new System.EventHandler(this.IM_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_tickets;
        private System.Windows.Forms.Button btn_config;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_ultimo_reg;
        private System.Windows.Forms.Label lbl_ultima_lectura;
        private System.Windows.Forms.Button btn_iniciar;
        private System.Windows.Forms.Button btn_detener;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalle;
        private System.Windows.Forms.Label lblEstado;
    }
}

