using Negocios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrograVProyectoll.Pages
{
    public partial class Transferencias_Internas : System.Web.UI.Page
    {
        Cn_Recursos cuentaManager = new Cn_Recursos();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Establecer el valor predeterminado del label
            lblFechaHora.Text = DateTime.Now.ToString();
            lblCanal.Text = "web";
            ClientScript.RegisterForEventValidation(btnTransferir.UniqueID);
        }

        protected  async void btnTransferir_Click(object sender, EventArgs e)
        {
            string numeroCuenta = Request.Form[ddlCuentaOrigen.UniqueID];
            string correoNotificacion = txtCorreoNotificacion.Value;
            int monto = int.Parse(txtMonto.Value);
            string numeroCuentaDestino = txtCuentaDestino.Value;
            string concepto = txtConcepto.Value;
            string canal = lblCanal.Text; // Obtener el valor del control "lblCanal"
            string tipoTransaccion = ddlTipoTransaccion.SelectedValue; // Obtener el valor seleccionado del control "ddlTipoTransaccion"
            string responseMessage = "";
            string responseError = "";
            string ApimovimientoAhorros = "https://localhost:44305/api/moviregistrarMovimientoCuentaAhorro";
            string ApimovimientoCorriente = "https://localhost:44305/api/movimientos/registrarMovimientoCuentaCorriente";
            // Realizar la transferencia y obtener los detalles (número de cuenta, monto, etc.)


            string mensaje = $"Ha recibido una transferencia en su cuenta #{numeroCuentaDestino} por un monto de {monto}.";


            string apicorreo = "https://localhost:44305/api/correo/enviar";


  
            var correo = new
            {
                Correo = correoNotificacion,
                Asunto = "Notificación de Transferencia",
                Mensaje = mensaje
            };

            string jsonMensaje = JsonConvert.SerializeObject(correo);
            var contentCorreo = new StringContent(jsonMensaje, Encoding.UTF8, "application/json");
            string tipoCuentaOrigen = cuentaManager.ObtenerTipoCuenta(numeroCuenta);
            string tipoCuentaDestino = cuentaManager.ObtenerTipoCuenta(numeroCuentaDestino);
            if (tipoCuentaOrigen == "Cuenta Corriente")
            {

                // Crear el objeto de movimiento para cuenta corriente
                var movimientoData = new
                {
                    CuentaCorriente = numeroCuenta,
                    cuentaDestino = numeroCuentaDestino,
                    Fecha = DateTime.Now,
                    TipoMovimiento = "Debito",
                    Monto = monto,
                    TipoTransaccion = "Interna",
                    Descripcion = concepto,
                    Canal = canal
                };

                string jsonData = JsonConvert.SerializeObject(movimientoData);
                // Crear el contenido de la solicitud HTTP
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    // Enviar la solicitud POST a la API
                    HttpResponseMessage response = await httpClient.PostAsync(ApimovimientoCorriente, content);
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
                    if (tipoCuentaDestino == "Cuenta Corriente")
                    {
                        var movimiento = new
                        {
                            CuentaCorriente = numeroCuentaDestino,
                            cuentaDestino = numeroCuentaDestino,
                            Fecha = DateTime.Now,
                            TipoMovimiento = "Credito",
                            Monto = monto,
                            TipoTransaccion = "Interna",
                            Descripcion = concepto,
                            Canal = canal
                        };

                        string jsonDataDestino = JsonConvert.SerializeObject(movimiento);
                        // Crear el contenido de la solicitud HTTP
                        var contentDestino = new StringContent(jsonDataDestino, Encoding.UTF8, "application/json");
                        using (var httpClient2 = new HttpClient())
                        {
                            HttpResponseMessage responseDestino = await httpClient.PostAsync(ApimovimientoCorriente, contentDestino);
                        }
                    }
                    else
                    {
                        var movimiento = new
                        {
                            Cuenta = numeroCuentaDestino,
                            cuentaDestino = numeroCuentaDestino,
                            Fecha = DateTime.Now,
                            TipoMovimiento = "Credito",
                            Monto = monto,
                            TipoTransaccion = "Interna",
                            Descripcion = concepto,
                            Canal = canal
                        };

                        string jsonDataDestino = JsonConvert.SerializeObject(movimiento);
                        // Crear el contenido de la solicitud HTTP
                        var contentDestino = new StringContent(jsonDataDestino, Encoding.UTF8, "application/json");
                        using (var httpClient2 = new HttpClient())
                        {
                            HttpResponseMessage responseDestino = await httpClient.PostAsync(ApimovimientoAhorros, contentDestino);
                        }
                    }

                }
            }
            else
            {


                var movimientoData = new
                {
                    Cuenta = numeroCuenta,
                    cuentaDestino = numeroCuentaDestino,
                    Fecha = DateTime.Now,
                    TipoMovimiento = "Debito",
                    Monto = monto,
                    TipoTransaccion = "Interna",
                    Descripcion = concepto,
                    Canal = canal
                };

                string jsonData = JsonConvert.SerializeObject(movimientoData);
                // Crear el contenido de la solicitud HTTP
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    // Enviar la solicitud POST a la API
                    HttpResponseMessage response = await httpClient.PostAsync(ApimovimientoAhorros, content);
                    HttpResponseMessage responseCorreo = await httpClient.PostAsync(apicorreo, contentCorreo);

                    // Verificar si la respuesta es exitosa
                    if (response.IsSuccessStatusCode)
                    {

                        // Obtener el contenido de la respuesta
                        string successMessage = await response.Content.ReadAsStringAsync();
                        // Deserializar el JSON y obtener el valor del mensaje
                        var jsonDocument = JsonDocument.Parse(successMessage);
                        responseMessage = jsonDocument.RootElement.GetProperty("mensaje").GetString();
                        correoNotificacion = "";
                        monto = 0;
                        concepto = "";
                        correoNotificacion = "";
                        numeroCuentaDestino = "";


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
}