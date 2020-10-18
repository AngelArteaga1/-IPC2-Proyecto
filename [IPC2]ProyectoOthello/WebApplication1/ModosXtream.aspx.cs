using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ModosXtream : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LblUsuario.Text = (string)Session["Usuario"] + ":";
                Session["NoColores"] = 0;
                Session["NoColoresVS"] = 0;
            }
        }

        protected void ItemChanged(object sender, EventArgs e)
        {
            if (LblInvitado.Visible == false && TxtInvitado.Visible == false)
            {
                LblInvitado.Visible = true;
                TxtInvitado.Visible = true;
            }
            else
            {
                if (LblInvitado.Visible == true && TxtInvitado.Visible == true)
                {
                    LblInvitado.Visible = false;
                    TxtInvitado.Visible = false;
                }
            }
        }

        protected void ColorChanged(object sender, EventArgs e)
        {
            string ID = ((CheckBox)sender).ID;
            CheckBox Temporal = FindControl(ID) as CheckBox;
            LabelError1.Visible = false;
            if (ID == "Rojo")
            {
                if (Rojo.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    RojoVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    RojoVS.Visible = true;
                }
            }
            else if (ID == "Amarillo")
            {
                if (Amarillo.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    AmarilloVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    AmarilloVS.Visible = true;
                }
            }
            else if (ID == "Azul")
            {
                if (Azul.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    AzulVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    AzulVS.Visible = true;
                }
            }
            else if (ID == "Anaranjado")
            {
                if (Anaranjado.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    AnaranjadoVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    AnaranjadoVS.Visible = true;
                }
            }
            else if (ID == "Verde")
            {
                if (Verde.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    VerdeVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    VerdeVS.Visible = true;
                }
            }
            else if (ID == "Violeta")
            {
                if (Violeta.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    VioletaVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    VioletaVS.Visible = true;
                }
            }
            else if (ID == "Blanco")
            {
                if (Blanco.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    BlancoVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    BlancoVS.Visible = true;
                }
            }
            else if (ID == "Negro")
            {
                if (Negro.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    NegroVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    NegroVS.Visible = true;
                }
            }
            else if (ID == "Celeste")
            {
                if (Celeste.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    CelesteVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    CelesteVS.Visible = true;
                }
            }
            else if (ID == "Gris")
            {
                if (Gris.Checked)
                {
                    Session["NoColores"] = (int)Session["NoColores"] + 1;
                    GrisVS.Visible = false;
                }
                else
                {
                    Session["NoColores"] = (int)Session["NoColores"] - 1;
                    GrisVS.Visible = true;
                }
            }

            if ((int)Session["NoColores"] > 5)
            {
                Session["NoColores"] = (int)Session["NoColores"] - 1;
                CheckBox TemporalVS = FindControl(ID+"VS") as CheckBox;
                TemporalVS.Visible = true;
                LabelError1.Visible = true;
                Temporal.Checked = false;
            }
        }

        protected void ColorChangedVS(object sender, EventArgs e)
        {
            string ID = ((CheckBox)sender).ID;
            string ID1 = "";
            CheckBox Temporal = FindControl(ID) as CheckBox;
            LabelError2.Visible = false;
            if (ID == "RojoVS")
            {
                ID1 = "Rojo";
                if (RojoVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Rojo.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Rojo.Visible = true;
                }
            }
            else if (ID == "AmarilloVS")
            {
                ID1 = "Amarillo";
                if (AmarilloVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Amarillo.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Amarillo.Visible = true;
                }
            }
            else if (ID == "AzulVS")
            {
                ID1 = "Azul";
                if (AzulVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Azul.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Azul.Visible = true;
                }
            }
            else if (ID == "AnaranjadoVS")
            {
                ID1 = "Anaranjado";
                if (AnaranjadoVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Anaranjado.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Anaranjado.Visible = true;
                }
            }
            else if (ID == "VerdeVS")
            {
                ID1 = "Verde";
                if (VerdeVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Verde.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Verde.Visible = true;
                }
            }
            else if (ID == "VioletaVS")
            {
                ID1 = "Violeta";
                if (VioletaVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Violeta.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Violeta.Visible = true;
                }
            }
            else if (ID == "BlancoVS")
            {
                ID1 = "Blanco";
                if (BlancoVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Blanco.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Blanco.Visible = true;
                }
            }
            else if (ID == "NegroVS")
            {
                ID1 = "Negro";
                if (NegroVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Negro.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Negro.Visible = true;
                }
            }
            else if (ID == "CelesteVS")
            {
                ID1 = "Celeste";
                if (CelesteVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Celeste.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Celeste.Visible = true;
                }
            }
            else if (ID == "GrisVS")
            {
                ID1 = "Gris";
                if (GrisVS.Checked)
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] + 1;
                    Gris.Visible = false;
                }
                else
                {
                    Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                    Gris.Visible = true;
                }
            }
            if ((int)Session["NoColoresVS"] > 5)
            {
                Session["NoColoresVS"] = (int)Session["NoColoresVS"] - 1;
                CheckBox TemporalVS = FindControl(ID1) as CheckBox;
                TemporalVS.Visible = true;
                LabelError2.Visible = true;
                Temporal.Checked = false;
            }
        }
            protected void NuevaPartida1_Click1(object sender, EventArgs e)
        {

        }

        protected void BtnCargarPartida_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
    }
}