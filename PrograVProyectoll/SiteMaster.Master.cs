using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrograVProyectoll
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombreCliente"] != null)
            {


                string nombreCliente = Session["nombreCliente"].ToString();

                nombreCliente = nombreCliente.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("nombre:", "").Replace("string", "").Trim();


                nombreUsuario.InnerText = nombreCliente;
            }
        }
    }
}