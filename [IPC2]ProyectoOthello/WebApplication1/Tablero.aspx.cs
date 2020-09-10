using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Tablero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            D4.ImageUrl = "img/FichaB.png";
            D4.Enabled = false;
            E4.ImageUrl = "img/FichaN.png";
            E4.Enabled = false;
            D5.ImageUrl = "img/FichaN.png";
            D5.Enabled = false;
            E5.ImageUrl = "img/FichaB.png";
            E5.Enabled = false;
        }
    }
}