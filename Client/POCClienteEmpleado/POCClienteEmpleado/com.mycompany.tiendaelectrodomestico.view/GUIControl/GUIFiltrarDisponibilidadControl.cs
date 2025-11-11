using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIFiltrarDisponibilidadControl : Form
    {
        private RestClient client;

        public GUIFiltrarDisponibilidadControl()
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
        }

        private void CargarControlesDisponibles()
        {
            try
            {
                var request = new RestRequest("/televisor/control/disponibles", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
                {
                    gridControles.Rows.Clear();

                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement c in doc.RootElement.EnumerateArray())
                        {
                            int codigo = c.GetProperty("numeroReferencia").GetInt32();
                            string nombre = c.GetProperty("nombre").GetString();
                            double alcance = c.GetProperty("alcance").GetDouble();

                            string marca = "";
                            if (c.TryGetProperty("marca", out JsonElement marcaElem) && marcaElem.ValueKind == JsonValueKind.String)
                                marca = marcaElem.GetString();

                            string fecha = "";
                            if (c.TryGetProperty("fechaVenta", out JsonElement fechaElem) && fechaElem.ValueKind == JsonValueKind.String)
                                fecha = DateTime.Parse(fechaElem.GetString()).ToString("yyyy-MM-dd HH:mm:ss");

                            gridControles.Rows.Add(codigo, nombre, alcance, marca, fecha);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay controles disponibles.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener controles disponibles: " + ex.Message);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
    
            CargarControlesDisponibles();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
