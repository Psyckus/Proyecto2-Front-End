using Negocios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrograVProyectoll.Pages
{
    public partial class Transferencias_Interbancarias : System.Web.UI.Page
    {
        string responseMessage = "";
        string responseError = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblFechaHora.Text = DateTime.Now.ToString();
            lblCanal.Text = "web";
        }

        protected async void btnTransferir_Click(object sender, EventArgs e)
        {

            string numeroCuenta = Request.Form[ddlCuentaOrigen.UniqueID];
            string numeroCuentaDestino = txtCuentaDestino.Value;
            string TipoTransaccion = Request.Form[ddlTipoTranferencia.UniqueID];
            string cedulaDestino = txtCedulaDestino.Value;
            int monto = int.Parse(txtMonto.Value);
            string correoNotificacion = txtCorreoNotificacion.Value;
            string concepto = txtConcepto.Value;
            string cedulaOrigen = txtCedulaOrigen.Value;
            string moneda = Request.Form[ddlTipoMoneda.UniqueID];
            string canal = lblCanal.Text; // Obtener el valor del control "lblCanal"
            string digitos = Regex.Replace(numeroCuentaDestino, "[^0-9]", "");
            string primeroDigitos = digitos.Substring(0, 3);

            string apiTranferencias = "https://localhost:44305/api/Transferencias";
            string apicorreo = "https://localhost:44305/api/correo/enviar";
         

            string mensaje = $"Ha recibido una transferencia en su cuenta #{numeroCuentaDestino} por un monto de {monto}.";
            var correo = new
            {
                Correo = correoNotificacion ,
                Asunto= "Notificación de Transferencia",
                Mensaje= mensaje
            };
            string jsonMensaje = JsonConvert.SerializeObject(correo);
            var contentCorreo = new StringContent(jsonMensaje, Encoding.UTF8, "application/json");

            // Crear el objeto de movimiento para cuenta corriente
            var Transferencia = new
            {
                BancoOrigen = "001",
                BancoDestino = primeroDigitos,
                CuentaOrigen = numeroCuenta,
                CuentaDestino = numeroCuentaDestino,
                Monto = monto,
                CedulaOrigen = cedulaOrigen,
                CedulaDestino = cedulaDestino,
                TipoCedulaOrigen = "Nacional",
                TipoCedulaDestino = "Nacional",
                Moneda = moneda,
                Descripcion = concepto,

                Canal = canal,
                Tipo_Transaccion_ID = TipoTransaccion,

            };
            string jsonData = JsonConvert.SerializeObject(Transferencia);
            // Crear el contenido de la solicitud HTTP
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                // Enviar la solicitud POST a la API
                HttpResponseMessage response = await httpClient.PostAsync(apiTranferencias, content);
                HttpResponseMessage responseCorreo = await httpClient.PostAsync(apicorreo, contentCorreo);

                // Verificar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {

                    // Obtener el contenido de la respuesta
                    string successMessage = await response.Content.ReadAsStringAsync();
                    // Deserializar el JSON y obtener el valor del mensaje
                    var jsonDocument = JsonDocument.Parse(successMessage);
                    responseMessage = jsonDocument.RootElement.GetProperty("mensaje").GetString();



                   

                }
                else
                {

                    // Manejar el error en caso de que la solicitud falle
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    // Deserializar el JSON y obtener el valor del mensaje
                    var jsonDocument = JsonDocument.Parse(errorMessage);
                    responseError = jsonDocument.RootElement.GetProperty("mensaje").GetString();

                }
                string script = "<script>var responseMessage = '" + responseMessage + "'; var responseError = '" + responseError + "';</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "ResponseScript", script);
            }





        }
    }
}