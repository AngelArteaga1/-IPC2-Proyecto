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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool leido = false;
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Usuario = TxtUsuario.Text;
            string Pass = TxtPass.Text;

            if (Usuario == "")
            {
                LabelError.Text = "Falta ingresar su nombre de usuario";
                LabelError.Visible = true;
            }
            else if (Pass == "")
            {
                LabelError.Text = "Falta ingresar su contraseña";
                LabelError.Visible = true;
            }
            else
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    SqlCommand cmd = new SqlCommand("SELECT NombreUsuario FROM Usuarios WHERE NombreUsuario ='" + Usuario + "' AND Contrasena ='" + Pass + "'", SqlCon);
                    SqlDataReader consulta = cmd.ExecuteReader();
                    if (consulta.Read())
                    {
                        leido = true;
                    }
                    SqlCon.Close();
                    if (leido == true)
                    {
                        Response.Redirect("Principal.aspx");
                    }
                    else
                    {
                        LabelError.Text = "Los datos ingresados son incorrectos";
                        LabelError.Visible = true;
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}