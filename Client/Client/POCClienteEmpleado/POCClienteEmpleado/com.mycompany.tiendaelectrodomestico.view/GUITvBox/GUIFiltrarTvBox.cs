using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado.com.mycompany.tiendaelectrodomestico.view
{
    public partial class GUIFiltrarTvBox : Form
    {
        private RestClient client;

        public GUIFiltrarTvBox()
        {
            InitializeComponent();
            var options = new RestClientOptions("http://localhost:8090") // apuntando al MS cliente
            {
                Authenticator = new HttpBasicAuthenticator("admin", "admin")
            };
            client = new RestClient(options);

            this.Load += GUIFiltrarTvBox_Load;
        }

        private void GUIFiltrarTvBox_Load(object sender, EventArgs e)
        {
            gridTvBox.Columns.Clear();
            gridTvBox.Columns.Add("numeroReferencia", "Número Referencia");
            gridTvBox.Columns.Add("Nombre", "Nombre");
            gridTvBox.Columns.Add("Precio", "Precio");
            gridTvBox.Columns.Add("Marca", "Marca");
            gridTvBox.Columns.Add("FechaCreacion", "Fecha de Creación");
            gridTvBox.Columns.Add("televisorNumeroReferencia", "Televisor Nº Referencia"); // ← nueva columna
            gridTvBox.Columns["FechaCreacion"].Width = 150;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string marca = txtMarcaBuscar.Text.Trim();

            if (string.IsNullOrEmpty(marca))
            {
                MessageBox.Show("Ingrese una marca para filtrar.");
                return;
            }

            try
            {
                var request = new RestRequest($"/televisor/tvbox/marca/{marca}", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    gridTvBox.Rows.Clear();

                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        foreach (JsonElement tv in doc.RootElement.EnumerateArray())
                        {
                            int numeroReferencia = tv.GetProperty("numeroReferencia").GetInt32();
                            string nombre = tv.GetProperty("nombre").GetString();
                            double precio = tv.GetProperty("precio").GetDouble();
                            string marcaTv = tv.GetProperty("marca").GetString();
                            string fecha = tv.GetProperty("fechaCreacion").GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                            string televisorRef = tv.TryGetProperty("televisorNumeroReferencia", out JsonElement tvElem) &&
                                                  tvElem.ValueKind != JsonValueKind.Null
                                                  ? tvElem.GetInt32().ToString()
                                                  : "";

                            gridTvBox.Rows.Add(numeroReferencia, nombre, precio, marcaTv, fecha, televisorRef);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron TVBoxes para la marca especificada.");
                    gridTvBox.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar TVBoxes: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
