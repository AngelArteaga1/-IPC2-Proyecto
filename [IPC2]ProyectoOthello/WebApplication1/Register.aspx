<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="css/StyleRegister.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>Register</title>
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
            <div class="col-2"></div>
            <div class="col-8">
                <div id="centro">
                    <p id="encabezado">Nuevo Usuario</p>
                    <br />
                    <h1>¡Registrate!</h1>
                    <br />
                    <p id="relleno">Es gratis... como siempre debería de serlo.</p>
                    <br />
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="txtnombres" placeHolder="Nombres"></asp:TextBox>
                    <asp:TextBox ID="TxtApellido" runat="server" CssClass="txtnombres" placeHolder="Apellidos"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TxtUsuario" runat="server" CssClass="txtbox" placeHolder="Nombre de Usuario"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TxtPass" runat="server" CssClass="txtbox" TextMode="Password" placeHolder="Contraseña"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TxtPassX" runat="server" CssClass="txtbox" TextMode="Password" placeHolder="Confirmar Contraseña"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TxtCorreo" runat="server" CssClass="txtbox" TextMode="Email" placeHolder="Correo Electrónico"></asp:TextBox>
                    <br />
                    <asp:DropDownList ID="Paises" runat="server" CssClass="pais" placeHolder="Paises">
                        <asp:ListItem>Paises</asp:ListItem>
                        <asp:ListItem>ALA</asp:ListItem>
                        <asp:ListItem>ALB</asp:ListItem>
                        <asp:ListItem>BLR</asp:ListItem>
                        <asp:ListItem>BEL</asp:ListItem>
                        <asp:ListItem>BEN</asp:ListItem>
                        <asp:ListItem>CAN</asp:ListItem>
                        <asp:ListItem>CPV</asp:ListItem>
                        <asp:ListItem>CAF</asp:ListItem>
                        <asp:ListItem>DNK</asp:ListItem>
                        <asp:ListItem>DMA</asp:ListItem>
                        <asp:ListItem>DOM</asp:ListItem>
                        <asp:ListItem>ECU</asp:ListItem>
                        <asp:ListItem>GTM</asp:ListItem>
                        <asp:ListItem>GUM</asp:ListItem>
                        <asp:ListItem>GNB</asp:ListItem>
                        <asp:ListItem>IND</asp:ListItem>
                        <asp:ListItem>IRN</asp:ListItem>
                        <asp:ListItem>IDN</asp:ListItem>
                        <asp:ListItem>JOR</asp:ListItem>
                        <asp:ListItem>MAC</asp:ListItem>
                        <asp:ListItem>MUS</asp:ListItem>
                        <asp:ListItem>NAM</asp:ListItem>
                        <asp:ListItem>NER</asp:ListItem>
                        <asp:ListItem>POL</asp:ListItem>
                        <asp:ListItem>RUS</asp:ListItem>
                        <asp:ListItem>SAU</asp:ListItem>
                        <asp:ListItem>SVN</asp:ListItem>
                        <asp:ListItem>TUR</asp:ListItem>
                        <asp:ListItem>TTO</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>VEN</asp:ListItem>
                        <asp:ListItem>YEM</asp:ListItem>
                        <asp:ListItem>ZMB</asp:ListItem>
                        <asp:ListItem>ZWE</asp:ListItem>
                        <asp:ListItem>AFG</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtFecha" runat="server" TextMode="Date" CssClass="pais"></asp:TextBox>
                    <br />
                    <asp:Label ID="LabelError" runat="server" Text="Label" Visible="False" CssClass="labelE"></asp:Label>
                    <br />
                    <asp:Button ID="BtnRegistrarse" runat="server" Text="Registrarse" class="btn" OnClick="BtnRegistrarse_Click"/>
                </div>
            </div>
            <div class="col-2"></div>
        </section>

        </form>

        <footer class="fila">
            <div class="col-12"><div id="m1">&copy; Todos los derechos reservados.</div></div>
        </footer>
    </div>
</body>
</html>
