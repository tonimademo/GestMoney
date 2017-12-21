using GestMoney.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestMoney.Servicios
{
    class ServicioFactura
    {

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
                    if (parametros["importe"] == null)
                    {
                        result = new KeyValuePair<bool, object>(false, "El importe no puede estar vacio");
                    }
                    else if (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"]) == true)
                    {
                        result = new KeyValuePair<bool, object>(false, "La factura debe tener un tipo valido");
                    }
                    else
                    {

                        factura.Tipo = (parametros.ContainsKey("tipo")) ? parametros["tipo"].ToString() : null;
                        factura.Importe = (parametros.ContainsKey("importe")) ? Convert.ToDecimal(parametros["importe"].ToString()) : default(decimal);
                        factura.Fecha_Importe = (parametros.ContainsKey("fecha_importe")) ? Convert.ToDateTime(parametros["fecha_importe"].ToString()) : default(DateTime);
                        factura.Concepto = (parametros.ContainsKey("concepto")) ? parametros["concepto"].ToString() : null;

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

            try
            {

                if (parametros == null || parametros.Count == 0)
                {
                    result = new KeyValuePair<bool, object>(false, "Error: No hay parametros para modificar");
                }
                else if (condiciones == null || condiciones.Count == 0)
                {
                    result = new KeyValuePair<bool, object>(false, "Error: No hay condiciones para modificar");
                }
                else
                {
                    //Compruebo las condiciones
                    foreach (KeyValuePair<string, object> condicion in condiciones)
                    {
                        if (factura.campos.ContainsKey(condicion.Key))
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
                    else if (parametros.ContainsKey("tipo") && (parametros["tipo"] == null || Funciones.ExisteEnTabla("dbo.T_Tipo_Recibo", "nombre = " + parametros["tipo"]) == true))
                    {
                        result = new KeyValuePair<bool, object>(false, "La factura debe tener un tipo valido");
                    }
                    else
                    {
                        
                        factura.Tipo = (parametros.ContainsKey("tipo"))?parametros["tipo"].ToString():null;
                        factura.Concepto = (parametros.ContainsKey("concepto")) ? parametros["concepto"].ToString() : null;
                        factura.Fecha_Importe = (parametros.ContainsKey("fecha_importe")) ? Convert.ToDateTime(parametros["fecha_importe"].ToString()) : default(DateTime);
                        factura.Concepto = (parametros.ContainsKey("concepto")) ? parametros["concepto"].ToString() : null;

                        var modificar = factura.Modify(sql_condicion);

                        if (modificar.Key)
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
    }
}