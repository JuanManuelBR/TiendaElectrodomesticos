﻿using POCClienteEmpleado;
using POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCTiendaElectrodomestico
{
    public partial class GUIPrincipal : Form
    {
        public GUIPrincipal()
        {
            InitializeComponent();
        }

        private void optionSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void optionAcercaDe_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Desarrollado por:\nJuan Manuel Blandón Ramirez\nMiguel Angel Murillo De Los Rios\nDavid Estiven Mendez Lara\nJaminton Julian Leyton Camacho",
            "Acerca de",
             MessageBoxButtons.OK,
             MessageBoxIcon.Information
            );

        }

        private void optionLavadora_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "ESTAMOS TRABAJANDO EN ESTO\nPOR FAVOR TENGANOS PACIENCIA",
            "Alerta",
             MessageBoxButtons.OK,
             MessageBoxIcon.Error
            );
        }

        private void addTelevisorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIAddTelevisor gui = new GUIAddTelevisor();
            gui.Show();
        }

        private void addControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIAddControl gui = new GUIAddControl();
            gui.Show();
        }

        private void searchTelevisorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUISearchTelevisor gui = new GUISearchTelevisor();
            gui.Show();
        }

        private void searchControlToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUISearchControl gui = new GUISearchControl();
            gui.Show();
        }

        private void editTelevisorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GUIEditTelevisor gui = new GUIEditTelevisor();
            gui.Show();
        }

        private void editControlToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GUIEditControl gui = new GUIEditControl();
            gui.Show();
        }

        private void ListTelevisorToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GUIListTelevisor gui = new GUIListTelevisor();
            gui.Show();
        }

        private void listControlToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GUIListControl gui = new GUIListControl();
            gui.Show();
        }

        private void filtrarTelevisorToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            GUIFiltrarTelevisor gui = new GUIFiltrarTelevisor();
            gui.Show();
        }

        private void deleteTelevisorToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            GUIDeleteTelevisor gui = new GUIDeleteTelevisor();
            gui.Show();
        }

        private void deleteControlToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            GUIDeleteControl gui = new GUIDeleteControl();
            gui.Show();
        }

        private void filtrarMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIFiltrarMarcaControl gui = new GUIFiltrarMarcaControl();
            gui.Show();
        }

        private void filtrarDisponibilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIFiltrarDisponibilidadControl gui = new GUIFiltrarDisponibilidadControl();
            gui.Show();
        }
    }
}
