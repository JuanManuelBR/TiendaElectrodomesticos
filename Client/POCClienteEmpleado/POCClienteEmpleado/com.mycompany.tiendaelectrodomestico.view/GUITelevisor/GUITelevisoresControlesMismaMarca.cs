using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUITelevisoresControlesMismaMarca : Form
    {
        private RestClient client;

        public GUITelevisoresControlesMismaMarca()
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
            // Limpiar columnas si ya existen
            gridControles.Columns.Clear();

            gridControles.Columns.Add("numeroReferencia", "Número Referencia");
            gridControles.Columns.Add("MarcaControl", "Marca Control");
            gridControles.Columns.Add("Alcance", "Alcance");
            gridControles.Columns.Add("IdTelevisor", "ID Televisor");
            gridControles.Columns.Add("NombreTelevisor", "Nombre Televisor");
            gridControles.Columns.Add("MarcaTelevisor", "Marca Televisor");

            CargarMarcas();
        }

        private void CargarMarcas()
        {
            cmbMarcas.Items.Clear();

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
                            {
                                string marca = marcaElem.GetString();
                                if (!string.IsNullOrEmpty(marca))
                                    cmbMarcas.Items.Add(marca);
                            }
                        }
                    }

                    if (cmbMarcas.Items.Count == 0)
                    {
                        MessageBox.Show("No se encontraron marcas disponibles.");
                    }
                }
                else
                {
                    MessageBox.Show("Error al cargar las marcas. Verifique la conexión con el servidor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar marcas: {ex.Message}");
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (cmbMarcas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una marca primero.");
                return;
            }

            string marcaSeleccionada = cmbMarcas.SelectedItem.ToString();

            try
            {
                var request = new RestRequest($"/televisor/controles/marca/{marcaSeleccionada}", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
                {
                    gridControles.Rows.Clear();

                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement item in doc.RootElement.EnumerateArray())
                        {
                            // Extraer numeroReferenciaControl (camelCase)
                            int numeroReferencia = 0;
                            if (item.TryGetProperty("numeroReferenciaControl", out JsonElement numRefElem) &&
                                numRefElem.ValueKind == JsonValueKind.Number)
                                numeroReferencia = numRefElem.GetInt32();

                            // Extraer marcaControl (camelCase)
                            string marcaControl = "";
                            if (item.TryGetProperty("marcaControl", out JsonElement marcaCtrlElem) &&
                                marcaCtrlElem.ValueKind == JsonValueKind.String)
                                marcaControl = marcaCtrlElem.GetString() ?? "";

                            // Extraer alcance (camelCase)
                            double alcance = 0;
                            if (item.TryGetProperty("alcance", out JsonElement alcanceElem) &&
                                alcanceElem.ValueKind == JsonValueKind.Number)
                                alcance = alcanceElem.GetDouble();

                            // Extraer idTelevisor (camelCase)
                            int idTelevisor = 0;
                            if (item.TryGetProperty("idTelevisor", out JsonElement idTvElem) &&
                                idTvElem.ValueKind == JsonValueKind.Number)
                                idTelevisor = idTvElem.GetInt32();

                            // Extraer nombreTelevisor (camelCase)
                            string nombreTelevisor = "";
                            if (item.TryGetProperty("nombreTelevisor", out JsonElement nombreTvElem) &&
                                nombreTvElem.ValueKind == JsonValueKind.String)
                                nombreTelevisor = nombreTvElem.GetString() ?? "";

                            // Extraer marcaTelevisor (camelCase)
                            string marcaTelevisor = "";
                            if (item.TryGetProperty("marcaTelevisor", out JsonElement marcaTvElem) &&
                                marcaTvElem.ValueKind == JsonValueKind.String)
                                marcaTelevisor = marcaTvElem.GetString() ?? "";

                            gridControles.Rows.Add(
                                numeroReferencia,
                                marcaControl,
                                alcance,
                                idTelevisor,
                                nombreTelevisor,
                                marcaTelevisor
                            );
                        }
                    }

                    if (gridControles.Rows.Count == 0)
                    {
                        MessageBox.Show($"No se encontraron televisores con controles de la marca: {marcaSeleccionada}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"No se encontraron televisores con controles de la marca: {marcaSeleccionada}");
                }
                else
                {
                    MessageBox.Show($"Error del servidor:\nStatus: {response.StatusCode}\nMensaje: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}