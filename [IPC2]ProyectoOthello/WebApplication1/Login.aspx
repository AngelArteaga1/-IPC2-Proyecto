<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="ColumnasBoostrap.css"/>
    <link rel="stylesheet" href="css/StyleLogin.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Press+Start+2P&display=swap" rel="stylesheet"/>
    <title>Login</title>
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
                <div id="centro">
                    <p>Registro de Usuario</p>
                    <asp:Image ID="Image1" runat="server" Height="227px" Width="218px" ImageUrl="img/pngocean.com.png" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Ingrese su nombre de Usuario" class="label"></asp:Label>
                    <br />
                    <asp:TextBox ID="TxtUsuario" runat="server" Class="txtbox" placeHolder="Nombre Usuario"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Ingrese su Contraseña" class="label"></asp:Label>
                    <br />
                    <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" Class="txtbox" placeHolder="Contraseña"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnSesion" runat="server" Text="Iniciar Sesión" OnClick="Button1_Click" class="btn" />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="No eres miembro?"></asp:Label>
                    <br />
                    <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" OnClick="Button2_Click" class="btn"/>
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
