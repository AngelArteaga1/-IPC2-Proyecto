using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            String nombre = TxtNombre.Text;
            String apellido = TxtApellido.Text;
            String usuario = TxtUsuario.Text;
            String pass = TxtPass.Text;
            String passX = TxtPassX.Text;
            String correo = TxtCorreo.Text;
            String pais = Paises.SelectedValue;
            String date = TxtFecha.Text;
            int encontrado = 0;
            
            if (usuario != "")
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    SqlCommand cmd = new SqlCommand("SELECT NombreUsuario FROM Usuarios WHERE NombreUsuario ='"+usuario+"'", SqlCon);
                    SqlDataReader consulta = cmd.ExecuteReader();
                    if (consulta.Read())
                    {
                        encontrado = 1;
                    }
                    SqlCon.Close();
                }
            }

            if (nombre == "")
            {
                LabelError.Text = "Falta ingresar su nombre";
                LabelError.Visible = true;
            }
            else if (apellido == "")
            {
                LabelError.Text = "Falta ingresar su apellido";
                LabelError.Visible = true;
            }
            else if (usuario == "")
            {
                LabelError.Text = "Falta ingresar su nombre de usuario";
                LabelError.Visible = true;
            }
            else if (pass == "")
            {
                LabelError.Text = "Falta ingresar su contraseña";
                LabelError.Visible = true;
            }
            else if (passX == "")
            {
                LabelError.Text = "Falta ingresar su contraseña";
                LabelError.Visible = true;
            }
            else if (correo == "")
            {
                LabelError.Text = "Falta ingresar su correo";
                LabelError.Visible = true;
            }
            else if (pais == "Paises")
            {
                LabelError.Text = "Falta ingresar su pais";
                LabelError.Visible = true;
            }
            else if (date == "")
            {
                LabelError.Text = "Falta ingresar su fecha de nacimiento";
                LabelError.Visible = true;
            }
            else if (pass != passX)
            {
                LabelError.Text = "La contraseñas son incorrectas";
                LabelError.Visible = true;
            }
            else if (encontrado == 1)
            {
                TxtUsuario.Text = "";
                LabelError.Text = "Ese nombre de Usuario ya ha sido registrado";
                LabelError.Visible = true;
                encontrado = 0;
            }
            else
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    SqlCommand cmd = SqlCon.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Usuarios (NombreUsuario, Nombre, Apellido, " +
                        "Contrasena, Pais, Email, Nacimiento) VALUES ('" + usuario + "', '" + nombre + "', '" + apellido +
                        "', '" + pass + "', '" + pais + "', '" + correo + "', '" + date + "')";
                    cmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
                Session["Usuario"] = usuario;
                Response.Redirect("Principal.aspx");
            }
        }
    }
}