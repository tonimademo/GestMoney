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
        KeyValuePair<Boolean, string> result_test = new KeyValuePair<Boolean, string>();
        Dictionary<Boolean, string> result = new Dictionary<Boolean, string>();

        public frmTests()
        {
            InitializeComponent();
            SQLConecction conection = new SQLConecction();
            conection.ConnectToSql(baseDatos);

            //Inicio los tests
            TestFacturaConsulta(conection);
            
            TextRange rango = new TextRange(txtInfo.Document.ContentEnd, txtInfo.Document.ContentEnd);
            
            //Muestro los resultados en el formulario, cmabiando la fuente segun sea error o no
            foreach (KeyValuePair<Boolean, string> rst in result)
            {
                if (rst.Key == true)
                {
                    rango.Text = "Resultado: " + rst.Key + " || Valor: " + rst.Value;
                    rango.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
                    rango.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    rango.ApplyPropertyValue(TextElement.FontSizeProperty, "15");
                }
                else
                {
                    rango.Text = rst.Key + rst.Value;
                    rango.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                    rango.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    rango.ApplyPropertyValue(TextElement.FontSizeProperty, "15");
                }
            }
            
        }

        private void TestFacturaConsulta(SQLConecction conection)
        {
            //Tests de consulta de facturas
            Factura factura = new Factura(conection);
            
            if (factura.total.Count == 2)
            {
                result.Add(true, "Count = 2");
            }
            else
            {
               result.Add(false, "Error al consultar todas las facturas de la tabla");
            }

            //return new KeyValuePair<Boolean, string>(false, "Error al consultar la factura en la Base de Datos para los parametros: id= ");

        }
        
    }
    
}
