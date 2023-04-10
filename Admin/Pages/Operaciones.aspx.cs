using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database.ConexionDS;
using Models.DS;
using Database.ConexionPosbdat;
using Models.Constants;
using Models.Lookup;

namespace Admin.Pages
{
    public partial class Operaciones : System.Web.UI.Page
    {
        int id_candado = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.AppendHeader("Cache-Control", "no-store");

            //if (!IsPostBack && Session["usuario_red"] != null)
            if (!IsPostBack && Session["usuario_red"] != null)
            {
                id_candado = Convert.ToInt32(Session["id_candado"].ToString());
                llenaCandado();
                
                //Datos();
                //Permisos(id_candado);
            }

        }

        void Limpiar()
        {
            solicitante.Text = string.Empty;
            sucursal.Text = string.Empty;
            //candado = string.Empty;
            motivo.Text = string.Empty;
            monto.Text = string.Empty;
            EAN.Text = string.Empty;
        }

        protected void Apertura_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_agregarDescuento", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Solicitante", System.Data.SqlDbType.VarChar).Value = solicitante.Text;
                cmd.Parameters.Add("@Sucursal", System.Data.SqlDbType.VarChar).Value = sucursal.Text;
                cmd.Parameters.Add("@Candado", System.Data.SqlDbType.Int).Value = ddlCandado.Items[ddlCandado.SelectedIndex].Value; ;
                cmd.Parameters.Add("@Motivo", System.Data.SqlDbType.VarChar).Value = motivo.Text;
                cmd.Parameters.Add("@EAN", System.Data.SqlDbType.VarChar).Value = EAN.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Limpiar();
                Response.Redirect("Descuentos.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            try
            {
                RepositoryDS repositoryDS = new RepositoryDS(ConfigurationManager.ConnectionStrings);
                ConexionDS conexionDS = repositoryDS.ObtenerConexionesDSPorSucursal(Convert.ToInt32(sucursal.Text));

                RegistrarHistorial();

                RepositoryPosbdat repositoryPosbdat = new RepositoryPosbdat();
                repositoryPosbdat.CreateLookUp(conexionDS, ObtenerNombreLookup());

                Limpiar();
                Response.Redirect("CambioPrecio.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void llenaCandado()
        {

            DataTable subjects = new DataTable();

            using (con)
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * from candado", con);
                    adapter.Fill(subjects);

                    ddlCandado.DataSource = subjects;
                    ddlCandado.DataTextField = "descripcion";
                    ddlCandado.DataValueField = "id";
                    ddlCandado.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlCandado.Items.Insert(0, new ListItem("<Seleccione Candado>", "0"));

        }

        protected void ocultaCampos() 
        {
            divMotivo.Visible = false;
            divDescuento.Visible = false;
            divMonto.Visible = false;   
            divEAN .Visible=false;
            divTarjeta.Visible = false;
        }

        protected void ddlCandado_SelectedIndexChanged(object sender, EventArgs e)
        {

            ocultaCampos();

            //PK Cambio de precio
            //Motivo Monto EAN
            if (ddlCandado.SelectedValue == "1")
                {
               
                divMotivo.Visible = true;
                divMonto.Visible = true;
                divEAN.Visible = true;
                
            }
            //DT Descuento
            else if (ddlCandado.SelectedValue == "2")
            {
                divMotivo.Visible = true;
                divDescuento.Visible = true;
                divEAN.Visible = true;
            }
            //3 Tarjeta regalo
            else if (ddlCandado.SelectedValue == "3")
            {
                divMotivo.Visible = true;
                divMonto.Visible = true;
                divTarjeta.Visible=true;
            }
        }

        private void RegistrarHistorial()
        {
            SqlCommand cmd = new SqlCommand("sp_registra_historial", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@fecha_inicio", System.Data.SqlDbType.DateTime).Value = DateTime.Now.AddMinutes(-15);
            cmd.Parameters.Add("@nombre_solicitante", System.Data.SqlDbType.VarChar).Value = solicitante.Text;
            cmd.Parameters.Add("@sucursal", System.Data.SqlDbType.Int).Value = sucursal.Text;
            cmd.Parameters.Add("@id_candado", System.Data.SqlDbType.Int).Value = ddlCandado.Items[ddlCandado.SelectedIndex].Value;
            cmd.Parameters.Add("@fecha_fin", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@motivo", System.Data.SqlDbType.VarChar).Value = motivo.Text;
            cmd.Parameters.Add("@nombre_cas", System.Data.SqlDbType.VarChar).Value = EAN.Text;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public LookupModel ObtenerNombreLookup()
        {
            //PK Cambio de precio
            if (ddlCandado.SelectedValue == "1")
            {
                return new LookupModel()
                {
                    Col_Name = LookupsKey.PorcentajeMaxPK,
                    Val_Large = monto.Text
                };
            }
            //DT Descuento
            else if (ddlCandado.SelectedValue == "2")
            {
                return new LookupModel()
                {
                    Col_Name = LookupsKey.PorcentajeMaxDT,
                    Val_Large = descuento.Text
                };
            }
            //3 Tarjeta regalo
            else if (ddlCandado.SelectedValue == "3")
            {
                return new LookupModel()
                {
                    Col_Name = LookupsKey.Re2013021_Saldo,
                    Val_Large = monto.Text
                };
            }

            return null;
        }
    }
}