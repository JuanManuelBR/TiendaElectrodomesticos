using POCClienteEmpleado;
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

        private void optionAddTelevisor_Click(object sender, EventArgs e)
        {
            GUIAddTelevisor gui = new GUIAddTelevisor();
            gui.Show();
        }

        private void optionSearchTelevisor_Click(object sender, EventArgs e)
        {
            GUISearchTelevisor gui = new GUISearchTelevisor();
            gui.Show();
        }

        private void optionEditTelevisor_Click(object sender, EventArgs e)
        {
            GUIEditTelevisor gui = new GUIEditTelevisor();
            gui.Show();
        }

        private void optionListTelevisor_Click(object sender, EventArgs e)
        {
            GUIListTelevisor gui = new GUIListTelevisor();
            gui.Show();
        }

        private void optionDeleteTelevisor_Click(object sender, EventArgs e)
        {
            GUIDeleteTelevisor gui = new GUIDeleteTelevisor();
            gui.Show();
        }

        private void optionFiltrarTelevisor_Click(object sender, EventArgs e)
        {
            GUIFiltrarTelevisor gui = new GUIFiltrarTelevisor();
            gui.Show();
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
    }
}
