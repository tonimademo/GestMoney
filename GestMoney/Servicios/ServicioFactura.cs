using GestMoney.Clases;
using System;
using System.Collections.Generic;
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

                    factura._tipo = parametros["tipo"].ToString();
                    factura._importe = (Decimal)parametros["importe"];
                    factura._fecha_importe = (DateTime)(parametros["fecha"]);
                    factura._concepto = parametros["observaciones"].ToString();

                    var id_result = factura.Insert();

                    if (id_result == "0")
                    {
                        result = new KeyValuePair<bool, object>(false, id_result);
                    }
                    else
                    {
                        result = new KeyValuePair<bool, object>(true, id_result);
                    }
                    
                }
            }
            return result;
        }

        public KeyValuePair<bool, string> Modify(Dictionary<string, object> condiciones, Dictionary<string, object> parametros)
        {

            Factura factura = new Factura();
            var result = new KeyValuePair<bool, string>();
            string sql_condicion = " WHERE 1 = 1 ";
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
                        if (factura.campos.Any(item => item == condicion.Key) == False)
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
                        
                        
                        (parametros.ContainsKey("tipo"))?factura._tipo = parametros["tipo"].ToString():null;
                        (parametros.ContainsKey("concepto"))?factura._concepto = parametros["concepto"].ToString():null;
                        (parametros.ContainsKey("importe"))?factura._importe = (Decimal)parametros["importe"]:null;
                        (parametros.ContainsKey("fecha"))?factura._fecha_importe = (DateTime)(parametros["fecha"]):null;

                        var id_result = factura.Modify(sql_condicion);

                        if (id_result == "0")
                        {
                            result = new KeyValuePair<bool, object>(false, id_result);
                        }
                        else
                        {
                            result = new KeyValuePair<bool, object>(true, id_result);
                        }
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