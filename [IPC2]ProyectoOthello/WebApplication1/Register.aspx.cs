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

        //MySqlConnection conectar = new MySqlConnection("Database=iGameOthelloDB;Data Source=localhost;User id=admin;Password=");
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            String nombre = TxtNombre.Text;
            String apellido = TxtApellido.Text;
            String usuario = TxtUsuario.Text;
            String pass = TxtPass.Text;
            String passX = TxtPassX.Text;
            String correo = TxtCorreo.Text;
            String pais = Paises.SelectedValue;
            String date = TxtFecha.Text;
            SqlConnection conectar = new SqlConnection("Server=localhost; Database=iGameOthelloDB; Trusted_Connection = true;");
            SqlCommand cmd = new SqlCommand("select * from Usuarios");
        }
    }
}