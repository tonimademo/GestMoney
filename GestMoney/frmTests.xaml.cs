using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para Tests.xaml
    /// </summary>

    public partial class frmTests : Window
    {

        public frmTests()
        {
            InitializeComponent();
            Funciones.Tests tests = new Funciones.Tests();

            Dictionary<Boolean, string> result = new Dictionary<Boolean, string>();
            KeyValuePair<Boolean, string> result_test = new KeyValuePair<Boolean, string>();

            result_test = tests.Iniciar();
            result.Add(result_test.Key, result_test.Value);

            TextRange rango = new TextRange(txtInfo.Document.ContentEnd, txtInfo.Document.ContentEnd);
            

            foreach (KeyValuePair<Boolean, string> rst in result)
            {
                if (rst.Key == true)
                {
                    rango.Text = rst.Key + rst.Value;
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
        
    }
}
