using GestMoney.Clases;
using System;
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
        private String baseDatos = "GestMoney_Test";
        private String vRecibos = "SELECT * FROM dbo.vRecibo";
        private SqlDataAdapter dataAdapter;
        private SqlCommandBuilder commandBuilder;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Inicio_Loaded(object sender, RoutedEventArgs e)
        {
            
            DataTable dataTable = new DataTable();
            SQLConecction conection = new SQLConecction();
            conection.ConnectToSql(baseDatos);
                       
            dataAdapter = new SqlDataAdapter(vRecibos, conection.conn);

            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataTable);
            dgvRecibos.ItemsSource = dataTable.DefaultView;

        }
    }
}