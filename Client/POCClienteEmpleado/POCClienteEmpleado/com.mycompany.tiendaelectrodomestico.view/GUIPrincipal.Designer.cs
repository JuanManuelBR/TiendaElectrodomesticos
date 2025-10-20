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
            this.televisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionSearchTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.televisorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionEditTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.televisorToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionListTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.televisorToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionFiltrarTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.televisorToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.marcaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disponiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionDeleteTelevisor = new System.Windows.Forms.ToolStripMenuItem();
            this.televisorToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.controlRemotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.optionArchivo.Size = new System.Drawing.Size(92, 29);
            this.optionArchivo.Text = "Archivo";
            // 
            // optionSalir
            // 
            this.optionSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.optionSalir.Name = "optionSalir";
            this.optionSalir.Size = new System.Drawing.Size(137, 30);
            this.optionSalir.Text = "Salir";
            this.optionSalir.Click += new System.EventHandler(this.optionSalir_Click);
            // 
            // optionProducto
            // 
            this.optionProducto.BackColor = System.Drawing.Color.DarkGray;
            this.optionProducto.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionTelevisor,
            this.controlRemotoToolStripMenuItem});
            this.optionProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionProducto.Name = "optionProducto";
            this.optionProducto.Size = new System.Drawing.Size(104, 29);
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
            this.optionTelevisor.Size = new System.Drawing.Size(224, 30);
            this.optionTelevisor.Text = "Televisor";
            // 
            // optionAddTelevisor
            // 
            this.optionAddTelevisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.optionAddTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.televisorToolStripMenuItem,
            this.controlToolStripMenuItem});
            this.optionAddTelevisor.Name = "optionAddTelevisor";
            this.optionAddTelevisor.Size = new System.Drawing.Size(161, 30);
            this.optionAddTelevisor.Text = "Add";
            // 
            // televisorToolStripMenuItem
            // 
            this.televisorToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.añadirPequeño;
            this.televisorToolStripMenuItem.Name = "televisorToolStripMenuItem";
            this.televisorToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.televisorToolStripMenuItem.Text = "Televisor";
            this.televisorToolStripMenuItem.Click += new System.EventHandler(this.addTelevisorToolStripMenuItem_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_previeww;
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(178, 30);
            this.controlToolStripMenuItem.Text = "Control";
            this.controlToolStripMenuItem.Click += new System.EventHandler(this.addControlToolStripMenuItem_Click);
            // 
            // optionSearchTelevisor
            // 
            this.optionSearchTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.televisorToolStripMenuItem1,
            this.controlToolStripMenuItem1});
            this.optionSearchTelevisor.Name = "optionSearchTelevisor";
            this.optionSearchTelevisor.Size = new System.Drawing.Size(161, 30);
            this.optionSearchTelevisor.Text = "Search";
            // 
            // televisorToolStripMenuItem1
            // 
            this.televisorToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.buscarPequeño;
            this.televisorToolStripMenuItem1.Name = "televisorToolStripMenuItem1";
            this.televisorToolStripMenuItem1.Size = new System.Drawing.Size(178, 30);
            this.televisorToolStripMenuItem1.Text = "Televisor";
            this.televisorToolStripMenuItem1.Click += new System.EventHandler(this.searchTelevisorToolStripMenuItem1_Click);
            // 
            // controlToolStripMenuItem1
            // 
            this.controlToolStripMenuItem1.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_previessw;
            this.controlToolStripMenuItem1.Name = "controlToolStripMenuItem1";
            this.controlToolStripMenuItem1.Size = new System.Drawing.Size(178, 30);
            this.controlToolStripMenuItem1.Text = "Control";
            this.controlToolStripMenuItem1.Click += new System.EventHandler(this.searchControlToolStripMenuItem1_Click);
            // 
            // optionEditTelevisor
            // 
            this.optionEditTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.televisorToolStripMenuItem2,
            this.controlToolStripMenuItem2});
            this.optionEditTelevisor.Name = "optionEditTelevisor";
            this.optionEditTelevisor.Size = new System.Drawing.Size(161, 30);
            this.optionEditTelevisor.Text = "Edit";
            // 
            // televisorToolStripMenuItem2
            // 
            this.televisorToolStripMenuItem2.Image = global::POCClienteEmpleado.Properties.Resources.EditarPequeño;
            this.televisorToolStripMenuItem2.Name = "televisorToolStripMenuItem2";
            this.televisorToolStripMenuItem2.Size = new System.Drawing.Size(178, 30);
            this.televisorToolStripMenuItem2.Text = "Televisor";
            this.televisorToolStripMenuItem2.Click += new System.EventHandler(this.editTelevisorToolStripMenuItem2_Click);
            // 
            // controlToolStripMenuItem2
            // 
            this.controlToolStripMenuItem2.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_prewwview;
            this.controlToolStripMenuItem2.Name = "controlToolStripMenuItem2";
            this.controlToolStripMenuItem2.Size = new System.Drawing.Size(178, 30);
            this.controlToolStripMenuItem2.Text = "Control";
            this.controlToolStripMenuItem2.Click += new System.EventHandler(this.editControlToolStripMenuItem2_Click);
            // 
            // optionListTelevisor
            // 
            this.optionListTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.televisorToolStripMenuItem3,
            this.controlToolStripMenuItem3});
            this.optionListTelevisor.Name = "optionListTelevisor";
            this.optionListTelevisor.Size = new System.Drawing.Size(161, 30);
            this.optionListTelevisor.Text = "List";
            // 
            // televisorToolStripMenuItem3
            // 
            this.televisorToolStripMenuItem3.Image = global::POCClienteEmpleado.Properties.Resources.listar;
            this.televisorToolStripMenuItem3.Name = "televisorToolStripMenuItem3";
            this.televisorToolStripMenuItem3.Size = new System.Drawing.Size(178, 30);
            this.televisorToolStripMenuItem3.Text = "Televisor";
            this.televisorToolStripMenuItem3.Click += new System.EventHandler(this.ListTelevisorToolStripMenuItem3_Click);
            // 
            // controlToolStripMenuItem3
            // 
            this.controlToolStripMenuItem3.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_preeeeview;
            this.controlToolStripMenuItem3.Name = "controlToolStripMenuItem3";
            this.controlToolStripMenuItem3.Size = new System.Drawing.Size(178, 30);
            this.controlToolStripMenuItem3.Text = "Control";
            this.controlToolStripMenuItem3.Click += new System.EventHandler(this.listControlToolStripMenuItem3_Click);
            // 
            // optionFiltrarTelevisor
            // 
            this.optionFiltrarTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.televisorToolStripMenuItem4,
            this.controlToolStripMenuItem4});
            this.optionFiltrarTelevisor.Name = "optionFiltrarTelevisor";
            this.optionFiltrarTelevisor.Size = new System.Drawing.Size(161, 30);
            this.optionFiltrarTelevisor.Text = "Filtrar";
            // 
            // televisorToolStripMenuItem4
            // 
            this.televisorToolStripMenuItem4.Image = global::POCClienteEmpleado.Properties.Resources.filtrar;
            this.televisorToolStripMenuItem4.Name = "televisorToolStripMenuItem4";
            this.televisorToolStripMenuItem4.Size = new System.Drawing.Size(178, 30);
            this.televisorToolStripMenuItem4.Text = "Televisor";
            this.televisorToolStripMenuItem4.Click += new System.EventHandler(this.filtrarTelevisorToolStripMenuItem4_Click);
            // 
            // controlToolStripMenuItem4
            // 
            this.controlToolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marcaToolStripMenuItem,
            this.disponiToolStripMenuItem});
            this.controlToolStripMenuItem4.Image = global::POCClienteEmpleado.Properties.Resources.image_removesdbg_preview;
            this.controlToolStripMenuItem4.Name = "controlToolStripMenuItem4";
            this.controlToolStripMenuItem4.Size = new System.Drawing.Size(178, 30);
            this.controlToolStripMenuItem4.Text = "Control";
            // 
            // marcaToolStripMenuItem
            // 
            this.marcaToolStripMenuItem.Name = "marcaToolStripMenuItem";
            this.marcaToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.marcaToolStripMenuItem.Text = "Marca";
            this.marcaToolStripMenuItem.Click += new System.EventHandler(this.filtrarMarcaToolStripMenuItem_Click);
            // 
            // disponiToolStripMenuItem
            // 
            this.disponiToolStripMenuItem.Name = "disponiToolStripMenuItem";
            this.disponiToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.disponiToolStripMenuItem.Text = "Disponibilidad";
            this.disponiToolStripMenuItem.Click += new System.EventHandler(this.filtrarDisponibilidadToolStripMenuItem_Click);
            // 
            // optionDeleteTelevisor
            // 
            this.optionDeleteTelevisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.optionDeleteTelevisor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.televisorToolStripMenuItem5,
            this.controlToolStripMenuItem5});
            this.optionDeleteTelevisor.Name = "optionDeleteTelevisor";
            this.optionDeleteTelevisor.Size = new System.Drawing.Size(161, 30);
            this.optionDeleteTelevisor.Text = "Delete";
            // 
            // televisorToolStripMenuItem5
            // 
            this.televisorToolStripMenuItem5.Image = global::POCClienteEmpleado.Properties.Resources.eliminarPequeño;
            this.televisorToolStripMenuItem5.Name = "televisorToolStripMenuItem5";
            this.televisorToolStripMenuItem5.Size = new System.Drawing.Size(178, 30);
            this.televisorToolStripMenuItem5.Text = "Televisor";
            this.televisorToolStripMenuItem5.Click += new System.EventHandler(this.deleteTelevisorToolStripMenuItem5_Click);
            // 
            // controlToolStripMenuItem5
            // 
            this.controlToolStripMenuItem5.Image = global::POCClienteEmpleado.Properties.Resources.image_removebg_preswview1;
            this.controlToolStripMenuItem5.Name = "controlToolStripMenuItem5";
            this.controlToolStripMenuItem5.Size = new System.Drawing.Size(178, 30);
            this.controlToolStripMenuItem5.Text = "Control";
            this.controlToolStripMenuItem5.Click += new System.EventHandler(this.deleteControlToolStripMenuItem5_Click);
            // 
            // controlRemotoToolStripMenuItem
            // 
            this.controlRemotoToolStripMenuItem.Name = "controlRemotoToolStripMenuItem";
            this.controlRemotoToolStripMenuItem.Size = new System.Drawing.Size(224, 30);
            this.controlRemotoToolStripMenuItem.Text = "Lavadora";
            this.controlRemotoToolStripMenuItem.Click += new System.EventHandler(this.optionLavadora_Click);
            // 
            // optionAyuda
            // 
            this.optionAyuda.BackColor = System.Drawing.Color.DarkGray;
            this.optionAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionAcercaDe});
            this.optionAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionAyuda.Name = "optionAyuda";
            this.optionAyuda.Size = new System.Drawing.Size(83, 29);
            this.optionAyuda.Text = "Ayuda";
            // 
            // optionAcercaDe
            // 
            this.optionAcercaDe.BackColor = System.Drawing.Color.White;
            this.optionAcercaDe.Name = "optionAcercaDe";
            this.optionAcercaDe.Size = new System.Drawing.Size(207, 30);
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
            this.menuStrip1.Size = new System.Drawing.Size(901, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // GUIPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = global::POCClienteEmpleado.Properties.Resources.Adobe_Express___file;
            this.ClientSize = new System.Drawing.Size(901, 506);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.ToolStripMenuItem televisorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem televisorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem televisorToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem televisorToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem televisorToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem televisorToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem marcaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disponiToolStripMenuItem;
    }
}

