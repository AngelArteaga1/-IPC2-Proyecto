<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="WebApplication1.Perfil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="css/StylePerfil.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>Perfil</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="contenedor">
            <header class="fila">
            <div class="col-3 fila">
                <img src="img/pngwing.com.png" class="col-4" id="img1"/>
                <div id="logo" class="col-8">iGameGT</div>
            </div>
            <div class="col-6"></div>
            <div class="col-2">
                <div class="usuario">
                    <asp:Label ID="Label1" runat="server" Text="Usuario" CssClass="usuario1"></asp:Label>
                </div>
            </div>
            <div class="col-1">
                <asp:ImageButton ID="BtnPerfil" runat="server" ImageUrl="img/Usuario.png" CssClass="imgUsuario" OnClick="BtnPerfil_Click"/>
            </div>
        </header>

        <nav class="fila">
            <div class="col-12"><div id="a1"></div></div>
        </nav>

            <section>

                <div class="fila">
                    <div class="col-12 extra"></div>
                </div>

                <div class="fila">
                    <div class="col-1"></div>
                    <div class="col-10">
                        <div class="datos fila">
                            <h1 class="col-12 elemento">Datos del Usuario:</h1>
                            <asp:Label ID="Label2" runat="server" Text="Nombres:" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="Apellidos:" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="Nacimiento:" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text="Email:" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="LblNombre" runat="server" Text="Label" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="LblApellido" runat="server" Text="Label" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="LblNacimiento" runat="server" Text="Label" CssClass="col-3 elemento"></asp:Label>
                            <asp:Label ID="LblEmail" runat="server" Text="Label" CssClass="col-3 elemento email"></asp:Label>
                        </div>
                    </div>
                    <div class="col-1"></div>
                </div>
                <div class="fila">
                    <div class="col-12 extra"></div>
                </div>
                <div class="fila">
                    <div class="col-1"></div>
                    <div class="col-10">
                        <div class="estadisticas">
                            <p>Partidas Ganadas:</p>
                            <br />
                            <asp:Label ID="LblGanadas" runat="server" Text="Label" CssClass="label"></asp:Label>
                            <br />
                            <hr />
                            <p>Partidas Perdidas:</p>
                            <br />
                            <asp:Label ID="LblPerdidas" runat="server" Text="Label" CssClass="label"></asp:Label>
                            <br />
                            <hr />
                            <p>Partidas Empatadas:</p>
                            <br />
                            <asp:Label ID="LblEmparadas" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="col-1"></div>
                </div>
                <div class="fila">
                    <div class="col-12 extra"></div>
                </div>
            </section>

            <footer class="fila">
            <div class="col-12"><div id="m1">&copy; Todos los derechos reservados.</div></div>
            </footer>

        </div>
    </form>
</body>
</html>
