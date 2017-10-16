using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace GestMoney.Funciones
{
    class SQLConecction
    {
        public SqlConnection conn = new SqlConnection();

        public void ConnectToSql(string database)
        {
            conn.ConnectionString = "Data Source=TONIMA-PC\\SQLEXPRESS;Initial Catalog=" + database + ";Integrated Security=SSPI;";
            try
            {
                conn.Open();
                // Insert code to process data.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos" + database);
                conn.Close();
            }
        }
    }
}
