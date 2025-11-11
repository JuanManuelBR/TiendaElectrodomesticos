using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIEditTelevisor : Form
    {
        public GUIEditTelevisor()
        {
            InitializeComponent();
        }

        // 🔹 Guardamos los controles actuales del televisor
        private List<int> controlesActuales = new List<int>();

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
                var request = new RestRequest($"/televisor/{codigoBuscar}", Method.Get);
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

                        // 🔹 Leer lista de controles
                        controlesActuales.Clear();
                        if (root.TryGetProperty("controles", out JsonElement controlesElem) && controlesElem.ValueKind == JsonValueKind.Array)
                        {
                            foreach (JsonElement control in controlesElem.EnumerateArray())
                            {
                                if (control.TryGetProperty("numeroReferencia", out JsonElement numRefElem))
                                {
                                    controlesActuales.Add(numRefElem.GetInt32());
                                }
                            }
                        }

                        // Mostrar los números separados por comas
                        txtCodigoControl.Text = string.Join(",", controlesActuales);
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

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };
                var client = new RestClient(options);

                // 🔹 Actualizar datos del televisor
                var request = new RestRequest($"/televisor/{codigoTv}", Method.Put);
                var body = new
                {
                    tipo = "televisor",
                    numeroReferencia = codigoTv,
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

                // 🔹 Manejo de asignación/desasignación de controles
                var nuevosCodigos = new List<int>();
                if (!string.IsNullOrWhiteSpace(txtCodigoControl.Text))
                {
                    var partes = txtCodigoControl.Text.Split(',')
                        .Select(p => p.Trim())
                        .Where(p => int.TryParse(p, out _))
                        .Select(int.Parse)
                        .ToList();
                    nuevosCodigos.AddRange(partes);
                }

                // Determinar cuáles se agregaron y cuáles se quitaron
                var agregar = nuevosCodigos.Except(controlesActuales).ToList();
                var quitar = controlesActuales.Except(nuevosCodigos).ToList();

                // 🔹 Asignar nuevos controles
                foreach (var codigoControl in agregar)
                {
                    var requestCheck = new RestRequest($"/televisor/control/{codigoControl}", Method.Get);
                    var responseCheck = client.Execute(requestCheck);
                    if (!responseCheck.IsSuccessful)
                    {
                        MessageBox.Show($"El control {codigoControl} no existe. No se asignará.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    var requestAsignar = new RestRequest($"/televisor/asignarControl/{codigoTv}/{codigoControl}", Method.Put);
                    var responseAsignar = client.Execute(requestAsignar);

                    if (!responseAsignar.IsSuccessful)
                    {
                        MessageBox.Show($"No se pudo asignar el control {codigoControl}. Es posible que ya esté asignado a otro televisor.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // 🔹 Desasignar controles eliminados
                foreach (var codigoControl in quitar)
                {
                    var requestDesasignar = new RestRequest($"/televisor/desasignarControl/{codigoTv}/{codigoControl}", Method.Put);
                    var responseDesasignar = client.Execute(requestDesasignar);

                    if (!responseDesasignar.IsSuccessful)
                    {
                        MessageBox.Show($"No se pudo desasignar el control {codigoControl}.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                MessageBox.Show("Televisor actualizado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Actualizamos la lista actual de controles para futuras ediciones
                controlesActuales = nuevosCodigos;
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
