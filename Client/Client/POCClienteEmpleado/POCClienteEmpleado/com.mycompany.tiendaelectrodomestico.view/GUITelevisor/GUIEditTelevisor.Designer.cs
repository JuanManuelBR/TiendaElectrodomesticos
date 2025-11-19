using System.Drawing;

namespace POCClienteEmpleado
{
    partial class GUIEditTelevisor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombreTelevisor = new System.Windows.Forms.TextBox();
            this.txtPrecioTelevisor = new System.Windows.Forms.TextBox();
            this.txtMarcaTelevisor = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigoTelevisorBuscar = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnActualizar = new System.Windows.Forms.Button();
            this.txtFechaCreacionTelevisor = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCodigoControl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTvBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Actualizar Televisor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.Location = new System.Drawing.Point(7, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label3.Location = new System.Drawing.Point(15, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Precio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label4.Location = new System.Drawing.Point(15, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Marca:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label5.Location = new System.Drawing.Point(12, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Fecha Creacion:";
            // 
            // txtNombreTelevisor
            // 
            this.txtNombreTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNombreTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreTelevisor.Location = new System.Drawing.Point(94, 101);
            this.txtNombreTelevisor.Name = "txtNombreTelevisor";
            this.txtNombreTelevisor.Size = new System.Drawing.Size(275, 32);
            this.txtNombreTelevisor.TabIndex = 6;
            // 
            // txtPrecioTelevisor
            // 
            this.txtPrecioTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPrecioTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioTelevisor.Location = new System.Drawing.Point(94, 152);
            this.txtPrecioTelevisor.Name = "txtPrecioTelevisor";
            this.txtPrecioTelevisor.Size = new System.Drawing.Size(275, 32);
            this.txtPrecioTelevisor.TabIndex = 7;
            // 
            // txtMarcaTelevisor
            // 
            this.txtMarcaTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMarcaTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarcaTelevisor.Location = new System.Drawing.Point(94, 196);
            this.txtMarcaTelevisor.Name = "txtMarcaTelevisor";
            this.txtMarcaTelevisor.Size = new System.Drawing.Size(275, 32);
            this.txtMarcaTelevisor.TabIndex = 8;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSalir.Location = new System.Drawing.Point(422, 384);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(81, 30);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnBuscar.Location = new System.Drawing.Point(12, 385);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(83, 30);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label7.Location = new System.Drawing.Point(12, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(276, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "N° Referencia Televisor Buscar:";
            // 
            // txtCodigoTelevisorBuscar
            // 
            this.txtCodigoTelevisorBuscar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigoTelevisorBuscar.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoTelevisorBuscar.Location = new System.Drawing.Point(294, 58);
            this.txtCodigoTelevisorBuscar.Name = "txtCodigoTelevisorBuscar";
            this.txtCodigoTelevisorBuscar.Size = new System.Drawing.Size(204, 32);
            this.txtCodigoTelevisorBuscar.TabIndex = 15;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnActualizar.Location = new System.Drawing.Point(212, 384);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(104, 31);
            this.btnActualizar.TabIndex = 16;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txtFechaCreacionTelevisor
            // 
            this.txtFechaCreacionTelevisor.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.txtFechaCreacionTelevisor.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtFechaCreacionTelevisor.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaCreacionTelevisor.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFechaCreacionTelevisor.Location = new System.Drawing.Point(168, 244);
            this.txtFechaCreacionTelevisor.Name = "txtFechaCreacionTelevisor";
            this.txtFechaCreacionTelevisor.Size = new System.Drawing.Size(210, 33);
            this.txtFechaCreacionTelevisor.TabIndex = 17;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::POCClienteEmpleado.Properties.Resources.EditarPequeño;
            this.pictureBox1.Location = new System.Drawing.Point(377, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 142);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // txtCodigoControl
            // 
            this.txtCodigoControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigoControl.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoControl.Location = new System.Drawing.Point(196, 292);
            this.txtCodigoControl.Name = "txtCodigoControl";
            this.txtCodigoControl.Size = new System.Drawing.Size(210, 32);
            this.txtCodigoControl.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "Controles Remotos:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 342);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 24);
            this.label8.TabIndex = 22;
            this.label8.Text = "Tv Boxes:";
            // 
            // txtTvBox
            // 
            this.txtTvBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTvBox.Location = new System.Drawing.Point(115, 341);
            this.txtTvBox.Name = "txtTvBox";
            this.txtTvBox.Size = new System.Drawing.Size(210, 32);
            this.txtTvBox.TabIndex = 23;
            // 
            // GUIEditTelevisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(515, 448);
            this.Controls.Add(this.txtTvBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCodigoControl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFechaCreacionTelevisor);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.txtCodigoTelevisorBuscar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtMarcaTelevisor);
            this.Controls.Add(this.txtPrecioTelevisor);
            this.Controls.Add(this.txtNombreTelevisor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUIEditTelevisor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUIEditTelevisor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombreTelevisor;
        private System.Windows.Forms.TextBox txtPrecioTelevisor;
        private System.Windows.Forms.TextBox txtMarcaTelevisor;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodigoTelevisorBuscar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DateTimePicker txtFechaCreacionTelevisor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCodigoControl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTvBox;
    }
}
