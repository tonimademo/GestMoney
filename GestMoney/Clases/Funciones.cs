using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GestMoney.Clases
{
    class Funciones
    {
        public static bool ExisteEnTabla(string tabla, string condicion)
        {
            SqlCommand command;
            bool result;

            try
            {
                command = new SqlCommand("select id from " + tabla + " where " + condicion, SQLConecction.conn);

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
                
            }catch (Exception e){
                return false;
            }
        }
    }
}
