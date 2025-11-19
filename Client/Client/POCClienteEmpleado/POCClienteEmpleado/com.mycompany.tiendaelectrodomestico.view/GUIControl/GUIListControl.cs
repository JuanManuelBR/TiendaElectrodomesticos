using RestSharp;
using System;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIListControl : Form
    {
        public GUIListControl()
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
                var request = new RestRequest("/televisor/control", Method.Get);

                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        var root = doc.RootElement;

                        DataTable tabla = new DataTable();
                        tabla.Columns.Add("numeroReferencia");
                        tabla.Columns.Add("Nombre");
                        tabla.Columns.Add("Alcance (m)");
                        tabla.Columns.Add("Marca");
                        tabla.Columns.Add("Fecha de Venta");

                        foreach (var control in root.EnumerateArray())
                        {
                            tabla.Rows.Add(
                                control.GetProperty("numeroReferencia").GetInt32(),
                                control.GetProperty("nombre").GetString(),
                                control.GetProperty("alcance").GetDouble(),
                                control.GetProperty("marca").GetString(),
                                control.GetProperty("fechaVenta").GetDateTime().ToString("yyyy-MM-dd HH:mm:ss")
                            );
                        }

                        dgvControles.DataSource = tabla;
                        dgvControles.Columns["Fecha de Venta"].Width = 150;
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
