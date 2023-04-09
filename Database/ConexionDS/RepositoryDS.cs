using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;
using Models.DS;

namespace Database.ConexionDS
{  
    public class RepositoryDS
    {
        private readonly SqlConnection con;

        public RepositoryDS(ConnectionStringSettingsCollection ConnectionStrings) 
        {
            con = new SqlConnection(ConnectionStrings["conexionDS"].ToString());
        }

        public Models.DS.ConexionDS ObtenerConexionesDSPorSucursal(int sucursalId)
        {
            Models.DS.ConexionDS conexionDS = null;

            con.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [FacturacionWeb].[dbo].[ConexionDS] WHERE NoTienda=@sucursalId", con);
            command.Parameters.AddWithValue("@sucursalId", sucursalId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    conexionDS = new Models.DS.ConexionDS()
                    {
                        NoTienda = (int)reader["NoTienda"],
                        Nombre = reader["Nombre"] != null ? reader["Nombre"].ToString().Trim() : null,
                        IP = (string)reader["IP"] != null ? reader["IP"].ToString().Trim() : null,
                        UserID = (string)reader["UserID"] != null ? reader["UserID"].ToString().Trim() : null,
                        Password = (string)reader["Password"] != null ? reader["Password"].ToString().Trim() : null,
                        Actualizacion = (int)reader["Actualizacion"],
                        Zona = (int)reader["Zona"],
                        SQL2008 = (int)reader["SQL2008"]
                    };    
                }
            }

            con.Close();

            return conexionDS;
        }
    }
}
