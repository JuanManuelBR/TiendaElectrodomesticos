using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIFiltrarMarcaControl : Form
    {
        private RestClient client;

        public GUIFiltrarMarcaControl()
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

            gridControles.Columns.Add("numeroReferencia", "numeroReferencia");
            gridControles.Columns.Add("Nombre", "Nombre");
            gridControles.Columns.Add("Alcance", "Alcance");
            gridControles.Columns.Add("Marca", "Marca");
            gridControles.Columns.Add("FechaVenta", "Fecha de Venta");
            gridControles.Columns["FechaVenta"].Width = 150;

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
                            int codigo = control.GetProperty("numeroReferencia").GetInt32();
                            string nombre = control.GetProperty("nombre").GetString();

                            double alcance = 0;
                            if (control.TryGetProperty("alcance", out JsonElement alcanceElem) && alcanceElem.ValueKind == JsonValueKind.Number)
                                alcance = alcanceElem.GetDouble();

                            string marca = "";
                            if (control.TryGetProperty("marca", out JsonElement marcaElem) && marcaElem.ValueKind == JsonValueKind.String)
                                marca = marcaElem.GetString();

                            string fecha = "";
                            if (control.TryGetProperty("fechaVenta", out JsonElement fechaElem) &&
                                (fechaElem.ValueKind == JsonValueKind.String || fechaElem.ValueKind == JsonValueKind.Number))
                            {
                                if (fechaElem.ValueKind == JsonValueKind.String && DateTime.TryParse(fechaElem.GetString(), out DateTime dt))
                                    fecha = dt.ToString("yyyy-MM-dd HH:mm:ss");
                                else if (fechaElem.ValueKind == JsonValueKind.Number)
                                    fecha = fechaElem.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                            }

                            string codigoTv = "";
                            if (control.TryGetProperty("televisor", out JsonElement tvElem) && tvElem.ValueKind == JsonValueKind.Object)
                            {
                                if (tvElem.TryGetProperty("codigo", out JsonElement tvCodigoElem) && tvCodigoElem.ValueKind == JsonValueKind.Number)
                                    codigoTv = tvCodigoElem.GetInt32().ToString();
                            }

                            gridControles.Rows.Add(codigo, nombre, alcance, marca, fecha, codigoTv);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron controles de esa marca.");
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
