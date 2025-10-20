using RestSharp;
using RestSharp.Authenticators;
using System;
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
                var request = new RestRequest($"/electrodomestico/{codigoBuscar}", Method.Get);
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

                    
                        if (root.TryGetProperty("controlRemoto", out JsonElement controlElem) &&
                            controlElem.ValueKind == JsonValueKind.Object)
                        {
                        
                            if (controlElem.TryGetProperty("codigo", out JsonElement codigoControlElem))
                            {
                                txtCodigoControl.Text = codigoControlElem.GetInt32().ToString();
                            }
                            else
                            {
                                txtCodigoControl.Text = "No hay control asignado";
                            }
                        }
                        else
                        {
                         
                            txtCodigoControl.Text = "No hay control asignado";
                        }
                    }
                }
                else
                {
                 
                    txtNombreTelevisor.Clear();
                    txtPrecioTelevisor.Clear();
                    txtMarcaTelevisor.Clear();
                    txtFechaCreacionTelevisor.Clear();
                    txtCodigoControl.Clear();

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
