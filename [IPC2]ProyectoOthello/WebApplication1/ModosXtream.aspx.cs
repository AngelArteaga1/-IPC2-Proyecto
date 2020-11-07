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

        protected void NuevaPartida1_Click2(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0) //MODO CONTRA LA MAQUINA
            {
                Boolean Cargada = false;
                Session["Modo"] = true; //MODO CONTRA LA MAQUINA *TRUE*
                if (RadioButtonList2.SelectedIndex == 0)
                {
                    Session["Reto"] = "Normal";
                }
                else
                {
                    Session["Reto"] = "Inverso";
                }
                if(RadioButtonList3.SelectedIndex == 0)
                {
                    Session["Apertura"] = "personalizada";
                }
                else
                {
                    Session["Apertura"] = "normal";
                }
                Validaciones(Cargada, "CPU");
            }
            else //MODO CONTRA EL JUGADOR
            {
                Session["Modo"] = false; //MODO CONTRA EL JUGADOR *FALSE*
                if (TxtInvitado.Text == "")
                {
                    LblError.Text = "Porfavor ingrese todos los datos";
                    LblError.Visible = true;
                }
                else
                {
                    //Validar el Reto
                    Boolean Cargada = false;
                    if (RadioButtonList2.SelectedIndex == 0)
                    {
                        Session["Reto"] = "Normal";
                    }
                    else
                    {
                        Session["Reto"] = "Inverso";
                    }
                    if (RadioButtonList3.SelectedIndex == 0)
                    {
                        Session["Apertura"] = "personalizada";
                    }
                    else
                    {
                        Session["Apertura"] = "normal";
                    }
                    Validaciones(Cargada, TxtInvitado.Text);
                }
            }
        }

        protected void BtnCargarPartida_Click(object sender, EventArgs e)
        {
            Boolean Cargada = true;
            if (FileUpload1.HasFile)
            {
                //NOMBRE DEL ARCHIVO
                string Nombre = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (extension == ".xml" || extension == ".Xml" || extension == ".XML")
                {
                    LblError.Visible = false;
                    FileUpload1.SaveAs(Server.MapPath("~/FolderXML/" + FileUpload1.FileName));
                    Session["Archivo"] = Nombre;
                    if (RadioButtonList1.SelectedIndex == 0)
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
                    if (RadioButtonList2.SelectedIndex == 0)
                    {
                        Session["Reto"] = "Normal";
                    }
                    else
                    {
                        Session["Reto"] = "Inverso";
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
                    Session["Primero"] = (string)Session["Usuario"]; //PRIMERO ALEATORIA
                    Session["Segundo"] = Invitado; //SEGUNDO ALEATORIA
                    Application["Turno"] = true;
                }
                else if (jugador == 1)
                {
                    Session["Segundo"] = Invitado; //PRIMERO ALEATORIA
                    Session["Primero"] = (string)Session["Usuario"]; //NEGRAS ALEATORIA
                    Application["Turno"] = false;
                }
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                Session["Primero"] = (string)Session["Usuario"]; //PRIMERO USUARIO
                Session["Segundo"] = Invitado; //SEGUNDO INVITADO
                Application["Turno"] = true;
            }
            else if (DropDownList1.SelectedIndex == 2)
            {
                Session["Segundo"] = Invitado; //PRIMERO INVITADO
                Session["Primero"] = (string)Session["Usuario"]; //SEGUNDO USUARIO
                Application["Turno"] = false;
            }
            Session["Cargada"] = Cargada;
            Boolean valido = IngresarColoresUsuario();
            Boolean valido2 = IngresarColoresRival();
            if(valido == true && valido2 == true)
            {
                //Obtener columnas y filas
                int columnas = Int32.Parse(DpColumnas.SelectedValue);
                Session["Columnas"] = columnas;
                int filas = Int32.Parse(DpFilas.SelectedValue);
                Session["Filas"] = filas;
                //Redirect
                Response.Redirect("Xtream.aspx");
            }
        }

        public Boolean IngresarColoresUsuario()
        {
            Boolean resultado;
            Lista Usuario = new Lista();
            if (Rojo.Checked)
            {
                Usuario.Add("img/Rojo.png");
            }
            if (Amarillo.Checked)
            {
                Usuario.Add("img/Amarillo.png");
            }
            if (Azul.Checked)
            {
                Usuario.Add("img/Azul.png");
            }
            if (Anaranjado.Checked)
            {
                Usuario.Add("img/Anaranjado.png");
            }
            if (Verde.Checked)
            {
                Usuario.Add("img/Verde.png");
            }
            if (Violeta.Checked)
            {
                Usuario.Add("img/Violeta.png");
            }
            if (Blanco.Checked)
            {
                Usuario.Add("img/Blanco.png");
            }
            if (Negro.Checked)
            {
                Usuario.Add("img/Negro.png");
            }
            if (Celeste.Checked)
            {
                Usuario.Add("img/Celeste.png");
            }
            if (Gris.Checked)
            {
                Usuario.Add("img/Gris.png");
            }
            if ((int)Session["NoColores"] == 0)
            {
                resultado = false;
                LblError.Text = "Porfavor ingrese mínimo un color para el usuario";
                LblError.Visible = true;
            }
            else
            {
                Session["ColoresUsuario"] = Usuario;
                resultado = true;
            }
            return resultado;
        }

        public Boolean IngresarColoresRival()
        {
            Boolean resultado;
            Lista Rival = new Lista();
            if (RojoVS.Checked)
            {
                Rival.Add("img/Rojo.png");
            }
            if (AmarilloVS.Checked)
            {
                Rival.Add("img/Amarillo.png");
            }
            if (AzulVS.Checked)
            {
                Rival.Add("img/Azul.png");
            }
            if (AnaranjadoVS.Checked)
            {
                Rival.Add("img/Anaranjado.png");
            }
            if (VerdeVS.Checked)
            {
                Rival.Add("img/Verde.png");
            }
            if (VioletaVS.Checked)
            {
                Rival.Add("img/Violeta.png");
            }
            if (BlancoVS.Checked)
            {
                Rival.Add("img/Blanco.png");
            }
            if (NegroVS.Checked)
            {
                Rival.Add("img/Negro.png");
            }
            if (CelesteVS.Checked)
            {
                Rival.Add("img/Celeste.png");
            }
            if (GrisVS.Checked)
            {
                Rival.Add("img/Gris.png");
            }
            if ((int)Session["NoColoresVS"] == 0)
            {
                resultado = false;
                LblError.Text = "Porfavor ingrese mínimo un color para el rival";
                LblError.Visible = true;
            }
            else
            {
                Session["ColoresRival"] = Rival;
                resultado = true;
            }
            return resultado;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

    }
}