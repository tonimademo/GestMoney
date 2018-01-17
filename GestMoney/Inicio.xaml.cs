﻿using GestMoney.Clases;
using GestMoney.Servicios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private DataTable _datafill = new DataTable();
        public DataView Datafill
        {
            
        get {
                if (ServicioFactura.vRecibosSelect(ref _datafill).Key)
                {
                    return _datafill.DefaultView;
                }
                else
                {
                    return null;
                }
                
                }
        set {
                _datafill = value.Table;
            }
        }
        

        public  MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }

        private void Inicio_Loaded(object sender, RoutedEventArgs e)
        {
            //var data = ServicioFactura.vRecibosSelect(ref datafill);

            //if (ServicioFactura.vRecibosSelect(ref datafill).Key)
            //{
            //    datafill.DefaultView.RowFilter = "1=1";
            //    dgvRecibos.ItemsSource = datafill.DefaultView;
            //    //ColorRows();

            //}
            _datafill.DefaultView.RowFilter = "1=1";
        }
        

        private void txtFiltroDesde_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
            var filtro_local = (txtFiltroDesde.Text != "") ? string.Format(" AND fecha_importe >= '{0}'", txtFiltroDesde.Text) : "";
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex = "[AND|OR]*\\s*fecha_importe\\s>=\\s'[0-9][0-9]\\/[0-9][0-9]\\/[0-9][0-9][0-9][0-9]'";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            Funciones.ModificarFiltro(ref _datafill, _datafill.DefaultView.RowFilter, filtro_local, regex);

            //Aplico el nuevo filtro
            //dgvRecibos.ItemsSource = datafill.DefaultView;
            Datafill = _datafill.DefaultView;
        }

        private void txtFiltroHasta_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
            var filtro_local = (txtFiltroHasta.Text != "") ? string.Format(" AND fecha_importe >= '{0}'", txtFiltroHasta.Text) : "";
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex = "[AND|OR]*\\s*fecha_importe\\s<=\\s'[0-9][0-9]\\/[0-9][0-9]\\/[0-9][0-9][0-9][0-9]'";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            Funciones.ModificarFiltro(ref _datafill, _datafill.DefaultView.RowFilter, filtro_local, regex);

            //Aplico el nuevo filtro
            //dgvRecibos.ItemsSource = datafill.DefaultView;
            Datafill = _datafill.DefaultView;
        }
        
        private void txtImporteMayor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
            var filtro_local = (txtImporteMayor.Text != "") ? string.Format(" AND importe >= {0}", txtImporteMayor.Text) : "";
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex = "[AND|OR]*\\s*importe\\s>=\\s[0-9]+[,|.]*[0-9]*";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            Funciones.ModificarFiltro(ref _datafill, _datafill.DefaultView.RowFilter, filtro_local, regex);

            //Aplico el nuevo filtro
            //dgvRecibos.ItemsSource = datafill.DefaultView;
        }

        private void txtImporteMenor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Creo el filtro que aplica de cambiar el valor del campo
           var filtro_local = (txtImporteMenor.Text != "") ? string.Format(" AND importe <= {0}", txtImporteMenor.Text) : "";
            // Defino la exp regular que quitara el filtro en caso de no tener valor en el campo
            var regex = "[AND|OR]*\\s*importe\\s<=\\s[0-9]+[,|.]*[0-9]*";

            //Si he quitado el valor y lo dejo vacio, no tiene que aplicar el campo al filtro y lo tengo que quitar por lo que paso una exp reg para saber que quitar
            Funciones.ModificarFiltro(ref _datafill, _datafill.DefaultView.RowFilter, filtro_local, regex);

            //Aplico el nuevo filtro
            //dgvRecibos.ItemsSource = datafill.DefaultView;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            var frmNuevoR = new frmNuevoRecibo();
            frmNuevoR.ShowDialog();
        }

    }
}