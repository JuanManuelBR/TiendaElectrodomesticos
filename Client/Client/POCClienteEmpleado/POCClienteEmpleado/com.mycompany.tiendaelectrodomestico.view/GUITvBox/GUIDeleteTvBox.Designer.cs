using System.Drawing;

namespace POCClienteEmpleado
{
    partial class GUIDeleteTvBox
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
            this.txtNombreTvBox = new System.Windows.Forms.TextBox();
            this.txtPrecioTvBox = new System.Windows.Forms.TextBox();
            this.txtMarcaTvBox = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigoTvBoxBuscar = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtFechaCreacionTvBox = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelevisorAsociado = new System.Windows.Forms.TextBox();
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
            this.label1.Location = new System.Drawing.Point(134, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Eliminar TvBox";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label3.Location = new System.Drawing.Point(14, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Precio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label4.Location = new System.Drawing.Point(14, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Marca:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label5.Location = new System.Drawing.Point(12, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Fecha Creacion:";
            // 
            // txtNombreTvBox
            // 
            this.txtNombreTvBox.BackColor = System.Drawing.Color.LightGray;
            this.txtNombreTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreTvBox.Location = new System.Drawing.Point(102, 105);
            this.txtNombreTvBox.Name = "txtNombreTvBox";
            this.txtNombreTvBox.ReadOnly = true;
            this.txtNombreTvBox.Size = new System.Drawing.Size(275, 32);
            this.txtNombreTvBox.TabIndex = 6;
            // 
            // txtPrecioTvBox
            // 
            this.txtPrecioTvBox.BackColor = System.Drawing.Color.LightGray;
            this.txtPrecioTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioTvBox.Location = new System.Drawing.Point(102, 154);
            this.txtPrecioTvBox.Name = "txtPrecioTvBox";
            this.txtPrecioTvBox.ReadOnly = true;
            this.txtPrecioTvBox.Size = new System.Drawing.Size(275, 32);
            this.txtPrecioTvBox.TabIndex = 7;
            // 
            // txtMarcaTvBox
            // 
            this.txtMarcaTvBox.BackColor = System.Drawing.Color.LightGray;
            this.txtMarcaTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarcaTvBox.Location = new System.Drawing.Point(102, 207);
            this.txtMarcaTvBox.Name = "txtMarcaTvBox";
            this.txtMarcaTvBox.ReadOnly = true;
            this.txtMarcaTvBox.Size = new System.Drawing.Size(275, 32);
            this.txtMarcaTvBox.TabIndex = 8;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnSalir.Location = new System.Drawing.Point(442, 337);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(86, 33);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnBuscar.Location = new System.Drawing.Point(12, 338);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(84, 33);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label7.Location = new System.Drawing.Point(9, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(263, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "N° Referencia TvBox Eliminar:";
            // 
            // txtCodigoTvBoxBuscar
            // 
            this.txtCodigoTvBoxBuscar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigoTvBoxBuscar.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoTvBoxBuscar.Location = new System.Drawing.Point(301, 58);
            this.txtCodigoTvBoxBuscar.Name = "txtCodigoTvBoxBuscar";
            this.txtCodigoTvBoxBuscar.Size = new System.Drawing.Size(191, 32);
            this.txtCodigoTvBoxBuscar.TabIndex = 15;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtFechaCreacionTvBox
            // 
            this.txtFechaCreacionTvBox.BackColor = System.Drawing.Color.LightGray;
            this.txtFechaCreacionTvBox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaCreacionTvBox.Location = new System.Drawing.Point(163, 250);
            this.txtFechaCreacionTvBox.Name = "txtFechaCreacionTvBox";
            this.txtFechaCreacionTvBox.ReadOnly = true;
            this.txtFechaCreacionTvBox.Size = new System.Drawing.Size(214, 32);
            this.txtFechaCreacionTvBox.TabIndex = 17;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnEliminar.Location = new System.Drawing.Point(227, 338);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(88, 32);
            this.btnEliminar.TabIndex = 18;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::POCClienteEmpleado.Properties.Resources.delete;
            this.pictureBox1.Location = new System.Drawing.Point(399, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 176);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label6.Location = new System.Drawing.Point(14, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "Num Referencia TV:";
            // 
            // txtTelevisorAsociado
            // 
            this.txtTelevisorAsociado.BackColor = System.Drawing.Color.LightGray;
            this.txtTelevisorAsociado.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelevisorAsociado.Location = new System.Drawing.Point(202, 293);
            this.txtTelevisorAsociado.Name = "txtTelevisorAsociado";
            this.txtTelevisorAsociado.ReadOnly = true;
            this.txtTelevisorAsociado.Size = new System.Drawing.Size(214, 32);
            this.txtTelevisorAsociado.TabIndex = 21;
            // 
            // GUIDeleteTvBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(544, 394);
            this.Controls.Add(this.txtTelevisorAsociado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.txtFechaCreacionTvBox);
            this.Controls.Add(this.txtCodigoTvBoxBuscar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtMarcaTvBox);
            this.Controls.Add(this.txtPrecioTvBox);
            this.Controls.Add(this.txtNombreTvBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUIDeleteTvBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUIDeleteTvBox";
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
        private System.Windows.Forms.TextBox txtNombreTvBox;
        private System.Windows.Forms.TextBox txtPrecioTvBox;
        private System.Windows.Forms.TextBox txtMarcaTvBox;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodigoTvBoxBuscar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtFechaCreacionTvBox;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelevisorAsociado;
    }
}
