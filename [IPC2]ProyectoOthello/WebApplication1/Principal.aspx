<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="WebApplication1.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="css/StylePrincipal.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>Principal</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="contenedor">

        <header class="fila">
            <div class="col-3 fila">
                <img src="img/pngwing.com.png" class="col-4" id="img1"/>
                <div id="logo" class="col-8">iGameGT</div>
            </div>
            <div class="col-8"></div>
            <div class="col-1">
                <asp:Label ID="Label1" runat="server" Text="Usuario" CssClass="usuario"></asp:Label>
            </div>
        </header>

        <nav class="fila">
            <div class="col-12"><div id="a1"></div></div>
        </nav>

        <section class="fila">
            <div class="col-5">
                <div id="izq">
                    <img src="img/Othello.png" id="imagenOthello" align="center"/>
                    <br />
                    <p>El objetivo del juego es tener más fichas del propio color sobre el tablero al final de la partida.</p>
                    <p>
                        Se emplea un tablero de 8 filas por 8 columnas y 64 fichas idénticas, redondas, blancas por una cara y negras por la otra. 
                        Juegan dos jugadores, uno lleva las blancas y el otro las negras. 
                        De inicio se colocan cuatro fichas como en el diagrama, dos fichas blancas en D4 y E5, y dos negras en E4 y D5.
                    </p>
                    <p>
                        Comienzan a mover las negras. Un movimiento consiste en colocar una ficha propia sobre el tablero de forma que 'flanquee' una o varias fichas contrarias. 
                        Las fichas flanqueadas son volteadas para mostrar el color propio.
                    </p>
                    <p>
                        Es obligatorio voltear todas las fichas flanqueadas entre la ficha que se coloca y las que ya estaban colocadas. 
                        Una vez volteadas las fichas el turno pasa al contrario que procede de la misma forma con sus fichas. Si un jugador no tiene ninguna posibilidad de mover, el turno pasa al contrario. 
                        La partida termina cuando ninguno de los dos jugadores puede mover. Normalmente cuando el tablero está lleno o prácticamente lleno. 
                        Gana el jugador que acaba con más fichas propias sobre el tablero. Es posible el empate.
                    </p>
                </div>
            </div>
            <div class="col-7">
                <div id="der">
                    <h1>Othello</h1>
                    <br />
                    <asp:Button ID="BtnJugar" runat="server" Text="JUGAR" cssClass="btn"/>
                    <asp:Button ID="BtnTorneos" runat="server" Text="TORNEO" cssClass="btn" />
                    <br />
                    <p>Disfruta de nuestros maravillosos modos de juego...</p>
                    <br />
                    <div class="fila">
                        <div class="col-4">Contra Ordenador<br />X</div>
                        <div class="col-4">Dos Jugadores<br />X</div>
                        <div class="col-4">Torneos<br />X</div>
                    </div>
                    <br />
                    <h2>Juega y aprende othello en el sitio web número 1</h2>
                    <br />
                    <asp:Button ID="BtnEstadisticas" runat="server" Text="CARGAR PARTIDA" CssClass="btn est"/>
                </div>
            </div>
        </section>

        <footer class="fila">
            <div class="col-12"><div id="m1">&copy; Todos los derechos reservados.</div></div>
        </footer>
    </div>
    </form>
</body>
</html>
