using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
using GestMoney.Clases;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para Tests.xaml
    /// </summary>

    public partial class frmTests : Window
    {

        
        //defino result_test como la clave-valor de cada test y result como el diccionario que los engloba
        private KeyValuePair<Boolean, string> result_test = new KeyValuePair<Boolean, string>();
        private List<KeyValuePair<Boolean, string>>  result = new List<KeyValuePair<Boolean, string>>();

        public frmTests()
        {
            InitializeComponent();
            

            //Inicio los tests
            TestFacturaConsulta();
            
            TextRange rango;
            int cont = 0;

            //Muestro los resultados en el formulario, cmabiando la fuente segun sea error o no
            foreach (KeyValuePair<Boolean, string> rst in result)
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
            //Test 0: Limpiar la BD
            factura = new Factura();
            result_test = factura.DeleteAll();

            if (result_test.Key == true)
            {
                result.Add(new KeyValuePair<Boolean, string>(true, "Delete Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<Boolean, string>(false, "Error al insertar recibos"));
            }

            //Test 1: Insert de facturas correcto
            factura = new Factura();
            valores = new Dictionary<string, object>{ { "tipo", 1 }, { "importe", 53.00 }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = factura.Insert(valores);
               
            if (result_test.Key ==true)
            {
                result.Add(new KeyValuePair<Boolean, string> (true, "Insert Recibo Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<Boolean, string>(false, "Error al insertar recibos"));
            }

            //Test 2: Insert de facturas erroneo (importe nulo)
            factura = new Factura();
            valores = new Dictionary<string, object> { { "tipo", 1 }, { "importe", null }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = factura.Insert(valores);

            if (result_test.Key == false && result_test.Value == "El importe no puede estar vacio")
            {
                result.Add(new KeyValuePair<Boolean, string>(true, "Error de importe null Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<Boolean, string>(false, "Error en comprobacion de importe"));
            }

            //Test 3: Insert de facturas erroneo (tipo nulo)
            factura = new Factura();
            valores = new Dictionary<string, object> { { "tipo", null }, { "importe", 19.23 }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = factura.Insert(valores);

            if (result_test.Key == false && result_test.Value == "La factura debe tener un tipo valido")
            {
                result.Add(new KeyValuePair<Boolean, string>(true, "Error de tipo no valido Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<Boolean, string>(false, "Error en comprobacion de tipo"));
            }

            //Test 4: Consulta de todas las facturas
            factura = new Factura();
          
            if (factura.total.Count == 1)
            {
                result.Add(new KeyValuePair<Boolean, string>(true, "Count = 1"));
            }
            else
            {
               result.Add(new KeyValuePair<Boolean, string>(false, "Error al consultar todas las facturas de la tabla"));
            }

            //Test 5: Modificar consultas
            factura = new Factura();
            valores = new Dictionary<string, object> { { "importe", 100 }, { "concepto", "Modificacion_1" }, { "fecha_importe", "24/10/2017" } };
            condiciones = new Dictionary<string, object> { { "concepto", "Test_1" } };
            result_test = factura.Modify(condiciones, valores);

            if (result_test.Key == true)
            {
                result.Add(new KeyValuePair<Boolean, string>(true, "Modificacion Correcta"));
            }
            else
            {
                result.Add(new KeyValuePair<Boolean, string>(false, "Error en la modificacion del recibo"));
            }
            //return new KeyValuePair<Boolean, string>(false, "Error al consultar la factura en la Base de Datos para los parametros: id= ");

        }
        
    }
    
}
