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
                var request = new RestRequest("/televisor", Method.Get);
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
                        tabla.Columns.Add("controles");

                        foreach (var televisor in root.EnumerateArray())
                        {
                            int codigo = televisor.GetProperty("numeroReferencia").GetInt32();
                            string nombre = televisor.GetProperty("nombre").GetString();
                            string marca = televisor.GetProperty("marca").GetString();
                            string fecha = televisor.GetProperty("fechaCreacion").GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                            double precio = televisor.GetProperty("precio").GetDouble();

                            string codigosControles = "";
                            if (televisor.TryGetProperty("controles", out JsonElement controlesElem) &&
                                controlesElem.ValueKind == JsonValueKind.Array)
                            {
                                List<string> codigos = new List<string>();
                                foreach (JsonElement c in controlesElem.EnumerateArray())
                                {
                                    if (c.TryGetProperty("numeroReferencia", out JsonElement numElem))
                                    {
                                        codigos.Add(numElem.GetInt32().ToString());
                                    }
                                }
                                codigosControles = string.Join(", ", codigos);
                            }

                            tabla.Rows.Add(codigo, nombre, marca, fecha, precio, codigosControles);
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
