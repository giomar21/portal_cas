using Models.Lookup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ConexionPosbdat
{
    public class RepositoryPosbdat
    {
        public bool CreateLookUp(Models.DS.ConexionDS conexionDS, LookupModel lookupModel)
        {

#if DEBUG
            conexionDS.IP = "HOST-GIO\\SQLEXPRESS";
            conexionDS.UserID = "sa";
            conexionDS.Password = "1234";
#endif
            SqlConnection con = new SqlConnection($"Data Source={conexionDS.IP};user id={conexionDS.UserID};password={conexionDS.Password};initial catalog=Posbdat;Trusted_Connection=False;");

            try
            {              
                string sqlCommand = 
                    $"IF EXISTS(select * from posbdat..lookup where col_name = '{lookupModel.Col_Name}') " +
                    $"DELETE posbdat..lookup where col_name = '{lookupModel.Col_Name}' and lookup_code = 0 " +
                    $"BEGIN " +
                    $"INSERT INTO posbdat..lookup values ('{lookupModel.Col_Name}',0,Null,{lookupModel.Val_Large}) " +
                    $"END";

                SqlCommand command = new SqlCommand(sqlCommand, con);
                con.Open();

                int result = command.ExecuteNonQuery();

                return result >= 1;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }

           
        }
    }
}
