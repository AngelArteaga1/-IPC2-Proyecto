using System;
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
    public partial class Tablero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblSucc.Visible = false;
            if (!IsPostBack)
            {
                int ContadorBlancas = 0;
                int ContadorNegras = 0;
                Session["ContadorBlancas"] = ContadorBlancas;
                Session["ContadorNegras"] = ContadorNegras;
                int[,] Tablero = new int[8,8];
                Boolean Cargada = (Boolean)Session["Cargada"];
                if (Cargada == false)
                {
                    Boolean turno = true;
                    Application["Turno"] = turno;
                    if ((Boolean)Application["Turno"] == true)
                    {
                        LblTurno.Text = (string)Session["Blancas"];
                        LblEspera.Text = (string)Session["Negras"];
                    }
                    else if ((Boolean)Application["Turno"] == false)
                    {
                        LblTurno.Text = (string)Session["Negras"];
                        LblEspera.Text = (string)Session["Blancas"];
                    }
                    D4.ImageUrl = "img/FichaB.png";
                    D4.Enabled = false;
                    Tablero[3, 3] = 1;
                    E4.ImageUrl = "img/FichaN.png";
                    E4.Enabled = false;
                    Tablero[3, 4] = 2;
                    D5.ImageUrl = "img/FichaN.png";
                    D5.Enabled = false;
                    Tablero[4, 3] = 2;
                    E5.ImageUrl = "img/FichaB.png";
                    E5.Enabled = false;
                    Tablero[4, 4] = 1;
                    Session["Tablero"] = Tablero;
                    if ((string)Session["Blancas"] == "CPU")
                    {
                        Session["ContadorPila"] = 0;
                        TiroMaquina();
                    }
                }
                if (Cargada == true)
                {
                    Session["Tablero"] = Tablero;
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
                    if ((Boolean)Application["Turno"] == true)
                    {
                        LblTurno.Text = (string)Session["Blancas"];
                        LblEspera.Text = (string)Session["Negras"];
                    }
                    else if((Boolean)Application["Turno"] == false)
                    {
                        LblTurno.Text = (string)Session["Negras"];
                        LblEspera.Text = (string)Session["Blancas"];
                    }
                }
                LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Blancas"] + ": " + (int)Session["ContadorBlancas"];
                LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Negras"] + ": " + (int)Session["ContadorNegras"];
                //TIRO DE LA MAQUINA
                if ((Boolean)Application["Turno"] == true && (string)Session["Blancas"] == "CPU")
                {
                    Session["ContadorPila"] = 0;
                    TiroMaquina();
                }
                if ((Boolean)Application["Turno"] == false && (string)Session["Negras"] == "CPU")
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
            string columna = StrCasilla.Substring(0,1);
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
            string fila = StrCasilla.Substring(1,1);
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
                        string fil = (i+1).ToString();
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
                    int fil = int.Parse(position.Substring(0,1));
                    int col = int.Parse(position.Substring(1,1));
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
                        string fil = (Fila+1).ToString();
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
                        Casillas.ImageUrl = "img/FichaB.png";
                    }
                    else
                    {
                        Casillas.ImageUrl = "img/FichaN.png";
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
                    Casilla.ImageUrl = "img/FichaB.png";
                    Casilla.Enabled = false;
                    int fil = EncontrarFila(Casilla.ID);
                    int col = EncontrarColumna(Casilla.ID);
                    Tablero[fil, col] = id;
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    LblTurno.Text = (String)Session["Negras"];
                    LblEspera.Text = (String)Session["Blancas"];
                    Session["ContadorBlancas"] = (int)Session["ContadorBlancas"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Blancas"] + ": " + (int)Session["ContadorBlancas"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Negras"] + ": " + (int)Session["ContadorNegras"];
                    Application["Turno"] = false;
                    PartidaContinua();
                    if ((Boolean)Session["Modo"] == true)
                    {
                        TiroMaquina();
                    }
                }
                else
                {
                    Casilla.ImageUrl = "img/FichaN.png";
                    Casilla.Enabled = false;
                    int fil = EncontrarFila(Casilla.ID);
                    int col = EncontrarColumna(Casilla.ID);
                    Tablero[fil, col] = id;
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    LblTurno.Text = (String)Session["Blancas"];
                    LblEspera.Text = (String)Session["Negras"];
                    Session["ContadorNegras"] = (int)Session["ContadorNegras"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Blancas"] + ": " + (int)Session["ContadorBlancas"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Negras"] + ": " + (int)Session["ContadorNegras"];
                    Application["Turno"] = true;
                    PartidaContinua();
                    if ((Boolean)Session["Modo"] == true)
                    {
                        TiroMaquina();
                    }
                }
            }
            else
            {
                LblInv1.Visible = true;
                LblInv2.Visible = true;
                ImgSad.Visible = true;
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

                if (Turno == true)
                {
                    Casilla.ImageUrl = "img/FichaB.png";
                    Casilla.Enabled = false;
                    int fil = EncontrarFila(Casilla.ID);
                    int col = EncontrarColumna(Casilla.ID);
                    Tablero[fil, col] = id;
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    LblTurno.Text = (String)Session["Negras"];
                    LblEspera.Text = (String)Session["Blancas"];
                    Session["ContadorBlancas"] = (int)Session["ContadorBlancas"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Blancas"] + ": " + (int)Session["ContadorBlancas"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Negras"] + ": " + (int)Session["ContadorNegras"];
                    Application["Turno"] = false;
                    PartidaContinua();
                }
                else
                {
                    Casilla.ImageUrl = "img/FichaN.png";
                    Casilla.Enabled = false;
                    int fil = EncontrarFila(Casilla.ID);
                    int col = EncontrarColumna(Casilla.ID);
                    Tablero[fil, col] = id;
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    LblTurno.Text = (String)Session["Blancas"];
                    LblEspera.Text = (String)Session["Negras"];
                    Session["ContadorNegras"] = (int)Session["ContadorNegras"] + 1;
                    LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Blancas"] + ": " + (int)Session["ContadorBlancas"];
                    LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Negras"] + ": " + (int)Session["ContadorNegras"];
                    Application["Turno"] = true;
                    PartidaContinua();
                }
            }

        }

        public void PartidaContinua()
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
                    if (Tablero[i,j] == 0)
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
                    if (Continua == false && Tablero[i,j] == 0)
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
                int Blancas = 0;
                int Negras = 0;
                for (int i = 0; i < Tablero.GetLength(0); i++)
                {
                    for (int j = 0; j < Tablero.GetLength(1); j++)
                    {
                        if (Tablero[i,j] == 1)
                        {
                            Blancas++;
                        }
                        else if(Tablero[i,j] == 2)
                        {
                            Negras++;
                        }
                    }
                }
                String Resultados = "";
                string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
                if ((string)Session["Usuario"] == (string)Session["Blancas"])
                {
                    if (Blancas > Negras)
                    {
                        Resultados = "¡" + ((string)Session["Usuario"]).ToUpper() + " HAS GANADO LA PARTIDA! :) | " 
                            + ((string)Session["Blancas"]).ToUpper() + ": " + Blancas + " fichas | "
                            + ((string)Session["Negras"]).ToUpper() + ": " + Negras + " fichas";

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
                            cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos) " +
                                "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "')";
                            cmd1.ExecuteNonQuery();

                            //Obteniendo el id de la ultima partida
                            string idPartida = GetidPartida();

                            //Obteninedo el id del usuario
                            string idUsuario = GetidUsuario();

                            //Colocando los resultados de la partida
                            SqlCommand cmd3 = SqlCon.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                                "idPartida) VALUES ('BLANCAS', 'GANADOR', '" + idUsuario +
                                "', '" + idPartida + "')";
                            cmd3.ExecuteNonQuery();
                            SqlCon.Close();
                        }
                    }
                    else if (Negras > Blancas)
                    {
                        Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS PERDIDO LA PARTIDA... :( | "
                            + ((string)Session["Blancas"]).ToUpper() + ": " + Blancas + " fichas | "
                            + ((string)Session["Negras"]).ToUpper() + ": " + Negras + " fichas";

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
                            cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos) " +
                                "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "')";
                            cmd1.ExecuteNonQuery();

                            //Obteniendo el id de la ultima partida
                            string idPartida = GetidPartida();

                            //Obteninedo el id del usuario
                            string idUsuario = GetidUsuario();

                            //Colocando los resultados de la partida
                            SqlCommand cmd3 = SqlCon.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                                "idPartida) VALUES ('BLANCAS', 'PERDEDOR', '" + idUsuario +
                                "', '" + idPartida + "')";
                            cmd3.ExecuteNonQuery();
                            SqlCon.Close();
                        }
                    }
                    else if(Negras == Blancas)
                    {
                        Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS EMPATADO LA PARTIDA :O | "
                            + ((string)Session["Blancas"]).ToUpper() + ": " + Blancas + " fichas | "
                            + ((string)Session["Negras"]).ToUpper() + ": " + Negras + " fichas";

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
                            cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos) " +
                                "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "')";
                            cmd1.ExecuteNonQuery();

                            //Obteniendo el id de la ultima partida
                            string idPartida = GetidPartida();

                            //Obteninedo el id del usuario
                            string idUsuario = GetidUsuario();

                            //Colocando los resultados de la partida
                            SqlCommand cmd3 = SqlCon.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                                "idPartida) VALUES ('BLANCAS', 'EMPATE', '" + idUsuario +
                                "', '" + idPartida + "')";
                            cmd3.ExecuteNonQuery();
                            SqlCon.Close();
                        }
                    }
                } else if ((string)Session["Usuario"] == (string)Session["Negras"])
                {
                    if (Blancas < Negras)
                    {
                        Resultados = "¡" + ((string)Session["Usuario"]).ToUpper() + " HAS GANADO LA PARTIDA! :) | "
                            + ((string)Session["Blancas"]).ToUpper() + ": " + Blancas + " fichas | "
                            + ((string)Session["Negras"]).ToUpper() + ": " + Negras + " fichas";

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
                            cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos) " +
                                "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "')";
                            cmd1.ExecuteNonQuery();

                            //Obteniendo el id de la ultima partida
                            string idPartida = GetidPartida();

                            //Obteninedo el id del usuario
                            string idUsuario = GetidUsuario();

                            //Colocando los resultados de la partida
                            SqlCommand cmd3 = SqlCon.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                                "idPartida) VALUES ('NEGRAS', 'GANADOR', '" + idUsuario +
                                "', '" + idPartida + "')";
                            cmd3.ExecuteNonQuery();
                            SqlCon.Close();
                        }
                    }
                    else if (Negras < Blancas)
                    {
                        Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS PERDIDO LA PARTIDA... :( | "
                            + ((string)Session["Blancas"]).ToUpper() + ": " + Blancas + " fichas | "
                            + ((string)Session["Negras"]).ToUpper() + ": " + Negras + " fichas";

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
                            cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos) " +
                                "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "')";
                            cmd1.ExecuteNonQuery();

                            //Obteniendo el id de la ultima partida
                            string idPartida = GetidPartida();

                            //Obteninedo el id del usuario
                            string idUsuario = GetidUsuario();

                            //Colocando los resultados de la partida
                            SqlCommand cmd3 = SqlCon.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                                "idPartida) VALUES ('NEGRAS', 'PERDEDOR', '" + idUsuario +
                                "', '" + idPartida + "')";
                            cmd3.ExecuteNonQuery();
                            SqlCon.Close();
                        }
                    }
                    else if (Negras == Blancas)
                    {
                        Resultados = ((string)Session["Usuario"]).ToUpper() + " HAS EMPATADO LA PARTIDA :O | "
                            + ((string)Session["Blancas"]).ToUpper() + ": " + Blancas + " fichas | "
                            + ((string)Session["Negras"]).ToUpper() + ": " + Negras + " fichas";

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
                            cmd1.CommandText = "INSERT INTO Partida (Modo, Estado, Movimientos) " +
                                "VALUES ('" + Modo + "', 'FINALIZADA', '" + Movimientos + "')";
                            cmd1.ExecuteNonQuery();

                            //Obteniendo el id de la ultima partida
                            string idPartida = GetidPartida();

                            //Obteninedo el id del usuario
                            string idUsuario = GetidUsuario();

                            //Colocando los resultados de la partida
                            SqlCommand cmd3 = SqlCon.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "INSERT INTO UsuarioPartida (ColorFicha, Resultado, idUsuario, " +
                                "idPartida) VALUES ('NEGRAS', 'EMPATE', '" + idUsuario +
                                "', '" + idPartida + "')";
                            cmd3.ExecuteNonQuery();
                            SqlCon.Close();
                        }
                    }
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + Resultados + "');", true);
            }

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
            Session["Partidas"] = (int)Session["Partidas"] + 1;
            GuardarPartida(false);
            LblSucc.Visible = true;
        }
    }
}