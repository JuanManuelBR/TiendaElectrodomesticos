using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUISearchTvBox : Form
    {
        public GUISearchTvBox()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar input
                if (string.IsNullOrWhiteSpace(txtCodigoTvBoxBuscar.Text))
                {
                    MessageBox.Show("Por favor ingrese un ID de TVBox.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCodigoTvBoxBuscar.Text, out int idBuscar))
                {
                    MessageBox.Show("El ID debe ser un número válido.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cliente REST
                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);

                // Nuevo endpoint TVBOX
                var request = new RestRequest($"/televisor/tvbox/numero/{idBuscar}", Method.Get);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    txtNombreTvBox.Clear();
                    txtPrecioTvBox.Clear();
                    txtMarcaTvBox.Clear();
                    txtFechaCreacionTvBox.Clear();
                    txtTelevisorAsociado.Clear();

                    MessageBox.Show("No se encontró el TVBox o hubo un error.\n" + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Procesar JSON
                using (JsonDocument doc = JsonDocument.Parse(response.Content))
                {
                    JsonElement root = doc.RootElement;

                    txtNombreTvBox.Text = root.GetProperty("nombre").GetString();
                    txtPrecioTvBox.Text = root.GetProperty("precio").GetDouble().ToString("F2");
                    txtMarcaTvBox.Text = root.GetProperty("marca").GetString();
                    txtFechaCreacionTvBox.Text =
                        DateTime.Parse(root.GetProperty("fechaCreacion").GetString())
                        .ToString("yyyy-MM-dd HH:mm:ss");

                    // televisorNumeroReferencia es opcional
                    if (root.TryGetProperty("televisorNumeroReferencia", out JsonElement tvRefElem) &&
                        tvRefElem.ValueKind != JsonValueKind.Null)
                    {
                        txtTelevisorAsociado.Text = tvRefElem.GetInt32().ToString();
                    }
                    else
                    {
                        txtTelevisorAsociado.Text = ""; // No tiene televisor asociado
                    }
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
