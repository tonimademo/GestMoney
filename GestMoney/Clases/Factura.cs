using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Reflection;

namespace GestMoney.Clases
{
    class Factura
    {

        private int id;
        private string tipo;
        private string concepto;
        private decimal importe;
        private DateTime fecha_importe;
        private DateTime gc_fecha;
        public List<Dictionary<string, object>> total = new List<Dictionary<string, object>>();
        public Dictionary<string, string> campos = new Dictionary<string, string> { { "id", "Id" }, { "tipo", "Tipo" }, { "concepto", "Concepto" }, { "importe", "Importe" },
            { "fecha_importe", "Fecha_Importe" }, { "gc_fecha", "Gc_Fecha" } };
   
        public int Id{get{return id;}}
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Concepto { get { return concepto; } set { concepto = value;} }
        public decimal Importe { get { return importe; } set { importe = value; } }
        public DateTime Fecha_Importe { get { return fecha_importe; } set { fecha_importe = value; } }
        public DateTime Gc_Fecha { get { return gc_fecha; } set { gc_fecha = value; } }

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

                command.Parameters.Add("@tipo", SqlDbType.VarChar, 30).Value = tipo;
                command.Parameters.Add("@importe", SqlDbType.VarChar, 30).Value = importe;
                command.Parameters.Add("@concepto", SqlDbType.VarChar, 30).Value = concepto;
                command.Parameters.Add("@fecha_importe", SqlDbType.VarChar, 30).Value = fecha_importe;
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
            PropertyInfo myPropInfo;

            try
            {
                var obj = 0;
                var value = 0;

                Type myType = typeof(Factura);
                // Get the PropertyInfo object by passing the property name.
                //myPropInfo = myType.GetProperty(campos);
                //var propertyInfo = Factura.GetType().GetProperty(campos[0]).GetValue(this, null);
                //if (propertyInfo == null) return  new KeyValuePair<bool, string>(false, "Error en el parametro");
                //propertyInfo.SetValue(obj, value);
                
                foreach (KeyValuePair<string, string> campo in campos)
                {
                    sql_update += ((primer_update) ? "" : ", ") + campo;
                    myPropInfo = myType.GetProperty(campo.Value);
                    myPropInfo.SetValue(obj, value);

                    if (campo.GetType() == typeof(string))
                    {
                        sql_update += " = '" + obj + "'";
                    }
                    else
                    {
                        sql_update +=  " = " + campo;

                       // var propertyInfo = GetType().GetProperty(propertyName);
                       // if (propertyInfo == null) return;
                       // propertyInfo.SetValue(obj, value);


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
