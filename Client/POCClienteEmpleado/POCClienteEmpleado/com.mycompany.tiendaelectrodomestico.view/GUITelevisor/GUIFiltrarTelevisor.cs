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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIFiltrarTelevisor : Form
    {
        private RestClient client;
        public GUIFiltrarTelevisor()
        {
            InitializeComponent();
            var options = new RestClientOptions("http://localhost:8090")
            {
                Authenticator = new HttpBasicAuthenticator("admin", "admin")
            };
            client = new RestClient(options);

            this.Load += GUIFiltrarTelevisor_Load;
        }

        private void GUIFiltrarTelevisor_Load(object sender, EventArgs e)
        {
            gridTelevisores.Columns.Add("numeroReferencia", "numeroReferencia");
            gridTelevisores.Columns.Add("Nombre", "Nombre");
            gridTelevisores.Columns.Add("Precio", "Precio");
            gridTelevisores.Columns.Add("Marca", "Marca");
            gridTelevisores.Columns.Add("FechaCreacion", "Fecha de Creación");
            gridTelevisores.Columns.Add("CodigoControl", "Controles");
            gridTelevisores.Columns["FechaCreacion"].Width = 150;

            try
            {
                var request = new RestRequest("/televisor/marcas", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement marca in doc.RootElement.EnumerateArray())
                        {
                            cmbMarcas.Items.Add(marca.GetString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error al cargar marcas: " + response.Content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excepción al cargar marcas: " + ex.Message);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (cmbMarcas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una marca primero.");
                return;
            }

            string marcaSeleccionada = cmbMarcas.SelectedItem.ToString();

            try
            {
                var request = new RestRequest($"/televisor/filtrar/{marcaSeleccionada}", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    gridTelevisores.Rows.Clear();

                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement tv in doc.RootElement.EnumerateArray())
                        {
                            int codigo = tv.GetProperty("numeroReferencia").GetInt32();
                            string nombre = tv.GetProperty("nombre").GetString();
                            double precio = tv.GetProperty("precio").GetDouble();
                            string marca = tv.GetProperty("marca").GetString();
                            string fecha = tv.GetProperty("fechaCreacion").GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                            string codigosControles = "";
                            if (tv.TryGetProperty("controles", out JsonElement controlesElem) &&
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

                            gridTelevisores.Rows.Add(codigo, nombre, precio, marca, fecha, codigosControles);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron televisores de esa marca.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
