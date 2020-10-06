<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModoDeJuego.aspx.cs" Inherits="WebApplication1.ModosDeJuego" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="css/StyleJuegos.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>ModosDeJuego</title>
</head>
<body>
    <div class="contenedor">

        <header class="fila">
            <div class="col-3 fila">
                <img src="img/pngwing.com.png" class="col-4" id="img1"/>
                <div id="logo" class="col-8">iGameGT</div>
            </div>
            <div class="col-9"></div>
        </header>

        <form id="form1" runat="server">

        <nav class="fila">
            <div class="col-12"><div id="a1"></div></div>
        </nav>

        <section class="fila">
            <div class="col-3"></div>
            <div class="col-6">
                <div id="centro" class="fila">
                    <h1 class="col-12">Modos de Juego</h1>
                    <p class="col-12 c1">Seleccione su modo de Juego:</p> 
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
                    <div class="col-12">
                        <div class="invitado">
                            <asp:Label ID="LblInvitado" runat="server" Text="Invitado: " Visible="false"></asp:Label>
                            <asp:TextBox ID="TxtInvitado" runat="server" placeholder="Nombre del Invitado" CssClass="txtbox" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-12">
                        <div>
                            <asp:Button ID="NuevaPartida1" runat="server" Text="NUEVA PARTIDA" class="btn" OnClick="NuevaPartida1_Click1"/>
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
