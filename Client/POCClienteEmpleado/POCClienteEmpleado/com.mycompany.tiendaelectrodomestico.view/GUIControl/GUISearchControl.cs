using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUISearchControl : Form
    {
        public GUISearchControl()
        {
            InitializeComponent();
        }

  
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
            
                if (string.IsNullOrWhiteSpace(txtCodigoControlBuscar.Text))
                {
                    MessageBox.Show("Por favor ingresa el código del control a buscar.",
                        "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int codigoBuscar = int.Parse(txtCodigoControlBuscar.Text);

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/televisor/control/{codigoBuscar}", Method.Get);

                var response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
                {
                    using (JsonDocument doc = JsonDocument.Parse(response.Content))
                    {
                        JsonElement root = doc.RootElement;

   
                        txtNombreControl.Text = root.GetProperty("nombre").GetString();
                        txtAlcanceControl.Text = root.GetProperty("alcance").GetDouble().ToString();
                        txtMarcaControl.Text = root.GetProperty("marca").GetString();
                        txtFechaVentaControl.Text = root.GetProperty("fechaVenta")
                            .GetDateTime()
                            .ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    LimpiarCampos();
                    MessageBox.Show("No se encontró ningún control con ese código.",
                        "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar el control: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void LimpiarCampos()
        {
            txtCodigoControlBuscar.Clear();
            txtNombreControl.Clear();
            txtAlcanceControl.Clear();
            txtMarcaControl.Clear();
            txtFechaVentaControl.Clear();
        }
    }
}
