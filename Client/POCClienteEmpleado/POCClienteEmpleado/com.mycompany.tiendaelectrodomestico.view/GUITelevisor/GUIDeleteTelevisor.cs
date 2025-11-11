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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace POCClienteEmpleado
{
    public partial class GUIDeleteTelevisor : Form
    {
        public GUIDeleteTelevisor()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
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
                            .GetDateTime()
                            .ToString("yyyy-MM-dd HH:mm:ss");

                        if (root.TryGetProperty("controles", out JsonElement controlesElem) && controlesElem.ValueKind == JsonValueKind.Array)
                        {
                            List<string> codigos = new List<string>();
                            foreach (var control in controlesElem.EnumerateArray())
                            {
                                if (control.TryGetProperty("numeroReferencia", out JsonElement codElem))
                                {
                                    codigos.Add(codElem.GetInt32().ToString());
                                }
                            }
                            txtCodigoControl.Text = string.Join(",", codigos);
                        }
                        else
                        {
                            txtCodigoControl.Text = "";
                        }

                        btnEliminar.Enabled = true;
                    }
                }
                else
                {
                    txtNombreTelevisor.Clear();
                    txtPrecioTelevisor.Clear();
                    txtMarcaTelevisor.Clear();
                    txtCodigoControl.Clear();
                    btnEliminar.Enabled = false;

                    MessageBox.Show(response.Content,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Ocurrió un error: " + ex.Message,
                    "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoEliminar = int.Parse(txtCodigoTelevisorBuscar.Text);

                var options = new RestClientOptions("http://localhost:8090")
                {
                    Authenticator = new HttpBasicAuthenticator("admin", "admin")
                };

                var client = new RestClient(options);

                var request = new RestRequest($"/televisor/{codigoEliminar}", Method.Delete);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Televisor eliminado correctamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtCodigoTelevisorBuscar.Clear();
                    txtNombreTelevisor.Clear();
                    txtPrecioTelevisor.Clear();
                    txtMarcaTelevisor.Clear();
                    txtFechaCreacionTelevisor.Clear();
                    txtCodigoControl.Clear();
                    btnEliminar.Enabled = false;
                }
                else
                {
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
