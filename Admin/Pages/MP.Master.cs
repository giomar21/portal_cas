using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Pages
{
    public partial class MP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");

            if (Session["usuario_red"] != null)
            {
                divuser.Visible = true;
                //divbuttons.Visible = true;
                menu.Visible = true;
                lblusuario.Text = Session["usuario_red"].ToString();
                //Response.Redirect("Operaciones.aspx");
            }
            else
            {
                navbarBasicExample.Visible = false;
                divuser.Visible = false;
                menu.Visible = true;
                //divbuttons.Visible = false;
                lblusuario.Text = string.Empty;
            }
        }


        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registrarse.aspx");
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Unnamed_Click2(object sender, EventArgs e)
        {
            Session["usuario_red"] = null;
            Session["id_candado"] = null;
            Response.Redirect("Login.aspx");
            HttpContext.Current.Session.Abandon();
        }

        protected void Operaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operaciones.aspx");
        }

        protected void Reportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reportes.aspx");
        }


    }
}