using GestMoney.Clases;
using GestMoney.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataTable datafill = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Inicio_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ServicioFactura.vRecibosSelect(ref datafill);
            
            if (data.Key && data.Value.GetType() == typeof(DataTable))
            {
                dgvRecibos.ItemsSource = datafill.DefaultView;
            }

        }

        private void txtFiltroDesde_CalendarClosed(object sender, RoutedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
            var filtro_local = (txtFiltroDesde.Text != "") ? string.Format("fecha_importe >= '%{0}%'", txtFiltroDesde.Text):"" ;
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex_quitar = "[AND|OR]*\\s*fecha_importe\\s>=\\s'[0-9][0-9]\\/[0-9][0-9]\\/[0-9][0-9][0-9][0-9]'";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            if (filtro_local == "")
            {
                Funciones.ModificarFiltro(ref datafill, datafill.DefaultView.RowFilter, filtro_local, quitar: regex_quitar);
            }
            else
            {   //Si tiene valor el campo y tengo el filtro a añadir, lo paso como parametro indicando que no ha de eliminarse
                Funciones.ModificarFiltro(ref datafill, datafill.DefaultView.RowFilter, filtro_local);
            }
            
          
            dgvRecibos.ItemsSource = datafill.DefaultView;

        }
    }
}