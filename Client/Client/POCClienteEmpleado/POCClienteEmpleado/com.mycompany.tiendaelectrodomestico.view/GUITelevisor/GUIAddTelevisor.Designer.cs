using System.Drawing;

namespace POCClienteEmpleado
{
    partial class GUIAddTelevisor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFechaCreacionTelevisor = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombreTelevisor = new System.Windows.Forms.TextBox();
            this.txtPrecioTelevisor = new System.Windows.Forms.TextBox();
            this.txtMarcaTelevisor = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodigoTelevisor = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigoControl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTvBoxes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agregar Televisor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Precio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Marca:";
            // 
            // txtFechaCreacionTelevisor
            // 
            this.txtFechaCreacionTelevisor.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.txtFechaCreacionTelevisor.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtFechaCreacionTelevisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaCreacionTelevisor.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFechaCreacionTelevisor.Location = new System.Drawing.Point(193, 255);
            this.txtFechaCreacionTelevisor.Name = "txtFechaCreacionTelevisor";
            this.txtFechaCreacionTelevisor.Size = new System.Drawing.Size(210, 29);
            this.txtFechaCreacionTelevisor.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Fecha Creacion:";
            // 
            // txtNombreTelevisor
            // 
            this.txtNombreTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNombreTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreTelevisor.Location = new System.Drawing.Point(144, 121);
            this.txtNombreTelevisor.Name = "txtNombreTelevisor";
            this.txtNombreTelevisor.Size = new System.Drawing.Size(217, 32);
            this.txtNombreTelevisor.TabIndex = 6;
            // 
            // txtPrecioTelevisor
            // 
            this.txtPrecioTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPrecioTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioTelevisor.Location = new System.Drawing.Point(144, 166);
            this.txtPrecioTelevisor.Name = "txtPrecioTelevisor";
            this.txtPrecioTelevisor.Size = new System.Drawing.Size(217, 32);
            this.txtPrecioTelevisor.TabIndex = 7;
            // 
            // txtMarcaTelevisor
            // 
            this.txtMarcaTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMarcaTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarcaTelevisor.Location = new System.Drawing.Point(144, 208);
            this.txtMarcaTelevisor.Name = "txtMarcaTelevisor";
            this.txtMarcaTelevisor.Size = new System.Drawing.Size(217, 32);
            this.txtMarcaTelevisor.TabIndex = 8;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(417, 387);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(86, 31);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGuardar.Location = new System.Drawing.Point(16, 387);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(89, 31);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "N° Referencia:";
            // 
            // txtCodigoTelevisor
            // 
            this.txtCodigoTelevisor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigoTelevisor.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoTelevisor.Location = new System.Drawing.Point(144, 73);
            this.txtCodigoTelevisor.Name = "txtCodigoTelevisor";
            this.txtCodigoTelevisor.Size = new System.Drawing.Size(217, 32);
            this.txtCodigoTelevisor.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::POCClienteEmpleado.Properties.Resources.añadirPequeño;
            this.pictureBox1.Location = new System.Drawing.Point(374, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 142);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 24);
            this.label7.TabIndex = 15;
            this.label7.Text = "Controles Remotos:";
            // 
            // txtCodigoControl
            // 
            this.txtCodigoControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigoControl.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoControl.Location = new System.Drawing.Point(193, 301);
            this.txtCodigoControl.Name = "txtCodigoControl";
            this.txtCodigoControl.Size = new System.Drawing.Size(210, 32);
            this.txtCodigoControl.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "Tv Boxes:";
            // 
            // txtTvBoxes
            // 
            this.txtTvBoxes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTvBoxes.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTvBoxes.Location = new System.Drawing.Point(144, 346);
            this.txtTvBoxes.Name = "txtTvBoxes";
            this.txtTvBoxes.Size = new System.Drawing.Size(259, 32);
            this.txtTvBoxes.TabIndex = 18;
            // 
            // GUIAddTelevisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(515, 430);
            this.Controls.Add(this.txtTvBoxes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCodigoControl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCodigoTelevisor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtMarcaTelevisor);
            this.Controls.Add(this.txtPrecioTelevisor);
            this.Controls.Add(this.txtNombreTelevisor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFechaCreacionTelevisor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUIAddTelevisor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUIAddTelevisor";
            this.Load += new System.EventHandler(this.GUIAddTelevisor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker txtFechaCreacionTelevisor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombreTelevisor;
        private System.Windows.Forms.TextBox txtPrecioTelevisor;
        private System.Windows.Forms.TextBox txtMarcaTelevisor;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCodigoTelevisor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodigoControl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTvBoxes;
    }
}
