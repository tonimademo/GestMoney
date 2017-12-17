using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

namespace GestMoney.Clases
{
    class Factura
    {
        public int _id;
        public string _tipo;
        public string _concepto;
        public decimal _importe;
        public DateTime _fecha_importe;
        public DateTime _gc_fecha;
        public List<Dictionary<string, object>> total = new List<Dictionary<string, object>>();
        public List<string> campos = new List<string> { "_id", "_tipo", "_concepto", "_fecha_importe" };

        public Factura(){
        }

        public Factura(SQLConecction conection, int id = 0)
        {

            SqlCommand command;
            if (id == 0)
            {
                command = new SqlCommand("Select * from dbo.Recibo", SQLConecction.conn);
            }
            else
            {
                command = new SqlCommand("Select * from dbo.Recibo where id = " + id, SQLConecction.conn);
            }
            
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows && id != 0)
                {
                    _id = (int) reader["id"];
                    _tipo = (string) reader["id"];
                    _concepto = (string) reader["id"];
                    _fecha_importe = (DateTime) reader["id"];
                    _gc_fecha = (DateTime) reader["id"];
                    
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
                reader.Close();
            }
            
        }

        public string Insert()
        {
    
            var command = new SqlCommand("insert into dbo.Recibo (tipo, importe, concepto, fecha_importe) values (@tipo, @importe, @concepto, @fecha_importe)");
            //Preparo las variables por inyteccion
            using (command)
            {
                command.Connection = SQLConecction.conn;

                command.Parameters.Add("@tipo", SqlDbType.VarChar, 30).Value = _tipo;
                command.Parameters.Add("@importe", SqlDbType.VarChar, 30).Value = _importe;
                command.Parameters.Add("@concepto", SqlDbType.VarChar, 30).Value = _concepto;
                command.Parameters.Add("@fecha_importe", SqlDbType.VarChar, 30).Value = _fecha_importe;
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

            }
            command.ExecuteNonQuery();
            
            return command.Parameters["@ID"].Value.ToString();

        }

        public KeyValuePair<bool, string> DeleteAll()
        {

            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>();
            SqlCommand command;
            
            //Realizo las comprobaciones antes de insertar
            command = new SqlCommand("DELETE FROM dbo.Recibo");
            //Preparo las variables por inyteccion
            using (command)
            {
                command.Connection = SQLConecction.conn;

            }
            command.ExecuteNonQuery();

            result = new KeyValuePair<Boolean, string>(true, "");
            
            return result;
        }

        public KeyValuePair<bool, string> Modify(string sql_condicion)
        {

            var result = new KeyValuePair<bool, string>();
            string sql_update = "UPDATE dbo.Recibo set ";
            bool primer_update = true;

            try
            {
                var propertyInfo = GetType().GetProperty(campos(0));
                if (propertyInfo == null) return;
                propertyInfo.SetValue(obj, value);
                
                foreach (KeyValuePair<string, object> campo in campos)
                {
                    sql_update += ((primer_update) ? "" : ", ") + parametro.Key;
                    if (campo.Value.GetType() == typeof(string) && GetType().GetProperty(propertyName))
                    {
                        sql_update += " = '" + parametro.Value + "'";
                    }
                    else
                    {
                        sql_update +=  " = " + parametro.Value;

                        var propertyInfo = GetType().GetProperty(propertyName);
                        if (propertyInfo == null) return;
                        propertyInfo.SetValue(obj, value);


                    }
                    primer_update = false;
                    
                }
                //Preparo las variables por inyeccion
                using (var command = new SqlCommand(sql_update + sql_condicion))
                {
                    command.Connection = SQLConecction.conn;
                    command.ExecuteNonQuery();
                }
                        
                result = new KeyValuePair<bool, string>(true, "");
               
                return result;
            }
            catch (SqlException e)
            {
                return new KeyValuePair<bool, string>(false, "Error en la llamada SQL, Llame a un Administrador (" + e +")");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Error no controlado, Llame a un Administrador (" + e + ")");
            }
        }
    }
}
