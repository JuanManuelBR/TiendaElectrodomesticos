using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIListTelevisor : Form
    {
        public GUIListTelevisor()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest("/electrodomestico/getTelevisores", Method.Get);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        var root = doc.RootElement;

                        DataTable tabla = new DataTable();
                        tabla.Columns.Add("codigo");
                        tabla.Columns.Add("nombre");
                        tabla.Columns.Add("marca");
                        tabla.Columns.Add("fechaCreacion");
                        tabla.Columns.Add("precio");

                        foreach (var televisor in root.EnumerateArray())
                        {
                            tabla.Rows.Add(
                                televisor.GetProperty("codigo").GetInt32(),
                                televisor.GetProperty("nombre").GetString(),
                                televisor.GetProperty("marca").GetString(),
                                televisor.GetProperty("fechaCreacion").GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"),
                                televisor.GetProperty("precio").GetDouble()
                            );
                        }

                        dgvTelevisores.DataSource = tabla;
                        dgvTelevisores.Columns["fechaCreacion"].Width = 150;
                    }
                }
                else
                {
                    MessageBox.Show("Error: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
