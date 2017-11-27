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
    }
}
