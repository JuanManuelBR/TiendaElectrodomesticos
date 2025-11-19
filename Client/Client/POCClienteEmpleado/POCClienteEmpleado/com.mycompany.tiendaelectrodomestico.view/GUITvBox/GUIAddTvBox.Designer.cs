using System.Drawing;

namespace POCClienteEmpleado
{
    partial class GUIAddTvBox
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
            this.txtFechaCreacionTvBox = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombreTvBox = new System.Windows.Forms.TextBox();
            this.txtPrecioTvBox = new System.Windows.Forms.TextBox();
            this.txtMarcaTvBox = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodigoTvBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTvAsociado = new System.Windows.Forms.TextBox();
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
            this.label1.Size = new System.Drawing.Size(243, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agregar TvBox";
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
            // txtFechaCreacionTvBox
            // 
            this.txtFechaCreacionTvBox.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.txtFechaCreacionTvBox.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtFechaCreacionTvBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaCreacionTvBox.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFechaCreacionTvBox.Location = new System.Drawing.Point(193, 255);
            this.txtFechaCreacionTvBox.Name = "txtFechaCreacionTvBox";
            this.txtFechaCreacionTvBox.Size = new System.Drawing.Size(210, 29);
            this.txtFechaCreacionTvBox.TabIndex = 4;
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
            // txtNombreTvBox
            // 
            this.txtNombreTvBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNombreTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreTvBox.Location = new System.Drawing.Point(144, 121);
            this.txtNombreTvBox.Name = "txtNombreTvBox";
            this.txtNombreTvBox.Size = new System.Drawing.Size(217, 32);
            this.txtNombreTvBox.TabIndex = 6;
            // 
            // txtPrecioTvBox
            // 
            this.txtPrecioTvBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPrecioTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioTvBox.Location = new System.Drawing.Point(144, 166);
            this.txtPrecioTvBox.Name = "txtPrecioTvBox";
            this.txtPrecioTvBox.Size = new System.Drawing.Size(217, 32);
            this.txtPrecioTvBox.TabIndex = 7;
            // 
            // txtMarcaTvBox
            // 
            this.txtMarcaTvBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMarcaTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarcaTvBox.Location = new System.Drawing.Point(144, 208);
            this.txtMarcaTvBox.Name = "txtMarcaTvBox";
            this.txtMarcaTvBox.Size = new System.Drawing.Size(217, 32);
            this.txtMarcaTvBox.TabIndex = 8;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(417, 336);
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
            this.btnGuardar.Location = new System.Drawing.Point(16, 336);
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
            // txtCodigoTvBox
            // 
            this.txtCodigoTvBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigoTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoTvBox.Location = new System.Drawing.Point(144, 73);
            this.txtCodigoTvBox.Name = "txtCodigoTvBox";
            this.txtCodigoTvBox.Size = new System.Drawing.Size(217, 32);
            this.txtCodigoTvBox.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::POCClienteEmpleado.Properties.Resources.agregar;
            this.pictureBox1.Location = new System.Drawing.Point(374, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 166);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 24);
            this.label7.TabIndex = 15;
            this.label7.Text = "Codigo Tv:";
            // 
            // txtTvAsociado
            // 
            this.txtTvAsociado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTvAsociado.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTvAsociado.Location = new System.Drawing.Point(144, 295);
            this.txtTvAsociado.Name = "txtTvAsociado";
            this.txtTvAsociado.Size = new System.Drawing.Size(217, 32);
            this.txtTvAsociado.TabIndex = 16;
            // 
            // GUIAddTvBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(515, 379);
            this.Controls.Add(this.txtTvAsociado);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCodigoTvBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtMarcaTvBox);
            this.Controls.Add(this.txtPrecioTvBox);
            this.Controls.Add(this.txtNombreTvBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFechaCreacionTvBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUIAddTvBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUIAddTvBox";
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
        private System.Windows.Forms.DateTimePicker txtFechaCreacionTvBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombreTvBox;
        private System.Windows.Forms.TextBox txtPrecioTvBox;
        private System.Windows.Forms.TextBox txtMarcaTvBox;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCodigoTvBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTvAsociado;
    }
}
