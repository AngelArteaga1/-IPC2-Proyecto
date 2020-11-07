<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModosXtream.aspx.cs" Inherits="WebApplication1.ModosXtream" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="css/StyleModoXtream.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>ModosDeJuegoXtream</title>
</head>
<body>
    <div class="contenedor">
        <form id="form1" runat="server">
        <header class="fila">
            <div class="col-3 fila">
                <img src="img/pngwing.com.png" class="col-4" id="img1"/>
                <div id="logo" class="col-8">iGameGT</div>
            </div>
            <div class="col-8"></div>
            <div class="col-1">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="img/return.png" Cssclass="return" OnClick="ImageButton1_Click"/>
            </div>
        </header>

        <nav class="fila">
            <div class="col-12"><div id="a1"></div></div>
        </nav>

        <section class="fila">
            <div class="col-3"></div>
            <div class="col-6">
                <div id="centro" class="fila">
                    <h1 class="col-12">Modos de Juego</h1>
                    <div class="col-6">
                        <p class="c1">Ingrese el número de filas:</p>
                        <br />
                        <asp:DropDownList ID="DpFilas" runat="server" CssClass="txtbox">
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <p class="c1">Ingrese el número de columnas:</p>
                        <br />
                        <asp:DropDownList ID="DpColumnas" runat="server" CssClass="txtbox">
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <p class="col-12 c1">Seleccione su Modo de Juego:</p> 
                    <div = "fila imagenes">
                        <div class="col-6">
                            <img src="img/CPU2.png" class="icono1"/>
                        </div>
                        <div class="col-6">
                            <img src="img/DosJugadores.png" class="icono1"/>
                        </div>
                    </div>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" TextAlign="Left" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="ItemChanged">
                        <asp:ListItem class="col-6" Selected>Partida Individual</asp:ListItem>
                        <asp:ListItem class="col-6">Partida VS un Jugador</asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="col-12 select">
                        <p>¿Quien tiene el primer turno?</p>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="blancas">
                            <asp:ListItem>Aleatorio</asp:ListItem>
                            <asp:ListItem>Usuario</asp:ListItem>
                            <asp:ListItem>Invitado/Máquina</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <p class="col-12 c1">Seleccione los Colores de las Fichas:</p> 
                    <div class="col-12">
                        <div class="invitado">
                            <asp:Label ID="LblInvitado" runat="server" Text="Invitado: " Visible="false"></asp:Label>
                            <asp:TextBox ID="TxtInvitado" runat="server" placeholder="Nombre del Invitado" CssClass="txtbox" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="LblJugadores"><asp:Label ID="LblUsuario" runat="server" Text="Label"></asp:Label></div>
                        <br />
                        <asp:CheckBox ID="Rojo" runat="server" Text="Rojo" AutoPostBack="true" CssClass="rojo" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Amarillo" runat="server" Text="Amarillo" AutoPostBack="true" CssClass="amarillo" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Azul" runat="server" Text="Azul" AutoPostBack="true" CssClass="azul" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Anaranjado" runat="server" Text="Anaranjado" AutoPostBack="true" CssClass="anaranjado" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Verde" runat="server" Text="Verde" AutoPostBack="true" CssClass="verde" OnCheckedChanged="ColorChanged"/>
                        <br />
                        <br />
                        <asp:CheckBox ID="Violeta" runat="server" Text="Violeta" AutoPostBack="true" CssClass="violeta" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Blanco" runat="server" Text="Blanco" AutoPostBack="true" CssClass="blanco" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Negro" runat="server" Text="Negro" AutoPostBack="true" CssClass="negro" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Celeste" runat="server" Text="Celeste" AutoPostBack="true" CssClass="celeste" OnCheckedChanged="ColorChanged"/>
                        <asp:CheckBox ID="Gris" runat="server" Text="Gris" AutoPostBack="true" CssClass="gris" OnCheckedChanged="ColorChanged"/>
                        <br />
                        <asp:Label ID="LabelError1" runat="server" Text="Solo se pueden escoger 5 colores :c" CssClass="error" Visible="false"></asp:Label>
                        <br />
                        <div class="LblJugadores"><asp:Label ID="lblRival" runat="server" Text="Rival:"></asp:Label></div>
                        <br />
                        <asp:CheckBox ID="RojoVS" runat="server" Text="Rojo" AutoPostBack="true" CssClass="rojo" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="AmarilloVS" runat="server" Text="Amarillo" AutoPostBack="true" CssClass="amarillo" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="AzulVS" runat="server" Text="Azul" AutoPostBack="true" CssClass="azul" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="AnaranjadoVS" runat="server" Text="Anaranjado" AutoPostBack="true" CssClass="anaranjado" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="VerdeVS" runat="server" Text="Verde" AutoPostBack="true" CssClass="verde" OnCheckedChanged="ColorChangedVS"/>
                        <br />
                        <br />
                        <asp:CheckBox ID="VioletaVS" runat="server" Text="Violeta" AutoPostBack="true" CssClass="violeta" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="BlancoVS" runat="server" Text="Blanco" AutoPostBack="true" CssClass="blanco" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="NegroVS" runat="server" Text="Negro" AutoPostBack="true" CssClass="negro" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="CelesteVS" runat="server" Text="Celeste" AutoPostBack="true" CssClass="celeste" OnCheckedChanged="ColorChangedVS"/>
                        <asp:CheckBox ID="GrisVS" runat="server" Text="Gris" AutoPostBack="true" CssClass="gris" OnCheckedChanged="ColorChangedVS"/>
                        <br />
                        <asp:Label ID="LabelError2" runat="server" Text="Solo se pueden escoger 5 colores :c" CssClass="error" Visible="false"></asp:Label>
                    </div>
                    <p class="col-12 c1">Seleccione el Reto:</p> 
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" TextAlign="Left" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem class="col-6" Selected>Normal</asp:ListItem>
                        <asp:ListItem class="col-6">Reto Inverso</asp:ListItem>
                    </asp:RadioButtonList>
                    <p class="col-12 c1">Seleccione la apertura:</p> 
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" TextAlign="Left" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem class="col-6">Personalizada</asp:ListItem>
                        <asp:ListItem class="col-6" Selected>Normal</asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="col-12">
                        <div>
                            <asp:Button ID="BtnJugar" runat="server" Text="NUEVA PARTIDA" CssClass="btn" OnClick="NuevaPartida1_Click2"/>
                        </div>
                    </div>
                    <div class="col-12"><hr /></div>
                    <div class="col-12">
                        <p class="c2">Tambien puedes...</p>
                        <asp:FileUpload ID="FileUpload1" runat="server" cssClass="file"/>
                        <br />
                        <asp:Button ID="BtnCargarPartida" runat="server" Text="CARGAR PARTIDA" CssClass="btn" OnClick="BtnCargarPartida_Click"/>
                        <br />
                        <asp:Label ID="LblError" runat="server" Text="No ha seleccionado ningun archivo" CssClass="error" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-3"></div>
        </section>

        </form>

        <footer class="fila">
            <div class="col-12"><div id="m1">&copy; Todos los derechos reservados.</div></div>
        </footer>
    </div>
</body>
</html>
