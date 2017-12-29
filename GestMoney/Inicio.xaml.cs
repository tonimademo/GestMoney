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
            
            if (data.Key)
            {
                datafill.DefaultView.RowFilter = "1=1";
                dgvRecibos.ItemsSource = datafill.DefaultView;
            }

        }

        private void txtFiltroDesde_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
            var filtro_local = (txtFiltroDesde.Text != "") ? string.Format(" AND fecha_importe >= '{0}'", txtFiltroDesde.Text) : "";
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex = "[AND|OR]*\\s*fecha_importe\\s>=\\s'[0-9][0-9]\\/[0-9][0-9]\\/[0-9][0-9][0-9][0-9]'";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            Funciones.ModificarFiltro(ref datafill, datafill.DefaultView.RowFilter, filtro_local, regex);
            
            //Aplico el nuevo filtro
            dgvRecibos.ItemsSource = datafill.DefaultView;
        }

        private void txtFiltroHasta_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
            var filtro_local = (txtFiltroHasta.Text != "") ? string.Format(" AND fecha_importe >= '{0}'", txtFiltroHasta.Text) : "";
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex = "[AND|OR]*\\s*fecha_importe\\s<=\\s'[0-9][0-9]\\/[0-9][0-9]\\/[0-9][0-9][0-9][0-9]'";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            Funciones.ModificarFiltro(ref datafill, datafill.DefaultView.RowFilter, filtro_local, regex);

            //Aplico el nuevo filtro
            dgvRecibos.ItemsSource = datafill.DefaultView;
        }
    }
}