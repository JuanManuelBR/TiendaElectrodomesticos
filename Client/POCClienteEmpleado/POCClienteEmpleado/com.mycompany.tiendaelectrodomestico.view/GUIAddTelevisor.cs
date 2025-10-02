using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIAddTelevisor : Form
    {
        public GUIAddTelevisor()
        {
            InitializeComponent();
        }
       

        //BntGuardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoTv = int.Parse(txtCodigoTelevisor.Text);
                string nombreTv = txtNombreTelevisor.Text;
                double precioTv = double.Parse(txtPrecioTelevisor.Text);
                string marcaTv = txtMarcaTelevisor.Text;
                DateTime fechaCreacionTv = txtFechaCreacionTelevisor.Value;

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);
                var request = new RestRequest("/electrodomestico/add", Method.Post);


                request.AddHeader("Content-Type", "application/json");


                var body = new
                {
                    tipo = "televisor",
                    codigo = codigoTv,
                    nombre = nombreTv,
                    marca = marcaTv,
                    fechaCreacion = fechaCreacionTv.ToString("yyyy-MM-ddTHH:mm:ss"),
                    precio = precioTv
                };

                request.AddJsonBody(body);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Electrodoméstico guardado con éxito",
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
        
        //BntGuardar
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
