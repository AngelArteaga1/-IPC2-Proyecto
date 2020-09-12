using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebApplication1
{
    public partial class Tablero : System.Web.UI.Page
    {

        public void ColocarFicha(ImageButton Casilla)
        {
            Boolean turno = (Boolean)Application["Turno"];
            if (turno == true)
            {
                Casilla.ImageUrl = "img/FichaB.png";
                Casilla.Enabled = false;
                Application["Turno"] = false;
            }
            if(turno == false)
            {
                Casilla.ImageUrl = "img/FichaN.png";
                Casilla.Enabled = false;
                Application["Turno"] = true;
            }
        }
        public void ColocarPartida(String color, String columna, String fila)
        {
            //Boolean[,] Tablero = new Boolean[8, 8];
            String position = columna + fila;
            ImageButton Casilla = FindControl(position) as ImageButton;
            if(Casilla.Enabled == true)
            {
                if (color == "blanco")
                {
                    Casilla.ImageUrl = "img/FichaB.png";
                    Casilla.Enabled = false;
                }
                if (color == "negro")
                {
                    Casilla.ImageUrl = "img/FichaN.png";
                    Casilla.Enabled = false;
                }
            }
            //Tablero[FILA, COLUMNA] = COLOR;
        }

        public void GuardarPartida()
        {
            Boolean turno = (Boolean)Application["Turno"];
            string columna = "";
            string fila = "";
            string color = "";
            int n = (int)Session["Partidas"];
            string Ruta = @"C:\Users\admin\Documents\GitHub\-IPC2-Proyecto\[IPC2]ProyectoOthello\WebApplication1\PartidasGuardadas\Partida" + n.ToString() + ".xml";
            XmlWriterSettings Confi = new XmlWriterSettings();
            Confi.Indent = true;
            XmlWriter writer = XmlWriter.Create(Ruta, Confi);
            writer.WriteStartDocument();
            writer.WriteStartElement("tablero");
            for (int i = 1; i<= 8; i++)
            {
                fila = i.ToString();
                for(int j = 1; j<=8; j++)
                {
                    if (j == 1)
                        columna = "A";
                    if (j == 2)
                        columna = "B";
                    if (j == 3)
                        columna = "C";
                    if (j == 4)
                        columna = "D";
                    if (j == 5)
                        columna = "E";
                    if (j == 6)
                        columna = "F";
                    if (j == 7)
                        columna = "G";
                    if (j == 8)
                        columna = "H";
                    String position = columna + fila;
                    ImageButton Casilla = FindControl(position) as ImageButton;
                    if (Casilla.Enabled == false)
                    {
                        if (Casilla.ImageUrl == "img/FichaB.png")
                        {
                            color = "blanco";
                        }
                        else
                        {
                            color = "negro";
                        }
                        writer.WriteStartElement("ficha");
                        writer.WriteStartElement("color");
                        writer.WriteString(color);
                        writer.WriteEndElement();
                        writer.WriteStartElement("columna");
                        writer.WriteString(columna);
                        writer.WriteEndElement();
                        writer.WriteStartElement("fila");
                        writer.WriteString(fila);
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                }
            }
            writer.WriteStartElement("siguienteTiro");
            writer.WriteStartElement("color");
            if (turno == true)
                writer.WriteString("blanco");
            if (turno == false)
                writer.WriteString("negro");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LblSucc.Visible = false;
            if (!IsPostBack)
            {
                Boolean[,] Tablero = new Boolean[8,8];
                Boolean Cargada = (Boolean)Session["Cargada"];
                if (Cargada == false)
                {
                    Boolean turno = true;
                    Application["Turno"] = turno;
                    D4.ImageUrl = "img/FichaB.png";
                    D4.Enabled = false;
                    E4.ImageUrl = "img/FichaN.png";
                    E4.Enabled = false;
                    D5.ImageUrl = "img/FichaN.png";
                    D5.Enabled = false;
                    E5.ImageUrl = "img/FichaB.png";
                    E5.Enabled = false;
                }
                if (Cargada == true)
                {
                    int nFicha = 0;
                    String color ="";
                    String columna = "";
                    String fila = "";
                    string ruta = @"C:\Users\admin\Documents\GitHub\-IPC2-Proyecto\[IPC2]ProyectoOthello\WebApplication1\FolderXML\" + (String)Session["Archivo"];
                    XmlReader reader = XmlReader.Create(ruta);
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "ficha":
                                    if(nFicha != 0)
                                    {
                                        ColocarPartida(color, columna, fila);
                                    }
                                    nFicha++;
                                    break;
                                case "color":
                                    color = reader.ReadString();
                                    break;
                                case "columna":
                                    columna = reader.ReadString();
                                    break;
                                case "fila":
                                    fila = reader.ReadString();
                                    break;
                                case "siguienteTiro":
                                    if (reader.ReadString() == "blanco")
                                    {
                                        Application["Turno"] = true;
                                    }
                                    if (reader.ReadString() == "negro")
                                    {
                                        Application["Turno"] = false;
                                    }
                                    break;
                            }
                        }
                    }
                    ColocarPartida(color, columna, fila);
                    if (Application["Turno"] == null)
                    {
                        Application["Turno"] = true;
                    }
                    reader.Close();
                }
            }
        }

        protected void A1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A1);   }
        protected void B1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B1);   }
        protected void C1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C1);   }
        protected void D1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D1);   }
        protected void E1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E1);   }
        protected void F1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F1);   }
        protected void G1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G1);   }
        protected void H1_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H1);   }
        protected void A2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A2);   }
        protected void B2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B2);   }
        protected void C2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C2);   }
        protected void D2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D2);   }
        protected void E2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E2);   }
        protected void F2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F2);   }  
        protected void G2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G2);   }
        protected void H2_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H2);   }
        protected void A3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A3);   }
        protected void B3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B3);   }
        protected void C3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C3);   }
        protected void D3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D3);   }
        protected void E3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E3);   }
        protected void F3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F3);   }
        protected void G3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G3);   }
        protected void H3_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H3);   }
        protected void A4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A4);   }
        protected void B4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B4);   }
        protected void C4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C4);   }
        protected void D4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D4);   }
        protected void E4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E4);   }
        protected void F4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F4);   }
        protected void G4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G4);   }
        protected void H4_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H4);   }
        protected void A5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A5);   }
        protected void B5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B5);   }
        protected void C5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C5);   }
        protected void D5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D5);   }
        protected void E5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E5);   }
        protected void F5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F5);   }
        protected void G5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G5);   }
        protected void H5_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H5);   }
        protected void A6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A6);   }
        protected void B6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B6);   }
        protected void C6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C6);   }
        protected void D6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D6);   }
        protected void E6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E6);   }
        protected void F6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F6);   }
        protected void G6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G6);   }
        protected void H6_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H6);   }
        protected void A7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A7);   }
        protected void B7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B7);   }
        protected void C7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C7);   }
        protected void D7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D7);   }
        protected void E7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E7);   }
        protected void F7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F7);   }
        protected void G7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G7);   }
        protected void H7_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H7);   }
        protected void A8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(A8);   }
        protected void B8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(B8);   }
        protected void C8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(C8);   }
        protected void D8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(D8);   }
        protected void E8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(E8);   }
        protected void F8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(F8);   }
        protected void G8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(G8);   }
        protected void H8_Click(object sender, ImageClickEventArgs e){  ColocarFicha(H8);   }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GuardarPartida();
            Session["Partidas"] = (int)Session["Partidas"] + 1;
            LblSucc.Visible = true;
        }
    }
}