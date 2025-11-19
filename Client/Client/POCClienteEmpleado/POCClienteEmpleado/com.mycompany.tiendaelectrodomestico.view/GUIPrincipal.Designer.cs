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
            this.televisoresControlesMismaMarcaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlRemotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marcaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disponibilidadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlesDeMarcaAsignadosATVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.controlRemotoToolStripMenuItem,
            this.tvBoxToolStripMenuItem});
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
            this.optionDeleteTelevisor,
            this.televisoresControlesMismaMarcaToolStripMenuItem1});
            this.optionTelevisor.Name = "optionTelevisor";
            this.optionTelevisor.Size = new System.Drawing.Size(180, 24);
            this.optionTelevisor.Text = "Televisor";
            // 
            // optionAddTelevisor
            // 
            this.optionAddTelevisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.optionAddTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.añadirPequeño;
            this.optionAddTelevisor.Name = "optionAddTelevisor";
            this.optionAddTelevisor.Size = new System.Drawing.Size(315, 24);
            this.optionAddTelevisor.Text = "Add";
            this.optionAddTelevisor.Click += new System.EventHandler(this.addTelevisorToolStripMenuItem_Click);
            // 
            // optionSearchTelevisor
            // 
            this.optionSearchTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.buscarPequeño;
            this.optionSearchTelevisor.Name = "optionSearchTelevisor";
            this.optionSearchTelevisor.Size = new System.Drawing.Size(315, 24);
            this.optionSearchTelevisor.Text = "Search";
            this.optionSearchTelevisor.Click += new System.EventHandler(this.searchTelevisorToolStripMenuItem1_Click);
            // 
            // optionEditTelevisor
            // 
            this.optionEditTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.EditarPequeño;
            this.optionEditTelevisor.Name = "optionEditTelevisor";
            this.optionEditTelevisor.Size = new System.Drawing.Size(315, 24);
            this.optionEditTelevisor.Text = "Edit";
            this.optionEditTelevisor.Click += new System.EventHandler(this.editTelevisorToolStripMenuItem2_Click);
            // 
            // optionListTelevisor
            // 
            this.optionListTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.listar;
            this.optionListTelevisor.Name = "optionListTelevisor";
            this.optionListTelevisor.Size = new System.Drawing.Size(315, 24);
            this.optionListTelevisor.Text = "List";
            this.optionListTelevisor.Click += new System.EventHandler(this.ListTelevisorToolStripMenuItem3_Click);
            // 
            // optionFiltrarTelevisor
            // 
            this.optionFiltrarTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.filtrar;
            this.optionFiltrarTelevisor.Name = "optionFiltrarTelevisor";
            this.optionFiltrarTelevisor.Size = new System.Drawing.Size(315, 24);
            this.optionFiltrarTelevisor.Text = "Filtrar";
            this.optionFiltrarTelevisor.Click += new System.EventHandler(this.filtrarTelevisorToolStripMenuItem4_Click);
            // 
            // optionDeleteTelevisor
            // 
            this.optionDeleteTelevisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.optionDeleteTelevisor.Image = global::POCClienteEmpleado.Properties.Resources.eliminarPequeño;
            this.optionDeleteTelevisor.Name = "optionDeleteTelevisor";
            this.optionDeleteTelevisor.Size = new System.Drawing.Size(315, 24);
            this.optionDeleteTelevisor.Text = "Delete";
            this.optionDeleteTelevisor.Click += new System.EventHandler(this.deleteTelevisorToolStripMenuItem5_Click);
            // 
            // televisoresControlesMismaMarcaToolStripMenuItem1
            // 
            this.televisoresControlesMismaMarcaToolStripMenuItem1.Name = "televisoresControlesMismaMarcaToolStripMenuItem1";
            this.televisoresControlesMismaMarcaToolStripMenuItem1.Size = new System.Drawing.Size(315, 24);
            this.televisoresControlesMismaMarcaToolStripMenuItem1.Text = "TelevisoresControlesMismaMarca";
            this.televisoresControlesMismaMarcaToolStripMenuItem1.Click += new System.EventHandler(this.televisoresControlesMismaMarcaToolStripMenuItem_Click);
            // 
            // controlRemotoToolStripMenuItem
            // 
            this.controlRemotoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.editToolStripMenuItem,
            this.listToolStripMenuItem,
            this.filtrarToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.controlesDeMarcaAsignadosATVToolStripMenuItem});
            this.controlRemotoToolStripMenuItem.Name = "controlRemotoToolStripMenuItem";
            this.controlRemotoToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.controlRemotoToolStripMenuItem.Text = "Control";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.addToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_previeww;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addControlToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_previessw;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchControlToolStripMenuItem1_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_prewwview;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editControlToolStripMenuItem2_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_preeeeview;
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.listToolStripMenuItem.Text = "List";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listControlToolStripMenuItem3_Click);
            // 
            // filtrarToolStripMenuItem
            // 
            this.filtrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marcaToolStripMenuItem1,
            this.disponibilidadToolStripMenuItem});
            this.filtrarToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removesdbg_preview;
            this.filtrarToolStripMenuItem.Name = "filtrarToolStripMenuItem";
            this.filtrarToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.filtrarToolStripMenuItem.Text = "Filtrar";
            // 
            // marcaToolStripMenuItem1
            // 
            this.marcaToolStripMenuItem1.Name = "marcaToolStripMenuItem1";
            this.marcaToolStripMenuItem1.Size = new System.Drawing.Size(176, 24);
            this.marcaToolStripMenuItem1.Text = "Marca";
            this.marcaToolStripMenuItem1.Click += new System.EventHandler(this.filtrarMarcaToolStripMenuItem_Click);
            // 
            // disponibilidadToolStripMenuItem
            // 
            this.disponibilidadToolStripMenuItem.Name = "disponibilidadToolStripMenuItem";
            this.disponibilidadToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.disponibilidadToolStripMenuItem.Text = "Disponibilidad";
            this.disponibilidadToolStripMenuItem.Click += new System.EventHandler(this.filtrarDisponibilidadToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.deleteToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_preswview1;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteControlToolStripMenuItem5_Click);
            // 
            // controlesDeMarcaAsignadosATVToolStripMenuItem
            // 
            this.controlesDeMarcaAsignadosATVToolStripMenuItem.Name = "controlesDeMarcaAsignadosATVToolStripMenuItem";
            this.controlesDeMarcaAsignadosATVToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.controlesDeMarcaAsignadosATVToolStripMenuItem.Text = "ControlMarcaAsignadosTV";
            this.controlesDeMarcaAsignadosATVToolStripMenuItem.Click += new System.EventHandler(this.controlesDeMarcaAsignadosATVToolStripMenuItem_Click);
            // 
            // tvBoxToolStripMenuItem
            // 
            this.tvBoxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.searchToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.listToolStripMenuItem1,
            this.filtrarToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.tvBoxToolStripMenuItem.Name = "tvBoxToolStripMenuItem";
            this.tvBoxToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.tvBoxToolStripMenuItem.Text = "TvBox";
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.addToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.agregar;
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.Buscar1;
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.searchToolStripMenuItem1.Text = "Search";
            this.searchToolStripMenuItem1.Click += new System.EventHandler(this.searchToolStripMenuItem1_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.Editar1;
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.editToolStripMenuItem1.Text = "Edit";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // listToolStripMenuItem1
            // 
            this.listToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.listar1;
            this.listToolStripMenuItem1.Name = "listToolStripMenuItem1";
            this.listToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.listToolStripMenuItem1.Text = "Filtrar";
            this.listToolStripMenuItem1.Click += new System.EventHandler(this.listToolStripMenuItem1_Click);
            // 
            // filtrarToolStripMenuItem1
            // 
            this.filtrarToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.filtrar1;
            this.filtrarToolStripMenuItem1.Name = "filtrarToolStripMenuItem1";
            this.filtrarToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.filtrarToolStripMenuItem1.Text = "List";
            this.filtrarToolStripMenuItem1.Click += new System.EventHandler(this.filtrarToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.deleteToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.delete;
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
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
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionArchivo,
            this.optionProducto,
            this.optionAyuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
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
        private System.Windows.Forms.ToolStripMenuItem controlRemotoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disponibilidadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlesDeMarcaAsignadosATVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem televisoresControlesMismaMarcaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tvBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem filtrarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
    }
}

