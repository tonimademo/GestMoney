﻿using System;
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
        private int id_grupo;
        private int id_clase;
        private DateTime fecha_importe;
        private DateTime gc_fecha_creacion;
        private DateTime gc_fecha_modificacion;
        public List<Dictionary<string, object>> total = new List<Dictionary<string, object>>();
        public List<string> campos = new List<string> { "Id", "Tipo", "Concepto", "Importe", "Fecha_Importe", "Gc_Fecha_Creacion", "Gc_Fecha_Creacion"  };


        public int Id{get{return id;}}
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Grupo { get { return SQLConection.ExtraeDato("T_Grupo", "nombre","id = " + id); } set { tipo = value; } }
        public string Clase { get { return SQLConection.ExtraeDato("T_Clase", "nombre", "id = " + id); } set { tipo = value; } }
        public string Concepto { get { return concepto; } set { concepto = value;} }
        public decimal Importe { get { return importe; } set { importe = value; } }
        public DateTime Fecha_Importe { get { return fecha_importe; } set { fecha_importe = value; } }
        public DateTime Gc_Fecha_Creacion { get { return gc_fecha_creacion; } set { gc_fecha_creacion = value; } }
        public DateTime Gc_Fecha_Modificacion { get { return gc_fecha_creacion; } set { gc_fecha_creacion = value; } }

        public Factura(){
        }

        public Factura(int id)
        {
            SqlConnection connection = new SqlConnection(SQLConection.ConnectionString);
            SqlCommand command;
            if (id == 0)
            {
                command = new SqlCommand("Select * from dbo.Recibo", connection);
            }
            else
            {
                command = new SqlCommand("Select * from dbo.Recibo where id = " + id, connection);
            }

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows && id != 0)
                {
                    //TODO: pasar a foreach generico para asignar atributos
                    id = (int) reader["id"];
                    tipo = (string) reader["tipo"];
                    id_grupo = (int)reader["id_grupo"];
                    id_clase = (int)reader["id_clase"];
                    importe = (Decimal)reader["importe"];
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
                        fila.Add("id_grupo", reader["id_grupo"]);
                        fila.Add("id_clase", reader["id_clase"]);
                        fila.Add("importe", reader["importe"]);
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

        public KeyValuePair<bool, Dictionary<int, List<string>>>  Tipos()
        {
            SqlConnection connection = new SqlConnection(SQLConection.ConnectionString);
            SqlCommand command;
            KeyValuePair<bool, Dictionary<int, List<string>>> result = new KeyValuePair<bool, Dictionary<int, List<string>>>();

            try
            {
                command = new SqlCommand("Select * from dbo.T_Recibo", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Dictionary<int, List<string>> fila = new Dictionary<int, List<string>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<string> fila_datos = new List<string>();
                            //TODO: pasar a foreach generico para asignar atributos
                            fila_datos.Add(reader["nombre"].ToString());
                            fila_datos.Add(reader["descripcion"].ToString());
                            fila.Add(Convert.ToInt32(reader["id"]), fila_datos);
                        }

                        result = new KeyValuePair<bool, Dictionary<int, List<string>>>(true, fila);
                    }
                    else
                    {
                        result = new KeyValuePair<bool, Dictionary<int, List<string>>>(true, fila);
                    }
                    reader.Close();
                }

                return result;
            }
            catch (SqlException e)
            {
                return new KeyValuePair<bool, Dictionary<int, List<string>>>(false, new Dictionary<int, List<string>>() { { 0, new List<string>() { "Error en la llamada SQL, Llame a un Administrador (" + e + ")" } } });
            }

        }

        public KeyValuePair<bool, Dictionary<int, List<string>>> Clases()
        {
            SqlConnection connection = new SqlConnection(SQLConection.ConnectionString);
            SqlCommand command;
            KeyValuePair<bool, Dictionary<int, List<string>>> result = new KeyValuePair<bool, Dictionary<int, List<string>>>();

            try
            {
                command = new SqlCommand("Select * from dbo.T_Clase", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Dictionary<int, List<string>> fila = new Dictionary<int, List<string>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<string> fila_datos = new List<string>();
                            //TODO: pasar a foreach generico para asignar atributos
                            fila_datos.Add(reader["nombre"].ToString());
                            fila_datos.Add(reader["descripcion"].ToString());
                            fila.Add(Convert.ToInt32(reader["id"]), fila_datos);
                        }

                        result = new KeyValuePair<bool, Dictionary<int, List<string>>>(true, fila);
                    }
                    else
                    {
                        result = new KeyValuePair<bool, Dictionary<int, List<string>>>(true, fila);
                    }
                    reader.Close();
                }

                return result;
            }
            catch (SqlException e)
            {
                return new KeyValuePair<bool, Dictionary<int, List<string>>>(false, new Dictionary<int, List<string>>() { { 0, new List<string>() { "Error en la llamada SQL, Llame a un Administrador (" + e + ")" } } });
            }

        }

        public KeyValuePair<bool, Dictionary<int, List<string>>> Grupos()
        {
            SqlConnection connection = new SqlConnection(SQLConection.ConnectionString);
            SqlCommand command;
            KeyValuePair<bool, Dictionary<int, List<string>>> result = new KeyValuePair<bool, Dictionary<int, List<string>>>();

            try
            {
                command = new SqlCommand("Select * from dbo.T_Grupo", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Dictionary<int, List<string>> fila = new Dictionary<int, List<string>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<string> fila_datos = new List<string>();
                            //TODO: pasar a foreach generico para asignar atributos
                            fila_datos.Add(reader["nombre"].ToString());
                            fila_datos.Add(reader["descripcion"].ToString());
                            fila.Add(Convert.ToInt32(reader["id"]), fila_datos);
                        }

                        result = new KeyValuePair<bool, Dictionary<int, List<string>>>(true, fila);
                    }
                    else
                    {
                        result = new KeyValuePair<bool, Dictionary<int, List<string>>>(true, fila);
                    }
                    reader.Close();
                }

                return result;
            }
            catch (SqlException e)
            {
                return new KeyValuePair<bool, Dictionary<int, List<string>>>(false, new Dictionary<int, List<string>>() { { 0, new List<string>() { "Error en la llamada SQL, Llame a un Administrador (" + e + ")" } } });
            }

        }

        public string Insert()
        {
            var command = new SqlCommand("insert into dbo.Recibo (tipo, importe, concepto, fecha_importe) values (@tipo, @importe, @concepto, @fecha_importe)");
            //Preparo las variables por inyteccion
            using (command)
            {
                command.Connection = new SqlConnection(SQLConection.ConnectionString);

                command.Parameters.Add("@tipo", SqlDbType.VarChar, 30).Value = tipo;
                command.Parameters.Add("@importe", SqlDbType.Money).Value = importe;
                command.Parameters.Add("@concepto", SqlDbType.VarChar, 30).Value = concepto;
                command.Parameters.Add("@fecha_importe", SqlDbType.VarChar, 30).Value = fecha_importe;
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

            
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            return command.Parameters["@ID"].Value.ToString();
        }

        public KeyValuePair<bool, string> DeleteAll()
        {

            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>();
            try
            {
                //Realizo las comprobaciones antes de insertar
                //Preparo las variables por inyteccion
                using (SqlCommand command = new SqlCommand("DELETE FROM dbo.Recibo"))
                {
                    command.Connection = new SqlConnection(SQLConection.ConnectionString);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                
                result = new KeyValuePair<bool, string>(true, "");
            
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
                    command.Connection = new SqlConnection(SQLConection.ConnectionString);
                    command.Connection.Open();
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
