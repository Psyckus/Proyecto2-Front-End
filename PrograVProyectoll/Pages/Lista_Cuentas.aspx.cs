using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;

namespace PrograVProyectoll.Pages
{
    public partial class Lista_Cuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<CN_ListaCuentas> CuentasActivas = ConsultarListaCuentasActivas("117010442");
            System.Text.StringBuilder strListaCuentasActivas = new System.Text.StringBuilder();
            string strCodigoListaCuentasActivas = null;
            CuentasActivas.ForEach(p =>
            {
                strListaCuentasActivas.Append("<tr>");
                strCodigoListaCuentasActivas = p.NumeroCuenta.ToString();
                strListaCuentasActivas.Append("<th scope=\"row\">")
                    .Append(strCodigoListaCuentasActivas)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(p.Saldo)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(p.TipoCuenta)
                    .Append("</td>")
                    .Append("</tr>");
            });
            // Agrega el código HTML a la página web para mostrarlo en pantalla
            this.lstfrmMantenimiento.InnerHtml = strListaCuentasActivas.ToString();
        }


        public static List<CN_ListaCuentas>ConsultarListaCuentasActivas(string identicacion)
        {

            string url = $"https://localhost:44305/api/clientes/ObtenerCuentasActivas?N_Identificacion={identicacion}";


            using (var client = new HttpClient())
            {

                var task = Task.Run(
                    async () =>
                    {
                        return await client.GetAsync(url);
                    }
                    );

                HttpResponseMessage message = task.Result;
                List<CN_ListaCuentas> lista = new List<CN_ListaCuentas>();

                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(
                        async () =>
                        {
                            return await message.Content.ReadAsStringAsync();
                        }
                        );

                    string resultStr = task2.Result;
                    lista = CN_ListaCuentas.FromJson(resultStr);
                }
                return lista;
            }
        }

    }
}