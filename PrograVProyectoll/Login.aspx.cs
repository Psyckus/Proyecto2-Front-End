using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrograVProyectoll
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
        }

        protected void bntIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string Password = input_Pass.Text;
                string Username = input_User.Text;

                Negocios.CN_Usuarios iUsuario = new Negocios.CN_Usuarios
                {
                    Password = Password,
                    Username = Username

                };

                string json = iUsuario.ToJsonString();

                using (var usuario = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await usuario.PostAsync("https://localhost:44305/usuarios/login",
                            new StringContent(json, Encoding.UTF8, "application/json"));
                    });

                    HttpResponseMessage message = task.Result;
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });

                    string resultado = task2.Result;
                    if (message.StatusCode == System.Net.HttpStatusCode.OK)
                    {


                        // Autenticación exitosa
                        //string token = JsonConvert.DeserializeObject<string>(resultado);
                        Session["identificacion"] = Username;
                        Session["nombreCliente"] = resultado;

                        Response.Redirect("~/Pages/Movimientos.aspx", false);
                    }
                    else if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Credenciales incorrectas
                        lblMensaje.Visible = true;
                        lblMensaje.Attributes["class"] = "alert alert-danger alert-dismissible fade show";
                        lblMensaje.InnerHtml = "Usuarios y/o contraseña incorrectos. <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>";
                    }
                    else if (message.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        lblMensaje.Visible = true;
                        lblMensaje.Attributes["class"] = "alert alert-warming alert-dismissible fade show";
                        lblMensaje.InnerHtml = "Error! Intentarlo más tarde. <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>";
                    }
                }
            }

            catch (Exception ex)
            {
                ArrayList errores = new ArrayList();
                while (ex != null)
                {
                    errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                Session["Error"] = errores;
                Response.Redirect("~/Paginas/PaginaError", false);
            }
        }
    }
}