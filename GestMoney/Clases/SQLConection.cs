using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace GestMoney.Clases
{
    public class SQLConection
    {
        public static string connection_string = "Data Source=DESKTOP-DCBRBGV;Initial Catalog=GestMoney;Integrated Security=SSPI;";
        public static string ConnectionString { get { return connection_string; } set { connection_string = value; } }

        public static bool ExisteEnTabla(string tabla, string condicion)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("select id from " + tabla + " where " + condicion, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    reader.Close();
                    return result;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObtenerDatobyID(string tabla, string condicion)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("select id from " + tabla + " where " + condicion, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
