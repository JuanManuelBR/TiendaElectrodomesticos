using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIEditTelevisor : Form
    {
        public GUIEditTelevisor()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoBuscar = int.Parse(txtCodigoTelevisorBuscar.Text);

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
                        txtPrecioTelevisor.Text = root.GetProperty("precio").GetDouble().ToString();
                        txtMarcaTelevisor.Text = root.GetProperty("marca").GetString();
                        txtFechaCreacionTelevisor.Text = root.GetProperty("fechaCreacion")
                            .GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                     
                        if (root.TryGetProperty("controlRemoto", out JsonElement controlElem) &&
                            controlElem.ValueKind != JsonValueKind.Null)
                        {
                          
                            if (controlElem.TryGetProperty("codigo", out JsonElement codElem))
                            {
                                txtCodigoControl.Text = codElem.GetInt32().ToString();
                            }
                            else if (controlElem.ValueKind == JsonValueKind.Number)
                            {
                                txtCodigoControl.Text = controlElem.GetInt32().ToString();
                            }
                            else
                            {
                                txtCodigoControl.Text = "";
                            }
                        }
                        else
                        {
                            txtCodigoControl.Text = "";
                        }
                    }
                }
                else
                {
                    txtNombreTelevisor.Clear();
                    txtPrecioTelevisor.Clear();
                    txtMarcaTelevisor.Clear();
                    txtCodigoControl.Clear();

                    MessageBox.Show("Televisor no encontrado o error al obtener datos.\n" + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoTv = int.Parse(txtCodigoTelevisorBuscar.Text);
                string nombreTv = txtNombreTelevisor.Text;
                double precioTv = double.Parse(txtPrecioTelevisor.Text);
                string marcaTv = txtMarcaTelevisor.Text;
                DateTime fechaCreacion = DateTime.Parse(txtFechaCreacionTelevisor.Text);

                int? codigoControl = null;
                if (!string.IsNullOrWhiteSpace(txtCodigoControl.Text))
                {
                    if (int.TryParse(txtCodigoControl.Text, out int parsed))
                        codigoControl = parsed;
                    else
                    {
                        MessageBox.Show("El código del control debe ser un número válido.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };
                var client = new RestClient(options);

         
                var request = new RestRequest($"/electrodomestico/{codigoTv}", Method.Put);

                var body = new
                {
                    tipo = "televisor",
                    codigo = codigoTv,
                    nombre = nombreTv,
                    marca = marcaTv,
                    fechaCreacion = fechaCreacion.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    precio = precioTv
                };

                request.AddJsonBody(body);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Error al actualizar el televisor: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                if (codigoControl == null)
                {
                    MessageBox.Show("Televisor actualizado correctamente (sin control remoto).",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

               
                var requestCheck = new RestRequest($"/electrodomestico/control/{codigoControl}", Method.Get);
                var responseCheck = client.Execute(requestCheck);
                if (!responseCheck.IsSuccessful)
                {
                    MessageBox.Show("El control con ese código no existe. Ingrese un control válido.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

              
                var requestAsignar = new RestRequest($"/electrodomestico/asignarControl/{codigoTv}/{codigoControl}", Method.Put);
                var responseAsignar = client.Execute(requestAsignar);

                if (responseAsignar.IsSuccessful)
                {
                    MessageBox.Show("Televisor actualizado y control asignado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                   
                    MessageBox.Show("No se pudo asignar el control. Es posible que ya esté asignado a otro televisor.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
