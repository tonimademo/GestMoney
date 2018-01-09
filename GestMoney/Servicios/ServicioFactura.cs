using GestMoney.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestMoney.Servicios
{
    class ServicioFactura
    {
        static private string vRecibos = "SELECT * FROM dbo.vRecibo";
        static private SqlDataAdapter dataAdapter;
        static private SqlCommandBuilder commandBuilder;

        static public KeyValuePair<bool, object> Insert(Dictionary<string, object> parametros)
        {
            Factura factura = new Factura();
            KeyValuePair<bool, object> result;

            try
            {
                if (parametros == null || parametros.Count == 0)
                {
                    result = new KeyValuePair<bool, object>(false, "Error: No hay parametros para insertar");
                }
                else
                {

                    //Realizo las comprobaciones antes de insertar
                    //Si el importe es nulo, al ser un campo obligatorio, error
                    if (parametros["importe"] == null)
                    {
                        result = new KeyValuePair<bool, object>(false, "El importe no puede estar vacio");
                    }//Si el tipo es nulo o no es uno valido, al ser un campo obligatorio, error
                    else if (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"]))
                    {
                        result = new KeyValuePair<bool, object>(false, "La factura debe tener un tipo valido");
                    }
                    else
                    {
                        //Asigno el valor de los parametros el objeto factura
                        factura.Tipo = (parametros.ContainsKey("tipo")) ? parametros["tipo"].ToString() : null;
                        factura.Importe = (parametros.ContainsKey("importe")) ? Convert.ToDecimal(parametros["importe"].ToString()) : default(decimal);
                        factura.Fecha_Importe = (parametros.ContainsKey("fecha_importe")) ? Convert.ToDateTime(parametros["fecha_importe"].ToString()) : default(DateTime);
                        factura.Concepto = (parametros.ContainsKey("concepto")) ? parametros["concepto"].ToString() : null;

                        //Llamo la clase factura para que lo inserte en la BD
                        var id_result = factura.Insert();

                        if (id_result == "0")
                        {
                            result = new KeyValuePair<bool, object>(false, factura);
                        }
                        else
                        {
                            result = new KeyValuePair<bool, object>(true, factura);
                        }

                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, object>(false, "Error no controlado, Llame a un Administrador (" + e + ")");
            }
        }

        static public KeyValuePair<bool, object> Modify(Dictionary<string, object> condiciones, Dictionary<string, object> parametros)
        {

            Factura factura = new Factura();
            var result = new KeyValuePair<bool, object>();
            string sql_condicion = " WHERE 1 = 1 ";
            bool primer_update = true;
            List<string> campos_cambiar = new List<string>();
            PropertyInfo myPropInfo;
            Type myType = typeof(Factura);
            object propertyVal;

            try
            {
                //Si los parametro que paso para actualizar es nulo o no no hay ninguno, devuelvo error porque que voy a actualziar, pues nada
                if (parametros == null || parametros.Count == 0)
                {
                    result = new KeyValuePair<bool, object>(false, "Error: No hay parametros para modificar");
                }
                //Si no hay ninguna condicion pues devuelvo error por que el usuario no va a actualizar todo
                else if (condiciones == null || condiciones.Count == 0)
                {
                    result = new KeyValuePair<bool, object>(false, "Error: No hay condiciones para modificar");
                }
                else
                {
                    //Compruebo las condiciones
                    foreach (KeyValuePair<string, object> condicion in condiciones)
                    {
                        if (factura.campos.Contains(condicion.Key))
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
                            return new KeyValuePair<bool, object>(false, "Las condicion " + condicion.Key + " no es valida");
                        }
                    }

                    //Compruebo los parametros
                    //Realizo las comprobaciones antes de modificar
                    if (parametros.ContainsKey("importe") && parametros["importe"] == null)
                    {
                        result = new KeyValuePair<bool, object>(false, "El importe no puede estar vacio");
                    }
                    else if (parametros.ContainsKey("tipo") && (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"])))
                    {
                        result = new KeyValuePair<bool, object>(false, "La factura debe tener un tipo valido");
                    }
                    else
                    {

                        foreach (KeyValuePair<string, object> parametro in parametros)
                        {
                            if (factura.campos.Contains(parametro.Key))
                            {
                                // Obtengo el atributo de la clase
                                myPropInfo = myType.GetProperty(parametro.Key);
                                //Cambio el valor que paso como parametro al tipo del atributo (asi puedo aceptar un string para un decimal por ejemplo)
                                propertyVal = Convert.ChangeType(parametro.Value, myPropInfo.PropertyType);
                                //Actualizo el valor del atributo con el valor del parametro con su tipo
                                myPropInfo.SetValue(factura, propertyVal, null);
                                //Añado el nombre del atributo cambiado a la lista de atributos a actualizar
                                campos_cambiar.Add(parametro.Key);
                            }
                        }
                        //Modifico desde la clase
                        var modificar = factura.Modify(sql_condicion, campos_cambiar);
                        //Compruebo el resultado
                        if (modificar.Key)
                        {
                            result = new KeyValuePair<bool, object>(true, factura);
                        }
                        else
                        {
                            result = new KeyValuePair<bool, object>(false, factura);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, object>(false, "Error no controlado, Llame a un Administrador (" + e + ")");
            }
        }


        static public KeyValuePair<bool, object> DeleteAll()
        {
            try
            {
                var result = new KeyValuePair<bool, string>();

                if (Funciones.ExisteEnTabla("dbo.T_Users", "nivel = 1 and usuario = '" + Environment.UserName + "'"))
                {
                    Factura factura = new Factura();
                    result = factura.DeleteAll();
                    if (result.Key) {
                        return new KeyValuePair<bool, object>(true, "");
                    }
                    else {
                        return new KeyValuePair<bool, object>(false, result.Value);
                    }                  
                }
                else
                {
                    return new KeyValuePair<bool, object>(false, "Error, El usuario no tiene permisos de administrador");
                }
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, object>(false, "Error no controlado, Llame a un Administrador (" + e + ")");
            }
        }

        static public KeyValuePair<bool, Dictionary<string, object>> Tipos()
        {
            try
            {
                var result = new KeyValuePair<bool, Dictionary<string, object>>();
                
                Factura factura = new Factura();
                result = factura.Tipos();
                if (result.Key)
                {
                    return new KeyValuePair<bool, object>(true, "");
                }
                else
                {
                    return new KeyValuePair<bool, object>(false, result.Value);
                }
               
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, object>(false, "Error no controlado, Llame a un Administrador (" + e + ")");
            }
        }

        static public KeyValuePair<bool, object> vRecibosSelect(ref DataTable datafill, int id = 0)
        {
            try
            {
                dataAdapter = (id == 0)? new SqlDataAdapter(vRecibos, SQLConecction.conn): new SqlDataAdapter(vRecibos + " where id = " + id, SQLConecction.conn);
                                
                dataAdapter.Fill(datafill);
                
                return new KeyValuePair<bool, object>(true, "");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, object>(false, "Error no controlado, Llame a un Administrador (" + e + ")");
            }
        }

    }
}