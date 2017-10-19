using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace GestMoney.Clases
{
    class Factura
    {
        public int id;
        public string tipo;
        public string concepto;
        public DateTime fecha_importe;
        public DateTime gc_fecha;
        public List<Dictionary<string, object>> total = new List<Dictionary<string, object>>();

        public Factura(){
        }

        public Factura(SQLConecction conection, int id = 0)
        {

            SqlCommand command;
            if (id == 0)
            {
                command = new SqlCommand("Select * from dbo.Factura", conection.conn);
            }
            else
            {
                command = new SqlCommand("Select * from dbo.Factura where id = " + id, conection.conn);
            }
            
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows && id != 0)
                {
                    id = (int) reader["id"];
                    tipo = (string) reader["id"];
                    concepto = (string) reader["id"];
                    fecha_importe = (DateTime) reader["id"];
                    gc_fecha = (DateTime) reader["id"];
                    
                }else if(reader.HasRows && id == 0)
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> fila = new Dictionary<string, object>();
                        fila.Add("id", reader["id"]);
                        fila.Add("tipo", reader["tipo"]);
                        fila.Add("concepto", reader["concepto"]);
                        fila.Add("fecha_importe", reader["fecha_importe"]);
                        fila.Add("gc_fecha", reader["gc_fecha"]);
                        total.Add(fila);
                    }
                    //reader.NextResult();
                }
            }

        }
    }
}
