using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
using GestMoney.Clases;
using GestMoney.Servicios;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para Tests.xaml
    /// </summary>

    public partial class frmTests : Window
    {

        
        //defino result_test como la clave-valor de cada test y result como el diccionario que los engloba
        private KeyValuePair<bool, object> result_test = new KeyValuePair<bool, object>();
        private List<KeyValuePair<bool, object>>  result = new List<KeyValuePair<bool, object>>();

        public frmTests()
        {
            InitializeComponent();
            

            //Inicio los tests
            TestFacturaConsulta();
            
            TextRange rango;
            int cont = 0;

            //Muestro los resultados en el formulario, cmabiando la fuente segun sea error o no
            foreach (KeyValuePair<bool, object> rst in result)
            {
                rango = new TextRange(txtInfo.Document.ContentEnd, txtInfo.Document.ContentEnd);

                rango.Text = "TEST " + cont + " -> Resultado: " + rst.Key + " || Valor: " + rst.Value + "\n";
                if (rst.Key == true)
                {
                    rango.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
                    rango.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    rango.ApplyPropertyValue(TextElement.FontSizeProperty, "15");
                }
                else
                {
                    rango.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                    rango.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    rango.ApplyPropertyValue(TextElement.FontSizeProperty, "15");
                }
                cont += 1;
            }
            
        }

        private void TestFacturaConsulta()
        {
            Factura factura;
            Dictionary<string, object> valores;
            Dictionary<string, object> condiciones;
            ServicioFactura sfactura;
            //Test 0: Limpiar la BD
            factura = new Factura();
            result_test = ServicioFactura.DeleteAll();

            if (result_test.Key == true)
            {
                result.Add(new KeyValuePair<bool, object>(true, "Delete Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<bool, object>(false, "Error al insertar recibos"));
            }

            //Test 1: Insert de facturas correcto
            factura = new Factura();
            valores = new Dictionary<string, object>{ { "tipo", 1 }, { "importe", 53.00 }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = ServicioFactura.Insert(valores);
               
            if (result_test.Key ==true)
            {
                result.Add(new KeyValuePair<bool, object> (true, "Insert Recibo Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<bool, object>(false, "Error al insertar recibos"));
            }

            //Test 2: Insert de facturas erroneo (importe nulo)
            factura = new Factura();
            valores = new Dictionary<string, object> { { "tipo", 1 }, { "importe", null }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = ServicioFactura.Insert(valores);

            if (result_test.Key == false && result_test.Value == "El importe no puede estar vacio")
            {
                result.Add(new KeyValuePair<bool, object>(true, "Error de importe null Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<bool, object>(false, "Error en comprobacion de importe"));
            }

            //Test 3: Insert de facturas erroneo (tipo nulo)
            factura = new Factura();
            valores = new Dictionary<string, object> { { "tipo", null }, { "importe", 19.23 }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = ServicioFactura.Insert(valores);

            if (result_test.Key == false && result_test.Value == "La factura debe tener un tipo valido")
            {
                result.Add(new KeyValuePair<bool, object>(true, "Error de tipo no valido Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<bool, object>(false, "Error en comprobacion de tipo"));
            }

            //Test 4: Consulta de todas las facturas
            factura = new Factura(0);
          
            if (factura.total.Count == 1)
            {
                result.Add(new KeyValuePair<bool, object>(true, "Count = 1"));
            }
            else
            {
               result.Add(new KeyValuePair<bool, object>(false, "Error al consultar todas las facturas de la tabla"));
            }

            //Test 5: Modificar consultas
            factura = new Factura();
            valores = new Dictionary<string, object> { { "Importe", 100 }, { "Concepto", "Modificacion_1" }, { "Fecha_Importe", "24/10/2017" } };
            condiciones = new Dictionary<string, object> { { "Concepto", "Test_1" } };
            result_test = ServicioFactura.Modify(condiciones, valores);

            if (result_test.Key == true)
            {
                result.Add(new KeyValuePair<bool, object>(true, "Modificacion Correcta"));
            }
            else
            {
                result.Add(new KeyValuePair<bool, object>(false, "Error en la modificacion del recibo"));
            }
            //return new KeyValuePair<Boolean, string>(false, "Error al consultar la factura en la Base de Datos para los parametros: id= ");

        }
        
    }
    
}
