using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        string Patron = "InfoToolsSV";

        protected void ingresar_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (checkAD())
                {
                    SqlCommand cmd = new SqlCommand("sp_login", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@usuario_red", System.Data.SqlDbType.VarChar).Value = usuario.Text;
                    cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = clave.Text;
                    // cmd.Parameters.Add("@Patron", System.Data.SqlDbType.VarChar).Value = Patron;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        Session["id_candado"] = rd[4].ToString();
                        Session["usuario_red"] = rd[1].ToString();
                        Response.Redirect("Index.aspx", false);
                    }
                    con.Close();
                } else
                {
                    errorLabel.Text = "Usuario no registrado <br> Favor de ingresar un usuario válido";
                    errorLabel.Visible = true;
                }
               
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
                errorLabel.Visible = true;
                throw;
            }
        }

        protected bool checkAD()
        {
            bool result = false;
            //  PrincipalContext pc = new PrincipalContext(ContextType.Domain, "Nivel_con");
            //return pc.ValidateCredentials("sopextsic11", "control27");

            //try
            //{
            //    LdapConnection connection = new LdapConnection("ldap.ccontrol.com.mx");
            //    NetworkCredential credential = new NetworkCredential("sopextsic11", "control27");
            //    connection.Credential = credential;
            //    connection.Bind();
            //    Console.WriteLine("logged in");
            //     result = true;
            //}
            //catch (LdapException lexc)
            //{
            //    String error = lexc.ServerErrorMessage;
            //    Console.WriteLine(lexc);
            //     result = false;
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc);
            //     result = false;
            //}
           
            return result =true;
        }


        public bool ActiveDirectoryAuthenticate()
        {
            bool result = false;
            using (DirectoryEntry _entry = new DirectoryEntry())
            {
                _entry.Username = "sopextsic11";
                _entry.Password = "control27";
                DirectorySearcher _searcher = new DirectorySearcher(_entry);
                _searcher.Filter = "(objectclass=user)";
                try
                {
                    SearchResult _sr = _searcher.FindOne();
                    string _name = _sr.Properties["displayname"][0].ToString();
                    result = true;
                }
                catch (Exception e)
                { /* Error handling omitted to keep code short: remember to handle exceptions !*/
                    errorLabel.Text = e.Message;
                    errorLabel.Visible = true;
                }
            }

            return result; //true = user authenticated!
        }

    }
}