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
                // 🎮 CÓDIGOS DE CONTROL REMOTO (separados por coma)
                // =============================
                List<int> codigosControles = new List<int>();

                if (!string.IsNullOrWhiteSpace(txtCodigoControl.Text))
                {
                    var partes = txtCodigoControl.Text.Split(',');

                    foreach (var parte in partes)
                    {
                        if (int.TryParse(parte.Trim(), out int codigo))
                        {
                            codigosControles.Add(codigo);
                        }
                        else
                        {
                            MessageBox.Show($"El valor '{parte}' no es un número válido.",
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
                        MessageBox.Show($"El control con código {codigoControl} no existe. " +
                            "Verifica que todos los controles existan antes de continuar.",
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
                // 🎮 ASIGNAR CONTROLES (si hay)
                // =============================
                if (codigosControles.Count == 0)
                {
                    MessageBox.Show("Televisor creado correctamente sin controles remotos.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool todosAsignados = true;
                bool errorAsignacion = false;

                foreach (var codigoControl in codigosControles)
                {
                    var requestAsignar = new RestRequest($"/televisor/asignarControl/{codigoTv}/{codigoControl}", Method.Put);
                    var responseAsignar = client.Execute(requestAsignar);

                    if (!responseAsignar.IsSuccessful)
                    {
                        todosAsignados = false;

                        if (responseAsignar.Content != null &&
                            responseAsignar.Content.Contains("No se pudo asignar el control remoto."))
                        {
                            MessageBox.Show($"El control {codigoControl} ya está asignado a otro televisor.\n" +
                                "El televisor no será creado para evitar inconsistencias.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            errorAsignacion = true;
                            break; // Detenemos el bucle
                        }
                        else
                        {
                            MessageBox.Show($"Error al asignar el control {codigoControl}: {responseAsignar.Content}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            errorAsignacion = true;
                            break;
                        }
                    }
                }
                if (errorAsignacion)
                {
                    var requestDelete = new RestRequest($"/televisor/{codigoTv}", Method.Delete);
                    client.Execute(requestDelete);
                    return;
                }
                if (todosAsignados)
                {
                    MessageBox.Show("Televisor creado y todos los controles asignados correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El televisor fue creado, pero uno o más controles no pudieron asignarse.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
