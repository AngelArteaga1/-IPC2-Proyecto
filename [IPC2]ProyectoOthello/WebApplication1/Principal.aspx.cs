using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = (String)Session["Usuario"];
        }

        protected void BtnJugar_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ModoDeJuego.aspx");
        }

        protected void BtnEstadisticas_Click(object sender, EventArgs e)
        {

        }
    }
}