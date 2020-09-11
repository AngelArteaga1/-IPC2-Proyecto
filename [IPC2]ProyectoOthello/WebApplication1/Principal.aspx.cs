﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = (String)Session["Usuario"];
        }

        protected void BtnJugar_Click1(object sender, EventArgs e)
        {
            Boolean Cargada = false;
            Session["Cargada"] = Cargada;
            Session["Partidas"] = (int)Session["Partidas"] + 1;
            Response.Redirect("Tablero.aspx");
        }

        protected void BtnEstadisticas_Click(object sender, EventArgs e)
        {
            Boolean Cargada = true;
            if (FileUpload1.HasFile)
            {
                string Nombre = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (extension == ".xml" || extension ==".Xml" || extension == ".XML")
                {
                    LblError.Visible = false;
                    FileUpload1.SaveAs(Server.MapPath("~/FolderXML/" + FileUpload1.FileName));
                    Session["Archivo"] = Nombre;
                    Session["Cargada"] = Cargada;
                    Session["Partidas"] = (int)Session["Partidas"] + 1;
                    Response.Redirect("Tablero.aspx");
                }
                else
                {
                    LblError.Text = "El archivo no es Xml";
                    LblError.Visible = true;
                }
            }
            else
            {
                LblError.Visible = true;
            }
        }
    }
}