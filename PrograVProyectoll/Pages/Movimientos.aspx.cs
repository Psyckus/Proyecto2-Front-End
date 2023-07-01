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
    public partial class Movimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string cuenta = Request.QueryString["numcuenta"];
                    string identificacion = Session["identificacion"] as string;
                    List<CN_ListaCuentas> ListaCuentasActivas = ObtenerCuentasActivas(identificacion);


                    ddl_cuentasActivas.DataSource = ListaCuentasActivas;
                    ddl_cuentasActivas.DataTextField = "NumeroCuenta";
                    ddl_cuentasActivas.DataValueField = "TipoCuenta";
                    ddl_cuentasActivas.DataBind();
                    ddl_cuentasActivas.Items.Insert(0, new ListItem("Seleccione una cuenta", "0"));

                    ListItem itemSeleccionado = ddl_cuentasActivas.Items.FindByText(cuenta);

                    if (itemSeleccionado != null)
                    {
                        itemSeleccionado.Selected = true;
                        ddl_cuentasActivas_SelectedIndexChanged(ddl_cuentasActivas, EventArgs.Empty);

                    }


                }
            }
            catch (Exception ex)
            {

            }
        }
        public static List<CN_ListaCuentas> ObtenerCuentasActivas(string identificacion)
        {


            string url = $"https://localhost:44305/api/clientes/ObtenerCuentasActivas?N_Identificacion={identificacion}";

            using (var cuentasActivas = new HttpClient())
            {


                var task = Task.Run(
                    async () =>
                    {

                        return await cuentasActivas.GetAsync(url);
                    });
                HttpResponseMessage message = task.Result;

                List<CN_ListaCuentas> listaCuentasActivas = new List<CN_ListaCuentas>();

                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(
                        async () =>
                        {
                            return await message.Content.ReadAsStringAsync();
                        });
                    string resultStr = task2.Result;
                    listaCuentasActivas = CN_ListaCuentas.FromJson(resultStr);
                }
                return listaCuentasActivas;
            }
        }

        public static List<CN_MovimientosCorrientes> ObtenerMovimientosCorrientes(string num_cuenta)
        {
            string url = $"https://localhost:44305/api/movimientos/obtenercorrientes/{num_cuenta}";

            using (var movimientosCorrientes = new HttpClient())
            {
                var task = Task.Run(
                    async () =>
                    {

                        return await movimientosCorrientes.GetAsync(url);
                    });
                HttpResponseMessage message = task.Result;

                List<CN_MovimientosCorrientes> listamovimientosCorrientes = new List<CN_MovimientosCorrientes>();

                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(
                        async () =>
                        {
                            return await message.Content.ReadAsStringAsync();
                        });
                    string resultStr = task2.Result;
                    listamovimientosCorrientes = CN_MovimientosCorrientes.FromJson(resultStr);
                }
                return listamovimientosCorrientes;
            }
        }


        public static List<CN_MovimientosAhorro> ObtenerMovimientosAhorro(string num_cuenta)
        {
            string url = $"https://localhost:44305/api/movimientos/obtener/{num_cuenta}";



            using (var movimientosAhorro = new HttpClient())
            {
                var task = Task.Run(
                    async () =>
                    {

                        return await movimientosAhorro.GetAsync(url);
                    });
                HttpResponseMessage message = task.Result;

                List<CN_MovimientosAhorro> listamovimientosAhorro = new List<CN_MovimientosAhorro>();

                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(
                        async () =>
                        {
                            return await message.Content.ReadAsStringAsync();
                        });
                    string resultStr = task2.Result;
                    listamovimientosAhorro = CN_MovimientosAhorro.FromJson(resultStr);
                }
                return listamovimientosAhorro;
            }
        }




        protected void ddl_cuentasActivas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cuenta = ddl_cuentasActivas.SelectedItem.Text;
            string value = ddl_cuentasActivas.SelectedValue.ToString();

            if (value == "Cuenta Corriente")
            {

                List<CN_MovimientosCorrientes> ListaMovimientosCorrientes = ObtenerMovimientosCorrientes(cuenta);
                System.Text.StringBuilder strListaProductos = new System.Text.StringBuilder();
                string strFechaMovimiento = null;
                ListaMovimientosCorrientes.ForEach(Lst =>
                {
                    strListaProductos.Append("<tr>");
                    strFechaMovimiento = Lst.Fecha.ToString();
                    strListaProductos.Append("<th scope=\"row\">")
                    .Append(strFechaMovimiento)
                    .Append("</th>")
                    .Append("<td>")
                    .Append(Lst.TipoMovimiento)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.Monto.ToString())
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.TipoTransaccion)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.Descripcion)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.Identificador)
                    .Append("</td>")
                    .Append("</tr>");
                });
                // Agrega el código HTML a la página web para mostrarlo en pantalla
                this.lstfrmMantenimiento.InnerHtml = strListaProductos.ToString();
            }
            if (value == "Cuenta Ahorro")
            {
                List<CN_MovimientosAhorro> ListaMovimientosAhorro = ObtenerMovimientosAhorro(cuenta);
                System.Text.StringBuilder strListaProductos = new System.Text.StringBuilder();
                string strFechaMovimiento = null;
                ListaMovimientosAhorro.ForEach(Lst =>
                {

                    strListaProductos.Append("<tr>");
                    strFechaMovimiento = Lst.Fecha.ToString();
                    strListaProductos.Append("<th scope=\"row\">")
                    .Append(strFechaMovimiento)
                    .Append("</th>")
                    .Append("<td>")
                    .Append(Lst.TipoMovimiento)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.Monto.ToString())
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.TipoTransaccion)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.Descripcion)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(Lst.Identificador)
                    .Append("</td>")
                    .Append("</tr>");
                });
                // Agrega el código HTML a la página web para mostrarlo en pantalla
                this.lstfrmMantenimiento.InnerHtml = strListaProductos.ToString();
            }
        }
    }
}