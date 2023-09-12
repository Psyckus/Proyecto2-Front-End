using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrograVProyectoll
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["identificacion"] != null)
            {
                string username = Session["identificacion"].ToString();
                string nombreCliente = Session["nombreCliente"].ToString();

                nombreCliente = nombreCliente.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("nombre:", "").Replace("string", "").Trim();

                lblWelcomeMessage.Text = "Bienvenido: " + nombreCliente;

            }
        }
    }
}