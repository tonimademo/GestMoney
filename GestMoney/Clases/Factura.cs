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
        private DateTime gc_fecha_creacion;
        private DateTime gc_fecha_modificacion;
        public List<Dictionary<string, object>> total = new List<Dictionary<string, object>>();
        public List<string> campos = new List<string> { "Id", "Tipo", "Concepto", "Importe", "Fecha_Importe", "Gc_Fecha_Creacion", "Gc_Fecha_Creacion"  };


        public int Id{get{return id;}}
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Concepto { get { return concepto; } set { concepto = value;} }
        public decimal Importe { get { return importe; } set { importe = value; } }
        public DateTime Fecha_Importe { get { return fecha_importe; } set { fecha_importe = value; } }
        public DateTime Gc_Fecha_Creacion { get { return gc_fecha_creacion; } set { gc_fecha_creacion = value; } }
        public DateTime Gc_Fecha_Modificacion { get { return gc_fecha_creacion; } set { gc_fecha_creacion = value; } }

        public Factura(){
        }

        public Factura(int id)
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
                    //TODO: pasar a foreach generico para asignar atributos
                    id = (int) reader["id"];
                    tipo = (string) reader["tipo"];
                    concepto = (string) reader["concepto"];
                    fecha_importe = (DateTime) reader["fecha_importe"];
                    gc_fecha_creacion = (DateTime) reader["gc_fecha_creacion"];
                    gc_fecha_modificacion = (DateTime)reader["gc_fecha_modificacion"];

                }
                else if(reader.HasRows && id == 0)
                {
                    while (reader.Read())
                    {
                        //TODO: pasar a foreach generico para asignar atributos
                        Dictionary<string, object> fila = new Dictionary<string, object>();
                        fila.Add("id", reader["id"]);
                        fila.Add("tipo", reader["tipo"]);
                        fila.Add("concepto", reader["concepto"]);
                        fila.Add("fecha_importe", reader["fecha_importe"]);
                        fila.Add("gc_fecha_creacion", reader["gc_fecha_creacion"]);
                        fila.Add("gc_fecha_modificacion", reader["gc_fecha_modificacion"]);
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
            try
            {
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
            catch (SqlException e)
            {
                return new KeyValuePair<bool, string>(false, "Error en la llamada SQL, Llame a un Administrador (" + e + ")");
            }
        }

        public KeyValuePair<bool, string> Modify(string sql_condicion, List<string> campos_cambiar)
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
             
                //Preparo los campos que voy a actualizar en la sentencia de update
                foreach (string campo in campos_cambiar)
                {
                    sql_update += ((primer_update) ? "" : ", ") + campo;
                    myPropInfo = myType.GetProperty(campo);
                    
                    //Si es un string o date, pongo compillas simples
                    if (myPropInfo.PropertyType == typeof(string) || myPropInfo.PropertyType == typeof(DateTime))
                    {
                        sql_update += " = '" + myPropInfo.GetValue(this) + "'";
                    }//Si no, no pongo nada
                    else
                    {
                        sql_update +=  " = " + myPropInfo.GetValue(this);
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
