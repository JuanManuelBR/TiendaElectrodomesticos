using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIEditControl : Form
    {
        public GUIEditControl()
        {
            InitializeComponent();
            btnActualizar.Enabled = false;
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

                        btnActualizar.Enabled = true;
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
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoControl = int.Parse(txtCodigoControlBuscar.Text);
                string nombre = txtNombreControl.Text;
                double alcance = double.Parse(txtAlcanceControl.Text);
                string marca = txtMarcaControl.Text;
                DateTime fechaVenta = DateTime.Parse(txtFechaVentaControl.Text);

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/televisor/control/{codigoControl}", Method.Put);
                var body = new
                {
                    conumeroReferenciadigo = codigoControl,
                    nombre = nombre,
                    alcance = alcance,
                    marca = marca,
                    fechaVenta = fechaVenta.ToString("yyyy-MM-dd'T'HH:mm:ss")
                };

                request.AddJsonBody(body);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Control actualizado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el control: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar el control: " + ex.Message,
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
            btnActualizar.Enabled = false;
        }
    }
}
