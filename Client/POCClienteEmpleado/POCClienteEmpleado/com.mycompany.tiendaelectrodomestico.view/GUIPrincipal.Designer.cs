namespace POCTiendaElectrodomestico
{
    partial class GUIPrincipal
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
            this.optionArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.optionSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.optionProducto = new System.Windows.Forms.ToolStripMenuItem();
            this.optionTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAddTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionSearchTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionEditTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionListTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionFiltrarTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionDeleteTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.optionLavadora = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionArchivo
            // 
            this.optionArchivo.BackColor = System.Drawing.Color.DarkGray;
            this.optionArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionSalir});
            this.optionArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionArchivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.optionArchivo.Name = "optionArchivo";
            this.optionArchivo.Size = new System.Drawing.Size(73, 24);
            this.optionArchivo.Text = "Archivo";
            // 
            // optionSalir
            // 
            this.optionSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.optionSalir.Name = "optionSalir";
            this.optionSalir.Size = new System.Drawing.Size(109, 24);
            this.optionSalir.Text = "Salir";
            this.optionSalir.Click += new System.EventHandler(this.optionSalir_Click);
            // 
            // optionProducto
            // 
            this.optionProducto.BackColor = System.Drawing.Color.DarkGray;
            this.optionProducto.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionTelevisor,
            this.optionLavadora});
            this.optionProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionProducto.Name = "optionProducto";
            this.optionProducto.Size = new System.Drawing.Size(85, 24);
            this.optionProducto.Text = "Producto";
            // 
            // optionTelevisor
            // 
            this.optionTelevisor.BackColor = System.Drawing.Color.White;
            this.optionTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionAddTelevisor,
            this.optionSearchTelevisor,
            this.optionEditTelevisor,
            this.optionListTelevisor,
            this.optionFiltrarTelevisor,
            this.optionDeleteTelevisor});
            this.optionTelevisor.Name = "optionTelevisor";
            this.optionTelevisor.Size = new System.Drawing.Size(180, 24);
            this.optionTelevisor.Text = "Televisor";
            // 
            // optionAddTelevisor
            // 
            this.optionAddTelevisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.optionAddTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.añadir;
            this.optionAddTelevisor.Name = "optionAddTelevisor";
            this.optionAddTelevisor.Size = new System.Drawing.Size(195, 24);
            this.optionAddTelevisor.Text = "Add Televisor";
            this.optionAddTelevisor.Click += new System.EventHandler(this.optionAddTelevisor_Click);
            // 
            // optionSearchTelevisor
            // 
            this.optionSearchTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.buscar;
            this.optionSearchTelevisor.Name = "optionSearchTelevisor";
            this.optionSearchTelevisor.Size = new System.Drawing.Size(195, 24);
            this.optionSearchTelevisor.Text = "Search Televisor";
            this.optionSearchTelevisor.Click += new System.EventHandler(this.optionSearchTelevisor_Click);
            // 
            // optionEditTelevisor
            // 
            this.optionEditTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.Editar;
            this.optionEditTelevisor.Name = "optionEditTelevisor";
            this.optionEditTelevisor.Size = new System.Drawing.Size(195, 24);
            this.optionEditTelevisor.Text = "Edit Televisor";
            this.optionEditTelevisor.Click += new System.EventHandler(this.optionEditTelevisor_Click);
            // 
            // optionListTelevisor
            // 
            this.optionListTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.listar;
            this.optionListTelevisor.Name = "optionListTelevisor";
            this.optionListTelevisor.Size = new System.Drawing.Size(195, 24);
            this.optionListTelevisor.Text = "List Televisor";
            this.optionListTelevisor.Click += new System.EventHandler(this.optionListTelevisor_Click);
            // 
            // optionFiltrarTelevisor
            // 
            this.optionFiltrarTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.filtrar;
            this.optionFiltrarTelevisor.Name = "optionFiltrarTelevisor";
            this.optionFiltrarTelevisor.Size = new System.Drawing.Size(195, 24);
            this.optionFiltrarTelevisor.Text = "Filtrar Televisor";
            this.optionFiltrarTelevisor.Click += new System.EventHandler(this.optionFiltrarTelevisor_Click);
            // 
            // optionDeleteTelevisor
            // 
            this.optionDeleteTelevisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.optionDeleteTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.eliminar;
            this.optionDeleteTelevisor.Name = "optionDeleteTelevisor";
            this.optionDeleteTelevisor.Size = new System.Drawing.Size(195, 24);
            this.optionDeleteTelevisor.Text = "Delete Televisor";
            this.optionDeleteTelevisor.Click += new System.EventHandler(this.optionDeleteTelevisor_Click);
            // 
            // optionLavadora
            // 
            this.optionLavadora.BackColor = System.Drawing.Color.Transparent;
            this.optionLavadora.Name = "optionLavadora";
            this.optionLavadora.Size = new System.Drawing.Size(180, 24);
            this.optionLavadora.Text = "Lavadora";
            this.optionLavadora.Click += new System.EventHandler(this.optionLavadora_Click);
            // 
            // optionAyuda
            // 
            this.optionAyuda.BackColor = System.Drawing.Color.DarkGray;
            this.optionAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionAcercaDe});
            this.optionAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionAyuda.Name = "optionAyuda";
            this.optionAyuda.Size = new System.Drawing.Size(66, 24);
            this.optionAyuda.Text = "Ayuda";
            // 
            // optionAcercaDe
            // 
            this.optionAcercaDe.BackColor = System.Drawing.Color.White;
            this.optionAcercaDe.Name = "optionAcercaDe";
            this.optionAcercaDe.Size = new System.Drawing.Size(166, 24);
            this.optionAcercaDe.Text = "Acerca de ...";
            this.optionAcercaDe.Click += new System.EventHandler(this.optionAcercaDe_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionArchivo,
            this.optionProducto,
            this.optionAyuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(676, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // GUIPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = global::POCClienteEmpleado.Properties.Resources.Adobe_Express___file;
            this.ClientSize = new System.Drawing.Size(676, 411);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUIPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUIPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem optionArchivo;
        private System.Windows.Forms.ToolStripMenuItem optionSalir;
        private System.Windows.Forms.ToolStripMenuItem optionProducto;
        private System.Windows.Forms.ToolStripMenuItem optionTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionAddTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionSearchTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionEditTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionListTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionDeleteTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionFiltrarTelevisor;
        private System.Windows.Forms.ToolStripMenuItem optionAyuda;
        private System.Windows.Forms.ToolStripMenuItem optionAcercaDe;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionLavadora;
    }
}

