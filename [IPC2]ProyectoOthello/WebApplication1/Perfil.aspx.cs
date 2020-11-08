using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ID
            string id = GetidUsuario();
            //Nombre de Usuario
            Label1.Text = (string)Session["Usuario"];
            //Nombre
            LblNombre.Text = GetNombre();
            //Apellido
            LblApellido.Text = GetApellido();
            //Email
            LblEmail.Text = GetEmail();
            //Nacimiento
            LblNacimiento.Text = GetNacimiento();
            //Ganadas
            LblGanadas.Text = GetGanadas(id);
            //Perdidas
            LblPerdidas.Text = GetPerdidas(id);
            //Empatadas
            LblEmparadas.Text = GetEmpatadas(id);
        }

        public string GetidUsuario()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string idUsuario = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT idUsuario As Valor FROM Usuarios WHERE NombreUsuario ='" + Session["Usuario"] + "'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                idUsuario = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return idUsuario;
        }

        public string GetNombre()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Nombre = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT Nombre As Valor FROM Usuarios WHERE NombreUsuario ='" + Session["Usuario"] + "'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Nombre = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Nombre;
        }

        public string GetApellido()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Apellido = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT Apellido As Valor FROM Usuarios WHERE NombreUsuario ='" + Session["Usuario"] + "'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Apellido = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Apellido;
        }

        public string GetEmail()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Email = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT Email As Valor FROM Usuarios WHERE NombreUsuario ='" + Session["Usuario"] + "'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Email = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Email;
        }

        public string GetNacimiento()
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Nacimiento = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT Nacimiento As Valor FROM Usuarios WHERE NombreUsuario ='" + Session["Usuario"] + "'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Nacimiento = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Nacimiento;
        }

        public string GetGanadas(string id)
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Ganadas = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT COUNT(Resultado) As Valor FROM UsuarioPartida WHERE idUsuario =" + id + " and Resultado = 'GANADOR'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Ganadas = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Ganadas;
        }

        public string GetPerdidas(string id)
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Perdidas = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT COUNT(Resultado) As Valor FROM UsuarioPartida WHERE idUsuario =" + id + " and Resultado = 'PERDEDOR'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Perdidas = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Perdidas;
        }

        public string GetEmpatadas(string id)
        {
            string connectionString = @"Data Source=DESKTOP-4LJMEBM;Initial Catalog=iGameOthelloDB;Integrated Security=True";
            string Empatadas = "";
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT COUNT(Resultado) As Valor FROM UsuarioPartida WHERE idUsuario =" + id + " and Resultado = 'EMPATE'";
                SqlCommand cmd = new SqlCommand(query, SqlCon);
                Empatadas = Convert.ToString(cmd.ExecuteScalar());
                SqlCon.Close();
            }
            return Empatadas;
        }

        protected void BtnPerfil_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
    }
}