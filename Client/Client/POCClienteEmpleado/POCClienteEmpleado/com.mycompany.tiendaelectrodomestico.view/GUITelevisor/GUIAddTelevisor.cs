using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIAddTelevisor : Form
    {
        public GUIAddTelevisor()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // =============================
                // 📺 DATOS DEL TELEVISOR
                // =============================
                int codigoTv = int.Parse(txtCodigoTelevisor.Text);
                string nombreTv = txtNombreTelevisor.Text;
                double precioTv = double.Parse(txtPrecioTelevisor.Text);
                string marcaTv = txtMarcaTelevisor.Text;
                DateTime fechaCreacionTv = txtFechaCreacionTelevisor.Value;

                // =============================
                // 🎮 CÓDIGOS DE CONTROL REMOTO
                // =============================
                List<int> codigosControles = new List<int>();
                if (!string.IsNullOrWhiteSpace(txtCodigoControl.Text))
                {
                    var partes = txtCodigoControl.Text.Split(',');
                    foreach (var parte in partes)
                    {
                        if (int.TryParse(parte.Trim(), out int codigo))
                            codigosControles.Add(codigo);
                        else
                        {
                            MessageBox.Show($"El valor '{parte}' no es un número válido.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // =============================
                // 📦 NÚMEROS DE TVBOX
                // =============================
                List<int> tvBoxes = new List<int>();
                if (!string.IsNullOrWhiteSpace(txtTvBoxes.Text))
                {
                    var partesTv = txtTvBoxes.Text.Split(',');
                    foreach (var parte in partesTv)
                    {
                        if (int.TryParse(parte.Trim(), out int tvBoxNum))
                            tvBoxes.Add(tvBoxNum);
                        else
                        {
                            MessageBox.Show($"El valor '{parte}' en TVBoxes no es un número válido.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // =============================
                // 🔗 CLIENTE REST
                // =============================
                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };
                var client = new RestClient(options);

                // =============================
                // ✅ VALIDAR CONTROLES EXISTENTES
                // =============================
                foreach (var codigoControl in codigosControles)
                {
                    var requestCheck = new RestRequest($"/televisor/control/{codigoControl}", Method.Get);
                    var responseCheck = client.Execute(requestCheck);

                    if (!responseCheck.IsSuccessful)
                    {
                        MessageBox.Show($"El control con código {codigoControl} no existe.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // =============================
                // ✅ VALIDAR TVBOX EXISTENTES
                // =============================
                foreach (var tvBoxNum in tvBoxes)
                {
                    var requestCheckTvBox = new RestRequest($"http://localhost:8091/api/tvbox/numero/{tvBoxNum}", Method.Get);
                    var responseTvBox = client.Execute(requestCheckTvBox);

                    if (!responseTvBox.IsSuccessful)
                    {
                        MessageBox.Show($"El TVBox con número de referencia {tvBoxNum} no existe.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // =============================
                // 🧱 CREAR TELEVISOR
                // =============================
                var request = new RestRequest("/televisor", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var body = new
                {
                    tipo = "televisor",
                    numeroReferencia = codigoTv,
                    nombre = nombreTv,
                    marca = marcaTv,
                    fechaCreacion = fechaCreacionTv.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    precio = precioTv
                };

                request.AddJsonBody(body);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Error al crear el televisor: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // =============================
                // 🎮 ASIGNAR CONTROLES
                // =============================
                foreach (var codigoControl in codigosControles)
                {
                    var requestAsignar = new RestRequest($"/televisor/asignarControl/{codigoTv}/{codigoControl}", Method.Put);
                    var responseAsignar = client.Execute(requestAsignar);

                    if (!responseAsignar.IsSuccessful)
                    {
                        MessageBox.Show($"Error al asignar el control {codigoControl}: {responseAsignar.Content}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        var requestDelete = new RestRequest($"/televisor/{codigoTv}", Method.Delete);
                        client.Execute(requestDelete);
                        return;
                    }
                }

                // =============================
                // 📦 ASIGNAR TVBOX
                // =============================
                foreach (var tvBoxNum in tvBoxes)
                {
                    var requestAsignarTvBox = new RestRequest($"televisor/tvbox/asignarTelevisor/{tvBoxNum}/{codigoTv}", Method.Put);
                    var responseAsignarTvBox = client.Execute(requestAsignarTvBox);

                    if (!responseAsignarTvBox.IsSuccessful)
                    {
                        MessageBox.Show($"Error al asignar el TVBox {tvBoxNum}: {responseAsignarTvBox.Content}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Eliminar televisor si falla asignación
                        var requestDelete = new RestRequest($"/televisor/{codigoTv}", Method.Delete);
                        client.Execute(requestDelete);
                        return;
                    }
                }

                MessageBox.Show("Televisor creado y todos los controles y TVBoxes asignados correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GUIAddTelevisor_Load(object sender, EventArgs e)
        {

        }
    }
}
