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

        private String baseDatos = "GestMoney_Test";
        //defino result_test como la clave-valor de cada test y result como el diccionario que los engloba
        private KeyValuePair<Boolean, string> result_test = new KeyValuePair<Boolean, string>();
        private List<KeyValuePair<Boolean, string>>  result = new List<KeyValuePair<Boolean, string>>();

        public frmTests()
        {
            InitializeComponent();
            SQLConecction conection = new SQLConecction();
            conection.ConnectToSql(baseDatos);

            //Inicio los tests
            TestFacturaConsulta(conection);
            
            TextRange rango;
            int cont = 1;

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

        private void TestFacturaConsulta(SQLConecction conection)
        {

            //Test 1: Insert de facturas
            Factura factura = new Factura();
            Dictionary<string, object> valores_insert = new Dictionary<string, object>{ { "tipo", 1 }, { "importe", 53.00 }, { "concepto", "Test_1" }, { "fecha_importe", "20/10/2017" } };
            result_test = factura.Insert(valores_insert, conection);
               
            if (result_test.Key ==true)
            {
                result.Add(new KeyValuePair<Boolean, string> (true, "Insert Recibo Correcto"));
            }
            else
            {
                result.Add(new KeyValuePair<Boolean, string>(false, "Error al insertar recibos"));
            }
            
            //Test 2: consulta de todas las facturas
            factura = new Factura(conection);
          
            if (factura.total.Count == 2)
            {
                result.Add(new KeyValuePair<Boolean, string>(true, "Count = 2"));
            }
            else
            {
               result.Add(new KeyValuePair<Boolean, string>(false, "Error al consultar todas las facturas de la tabla"));
            }

            //return new KeyValuePair<Boolean, string>(false, "Error al consultar la factura en la Base de Datos para los parametros: id= ");

        }
        
    }
    
}
