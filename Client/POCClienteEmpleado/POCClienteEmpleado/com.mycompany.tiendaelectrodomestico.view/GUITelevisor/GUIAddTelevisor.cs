using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
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
                int codigoTv = int.Parse(txtCodigoTelevisor.Text);
                string nombreTv = txtNombreTelevisor.Text;
                double precioTv = double.Parse(txtPrecioTelevisor.Text);
                string marcaTv = txtMarcaTelevisor.Text;
                DateTime fechaCreacionTv = txtFechaCreacionTelevisor.Value;

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
                if (codigoControl != null)
                {
                    var requestCheck = new RestRequest($"/electrodomestico/control/{codigoControl}", Method.Get);
                    var responseCheck = client.Execute(requestCheck);

                    if (!responseCheck.IsSuccessful)
                    {
                        MessageBox.Show("El control con ese código no existe. Ingrese solo un control existente o deje el campo vacío.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var request = new RestRequest("/electrodomestico", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var body = new
                {
                    tipo = "televisor",
                    codigo = codigoTv,
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
                if (codigoControl == null)
                {
                    MessageBox.Show("Televisor creado correctamente sin control remoto.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var requestAsignar = new RestRequest($"/electrodomestico/asignarControl/{codigoTv}/{codigoControl}", Method.Put);
                var responseAsignar = client.Execute(requestAsignar);

                if (responseAsignar.IsSuccessful)
                {
                    MessageBox.Show("Televisor creado y control asignado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (responseAsignar.Content != null && responseAsignar.Content.Contains("No se pudo asignar el control remoto."))
                    {
                        var requestDelete = new RestRequest($"/electrodomestico/{codigoTv}", Method.Delete);
                        client.Execute(requestDelete);

                        MessageBox.Show("El control remoto ya está asignado a otro televisor. " +
                            "No se creó el televisor para evitar inconsistencias.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Error al asignar el control al televisor.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
