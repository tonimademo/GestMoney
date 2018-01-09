using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GestMoney.Servicios;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para frmNuevoRecibo.xaml
    /// </summary>
    public partial class frmNuevoRecibo : Window
    {
        public frmNuevoRecibo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            KeyValuePair<bool, Dictionary<string, object>> tipos = new KeyValuePair<bool, Dictionary<string, object>>();
            tipos = ServicioFactura.Tipos();

            if (tipos.Key)
            {
                foreach (KeyValuePair<string, object> tipo in tipos.Value)
                {
                    cbTipoRecibo.Items.Add({ tipo.Key, tipo.Value });

                }

            }
            
            

        }
    }
}
