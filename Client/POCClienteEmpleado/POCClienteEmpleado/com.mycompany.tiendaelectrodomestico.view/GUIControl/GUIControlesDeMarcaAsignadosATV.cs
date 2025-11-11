using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIControlesDeMarcaAsignadosATV : Form
    {
        private RestClient client;

        public GUIControlesDeMarcaAsignadosATV()
        {
            InitializeComponent();

            var options = new RestClientOptions("http://localhost:8090")
            {
                Authenticator = new HttpBasicAuthenticator("admin", "admin")
            };
            client = new RestClient(options);

            this.Load += GUIFiltrarControl_Load;
        }

        private void GUIFiltrarControl_Load(object sender, EventArgs e)
        {
            // Configurar columnas del DataGridView
            gridControles.Columns.Clear();
            gridControles.Columns.Add("id", "ID");
            gridControles.Columns.Add("numeroReferencia", "Número Referencia");
            gridControles.Columns.Add("nombre", "Nombre");
            gridControles.Columns.Add("marca", "Marca");
            gridControles.Columns.Add("alcance", "Alcance (m)");
            gridControles.Columns.Add("fechaVenta", "Fecha de Venta");
            gridControles.Columns.Add("idTelevisor", "ID Televisor");

            gridControles.Columns["fechaVenta"].Width = 150;

            try
            {
                var request = new RestRequest("/televisor/control/marcas", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement marcaElem in doc.RootElement.EnumerateArray())
                        {
                            if (marcaElem.ValueKind == JsonValueKind.String)
                                cmbMarcas.Items.Add(marcaElem.GetString());
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
                var request = new RestRequest($"/televisor/control/filtrar/{marcaSeleccionada}", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
                {
                    gridControles.Rows.Clear();

                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement control in doc.RootElement.EnumerateArray())
                        {
                            // Extraer ID
                            int id = control.GetProperty("id").GetInt32();

                            // Extraer numeroReferencia
                            int numeroReferencia = control.GetProperty("numeroReferencia").GetInt32();

                            // Extraer nombre
                            string nombre = control.GetProperty("nombre").GetString();

                            // Extraer marca
                            string marca = control.GetProperty("marca").GetString();

                            // Extraer alcance
                            double alcance = 0;
                            if (control.TryGetProperty("alcance", out JsonElement alcanceElem) &&
                                alcanceElem.ValueKind == JsonValueKind.Number)
                            {
                                alcance = alcanceElem.GetDouble();
                            }

                            // Extraer fechaVenta
                            string fecha = "";
                            if (control.TryGetProperty("fechaVenta", out JsonElement fechaElem) &&
                                fechaElem.ValueKind == JsonValueKind.String)
                            {
                                if (DateTime.TryParse(fechaElem.GetString(), out DateTime dt))
                                {
                                    fecha = dt.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }

                            // Extraer ID del televisor
                            string idTelevisor = "";
                            if (control.TryGetProperty("televisor", out JsonElement tvElem) &&
                                tvElem.ValueKind == JsonValueKind.Object)
                            {
                                if (tvElem.TryGetProperty("id", out JsonElement tvIdElem) &&
                                    tvIdElem.ValueKind == JsonValueKind.Number)
                                {
                                    idTelevisor = tvIdElem.GetInt32().ToString();
                                }
                            }

                            // Agregar fila a la tabla
                            gridControles.Rows.Add(id, numeroReferencia, nombre, marca, alcance, fecha, idTelevisor);
                        }
                    }

                    if (gridControles.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron controles de esa marca asociados a televisores.");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron controles de esa marca.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}