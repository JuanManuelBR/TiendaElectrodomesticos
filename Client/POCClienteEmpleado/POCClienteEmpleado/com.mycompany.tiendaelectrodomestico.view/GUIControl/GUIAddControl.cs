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
    public partial class GUIAddControl : Form
    {
        public GUIAddControl()
        {
            InitializeComponent();
        }

        //BntGuardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoControl = int.Parse(txtCodigoControl.Text);
                string nombreControl = txtNombreControl.Text;
                double precioControl = double.Parse(txtAlcanceControl.Text);
                string marcaControl = txtMarcaControl.Text;
                string alcanceControl = txtAlcanceControl.Text;
                DateTime fechaVentaControl = txtFechaCreacionControl.Value;

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest("/televisor/control", Method.Post);


                request.AddHeader("Content-Type", "application/json");


                var body = new
                {
                    numeroReferencia = codigoControl,
                    nombre = nombreControl,
                    marca = marcaControl,
                    fechaVenta = fechaVentaControl.ToString("yyyy-MM-ddTHH:mm:ss"),
                    alcance = alcanceControl

                };

                request.AddJsonBody(body);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Control guardado con éxito",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(" Error: " + response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(" Ocurrió un error inesperado: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        //BntSalir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
