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

        public static string ExtraeDato(string tabla, string campo, string condicion)
        {
            string result = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("select "+ campo +" from " + tabla + " where " + condicion, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        //TODO: pasar a foreach generico para asignar atributos
                        result = reader[campo].ToString();
                    }
                    reader.Close();
                    return result;
                }

            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static int ObtenerDatobyID(string tabla, string condicion)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("select id from " + tabla + " where " + condicion, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        //TODO: pasar a foreach generico para asignar atributos
                        result = (int)reader["id"];
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
