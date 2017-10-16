using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace GestMoney.Funciones
{
    class Tests
    {

        public KeyValuePair<Boolean, string> Iniciar()
        {
            SQLConecction conection = new SQLConecction();
            conection.ConnectToSql("GestMoney_Test");
            SqlCommand command = new SqlCommand("Select id from dbo.Factura", conection.conn);
            

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new KeyValuePair<Boolean, string> (true, String.Format("{0}", reader["id"]) );
                }
                else
                {
                    return new KeyValuePair<Boolean, string>  ( false, "False" );
                }
            }

        }
    }
}
