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

        // 🔹 Guardamos los controles y TVBoxes actuales del televisor
        private List<int> controlesActuales = new List<int>();
        private List<int> tvBoxesActuales = new List<int>();

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
                        controlesActuales.Clear();
                        string codigosControles = "";
                        if (root.TryGetProperty("controles", out JsonElement controlesElem) &&
                            controlesElem.ValueKind == JsonValueKind.Array)
                        {
                            List<string> codigos = new List<string>();
                            foreach (JsonElement c in controlesElem.EnumerateArray())
                            {
                                if (c.TryGetProperty("numeroReferencia", out JsonElement numElem))
                                {
                                    int num = numElem.GetInt32();
                                    codigos.Add(num.ToString());
                                    controlesActuales.Add(num);
                                }
                            }
                            codigosControles = string.Join(", ", codigos);
                        }

                        txtCodigoControl.Text = string.IsNullOrEmpty(codigosControles)
                            ? "No hay controles asignados"
                            : codigosControles;

                        // TVBoxes
                        tvBoxesActuales.Clear();
                        string codigosTvBoxes = "";
                        if (root.TryGetProperty("tvboxNumeroReferencias", out JsonElement tvBoxesElem) &&
                            tvBoxesElem.ValueKind == JsonValueKind.Array)
                        {
                            List<string> boxNums = new List<string>();
                            foreach (JsonElement b in tvBoxesElem.EnumerateArray())
                            {
                                int num = b.GetInt32();
                                boxNums.Add(num.ToString());
                                tvBoxesActuales.Add(num);
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

                // ================================
                // 🔹 Manejo de controles
                // ================================
                var nuevosCodigos = new List<int>();
                if (!string.IsNullOrWhiteSpace(txtCodigoControl.Text))
                {
                    nuevosCodigos = txtCodigoControl.Text.Split(',')
                        .Select(p => p.Trim())
                        .Where(p => int.TryParse(p, out _))
                        .Select(int.Parse)
                        .ToList();
                }

                var agregarControles = nuevosCodigos.Except(controlesActuales).ToList();
                var quitarControles = controlesActuales.Except(nuevosCodigos).ToList();

                foreach (var codigoControl in agregarControles)
                {
                    var requestCheck = new RestRequest($"/televisor/control/{codigoControl}", Method.Get);
                    var responseCheck = client.Execute(requestCheck);
                    if (!responseCheck.IsSuccessful)
                        continue;

                    var requestAsignar = new RestRequest($"/televisor/asignarControl/{codigoTv}/{codigoControl}", Method.Put);
                    client.Execute(requestAsignar);
                }

                foreach (var codigoControl in quitarControles)
                {
                    var requestDesasignar = new RestRequest($"/televisor/desasignarControl/{codigoTv}/{codigoControl}", Method.Put);
                    client.Execute(requestDesasignar);
                }

                // ================================
                // 🔹 Manejo de TVBoxes
                // ================================
                var nuevosTvBoxes = new List<int>();
                if (!string.IsNullOrWhiteSpace(txtTvBox.Text))
                {
                    nuevosTvBoxes = txtTvBox.Text.Split(',')
                        .Select(p => p.Trim())
                        .Where(p => int.TryParse(p, out _))
                        .Select(int.Parse)
                        .ToList();
                }

                var agregarTvBoxes = nuevosTvBoxes.Except(tvBoxesActuales).ToList();
                var quitarTvBoxes = tvBoxesActuales.Except(nuevosTvBoxes).ToList();

                foreach (var numeroTvBox in agregarTvBoxes)
                {
                    var requestAsignarTvBox = new RestRequest(
                        $"/televisor/tvbox/asignarTelevisor/{numeroTvBox}/{codigoTv}", Method.Put);
                    client.Execute(requestAsignarTvBox);
                }

                foreach (var numeroTvBox in quitarTvBoxes)
                {
                    var requestDesasignarTvBox = new RestRequest(
                        $"/televisor/tvbox/desasignar/{numeroTvBox}", Method.Put);
                    client.Execute(requestDesasignarTvBox);
                }

                MessageBox.Show("Televisor actualizado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                controlesActuales = nuevosCodigos;
                tvBoxesActuales = nuevosTvBoxes;
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
