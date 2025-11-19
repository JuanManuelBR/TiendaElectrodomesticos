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
    public partial class GUIListTvBox : Form
    {
        public GUIListTvBox()
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
                var request = new RestRequest("/televisor/tvbox/all", Method.Get); // Cambiado el endpoint
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        var root = doc.RootElement;

                        DataTable tabla = new DataTable();
                        tabla.Columns.Add("numeroReferencia");
                        tabla.Columns.Add("nombre");
                        tabla.Columns.Add("marca");
                        tabla.Columns.Add("fechaCreacion");
                        tabla.Columns.Add("precio");
                        tabla.Columns.Add("televisorNumeroReferencia"); // Nuevo campo

                        foreach (var tvbox in root.EnumerateArray())
                        {
                            int numeroReferencia = tvbox.GetProperty("numeroReferencia").GetInt32();
                            string nombre = tvbox.GetProperty("nombre").GetString();
                            string marca = tvbox.GetProperty("marca").GetString();
                            string fecha = tvbox.GetProperty("fechaCreacion").GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                            double precio = tvbox.GetProperty("precio").GetDouble();
                            string televisorNumeroReferencia = tvbox.TryGetProperty("televisorNumeroReferencia", out JsonElement tnrElem) && tnrElem.ValueKind != JsonValueKind.Null
                                ? tnrElem.GetInt32().ToString()
                                : "";

                            tabla.Rows.Add(numeroReferencia, nombre, marca, fecha, precio, televisorNumeroReferencia);
                        }

                        dgvTvBox.DataSource = tabla;
                        dgvTvBox.Columns["fechaCreacion"].Width = 150;
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
