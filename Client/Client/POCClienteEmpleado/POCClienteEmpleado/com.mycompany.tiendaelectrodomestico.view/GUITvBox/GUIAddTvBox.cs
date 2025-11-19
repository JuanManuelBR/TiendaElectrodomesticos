using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIAddTvBox : Form
    {
        public GUIAddTvBox()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // =============================
                // 📺 DATOS DEL TV BOX
                // =============================
                int codigoTv = int.Parse(txtCodigoTvBox.Text);
                string nombreTv = txtNombreTvBox.Text;
                double precioTv = double.Parse(txtPrecioTvBox.Text);
                string marcaTv = txtMarcaTvBox.Text;
                DateTime fechaCreacionTv = txtFechaCreacionTvBox.Value;

                // =============================
                // 📺 Televisor asociado (opcional)
                // =============================
                int? televisorAsociado = null;

                if (!string.IsNullOrWhiteSpace(txtTvAsociado.Text))
                {
                    if (int.TryParse(txtTvAsociado.Text, out int tvNum))
                    {
                        televisorAsociado = tvNum;
                    }
                    else
                    {
                        MessageBox.Show("El número del televisor asociado no es válido.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // =============================
                // 🔗 CLIENTE REST
                // =============================
                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);

                // =============================
                // 🧱 CREAR TVBOX
                // =============================
                var request = new RestRequest("/televisor/tvbox", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var body = new
                {
                    numeroReferencia = codigoTv,
                    nombre = nombreTv,
                    precio = precioTv,
                    marca = marcaTv,
                    fechaCreacion = fechaCreacionTv.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    televisorNumeroReferencia = televisorAsociado
                };

                request.AddJsonBody(body);

                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Error al crear el TV Box: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("TV Box creado correctamente.",
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
