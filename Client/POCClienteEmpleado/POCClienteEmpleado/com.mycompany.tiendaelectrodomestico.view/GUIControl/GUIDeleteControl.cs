using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIDeleteControl : Form
    {
        public GUIDeleteControl()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
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

                        btnEliminar.Enabled = true;
                    }
                }
                else
                {
                    LimpiarCampos();
                    MessageBox.Show("No se encontró el control con ese código.",
                        "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar el control: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoEliminar = int.Parse(txtCodigoControlBuscar.Text);

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/televisor/control/{codigoEliminar}", Method.Delete);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Control eliminado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el control: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el control: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            btnEliminar.Enabled = false;
        }
    }
}
