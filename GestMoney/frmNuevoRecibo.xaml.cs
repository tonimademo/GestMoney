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
using GestMoney.Clases;
using GestMoney.Servicios;
using System.Linq;
using System.Globalization;

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

            KeyValuePair<bool, Dictionary<int, List<string>>> tipos = new KeyValuePair<bool, Dictionary<int, List<string>>>();
            KeyValuePair<bool, Dictionary<int, List<string>>> grupos = new KeyValuePair<bool, Dictionary<int, List<string>>>();
            KeyValuePair<bool, Dictionary<int, List<string>>> clases = new KeyValuePair<bool, Dictionary<int, List<string>>>();
            tipos = ServicioFactura.Tipos();
            grupos = ServicioFactura.Grupos();
            clases = ServicioFactura.Clases();

            if (tipos.Key)
            {
                foreach (KeyValuePair<int, List<string>> tipo in tipos.Value)
                {
                    cbTipoRecibo.Items.Add( new ComboItem(tipo.Value[0], tipo.Key));

                }

                foreach (KeyValuePair<int, List<string>> grupo in grupos.Value)
                {
                    cbGrupoRecibo.Items.Add(new ComboItem(grupo.Value[0], grupo.Key));

                }

                foreach (KeyValuePair<int, List<string>> clase in clases.Value)
                {
                    cbClaseRecibo.Items.Add(new ComboItem(clase.Value[0], clase.Key));

                }

            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, object> valores;
            KeyValuePair<bool, object> result_test = new KeyValuePair<bool, object>();
            ComboItem cbtipo;
            ComboItem cbclase;
            ComboItem cbgrupo;
            cbtipo = (ComboItem) cbTipoRecibo.SelectedItem;
            cbclase = (ComboItem)cbClaseRecibo.SelectedItem;
            cbgrupo = (ComboItem)cbGrupoRecibo.SelectedItem;


            valores = new Dictionary<string, object> { { "tipo", cbtipo.Valor }, { "clase", cbclase.Valor }, { "grupo", cbgrupo.Valor }, { "importe", txtImporte.Text }, { "concepto", txtConcepto.Text }, { "fecha_importe", txtFechaRecibo.Text } };
            result_test = ServicioFactura.Insert(valores);

            if (result_test.Key == true)
            {
                MessageBox.Show("Recibo insertado correctamente");
            }
            else
            {
                MessageBox.Show("Error al insertar el nuevo recibo: " + result_test.Value);
            }


        }
    }
}
