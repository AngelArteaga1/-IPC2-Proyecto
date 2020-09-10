<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tablero.aspx.cs" Inherits="WebApplication1.Tablero" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="TableroBoostrap.css"/>
    <link rel="stylesheet" href="css/StyleTablero.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>partida</title>
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
                    <div id="Tablero">
                        <div class="fila">
                            <asp:ImageButton ID="A1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                        </div>
                        <div class="fila">
                            <asp:ImageButton ID="A8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="B8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="C8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="D8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="E8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="F8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="G8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png"/>
                            <asp:ImageButton ID="H8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png"/>
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
