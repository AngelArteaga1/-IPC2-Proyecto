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
    public partial class Xtream : System.Web.UI.Page
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
                //Crear tablero de ints
                int filas = (int)Session["Filas"];
                int columnas = (int)Session["Columnas"];
                int[,] Tablero = new int[filas, columnas];
                //Crear tablero dinamico
                //https://social.msdn.microsoft.com/Forums/es-ES/916c61bc-3dec-4814-af2d-c606026fdd76/ayuda-con-botones-dinmicos-aspnet-c?forum=netfxwebes
                //https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.webcontrols.panel?view=netframework-4.8
                //https://stackoverflow.com/questions/3095228/adding-br-dynamically-between-controls-asp-net
                ImageButton[,] Botones = new ImageButton[filas, columnas];
                Boolean ColorCasilla = false;
                for (int i = 0; i < Botones.GetLength(0); i++)
                {
                    for (int j = 0; j < Botones.GetLength(1); j++)
                    {
                        string col = ConvertirColumna(j);
                        string fil = ConvertirFila(i);
                        Botones[i, j] = new ImageButton();
                        Botones[i, j].Click += new ImageClickEventHandler(ColocarFicha);
                        Botones[i, j].ID = col + fil;
                        Botones[i, j].ImageUrl = "img/dot.png";
                        if (ColorCasilla == true)
                        {
                            Botones[i, j].CssClass = "btn1";
                        }
                        else
                        {
                            Botones[i, j].CssClass = "btn2";
                        }
                        if (columnas == 20)
                        {
                            Botones[i, j].Width = 90;
                            Botones[i, j].Height = 90;
                        }
                        else
                        {
                            Botones[i, j].Width = 100;
                            Botones[i, j].Height = 100;
                        }
                        PnlTablero.Controls.Add(Botones[i, j]);
                        ColorCasilla = !ColorCasilla;
                    }
                    Label salto = new Label();
                    salto.Text = "<br/>";
                    PnlTablero.Controls.Add(salto);
                    ColorCasilla = !ColorCasilla;
                }
                //Verificar si es cargada
                Boolean Cargada = (Boolean)Session["Cargada"];
                //Realizar inicio sin carga
                if (Cargada == false)
                {
                    //Colocar turnos
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
                    //Si la apertura personalizada o nel
                    if ((String)Session["Apertura"] == "normal")
                    {
                        //Casilla 1
                        int colind = (columnas / 2) - 1;
                        int filind = (filas / 2) - 1;
                        //Casilla 2
                        int colind2 = (columnas / 2) - 1;
                        int filind2 = (filas / 2);
                        //Casilla 3
                        int colind3 = (columnas / 2);
                        int filind3 = (filas / 2) - 1;
                        //Casilla 4
                        int colind4 = (columnas / 2);
                        int filind4 = (filas / 2);
                        //Cambiar fichitas
                        Lista ColoresUsuario = (Lista)Session["ColoresUsuario"];
                        Lista ColoresRival = (Lista)Session["ColoresRival"];
                        //Primera
                        Botones[filind, colind].ImageUrl = ColoresUsuario.GetIndex();
                        Botones[filind, colind].Enabled = false;
                        ColoresUsuario.Next();
                        Tablero[filind, colind] = 1;
                        //Segunda
                        Botones[filind2, colind2].ImageUrl = ColoresRival.GetIndex();
                        Botones[filind2, colind2].Enabled = false;
                        ColoresRival.Next();
                        Tablero[filind2, colind2] = 2;
                        //Tercera
                        Botones[filind3, colind3].ImageUrl = ColoresRival.GetIndex();
                        Botones[filind3, colind3].Enabled = false;
                        ColoresRival.Next();
                        Tablero[filind3, colind3] = 2;
                        //Cuarta
                        Botones[filind4, colind4].ImageUrl = ColoresUsuario.GetIndex();
                        Botones[filind4, colind4].Enabled = false;
                        ColoresUsuario.Next();
                        Tablero[filind4, colind4] = 1;
                        //Volver a subirlos
                        Session["ColoresUsuario"] = ColoresUsuario;
                        Session["ColoresRival"] = ColoresRival;
                    }
                    else
                    {
                        //Casilla 1
                        int colind = (columnas / 2) - 1;
                        int filind = (filas / 2) - 1;
                        Session["colind"] = colind;
                        Session["filind"] = filind;
                        //Casilla 2
                        int colind2 = (columnas / 2) - 1;
                        int filind2 = (filas / 2);
                        Session["colind2"] = colind2;
                        Session["filind2"] = filind2;
                        //Casilla 3
                        int colind3 = (columnas / 2);
                        int filind3 = (filas / 2) - 1;
                        Session["colind3"] = colind3;
                        Session["filind3"] = filind3;
                        //Casilla 4
                        int colind4 = (columnas / 2);
                        int filind4 = (filas / 2);
                        Session["colind4"] = colind4;
                        Session["filind4"] = filind4;
                    }
                    //Guardar los tableros en la sesion
                    Session["Tablero"] = Tablero;
                    Session["Botones"] = Botones;
                    //Generar turno de CPU
                    if ((Boolean)Application["Turno"] == false && (string)Session["Segundo"] == "CPU")
                    {
                        Session["ContadorPila"] = 0;
                        TiroMaquina();
                    }
                }
                if (Cargada == true) //REALIZAR INICIO CON CARGA
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
            else
            {
                ImageButton[,] Botones = (ImageButton[,])Session["Botones"];
                for(int i = 0; i<Botones.GetLength(0); i++)
                {
                    for (int j = 0; j < Botones.GetLength(1); j++)
                    {
                        Botones[i, j].Click += new ImageClickEventHandler(ColocarFicha);
                        PnlTablero.Controls.Add(Botones[i, j]);
                    }
                    Label salto = new Label();
                    salto.Text = "<br/>";
                    PnlTablero.Controls.Add(salto);
                }
                Session["Botones"] = Botones;
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
            else if (columna == "I")
            {
                Columna = 8;
            }
            else if (columna == "J")
            {
                Columna = 9;
            }
            else if (columna == "K")
            {
                Columna = 10;
            }
            else if (columna == "L")
            {
                Columna = 11;
            }
            else if (columna == "M")
            {
                Columna = 12;
            }
            else if (columna == "N")
            {
                Columna = 13;
            }
            else if (columna == "O")
            {
                Columna = 14;
            }
            else if (columna == "P")
            {
                Columna = 15;
            }
            else if (columna == "Q")
            {
                Columna = 16;
            }
            else if (columna == "R")
            {
                Columna = 17;
            }
            else if (columna == "S")
            {
                Columna = 18;
            }
            else if (columna == "T")
            {
                Columna = 19;
            }
            return Columna;
        }

        public int EncontrarFila(String StrCasilla)
        {
            string fila = StrCasilla.Substring(1, (StrCasilla.Length-1));
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
            if (columna == 8)
            {
                Columna = "I";
            }
            if (columna == 9)
            {
                Columna = "J";
            }
            if (columna == 10)
            {
                Columna = "K";
            }
            if (columna == 11)
            {
                Columna = "L";
            }
            if (columna == 12)
            {
                Columna = "M";
            }
            if (columna == 13)
            {
                Columna = "N";
            }
            if (columna == 14)
            {
                Columna = "O";
            }
            if (columna == 15)
            {
                Columna = "P";
            }
            if (columna == 16)
            {
                Columna = "Q";
            }
            if (columna == 17)
            {
                Columna = "R";
            }
            if (columna == 18)
            {
                Columna = "S";
            }
            if (columna == 19)
            {
                Columna = "T";
            }
            return Columna;
        }

        public string ConvertirFila(int fila)
        {
            return (fila + 1).ToString();
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
                        string Position = (i).ToString() + " " +Columna.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                        string Position = (i).ToString() + " " + Columna.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                        string Position = (Fila).ToString() + " " + i.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                        string Position = (Fila).ToString() + " " +  i.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                        string Position = Fila.ToString() + " " +  i.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                        string Position = Fila.ToString() + " " + i.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                if (Encontrado == false && Error == false && Fila < Tablero.GetLength(0))
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
                        string Position = Fila.ToString() + " " + i.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
                if (Encontrado == false && Error == false && Fila < Tablero.GetLength(0))
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
                        string Position = Fila.ToString() + " " + i.ToString();

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
                    string[] posiciones = position.Split(' ');
                    int fil = int.Parse(posiciones[0]);
                    int col = int.Parse(posiciones[1]);
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
            ImageButton[,] Botones = (ImageButton[,])Session["Botones"];
            Boolean Turno = (Boolean)Application["Turno"];
            if (Argumento == true)
            {
                Stack pila = (Stack)Session["Pila"];
                for (int i = 1; i <= (int)Session["ContadorPila"]; i++)
                {
                    string position = pila.Pop();
                    int fila = EncontrarFila(position);
                    int col = EncontrarColumna(position);
                    if (Turno == true)
                    {
                        Lista temporal = (Lista)Session["ColoresUsuario"];
                        Botones[fila, col].ImageUrl = temporal.GetIndex();
                    }
                    else
                    {
                        Lista temporal = (Lista)Session["ColoresRival"];
                        Botones[fila, col].ImageUrl = temporal.GetIndex();
                    }
                }
            }
            Session["Botones"] = Botones;
        }

        private void ColocarFicha(object sender, ImageClickEventArgs e)
        {
            ImageButton[,] Botones = (ImageButton[,])Session["Botones"];
            ImageButton Casilla = (ImageButton)sender;
            int[,] Tablero = (int[,])Session["Tablero"];
            string StrCasilla = Casilla.ID;

            int Fila = EncontrarFila(StrCasilla);
            int Columna = EncontrarColumna(StrCasilla);

            Boolean Turno = (Boolean)Application["Turno"];
            //SI LA PARTIDA ES DE APERTURA PERSONALIZADA
            if ((int)Session["ContadorMovimientos"] < 4 && (string)Session["apertura"] == "personalizada" && (Boolean)Session["Cargada"] == false)
            {
                int colind = (int)Session["colind"];
                int filind = (int)Session["filind"];
                int colind2 = (int)Session["colind2"];
                int filind2 = (int)Session["filind2"];
                int colind3 = (int)Session["colind3"];
                int filind3 = (int)Session["filind3"];
                int colind4 = (int)Session["colind4"];
                int filind4 = (int)Session["filind4"];
                if (Casilla.ID == Botones[filind, colind].ID || Casilla.ID == Botones[filind2, colind2].ID || Casilla.ID == Botones[filind3, colind3].ID || Casilla.ID == Botones[filind4, colind4].ID)
                {
                    if ((Boolean)Application["Turno"] == true)
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
                    else if ((Boolean)Application["Turno"] == false)
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
            else //SI LA PARTIDA ES DE APERTURA NORMAL
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
                        //CAMBIA LA FICHA
                        Lista temporal = (Lista)Session["ColoresUsuario"];
                        Casilla.ImageUrl = temporal.GetIndex();
                        Casilla.Enabled = false;
                        temporal.Next();
                        Session["ColoresUsuario"] = temporal;
                        //COLOCA ENTEROS EN EL TABLERO
                        int fil = EncontrarFila(Casilla.ID);
                        int col = EncontrarColumna(Casilla.ID);
                        Tablero[fil, col] = id;
                        //ELIMINA MENSAJES DE ERROR
                        LblInv1.Visible = false;
                        LblInv2.Visible = false;
                        ImgSad.Visible = false;
                        //ACTUALIZA TURNOS
                        LblTurno.Text = (String)Session["Segundo"];
                        LblEspera.Text = (String)Session["Primero"];
                        //RELOJES
                        Reloj1.Enabled = false;
                        Reloj2.Enabled = true;
                        //VISUAL
                        Session["ContadorPrimero"] = (int)Session["ContadorPrimero"] + 1;
                        LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                        LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                        Application["Turno"] = false;
                        //VERIFICA SI LA PARTIDA CONTINUA
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
                            //REALIZA TIRO DE LA MAQUINA
                            if ((Boolean)Session["Modo"] == true)
                            {
                                TiroMaquina();
                            }
                        }
                    }
                    else
                    {
                        //CAMBIA LA FICHA
                        Lista temporal = (Lista)Session["ColoresRival"];
                        Casilla.ImageUrl = temporal.GetIndex();
                        temporal.Next();
                        Casilla.Enabled = false;
                        Session["ColoresRival"] = temporal;
                        //COLOCA ENTEROS EN EL TABLERO
                        int fil = EncontrarFila(Casilla.ID);
                        int col = EncontrarColumna(Casilla.ID);
                        Tablero[fil, col] = id;
                        //BORRAR LOS MENSAJES DE ERROR
                        LblInv1.Visible = false;
                        LblInv2.Visible = false;
                        ImgSad.Visible = false;
                        //ACTUALIZA TURNO
                        LblTurno.Text = (String)Session["Primero"];
                        LblEspera.Text = (String)Session["Segundo"];
                        //CAMBIA EL RELOJ
                        Reloj2.Enabled = false;
                        Reloj1.Enabled = true;
                        //VISUAL
                        Session["ContadorSegundo"] = (int)Session["ContadorSegundo"] + 1;
                        LblMov1.Text = "MOVIMIENTOS DE " + (string)Session["Primero"] + ": " + (int)Session["ContadorPrimero"];
                        LblMov2.Text = "MOVIMIENTOS DE " + (string)Session["Segundo"] + ": " + (int)Session["ContadorSegundo"];
                        Application["Turno"] = true;
                        //VERIFICA SI LA PARTIDA CONTINUA
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
                                //REPITE TURNO
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
                        else //SI SI CONTINUA
                        {
                            //EJECUTAR TIRO DE MAQUINA
                            if ((Boolean)Session["Modo"] == true)
                            {
                                TiroMaquina();
                            }
                        }
                    }
                }
                else
                { //MENSAJES DE ERROR
                    LblInv1.Text = "!TIRO INVALIDO!";
                    LblInv2.Text = "PORFAVOR REPITA SU JUGADA";
                    ImgSad.ImageUrl = "img/sad.png";
                    LblInv1.Visible = true;
                    LblInv2.Visible = true;
                    ImgSad.Visible = true;
                }
            }
            //BOTONES
            Session["Botones"] = Botones;
        }

        public void TiroMaquina()
        {
            Thread.Sleep(500);
            ImageButton[,] Botones = (ImageButton[,])Session["Botones"]; 
            int[,] Tablero = (int[,])Session["Tablero"];
            Boolean Turno = (Boolean)Application["Turno"];
            Boolean Encontrado = false;
            Stack pila = new Stack();
            int ContadorPila = 0;
            if ((int)Session["ContadorMovimientos"] < 4 && (string)Session["apertura"] == "personalizada" && (Boolean)Session["Cargada"] == false)
            {
                int colind = (int)Session["colind"];
                int filind = (int)Session["filind"];
                int colind2 = (int)Session["colind2"];
                int filind2 = (int)Session["filind2"];
                int colind3 = (int)Session["colind3"];
                int filind3 = (int)Session["filind3"];
                int colind4 = (int)Session["colind4"];
                int filind4 = (int)Session["filind4"];
                if (Botones[filind,colind].Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    Botones[filind, colind].ImageUrl = temporal.GetIndex();
                    Botones[filind, colind].Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[filind, colind]= 2;
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
                else if (Botones[filind2,colind2].Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    Botones[filind2, colind2].ImageUrl = temporal.GetIndex();
                    Botones[filind2, colind2].Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[filind2, colind2] = 2;
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
                else if (Botones[filind3,colind3].Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    Botones[filind3, colind3].ImageUrl = temporal.GetIndex();
                    Botones[filind3, colind3].Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[filind3, colind3] = 2;
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
                else if (Botones[filind4,colind4].Enabled == true)
                {
                    LblInv1.Visible = false;
                    LblInv2.Visible = false;
                    ImgSad.Visible = false;
                    Lista temporal = (Lista)Session["ColoresRival"];
                    Botones[filind4, colind4].ImageUrl = temporal.GetIndex();
                    Botones[filind4, colind4].Enabled = false;
                    temporal.Next();
                    Session["ColoresRival"] = temporal;
                    Tablero[filind4, colind4] = 2;
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
                                pila.Push(i.ToString() + " " + j.ToString());
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
                    string[] positions = casilla.Split(' ');
                    int i = Int32.Parse(positions[0]);
                    int j = Int32.Parse(positions[1]);

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
            Session["Botones"] = Botones;
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
            if ((string)Session["Reto"] == "Normal")
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
            //GuardarPartida(true);
            string Ruta = @"C:\Users\admin\Documents\GitHub\-IPC2-Proyecto\[IPC2]ProyectoOthello\WebApplication1\PartidasGuardadas\PartidaTemporal.xml";
            string Movimientos = File.ReadAllText(Ruta);
            return Movimientos;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
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