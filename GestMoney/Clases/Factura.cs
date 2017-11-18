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
        public List<string> campos = new List<string> { "id", "tipo", "concepto", "fecha_importe" };

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

        public KeyValuePair<bool, string> Insert(Dictionary<string, object> parametros)
        {

            var result = new KeyValuePair<bool, string>();
            SqlCommand command;
            if (parametros == null || parametros.Count == 0)
            {
                result = new KeyValuePair<bool, string>(false, "Error: No hay parametros para insertar");
            }
            else
            {

                //Realizo las comprobaciones antes de insertar
                if (parametros["importe"] == null)
                {
                    result = new KeyValuePair<bool, string>(false, "El importe no puede estar vacio");
                }
                else if (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"]) == true)
                {
                    result = new KeyValuePair<bool, string>(false, "La factura debe tener un tipo valido");
                }
                else
                {
                    
                    command = new SqlCommand("insert into dbo.Recibo (tipo, importe, concepto, fecha_importe) values (@tipo, @importe, @concepto, @fecha_importe)");
                    //Preparo las variables por inyteccion
                    using (command)
                    {
                        command.Connection = SQLConecction.conn;

                        command.Parameters.Add("@tipo", SqlDbType.VarChar, 30).Value = _tipo;
                        command.Parameters.Add("@importe", SqlDbType.VarChar, 30).Value = _importe;
                        command.Parameters.Add("@concepto", SqlDbType.VarChar, 30).Value = _concepto;
                        command.Parameters.Add("@fecha_importe", SqlDbType.VarChar, 30).Value = _fecha_importe;

                    }
                    command.ExecuteNonQuery();

                    result = new KeyValuePair<bool, string>(true, "");
                }
            }
            return result;
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

        public KeyValuePair<bool, string> Modify(Dictionary<string, object> condiciones, Dictionary<string, object> parametros)
        {

            var result = new KeyValuePair<bool, string>();
            string sql_condicion = " WHERE 1 = 1 ";
            string sql_update = "UPDATE dbo.Recibo set ";
            bool primer_update = true;

            try
            {

                if (parametros == null || parametros.Count == 0)
                {
                    result = new KeyValuePair<bool, string>(false, "Error: No hay parametros para modificar");
                }
                else if (condiciones == null || condiciones.Count == 0)
                {
                    result = new KeyValuePair<bool, string>(false, "Error: No hay condiciones para modificar");
                }
                else
                {
                    //Compruebo las condiciones
                    foreach (KeyValuePair<string, object> condicion in condiciones)
                    {
                        if (campos.Any(item => item == condicion.Key))
                        {
                            sql_condicion += " AND " + condicion.Key;
                            if (condicion.Value.GetType() == typeof(string))
                            {
                                sql_condicion += " = '" + condicion.Value + "'";
                            }
                            else
                            {
                                sql_condicion +=  " = " + condicion.Value;
                            }
                        
                        }
                        else
                        {
                            return new KeyValuePair<bool, string>(false, "Las condicion " + condicion.Key + " no es valida");
                        }
                    }

                    //Compruebo los parametros
                    //Realizo las comprobaciones antes de modificar
                    if (parametros.ContainsKey("importe") && parametros["importe"] == null)
                    {
                        result = new KeyValuePair<bool, string>(false, "El importe no puede estar vacio");
                    }
                    else if (parametros.ContainsKey("tipo") && (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"]) == true))
                    {
                        result = new KeyValuePair<bool, string>(false, "La factura debe tener un tipo valido");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, object> parametro in parametros)
                        {
                            sql_update += ((primer_update) ? "" : ", ") + parametro.Key;
                            if (parametro.Value.GetType() == typeof(string))
                            {
                                sql_update += " = '" + parametro.Value + "'";
                            }
                            else
                            {
                                sql_update +=  " = " + parametro.Value;
                            }
                            primer_update = false;


                        }
                        //Preparo las variables por inyteccion
                        using (var command = new SqlCommand(sql_update + sql_condicion))
                        {
                            command.Connection = SQLConecction.conn;
                            command.ExecuteNonQuery();
                        }
                        
                        result = new KeyValuePair<bool, string>(true, "");
                    }
                }
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
