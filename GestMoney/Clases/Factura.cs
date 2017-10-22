﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;

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
                command = new SqlCommand("Select * from dbo.Recibo", conection.conn);
            }
            else
            {
                command = new SqlCommand("Select * from dbo.Recibo where id = " + id, conection.conn);
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
            reader.Close();
        }

        public KeyValuePair<Boolean, string> Insert(Dictionary<string, object> parametros, SQLConecction conection)
        {

            KeyValuePair<Boolean, string> result = new KeyValuePair<Boolean, string>();
            SqlCommand command;
            if (parametros == null || parametros.Count == 0)
            {
                result = new KeyValuePair<Boolean, string>(false, "Error: No hay parametros para insertar");
            }
            else
            {

                //Realizo las comprobaciones antes de insertar
                if (parametros["importe"] == null)
                {
                    result = new KeyValuePair<Boolean, string>(false, "El importe no puede estar vacio");
                }
                else if (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"], conection) == true)
                {
                    result = new KeyValuePair<Boolean, string>(false, "La factura debe tener un tipo valido");
                }
                else
                {
                    
                    command = new SqlCommand("insert into dbo.Recibo (tipo, importe, concepto, fecha_importe) values (@tipo, @importe, @concepto, @fecha_importe)");
                    //Preparo las variables por inyteccion
                    using (command)
                    {
                        command.Connection = conection.conn;

                        command.Parameters.Add("@tipo", SqlDbType.VarChar, 30).Value = parametros["tipo"];
                        command.Parameters.Add("@importe", SqlDbType.VarChar, 30).Value = parametros["importe"];
                        command.Parameters.Add("@concepto", SqlDbType.VarChar, 30).Value = parametros["concepto"];
                        command.Parameters.Add("@fecha_importe", SqlDbType.VarChar, 30).Value = parametros["fecha_importe"];

                    }
                    command.ExecuteNonQuery();

                    result = new KeyValuePair<Boolean, string>(true, "");
                }
            }
            return result;
        }
    }
}
