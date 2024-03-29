﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace WebApplication1
{
    public partial class TableroXtream : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblSucc.Visible = false;
            if (!IsPostBack)
            {
                Session["ContadorPrimero"] = 0;
                Session["ContadorSegundo"] = 0;
                Session["ContadorMovimientos"] = 0;
                //RELOJES
                LblPrimero.Text = (string)Session["Primero"] + ":";
                Session["Minuto1"] = 0;
                Session["Segundo1"] = 0;
                LblSegundo.Text = (string)Session["Segundo"] + ":";
                Session["Minuto2"] = 0;
                Session["Segundo2"] = 0;
                //RELOJES
                int[,] Tablero = new int[8, 8];
                Boolean Cargada = (Boolean)Session["Cargada"];
                if (Cargada == false)
                {
                    if ((Boolean)Application["Turno"] == true)
                    {
                        LblTurno.Text = (string)Session["Primero"];
                        LblEspera.Text = (string)Session["Segundo"];
                        LblReloj1.Text = "0:0";
                        Reloj1.Enabled = true;
                    }
                    else if ((Boolean)Application["Turno"] == false)
                    {
                        LblTurno.Text = (string)Session["Segundo"];
                        LblEspera.Text = (string)Session["Primero"];
                        LblRejoj2.Text = "0:0";
                        Reloj2.Enabled = true;
                    }
                    Session["Tablero"] = Tablero;
                    if ((Boolean)Application["Turno"] == false && (string)Session["Segundo"] == "CPU")
                    {
                        Session["ContadorPila"] = 0;
                        TiroMaquina();
                    }
                }
                if (Cargada == true)
                {
                    Session["Tablero"] = Tablero;
                    int nFicha = 0;
                    String color = "";
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
                                    if (nFicha != 0)
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
                    if ((Boolean)Application["Turno"] == true)
                    {
                        LblTurno.Text = (string)Session["Primero"];
                        LblEspera.Text = (string)Session["Segundo"];
                    }
                    else if ((Boolean)Application["Turno"] == false)
                    {
                        LblTurno.Text = (string)Session["Segundo"];
                        LblEspera.Text = (string)Session["Primero"];
                    }
                    Session["ContadorMovimientos"] = 5;
                }

                LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                //TIRO DE LA MAQUINA
                if ((Boolean)Application["Turno"] == false && (string)Session["Primero"] == "CPU")
                {
                    Session["ContadorPila"] = 0;
                    TiroMaquina();
                }
            }
        }

        public void ColocarPartida(String color, String columna, String fila)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            String position = columna + fila;
            int col = EncontrarColumna(position);
            int fil = EncontrarFila(position);
            ImageButton Casilla = null;
            try
            {
                Casilla = FindControl(position) as ImageButton;
            }
            catch { }
            if (Casilla != null)
            {
                if (Casilla.Enabled == true)
                {
                    if (color == "blanco")
                    {
                        Tablero[fil, col] = 1;
                        Casilla.ImageUrl = "img/FichaB.png";
                        Casilla.Enabled = false;
                    }
                    if (color == "negro")
                    {
                        Tablero[fil, col] = 2;
                        Casilla.ImageUrl = "img/FichaN.png";
                        Casilla.Enabled = false;
                    }
                }
            }
            Session["Tablero"] = Tablero;
        }

        public void GuardarPartida(Boolean temporal)
        {
            Boolean turno = (Boolean)Application["Turno"];
            string columna = "";
            string fila = "";
            string color = "";
            int n = (int)Session["Partidas"];
            string Ruta = "";
            if (temporal == true)
            {
                Ruta = @"C:\Users\admin\Documents\GitHub\-IPC2-Proyecto\[IPC2]ProyectoOthello\WebApplication1\PartidasGuardadas\PartidaTemporal.xml";
            }
            else
            {
                Ruta = @"C:\Users\admin\Documents\GitHub\-IPC2-Proyecto\[IPC2]ProyectoOthello\WebApplication1\PartidasGuardadas\Partida" + n.ToString() + ".xml";
            }
            XmlWriterSettings Confi = new XmlWriterSettings();
            Confi.Indent = true;
            XmlWriter writer = XmlWriter.Create(Ruta, Confi);
            writer.WriteStartDocument();
            writer.WriteStartElement("tablero");
            for (int i = 1; i <= 8; i++)
            {
                fila = i.ToString();
                for (int j = 1; j <= 8; j++)
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

        public int EncontrarColumna(String StrCasilla)
        {
            int Columna = -1;
            string columna = StrCasilla.Substring(0, 1);
            if (columna == "A")
            {
                Columna = 0;
            }
            else if (columna == "B")
            {
                Columna = 1;
            }
            else if (columna == "C")
            {
                Columna = 2;
            }
            else if (columna == "D")
            {
                Columna = 3;
            }
            else if (columna == "E")
            {
                Columna = 4;
            }
            else if (columna == "F")
            {
                Columna = 5;
            }
            else if (columna == "G")
            {
                Columna = 6;
            }
            else if (columna == "H")
            {
                Columna = 7;
            }
            return Columna;
        }

        public int EncontrarFila(String StrCasilla)
        {
            string fila = StrCasilla.Substring(1, 1);
            return int.Parse(fila) - 1;
        }

        public string ConvertirColumna(int columna)
        {
            String Columna = "";
            if (columna == 0)
            {
                Columna = "A";
            }
            if (columna == 1)
            {
                Columna = "B";
            }
            if (columna == 2)
            {
                Columna = "C";
            }
            if (columna == 3)
            {
                Columna = "D";
            }
            if (columna == 4)
            {
                Columna = "E";
            }
            if (columna == 5)
            {
                Columna = "F";
            }
            if (columna == 6)
            {
                Columna = "G";
            }
            if (columna == 7)
            {
                Columna = "H";
            }
            return Columna;
        }

        //****************************************************************************************************************************
        //************************************************VERTICAL PARA ARRIBA********************************************************
        //****************************************************************************************************************************
        public Boolean Vertical_Arriba(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 10;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            for (int i = Fila - 1; i >= 0; i--)
            {
                if (Encontrado == false && Error == false)
                {
                    if (Tablero[i, Columna] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[i, Columna] == 2 || id == 2 && Tablero[i, Columna] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(Columna);
                        string fil = (i + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = (i).ToString() + Columna.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[i, Columna] == id)
                    {
                        Encontrado = true;
                    }
                }
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //***************************************************VERTICAL PARA ABAJO******************************************************
        //****************************************************************************************************************************
        public Boolean Vertical_Abajo(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            for (int i = Fila + 1; i < Tablero.GetLength(0); i++)
            {
                if (Encontrado == false && Error == false)
                {
                    if (Tablero[i, Columna] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[i, Columna] == 2 || id == 2 && Tablero[i, Columna] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(Columna);
                        string fil = (i + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = (i).ToString() + Columna.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[i, Columna] == id)
                    {
                        Encontrado = true;
                    }
                }
            }
            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //******************************************HORIZONTAL PARA LA IZQUIERDA******************************************************
        //****************************************************************************************************************************
        public Boolean Horizontal_Izquierda(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            for (int i = Columna - 1; i >= 0; i--)
            {
                if (Encontrado == false && Error == false)
                {
                    if (Tablero[Fila, i] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[Fila, i] == 2 || id == 2 && Tablero[Fila, i] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(i);
                        string fil = (Fila + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = (Fila).ToString() + i.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[Fila, i] == id)
                    {
                        Encontrado = true;
                    }
                }
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //********************************************HORIZONTAL PARA LA DERECHA******************************************************
        //****************************************************************************************************************************
        public Boolean Horizontal_Derecha(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            for (int i = Columna + 1; i < Tablero.GetLength(1); i++)
            {
                if (Encontrado == false && Error == false)
                {
                    if (Tablero[Fila, i] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[Fila, i] == 2 || id == 2 && Tablero[Fila, i] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(i);
                        string fil = (Fila + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = (Fila).ToString() + i.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[Fila, i] == id)
                    {
                        Encontrado = true;
                    }
                }
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //*****************************************DIAGONAL PARA IZQUIERDA-ARRIBA*****************************************************
        //****************************************************************************************************************************
        public Boolean Diagonal_Izquierda_Arriba(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            Fila--;
            for (int i = Columna - 1; i >= 0; i--)
            {
                if (Encontrado == false && Error == false && Fila >= 0)
                {
                    if (Tablero[Fila, i] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[Fila, i] == 2 || id == 2 && Tablero[Fila, i] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(i);
                        string fil = (Fila + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = Fila.ToString() + i.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[Fila, i] == id)
                    {
                        Encontrado = true;
                    }
                }
                Fila--;
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //******************************************DIAGONAL PARA DERECHA-ARRIBA******************************************************
        //****************************************************************************************************************************
        public Boolean Diagonal_Derecha_Arriba(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            Fila--;
            for (int i = Columna + 1; i < Tablero.GetLength(1); i++)
            {
                if (Encontrado == false && Error == false && Fila >= 0)
                {
                    if (Tablero[Fila, i] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[Fila, i] == 2 || id == 2 && Tablero[Fila, i] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(i);
                        string fil = (Fila + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = Fila.ToString() + i.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[Fila, i] == id)
                    {
                        Encontrado = true;
                    }
                }
                Fila--;
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //******************************************DIAGONAL PARA IZQUIERDA-ABAJO*****************************************************
        //****************************************************************************************************************************
        public Boolean Diagonal_Izquierda_Abajo(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            Fila++;
            for (int i = Columna - 1; i >= 0; i--)
            {
                if (Encontrado == false && Error == false && Fila <= 7)
                {
                    if (Tablero[Fila, i] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[Fila, i] == 2 || id == 2 && Tablero[Fila, i] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(i);
                        string fil = (Fila + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = Fila.ToString() + i.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[Fila, i] == id)
                    {
                        Encontrado = true;
                    }
                }
                Fila++;
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }

        //****************************************************************************************************************************
        //*******************************************DIAGONAL PARA DERECHA-ABAJO******************************************************
        //****************************************************************************************************************************
        public Boolean Diagonal_Derecha_Abajo(int Fila, int Columna, Boolean Verificar)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Boolean Error = false;
            int Contador = 0;
            Stack pila = new Stack();
            Stack pilaM = new Stack();
            int id = 0;
            if (Turno == true)
            {
                id = 1;
            }
            else if (Turno == false)
            {
                id = 2;
            }

            Fila++;
            for (int i = Columna + 1; i < Tablero.GetLength(1); i++)
            {
                if (Encontrado == false && Error == false && Fila <= 7)
                {
                    if (Tablero[Fila, i] == 0)
                    {
                        Error = true;
                    }
                    else if (id == 1 && Tablero[Fila, i] == 2 || id == 2 && Tablero[Fila, i] == 1) //Es diferente
                    {
                        string col = ConvertirColumna(i);
                        string fil = (Fila + 1).ToString();
                        string StrCasilla = col + fil;
                        string Position = Fila.ToString() + i.ToString();

                        pilaM.Push(Position);
                        pila.Push(StrCasilla);
                        Session["ContadorPila"] = (int)Session["ContadorPila"] + 1;
                        Contador++;
                    }
                    else if (Tablero[Fila, i] == id)
                    {
                        Encontrado = true;
                    }
                }
                Fila++;
            }

            if (Contador == 0)
            {
                Encontrado = false;
            }

            if (Encontrado == true && Verificar == true)
            {
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pilaM.Pop();
                    int fil = int.Parse(position.Substring(0, 1));
                    int col = int.Parse(position.Substring(1, 1));
                    Tablero[fil, col] = id;
                }
            }
            Session["Pila"] = pila;
            return Encontrado;
        }
        //****************************************************************************************************************************
        //****************************************************************************************************************************
        //****************************************************************************************************************************

        public void VoltearFichas(Boolean Argumento)
        {
            Boolean Turno = (Boolean)Application["Turno"];
            if (Argumento == true)
            {
                Stack pila = (Stack)Session["Pila"];
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pila.Pop();
                    ImageButton Casillas = FindControl(position) as ImageButton;
                    if (Turno == true)
                    {
                        Lista temporal = (Lista)Session["ColoresUsuario"];
                        Casillas.ImageUrl = temporal.GetIndex();
                    }
                    else
                    {
                        Lista temporal = (Lista)Session["ColoresRival"];
                        Casillas.ImageUrl = temporal.GetIndex();
                    }
                }
            }
        }

        public void ColocarFicha(ImageButton Casilla)
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            string StrCasilla = Casilla.ID;
            int Fila = EncontrarFila(StrCasilla);
            int Columna = EncontrarColumna(StrCasilla);

            Boolean Turno = (Boolean)Application["Turno"];
            if ((int)Session["ContadorMovimientos"] < 4)
            {
                if (Casilla.ID == "D4" || Casilla.ID == "E4" || Casilla.ID == "D5" || Casilla.ID == "E5")
                {
                    if((Boolean)Application["Turno"] == true)
                    {
                        LblInv1.Visible = false;
                        LblInv2.Visible = false;
                        ImgSad.Visible = false;
                        Lista temporal = (Lista)Session["ColoresUsuario"];
                        Casilla.ImageUrl = temporal.GetIndex();
                        Casilla.Enabled = false;
                        temporal.Next();
                        Session["ColoresUsuario"] = temporal;
                        Tablero[Fila, Columna] = 1;
                        Session["ContadorMovimientos"] = (int)Session["ContadorMovimientos"] + 1;
                        Application["Turno"] = false;

                        Reloj1.Enabled = false;
                        Reloj2.Enabled = true;

                        LblTurno.Text = (String)Session["Segundo"];
                        LblEspera.Text = (String)Session["Primero"];
                        Session["ContadorPrimero"] = (int)Session["ContadorPrimero"] + 1;
                        LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                        LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                        if ((Boolean)Session["Modo"] == true)
                        {
                            TiroMaquina();
                        }
                    }
                    else if((Boolean)Application["Turno"] == false)
                    {
                        LblInv1.Visible = false;
                        LblInv2.Visible = false;
                        ImgSad.Visible = false;

                        Lista temporal = (Lista)Session["ColoresRival"];
                        Casilla.ImageUrl = temporal.GetIndex();
                        Casilla.Enabled = false;
                        temporal.Next();
                        Session["ColoresRival"] = temporal;

                        Tablero[Fila, Columna] = 2;
                        Session["ContadorMovimientos"] = (int)Session["ContadorMovimientos"] + 1;
                        Application["Turno"] = true;

                        Reloj2.Enabled = false;
                        Reloj1.Enabled = true;

                        LblTurno.Text = (String)Session["Primero"];
                        LblEspera.Text = (String)Session["Segundo"];
                        Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                        LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                        LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                        if ((Boolean)Session["Modo"] == true)
                        {
                            TiroMaquina();
                        }
                    }
                }
                else
                {
                    LblInv1.Text = "!TIRO INVALIDO!";
                    LblInv2.Text = "PORFAVOR REPITA SU JUGADA";
                    ImgSad.ImageUrl = "img/sad.png";
                    LblInv1.Visible = true;
                    LblInv2.Visible = true;
                    ImgSad.Visible = true;
                }
            }
            else
            {
                Session["ContadorPila"] = 0;
                Boolean VerticalArriba = Vertical_Arriba(Fila, Columna, true);
                VoltearFichas(VerticalArriba);

                Session["ContadorPila"] = 0;
                Boolean VerticalAbajo = Vertical_Abajo(Fila, Columna, true);
                VoltearFichas(VerticalAbajo);

                Session["ContadorPila"] = 0;
                Boolean HorizontalIzquierda = Horizontal_Izquierda(Fila, Columna, true);
                VoltearFichas(HorizontalIzquierda);

                Session["ContadorPila"] = 0;
                Boolean HorizontalDerecha = Horizontal_Derecha(Fila, Columna, true);
                VoltearFichas(HorizontalDerecha);

                Session["ContadorPila"] = 0;
                Boolean DiagonalIzquierdaArriba = Diagonal_Izquierda_Arriba(Fila, Columna, true);
                VoltearFichas(DiagonalIzquierdaArriba);

                Session["ContadorPila"] = 0;
                Boolean DiagonalDerechaArriba = Diagonal_Derecha_Arriba(Fila, Columna, true);
                VoltearFichas(DiagonalDerechaArriba);

                Session["ContadorPila"] = 0;
                Boolean DiagonalIzquierdaAbajo = Diagonal_Izquierda_Abajo(Fila, Columna, true);
                VoltearFichas(DiagonalIzquierdaAbajo);

                Session["ContadorPila"] = 0;
                Boolean DiagonalDerechaAbajo = Diagonal_Derecha_Abajo(Fila, Columna, true);
                VoltearFichas(DiagonalDerechaAbajo);

                int id = 0;
                if (Turno == true)
                {
                    id = 1;
                }
                else if (Turno == false)
                {
                    id = 2;
                }

                if (VerticalArriba == true || VerticalAbajo == true
                    || HorizontalIzquierda == true || HorizontalDerecha == true
                    || DiagonalIzquierdaArriba == true || DiagonalDerechaArriba == true
                    || DiagonalIzquierdaAbajo == true || DiagonalDerechaAbajo == true)
                {
                    if (Turno == true)
                    {
                        Lista temporal = (Lista)Session["ColoresUsuario"];
                        Casilla.ImageUrl = temporal.GetIndex();
                        Casilla.Enabled = false;
                        temporal.Next();
                        Session["ColoresUsuario"] = temporal;
                        
                        int fil = EncontrarFila(Casilla.ID);
                        int col = EncontrarColumna(Casilla.ID);
                        Tablero[fil, col] = id;
                        LblInv1.Visible = false;
                        LblInv2.Visible = false;
                        ImgSad.Visible = false;
                        LblTurno.Text = (String)Session["Segundo"];
                        LblEspera.Text = (String)Session["Primero"];

                        Reloj1.Enabled = false;
                        Reloj2.Enabled = true;

                        Session["ContadorPrimero"] = (int)Session["ContadorPrimero"] + 1;
                        LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                        LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                        Application["Turno"] = false;
                        Boolean Continua = PartidaContinua();
                        if (Continua == false)
                        {
                            Application["Turno"] = true;
                            Boolean Continua2 = PartidaContinua();
                            if (Continua2 == false)
                            {
                                Application["Turno"] = false;
                                TerminarPartida();
                            }
                            else
                            {
                                Application["Turno"] = true;
                                LblTurno.Text = (String)Session["Primero"];
                                LblEspera.Text = (String)Session["Segundo"];

                                Reloj2.Enabled = false;
                                Reloj1.Enabled = true;

                                LblInv1.Text = "¡REPITE TURNO!";
                                LblInv2.Text = "El OTRO JUGADOR NO POSEE TIROS";
                                ImgSad.ImageUrl = "img/star.png";
                                LblInv2.Visible = true;
                                LblInv1.Visible = true;
                                ImgSad.Visible = true;
                            }
                        }
                        else
                        {
                            if ((Boolean)Session["Modo"] == true)
                            {
                                TiroMaquina();
                            }
                        }
                    }
                    else
                    {
                        Lista temporal = (Lista)Session["ColoresRival"];
                        Casilla.ImageUrl = temporal.GetIndex();
                        temporal.Next();
                        Casilla.Enabled = false;
                        Session["ColoresRival"] = temporal;
                        
                        int fil = EncontrarFila(Casilla.ID);
                        int col = EncontrarColumna(Casilla.ID);
                        Tablero[fil, col] = id;
                        LblInv1.Visible = false;
                        LblInv2.Visible = false;
                        ImgSad.Visible = false;
                        LblTurno.Text = (String)Session["Primero"];
                        LblEspera.Text = (String)Session["Segundo"];

                        Reloj2.Enabled = false;
                        Reloj1.Enabled = true;

                        Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                        LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                        LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                        Application["Turno"] = true;
                        Boolean Continua = PartidaContinua();
                        if (Continua == false)
                        {
                            Application["Turno"] = false;
                            Boolean Continua2 = PartidaContinua();
                            if (Continua2 == false)
                            {
                                Application["Turno"] = true;
                                TerminarPartida();
                            }
                            else
                            {
                                Application["Turno"] = false;
                                LblTurno.Text = (String)Session["Segundo"];
                                LblEspera.Text = (String)Session["Primero"];

                                Reloj1.Enabled = false;
                                Reloj2.Enabled = true;

                                LblInv1.Text = "¡REPITE TURNO!";
                                LblInv2.Text = "El OTRO JUGADOR NO POSEE TIROS";
                                ImgSad.ImageUrl = "img/star.png";
                                LblInv2.Visible = true;
                                LblInv1.Visible = true;
                                ImgSad.Visible = true;
                            }
                        }
                        else
                        {
                            if ((Boolean)Session["Modo"] == true)
                            {
                                TiroMaquina();
                            }
                        }
                    }
                }
                else
                {
                    LblInv1.Text = "!TIRO INVALIDO!";
                    LblInv2.Text = "PORFAVOR REPITA SU JUGADA";
                    ImgSad.ImageUrl = "img/sad.png";
                    LblInv1.Visible = true;
                    LblInv2.Visible = true;
                    ImgSad.Visible = true;
                }
            }
        }

        public void TiroMaquina()
        {
            Thread.Sleep(500);
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Stack pila = new Stack();
            int ContadorPila = 0;
            if ((int)Session["ContadorMovimientos"] < 4)
            {
                /*if (D4.Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    D4.ImageUrl = temporal.GetIndex();
                    D4.Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[3, 3] = 2;
                    Session["ContadorMovimientos"] = (int)Session["ContadorMovimientos"] + 1;
                    Application["Turno"] = true;

                    LblTurno.Text = (String)Session["Primero"];
                    LblEspera.Text = (String)Session["Segundo"];

                    Reloj2.Enabled = false;
                    Reloj1.Enabled = true;

                    Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                }
                else if(E4.Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    E4.ImageUrl = temporal.GetIndex();
                    E4.Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[3, 4] = 2;
                    Session["ContadorMovimientos"] = (int)Session["ContadorMovimientos"] + 1;
                    Application["Turno"] = true;

                    LblTurno.Text = (String)Session["Primero"];
                    LblEspera.Text = (String)Session["Segundo"];

                    Reloj2.Enabled = false;
                    Reloj1.Enabled = true;

                    Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                }
                else if (D5.Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    D5.ImageUrl = temporal.GetIndex();
                    D5.Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[4, 3] = 2;
                    Session["ContadorMovimientos"] = (int)Session["ContadorMovimientos"] + 1;
                    Application["Turno"] = true;

                    LblTurno.Text = (String)Session["Primero"];
                    LblEspera.Text = (String)Session["Segundo"];

                    Reloj2.Enabled = false;
                    Reloj1.Enabled = true;

                    Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                }
                else if (E5.Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    E5.ImageUrl = temporal.GetIndex();
                    E5.Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[4, 4] = 2;
                    Session["ContadorMovimientos"] = (int)Session["ContadorMovimientos"] + 1;
                    Application["Turno"] = true;

                    LblTurno.Text = (String)Session["Primero"];
                    LblEspera.Text = (String)Session["Segundo"];

                    Reloj2.Enabled = false;
                    Reloj1.Enabled = true;

                    Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                }*/
            }
            else
            {
                for (int i = 0; i < Tablero.GetLength(0); i++)
                {
                    for (int j = 0; j < Tablero.GetLength(1); j++)
                    {
                        if (Tablero[i, j] == 0)
                        {
                            Boolean VerticalArriba = Vertical_Arriba(i, j, false);
                            Boolean VerticalAbajo = Vertical_Abajo(i, j, false);
                            Boolean HorizontalIzquierda = Horizontal_Izquierda(i, j, false);
                            Boolean HorizontalDerecha = Horizontal_Derecha(i, j, false);
                            Boolean DiagonalIzquierdaArriba = Diagonal_Izquierda_Arriba(i, j, false);
                            Boolean DiagonalDerechaArriba = Diagonal_Derecha_Arriba(i, j, false);
                            Boolean DiagonalIzquierdaAbajo = Diagonal_Izquierda_Abajo(i, j, false);
                            Boolean DiagonalDerechaAbajo = Diagonal_Derecha_Abajo(i, j, false);
                            if (VerticalArriba == true || VerticalAbajo == true
                            || HorizontalIzquierda == true || HorizontalDerecha == true
                            || DiagonalIzquierdaArriba == true || DiagonalDerechaArriba == true
                            || DiagonalIzquierdaAbajo == true || DiagonalDerechaAbajo == true)
                            {
                                Encontrado = true;
                                pila.Push(i.ToString() + j.ToString());
                                ContadorPila++;
                            }
                        }
                    }
                }

                if (Encontrado == true)
                {
                    //ESCOGIENDO CASILLA ALEATORIA
                    Random aletario = new Random();
                    int CasillaAleatoria;
                    CasillaAleatoria = aletario.Next(1, ContadorPila + 1);
                    string casilla = "";
                    for (int m = 1; m <= CasillaAleatoria; m++)
                    {
                        casilla = pila.Pop();
                    }
                    int i = Int32.Parse(casilla.Substring(0, 1));
                    int j = Int32.Parse(casilla.Substring(1, 1));

                    //AHORA YA, A TIRAR :V
                    Session["ContadorPila"] = 0;
                    Boolean VerticalArriba1 = Vertical_Arriba(i, j, true);
                    VoltearFichas(VerticalArriba1);

                    Session["ContadorPila"] = 0;
                    Boolean VerticalAbajo1 = Vertical_Abajo(i, j, true);
                    VoltearFichas(VerticalAbajo1);

                    Session["ContadorPila"] = 0;
                    Boolean HorizontalIzquierda1 = Horizontal_Izquierda(i, j, true);
                    VoltearFichas(HorizontalIzquierda1);

                    Session["ContadorPila"] = 0;
                    Boolean HorizontalDerecha1 = Horizontal_Derecha(i, j, true);
                    VoltearFichas(HorizontalDerecha1);

                    Session["ContadorPila"] = 0;
                    Boolean DiagonalIzquierdaArriba1 = Diagonal_Izquierda_Arriba(i, j, true);
                    VoltearFichas(DiagonalIzquierdaArriba1);

                    Session["ContadorPila"] = 0;
                    Boolean DiagonalDerechaArriba1 = Diagonal_Derecha_Arriba(i, j, true);
                    VoltearFichas(DiagonalDerechaArriba1);

                    Session["ContadorPila"] = 0;
                    Boolean DiagonalIzquierdaAbajo1 = Diagonal_Izquierda_Abajo(i, j, true);
                    VoltearFichas(DiagonalIzquierdaAbajo1);

                    Session["ContadorPila"] = 0;
                    Boolean DiagonalDerechaAbajo1 = Diagonal_Derecha_Abajo(i, j, true);
                    VoltearFichas(DiagonalDerechaAbajo1);

                    String Fila = (i + 1).ToString();
                    String Columna = ConvertirColumna(j);
                    String position = Columna + Fila;

                    ImageButton Casilla = FindControl(position) as ImageButton;

                    int id = 0;
                    if (Turno == true)
                    {
                        id = 1;
                    }
                    else if (Turno == false)
                    {
                        id = 2;
                    }

                    Lista temporal = (Lista)Session["ColoresRival"];
                    Casilla.ImageUrl = temporal.GetIndex();
                    temporal.Next();
                    Casilla.Enabled = false;
                    Session["ColoresRival"] = temporal;

                    int fil = EncontrarFila(Casilla.ID);
                    int col = EncontrarColumna(Casilla.ID);
                    Tablero[fil, col] = id;
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    LblTurno.Text = (String)Session["Primero"];
                    LblEspera.Text = (String)Session["Segundo"];

                    Reloj2.Enabled = false;
                    Reloj1.Enabled = true;

                    Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                    Application["Turno"] = true;
                    Boolean Continua = PartidaContinua();
                    if (Continua == false)
                    {
                        TerminarPartida();
                    }
                }
            }
        }

        public Boolean PartidaContinua()
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Continua = false;
            Boolean Llena = false;
            //Si esta llena
            int contador = 0;
            for (int i = 0; i < Tablero.GetLength(0); i++)
            {
                for (int j = 0; j < Tablero.GetLength(1); j++)
                {
                    if (Tablero[i, j] == 0)
                    {
                        contador++;
                    }
                }
            }
            if (contador == 0)
            {
                Llena = true;
            }
            //Si no esta llena
            for (int i = 0; i < Tablero.GetLength(0); i++)
            {
                for (int j = 0; j < Tablero.GetLength(1); j++)
                {
                    if (Continua == false && Tablero[i, j] == 0)
                    {
                        Boolean VerticalArriba = Vertical_Arriba(i, j, false);
                        Boolean VerticalAbajo = Vertical_Abajo(i, j, false);
                        Boolean HorizontalIzquierda = Horizontal_Izquierda(i, j, false);
                        Boolean HorizontalDerecha = Horizontal_Derecha(i, j, false);
                        Boolean DiagonalIzquierdaArriba = Diagonal_Izquierda_Arriba(i, j, false);
                        Boolean DiagonalDerechaArriba = Diagonal_Derecha_Arriba(i, j, false);
                        Boolean DiagonalIzquierdaAbajo = Diagonal_Izquierda_Abajo(i, j, false);
                        Boolean DiagonalDerechaAbajo = Diagonal_Derecha_Abajo(i, j, false);
                        if (VerticalArriba == true || VerticalAbajo == true
                        || HorizontalIzquierda == true || HorizontalDerecha == true
                        || DiagonalIzquierdaArriba == true || DiagonalDerechaArriba == true
                        || DiagonalIzquierdaAbajo == true || DiagonalDerechaAbajo == true)
                        {
                            Continua = true;
                        }
                    }
                }
            }

            if (Continua == false || Llena == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void TerminarPartida()
        {
            int[,] Tablero = (int[,])Session["Tablero"];
            int Usuario = 0;
            int Rival = 0;
            for (int i = 0; i < Tablero.GetLength(0); i++)
            {
                for (int j = 0; j < Tablero.GetLength(1); j++)
                {
                    if (Tablero[i, j] == 1)
                    {
                        Usuario++;
                    }
                    else if (Tablero[i, j] == 2)
                    {
                        Rival++;
                    }
                }
            }
            String Resultados = "";
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            if((string)Session["Reto"] == "Normal")
            {
                if (Usuario > Rival)
                {
                    Resultados = "¡" + ((string)Session["Usuario"]).ToUpper() + " HAS GANADO LA PARTIDA! :) | "
                        + ((string)Session["Primero"]).ToUpper() + ": " + Usuario + " fichas | "
                        + ((string)Session["Segundo"]).ToUpper() + ": " + Rival + " fichas";

                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {

                        //Obteniendo el modo de la partida
                        string Modo = GetModo();
                        //Obteninedo los movimientos xml
                        string Movimientos = GetMovimientos();

                        //Ingresando los datos de la partida
                        SqlCon.Open();
                        SqlCommand cmd1 = SqlCon.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos, NoMovimientos) " +
                            "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "', " + Session["ContadorPrimero"] + ")";
                        cmd1.ExecuteNonQuery();

                        //Obteniendo el id de la ultima partida
                        string idPartida = GetidPartida();

                        //Obteninedo el id del usuario
                        string idUsuario = GetidUsuario();

                        //Colocando los resultados de la partida
                        SqlCommand cmd3 = SqlCon.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                            "idPartida) VALUES ('COLORES', 'GANADOR', '" + idUsuario +
                            "', '" + idPartida + "')";
                        cmd3.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                }
                else if (Rival > Usuario)
                {
                    Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS PERDIDO LA PARTIDA... :( | "
                        + ((string)Session["Primero"]).ToUpper() + ": " + Usuario + " fichas | "
                        + ((string)Session["Segundo"]).ToUpper() + ": " + Rival + " fichas";

                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {

                        //Obteniendo el modo de la partida
                        string Modo = GetModo();
                        //Obteninedo los movimientos xml
                        string Movimientos = GetMovimientos();

                        //Ingresando los datos de la partida
                        SqlCon.Open();
                        SqlCommand cmd1 = SqlCon.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos, NoMovimientos) " +
                            "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "', " + Session["ContadorPrimero"] + ")";
                        cmd1.ExecuteNonQuery();

                        //Obteniendo el id de la ultima partida
                        string idPartida = GetidPartida();

                        //Obteninedo el id del usuario
                        string idUsuario = GetidUsuario();

                        //Colocando los resultados de la partida
                        SqlCommand cmd3 = SqlCon.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                            "idPartida) VALUES ('COLORES', 'PERDEDOR', '" + idUsuario +
                            "', '" + idPartida + "')";
                        cmd3.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                }
                else if (Rival == Usuario)
                {
                    Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS EMPATADO LA PARTIDA :O | "
                        + ((string)Session["Blancas"]).ToUpper() + ": " + Usuario + " fichas | "
                        + ((string)Session["Negras"]).ToUpper() + ": " + Rival + " fichas";

                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {

                        //Obteniendo el modo de la partida
                        string Modo = GetModo();
                        //Obteninedo los movimientos xml
                        string Movimientos = GetMovimientos();

                        //Ingresando los datos de la partida
                        SqlCon.Open();
                        SqlCommand cmd1 = SqlCon.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos, NoMovimientos) " +
                            "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "', " + Session["ContadorPrimero"] + ")";
                        cmd1.ExecuteNonQuery();

                        //Obteniendo el id de la ultima partida
                        string idPartida = GetidPartida();

                        //Obteninedo el id del usuario
                        string idUsuario = GetidUsuario();

                        //Colocando los resultados de la partida
                        SqlCommand cmd3 = SqlCon.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                            "idPartida) VALUES ('COLORES', 'EMPATE', '" + idUsuario +
                            "', '" + idPartida + "')";
                        cmd3.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                }
            }
            else if ((string)Session["Reto"] == "Inverso")
            {
                if (Usuario < Rival)
                {
                    Resultados = "¡" + ((string)Session["Usuario"]).ToUpper() + " HAS GANADO LA PARTIDA! :) | "
                        + ((string)Session["Primero"]).ToUpper() + ": " + Usuario + " fichas | "
                        + ((string)Session["Segundo"]).ToUpper() + ": " + Rival + " fichas";

                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {

                        //Obteniendo el modo de la partida
                        string Modo = GetModo();
                        //Obteninedo los movimientos xml
                        string Movimientos = GetMovimientos();

                        //Ingresando los datos de la partida
                        SqlCon.Open();
                        SqlCommand cmd1 = SqlCon.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos, NoMovimientos) " +
                            "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "', " + Session["ContadorPrimero"] + ")";
                        cmd1.ExecuteNonQuery();

                        //Obteniendo el id de la ultima partida
                        string idPartida = GetidPartida();

                        //Obteninedo el id del usuario
                        string idUsuario = GetidUsuario();

                        //Colocando los resultados de la partida
                        SqlCommand cmd3 = SqlCon.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                            "idPartida) VALUES ('COLORES', 'GANADOR', '" + idUsuario +
                            "', '" + idPartida + "')";
                        cmd3.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                }
                else if (Rival < Usuario)
                {
                    Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS PERDIDO LA PARTIDA... :( | "
                        + ((string)Session["Primero"]).ToUpper() + ": " + Usuario + " fichas | "
                        + ((string)Session["Segundo"]).ToUpper() + ": " + Rival + " fichas";

                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {

                        //Obteniendo el modo de la partida
                        string Modo = GetModo();
                        //Obteninedo los movimientos xml
                        string Movimientos = GetMovimientos();

                        //Ingresando los datos de la partida
                        SqlCon.Open();
                        SqlCommand cmd1 = SqlCon.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos, NoMovimientos) " +
                            "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "', " + Session["ContadorPrimero"] + ")";
                        cmd1.ExecuteNonQuery();

                        //Obteniendo el id de la ultima partida
                        string idPartida = GetidPartida();

                        //Obteninedo el id del usuario
                        string idUsuario = GetidUsuario();

                        //Colocando los resultados de la partida
                        SqlCommand cmd3 = SqlCon.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                            "idPartida) VALUES ('COLORES', 'PERDEDOR', '" + idUsuario +
                            "', '" + idPartida + "')";
                        cmd3.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                }
                else if (Rival == Usuario)
                {
                    Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS EMPATADO LA PARTIDA :O | "
                        + ((string)Session["Blancas"]).ToUpper() + ": " + Usuario + " fichas | "
                        + ((string)Session["Negras"]).ToUpper() + ": " + Rival + " fichas";

                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {

                        //Obteniendo el modo de la partida
                        string Modo = GetModo();
                        //Obteninedo los movimientos xml
                        string Movimientos = GetMovimientos();

                        //Ingresando los datos de la partida
                        SqlCon.Open();
                        SqlCommand cmd1 = SqlCon.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos, NoMovimientos) " +
                            "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "', " + Session["ContadorPrimero"] + ")";
                        cmd1.ExecuteNonQuery();

                        //Obteniendo el id de la ultima partida
                        string idPartida = GetidPartida();

                        //Obteninedo el id del usuario
                        string idUsuario = GetidUsuario();

                        //Colocando los resultados de la partida
                        SqlCommand cmd3 = SqlCon.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                            "idPartida) VALUES ('COLORES', 'EMPATE', '" + idUsuario +
                            "', '" + idPartida + "')";
                        cmd3.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                }
            }
            
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + Resultados + "');", true);
        }

        public string GetidUsuario()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string idUsuario = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT idUsuario As Valor FROM Usuarios WHERE NombreUsuario ='" + Session["Usuario"] + "'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                idUsuario = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return idUsuario;
        }

        public string GetidPartida()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string idPartida = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT MAX(idPartida) As Valor FROM Partida";
                SqlCommand cmd2 = new SqlCommand(query, SqlCon);
                idPartida = Convert.ToString(cmd2.ExecuteScalar());
                SqlCon.Close();
            }
            return idPartida;
        }

        public string GetModo()
        {
            string Modo = "";
            if ((Boolean)Session["Modo"] == true)
            {
                Modo = "CPU";
            }
            else
            {
                Modo = "VS";
            }
            return Modo;
        }

        public string GetMovimientos()
        {
            GuardarPartida(true);
            string Ruta = @"C:\Users\admin\Documents\GitHub\-IPC2-Proyecto\[IPC2]ProyectoOthello\WebApplication1\PartidasGuardadas\PartidaTemporal.xml";
            string Movimientos = File.ReadAllText(Ruta);
            return Movimientos;
        }

        //protected void A1_Click(object sender, ImageClickEventArgs e) { ColocarFicha(A1); }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["Partidas"] = (int)Session["Partidas"] + 1;
            GuardarPartida(false);
            LblSucc.Visible = true;
        }

        protected void Reloj2_Tick(object sender, EventArgs e)
        {
            int minutos = (int)Session["Minuto2"];
            int segundos = (int)Session["Segundo2"];
            segundos++;
            if (segundos == 61)
            {
                minutos++;
                segundos = 0;
            }
            LblRejoj2.Text = minutos.ToString() + ":" + segundos.ToString();
            Session["Minuto2"] = minutos;
            Session["Segundo2"] = segundos;
        }

        protected void Reloj1_Tick(object sender, EventArgs e)
        {
            int minutos = (int)Session["Minuto1"];
            int segundos = (int)Session["Segundo1"];
            segundos++;
            if (segundos == 61)
            {
                minutos++;
                segundos = 0;
            }
            LblReloj1.Text = minutos.ToString() + ":" + segundos.ToString();
            Session["Minuto1"] = minutos;
            Session["Segundo1"] = segundos;
        }
    }
}