using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ModosDeJuego : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                if(LblInvitado.Visible == true && TxtInvitado.Visible == true)
                {
                    LblInvitado.Visible = false;
                    TxtInvitado.Visible = false;
                }
            }
        }

        protected void BtnCargarPartida_Click(object sender, EventArgs e)
        {
            Boolean Cargada = true;
            if (FileUpload1.HasFile)
            {
                string Nombre = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (extension == ".xml" || extension == ".Xml" || extension == ".XML")
                {
                    LblError.Visible = false;
                    FileUpload1.SaveAs(Server.MapPath("~/FolderXML/" + FileUpload1.FileName));
                    Session["Archivo"] = Nombre;
                    Session["Partidas"] = (int)Session["Partidas"] + 1;
                    if(RadioButtonList1.SelectedIndex == 0)
                    {
                        Session["Modo"] = true; //MODO CONTRA LA MAQUINA *TRUE*
                        Validaciones(Cargada, "CPU");
                    }
                    else
                    {
                        Session["Modo"] = false; //MODO CONTRA EL JUGADOR *FALSE*
                        if (TxtInvitado.Text == "")
                        {
                            LblError.Text = "Porfavor ingrese el nombre del invitado";
                            LblError.Visible = true;
                        }
                        else
                        {
                            Validaciones(Cargada, TxtInvitado.Text);
                        }
                    }
                    Validaciones(Cargada, TxtInvitado.Text);
                }
                else
                {
                    LblError.Text = "El archivo no es Xml";
                    LblError.Visible = true;
                }
            }
            else
            {
                LblError.Text = "No ha seleccionado ningun archivo";
                LblError.Visible = true;
            }
        }

        protected void NuevaPartida1_Click1(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0) //MODO CONTRA LA MAQUINA
            {
                Boolean Cargada = false;
                Session["Modo"] = true; //MODO CONTRA LA MAQUINA *TRUE*
                Validaciones(Cargada, "CPU");
            }
            else //MODO CONTRA EL JUGADOR
            {
                Session["Modo"] = false; //MODO CONTRA EL JUGADOR *FALSE*
                if(TxtInvitado.Text == "")
                {
                    LblError.Text = "Porfavor ingrese el nombre del invitado";
                    LblError.Visible = true;
                }
                else
                {
                    Boolean Cargada = false;
                    Validaciones(Cargada, TxtInvitado.Text);
                }
            }
        }

        public void Validaciones(Boolean Cargada, String Invitado)
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                Random aletario = new Random();
                int jugador;
                //Generando al numero aleatorio
                jugador = aletario.Next(0, 2);
                if (jugador == 0)
                {
                    Session["Blancas"] = (string)Session["Usuario"]; //BLANCAS ALEATORIA
                    Session["Negras"] = Invitado; //NEGRAS ALEATORIA
                }
                else if (jugador == 1)
                {

                    Session["Blancas"] = Invitado; //BLANCAS ALEATORIA
                    Session["Negras"] = (string)Session["Usuario"]; //NEGRAS ALEATORIA
                }
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                Session["Blancas"] = (string)Session["Usuario"]; //BLANCAS USUARIO
                Session["Negras"] = Invitado; //NEGRAS INVITADO
            }
            else if (DropDownList1.SelectedIndex == 2)
            {
                Session["Blancas"] = Invitado; //BLANCAS INVITADO
                Session["Negras"] = (string)Session["Usuario"]; //NEGRAS USUARIO
            }
            Session["Cargada"] = Cargada;
            Session["Partidas"] = (int)Session["Partidas"] + 1;
            Response.Redirect("Tablero.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
    }
}