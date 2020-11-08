<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xtream.aspx.cs" Inherits="WebApplication1.Xtream" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="TableroBoostrap.css"/>
    <link rel="stylesheet" href="css/StyleTablero.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>PartidaXtream</title>
</head>
<body>
    <div>
        <form id="form1" runat="server">

        <section>
            <div >
                <div class="header1">
                    <img src="img/pngwing.com.png" id="img1"/>
                    <div id="logo">iGameGT</div>
                </div>
                <div class="nav1"></div>
                <div id="down">
                    <div ><h1>Partida</h1></div>
                    <br />
                    <p id="turno">TURNO:</p>
                    <asp:Label ID="LblTurno" runat="server" Text="JUGADOR1" CssClass="jugador"></asp:Label>
                    <p class="col-12" id="espera">ESPERA:</p>
                    <br />
                    <asp:Label ID="LblEspera" runat="server" Text="JUGADOR2" CssClass="jugador2"></asp:Label>
                    <br />
                    <div id="relleno">
                        <!--https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.timer.ontick?view=netframework-4.8->
                        <!--https://sites.google.com/site/lagaterainformatica/home/-net/-net-c-/-generico/-control-time-net-que-se-ejecute-una-funcion-cada-x-tiempo-->
                        <!--RELOJES-->
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlID="Reloj1" />
                                <asp:AsyncPostBackTrigger ControlID="Reloj2" />
                            </Triggers>
                            <ContentTemplate>
                                <br />
                                <asp:Label ID="LblMov1" runat="server" Text="MOVIMIENTOS DE JUGADOR1: 0" class="movimientos"></asp:Label>
                                <br />
                                <div class="Reloj">
                                    <asp:Label ID="LblPrimero" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="LblReloj1" runat="server" Text="00:00"></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="LblMov2" runat="server" Text="MOVIMIENTOS DE JUGADOR2: 0" class="movimientos"></asp:Label>
                                <br />
                                <div class="Reloj">
                                    <asp:Label ID="LblSegundo" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="LblRejoj2" runat="server" Text="00:00"></asp:Label>
                                </div> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Timer ID="Reloj1" runat="server" Enabled="False" Interval="1000" OnTick="Cronometro1_Tick"></asp:Timer>
                        <asp:Timer ID="Reloj2" runat="server" Enabled="False" Interval="1000" OnTick="Cronometro2_Tick"></asp:Timer>
                        <br />
                        <!--RELOJES-->
                        <asp:Image ID="ImgSad" runat="server" ImageUrl="img/sad.png" cssClass="sadimg" Visible="False"/>
                        <br />
                        <asp:Label ID="LblInv1" runat="server" Text="!TIRO INVALIDO!" CssClass="tiroinvalido1" Visible="False"></asp:Label>
                        <br />
                        <asp:Label ID="LblInv2" runat="server" Text="PORFAVOR REPITA SU JUGADA" CssClass="tiroinvalido" Visible="False"></asp:Label>
                    </div>
                    <asp:Label ID="LblSucc" runat="server" Text="¡La partida ha sido guardada exitosamente!" CssClass="Succ" Visible="False" class="Succ"></asp:Label>
                    <asp:Button ID="Button2" runat="server" Text="ABANDONAR PARTIDA" CssClass="botones" OnClick="Button2_Click"/>
                </div>
            </div>
            <div></div>
                <div id="Tablero">
                    <asp:Panel ID="PnlTablero" runat="server"></asp:Panel>
                </div>
            <div></div>
        </section>
        </form>
        <footer class="fila">
            <div class="col-12"><div id="m1">&copy; Todos los derechos reservados.</div></div>
        </footer>
    </div>
</body>
</html>
