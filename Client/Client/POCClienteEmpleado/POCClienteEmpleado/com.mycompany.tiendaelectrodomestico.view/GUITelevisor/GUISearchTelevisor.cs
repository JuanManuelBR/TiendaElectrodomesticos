using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUISearchTelevisor : Form
    {
        public GUISearchTelevisor()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigoTelevisorBuscar.Text))
                {
                    MessageBox.Show("Por favor ingrese un código de televisor.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCodigoTelevisorBuscar.Text, out int codigoBuscar))
                {
                    MessageBox.Show("El código debe ser un número válido.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/televisor/{codigoBuscar}", Method.Get);
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        JsonElement root = doc.RootElement;

                        txtNombreTelevisor.Text = root.GetProperty("nombre").GetString();
                        txtPrecioTelevisor.Text = root.GetProperty("precio").GetDouble().ToString("F2");
                        txtMarcaTelevisor.Text = root.GetProperty("marca").GetString();
                        txtFechaCreacionTelevisor.Text = root.GetProperty("fechaCreacion")
                            .GetDateTime()
                            .ToString("yyyy-MM-dd HH:mm:ss");

                        // Controles remotos
                        string codigosControles = "";
                        if (root.TryGetProperty("controles", out JsonElement controlesElem) &&
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

                        txtCodigoControl.Text = string.IsNullOrEmpty(codigosControles)
                            ? "No hay controles asignados"
                            : codigosControles;

                        // TVBoxes
                        string codigosTvBoxes = "";
                        if (root.TryGetProperty("tvboxNumeroReferencias", out JsonElement tvBoxesElem) &&
                            tvBoxesElem.ValueKind == JsonValueKind.Array)
                        {
                            List<string> boxNums = new List<string>();
                            foreach (JsonElement b in tvBoxesElem.EnumerateArray())
                            {
                                boxNums.Add(b.GetInt32().ToString());
                            }
                            codigosTvBoxes = string.Join(", ", boxNums);
                        }

                        txtTvBox.Text = string.IsNullOrEmpty(codigosTvBoxes)
                            ? "No hay TVBoxes asignados"
                            : codigosTvBoxes;
                    }
                }
                else
                {
                    txtNombreTelevisor.Clear();
                    txtPrecioTelevisor.Clear();
                    txtMarcaTelevisor.Clear();
                    txtFechaCreacionTelevisor.Clear();
                    txtCodigoControl.Clear();
                    txtTvBox.Clear();

                    MessageBox.Show(response.Content,
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
