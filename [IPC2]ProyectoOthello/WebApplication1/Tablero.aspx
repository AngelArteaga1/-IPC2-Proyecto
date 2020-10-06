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
        <form id="form1" runat="server">

        <section class="fila">
            <div class="col-4 fila">
                <div class="col-12 header1">
                    <img src="img/pngwing.com.png" class="col-4" id="img1"/>
                    <div id="logo" class="col-8">iGameGT</div>
                </div>
                <div class="col-12 nav1"></div>
                <div class="fila col-12" id="down">
                    <div class="col-12"><h1>Partida</h1></div>
                    <p class="col-12" id="turno">TURNO:</p>
                    <asp:Label ID="LblTurno" runat="server" Text="LABEL" CssClass="col-12 jugador"></asp:Label>
                    <div id="relleno" class="col-12"></div>
                    <asp:Label ID="LblSucc" runat="server" Text="¡La partida ha sido guardada exitosamente!" CssClass="Succ" Visible="False" class="col-12 Succ"></asp:Label>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="GUARDAR PARTIDA" CssClass="botones" OnClick="Button1_Click" class="col-12"/>
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="ABANDONAR PARTIDA" CssClass="botones" OnClick="Button2_Click" class="col-12"/>
                </div>
            </div>
            <div class="col-1"></div>
            <div class="col-6">
                <div id="Tablero">
                    <div class="fila">
                        <asp:ImageButton ID="A1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="A1_Click"/>
                        <asp:ImageButton ID="B1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="B1_Click"/>
                        <asp:ImageButton ID="C1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="C1_Click"/>
                        <asp:ImageButton ID="D1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="D1_Click"/>
                        <asp:ImageButton ID="E1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="E1_Click"/>
                        <asp:ImageButton ID="F1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="F1_Click"/>
                        <asp:ImageButton ID="G1" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="G1_Click"/>
                        <asp:ImageButton ID="H1" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="H1_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="A2_Click"/>
                        <asp:ImageButton ID="B2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="B2_Click"/>
                        <asp:ImageButton ID="C2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="C2_Click"/>
                        <asp:ImageButton ID="D2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="D2_Click"/>
                        <asp:ImageButton ID="E2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="E2_Click"/>
                        <asp:ImageButton ID="F2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="F2_Click"/>
                        <asp:ImageButton ID="G2" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="G2_Click"/>
                        <asp:ImageButton ID="H2" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="H2_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="A3_Click"/>
                        <asp:ImageButton ID="B3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="B3_Click"/>
                        <asp:ImageButton ID="C3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="C3_Click"/>
                        <asp:ImageButton ID="D3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="D3_Click"/>
                        <asp:ImageButton ID="E3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="E3_Click"/>
                        <asp:ImageButton ID="F3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="F3_Click"/>
                        <asp:ImageButton ID="G3" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="G3_Click"/>
                        <asp:ImageButton ID="H3" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="H3_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="A4_Click"/>
                        <asp:ImageButton ID="B4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="B4_Click"/>
                        <asp:ImageButton ID="C4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="C4_Click"/>
                        <asp:ImageButton ID="D4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="D4_Click"/>
                        <asp:ImageButton ID="E4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="E4_Click"/>
                        <asp:ImageButton ID="F4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="F4_Click"/>
                        <asp:ImageButton ID="G4" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="G4_Click"/>
                        <asp:ImageButton ID="H4" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="H4_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="A5_Click"/>
                        <asp:ImageButton ID="B5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="B5_Click"/>
                        <asp:ImageButton ID="C5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="C5_Click"/>
                        <asp:ImageButton ID="D5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="D5_Click"/>
                        <asp:ImageButton ID="E5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="E5_Click"/>
                        <asp:ImageButton ID="F5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="F5_Click"/>
                        <asp:ImageButton ID="G5" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="G5_Click"/>
                        <asp:ImageButton ID="H5" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="H5_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="A6_Click"/>
                        <asp:ImageButton ID="B6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="B6_Click"/>
                        <asp:ImageButton ID="C6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="C6_Click"/>
                        <asp:ImageButton ID="D6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="D6_Click"/>
                        <asp:ImageButton ID="E6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="E6_Click"/>
                        <asp:ImageButton ID="F6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="F6_Click"/>
                        <asp:ImageButton ID="G6" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="G6_Click"/>
                        <asp:ImageButton ID="H6" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="H6_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="A7_Click"/>
                        <asp:ImageButton ID="B7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="B7_Click"/>
                        <asp:ImageButton ID="C7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="C7_Click"/>
                        <asp:ImageButton ID="D7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="D7_Click"/>
                        <asp:ImageButton ID="E7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="E7_Click"/>
                        <asp:ImageButton ID="F7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="F7_Click"/>
                        <asp:ImageButton ID="G7" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="G7_Click"/>
                        <asp:ImageButton ID="H7" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="H7_Click"/>
                    </div>
                    <div class="fila">
                        <asp:ImageButton ID="A8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="A8_Click"/>
                        <asp:ImageButton ID="B8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="B8_Click"/>
                        <asp:ImageButton ID="C8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="C8_Click"/>
                        <asp:ImageButton ID="D8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="D8_Click"/>
                        <asp:ImageButton ID="E8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="E8_Click"/>
                        <asp:ImageButton ID="F8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="F8_Click"/>
                        <asp:ImageButton ID="G8" runat="server" cssClass="btn2 col-1-t" ImageUrl="img/dot.png" OnClick="G8_Click"/>
                        <asp:ImageButton ID="H8" runat="server" cssClass="btn1 col-1-t" ImageUrl="img/dot.png" OnClick="H8_Click"/>
                    </div>
                </div>
            </div>
            <div class="col-1"></div>
        </section>
        </form>
        <footer class="fila">
            <div class="col-12"><div id="m1">&copy; Todos los derechos reservados.</div></div>
        </footer>
    </div>
</body>
</html>
