using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIEditTvBox : Form
    {
        public GUIEditTvBox()
        {
            InitializeComponent();
        }

        // 🔹 Guardamos los controles actuales del televisor
        private List<int> controlesActuales = new List<int>();

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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Número de referencia del TVBox
                if (!int.TryParse(txtCodigoTvBoxBuscar.Text, out int numeroReferencia))
                {
                    MessageBox.Show("El número de referencia debe ser válido.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Datos del formulario
                string nombre = txtNombreTvBox.Text;
                string marca = txtMarcaTvBox.Text;
                double precio = double.Parse(txtPrecioTvBox.Text);
                DateTime fechaCreacion = DateTime.Parse(txtFechaCreacionTvBox.Text);

                // -----------------------------------
                // VALIDAR TELEVISOR ASOCIADO
                // -----------------------------------
                int? televisorAsociado = null;

                if (string.IsNullOrWhiteSpace(txtTelevisorAsociado.Text))
                {
                    // → Caso: el campo está vacío → desasignar
                    televisorAsociado = null;
                }
                else
                {
                    // → Caso: el usuario escribió algo
                    if (!int.TryParse(txtTelevisorAsociado.Text, out int tvNuevo))
                    {
                        MessageBox.Show("El número de televisor debe ser numérico.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validar si el televisor existe
                    var clientCheck = new RestClient(new RestClientOptions("http://localhost:8090")
                    {
                        Authenticator = new HttpBasicAuthenticator("admin", "admin")
                    });

                    var requestCheck = new RestRequest($"/televisor/{tvNuevo}", Method.Get);
                    var responseCheck = clientCheck.Execute(requestCheck);

                    if (!responseCheck.IsSuccessful)
                    {
                        MessageBox.Show($"El televisor con número de referencia {tvNuevo} no existe.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // → Televisor existe → asignarlo
                    televisorAsociado = tvNuevo;
                }

                // Crear cliente REST
                var client = new RestClient(new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                });

                // -----------------------------------
                //      ARMAR PAYLOAD DE TVBOX
                // -----------------------------------
                var body = new
                {
                    id = 0,
                    numeroReferencia = numeroReferencia,
                    nombre = nombre,
                    precio = precio,
                    marca = marca,
                    fechaCreacion = fechaCreacion.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    televisorNumeroReferencia = televisorAsociado // ← aquí se asigna o desasigna
                };

                // -----------------------------------
                //   ENVIAR UPDATE AL BACKEND
                // -----------------------------------
                var request = new RestRequest($"/televisor/tvbox/referencia/{numeroReferencia}", Method.Put);
                string json = JsonSerializer.Serialize(body, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never // NO ignorar NULLS
                });
                request.AddStringBody(json, ContentType.Json);

                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Error al actualizar el TVBox:\n" + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("TVBox actualizado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
