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
    public partial class Mis_Cuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<CN_ListaCuentas> ListaCuentas = ConsultarCuentasXCliente("117010442"); //Se debe de cambiar el parametro por el variabel session
            System.Text.StringBuilder strListaProductos = new System.Text.StringBuilder();
            string strNumeroCuenta = null;
            ListaCuentas.ForEach(Lst =>
            {

                strListaProductos.Append("<tr>");
                strNumeroCuenta = Lst.NumeroCuenta;
                strListaProductos.Append("<th scope=\"row\">")
                .Append(strNumeroCuenta)
                .Append("</th>")
                .Append("<td>")
                .Append("<a href=\"/pages/Movimientos.aspx?tipocuenta=")
                .Append(Lst.TipoCuenta)
                .Append("&numcuenta=")
                .Append(strNumeroCuenta)
                .Append("\">")
                .Append(Lst.Saldo)
                .Append("</a>")
                .Append("</td>")
                .Append("<td>")
                .Append(Lst.TipoCuenta)
                .Append("</td>")
                .Append("</tr>");
            });
            // Agrega el código HTML a la página web para mostrarlo en pantalla
            this.lstfrmMantenimiento.InnerHtml = strListaProductos.ToString();
        }



        public static List<CN_ListaCuentas> ConsultarCuentasXCliente(string identificacion)
        {

            string url = $"https://localhost:44305/api/clientes/ObtenerCuentasXCliente?N_Identificacion={identificacion}";

            using (var cuenta = new HttpClient())
            {


                var task = Task.Run(
                    async () =>
                    {
                        return await cuenta.GetAsync(url);
                    });

                HttpResponseMessage message = task.Result;

                List<CN_ListaCuentas> listaCuentas = new List<CN_ListaCuentas>();

                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(
                        async () =>
                        {
                            return await message.Content.ReadAsStringAsync();
                        }
                        );

                    string resultStr = task2.Result;
                    listaCuentas = CN_ListaCuentas.FromJson(resultStr);
                }
                return listaCuentas;


            }




        }



















    }
}