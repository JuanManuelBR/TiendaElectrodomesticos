using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUISearchTelevisor : Form
    {
        public GUISearchTelevisor()
        {
            InitializeComponent();
        }
        //BntBuscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {


            try
            {
                int codigoBuscar = int.Parse(txtCodigoTelevisorBuscar.Text);

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/electrodomestico/search/{codigoBuscar}", Method.Get);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    // 👇 Usamos using clásico
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        JsonElement root = doc.RootElement;

                        txtNombreTelevisor.Text = root.GetProperty("nombre").GetString();
                        txtPrecioTelevisor.Text = root.GetProperty("precio").GetDouble().ToString();
                        txtMarcaTelevisor.Text = root.GetProperty("marca").GetString();
                        txtFechaCreacionTelevisor.Text = root.GetProperty("fechaCreacion")
                        .GetDateTime()
                        .ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    // Limpiar campos si no se encontró nada
                    txtNombreTelevisor.Clear();
                    txtPrecioTelevisor.Clear();
                    txtMarcaTelevisor.Clear();

                    MessageBox.Show( response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Ocurrió un error: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //BntSalir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }

}
