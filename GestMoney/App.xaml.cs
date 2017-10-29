using GestMoney.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GestMoney
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private String baseDatos;
        void Init(object sender, StartupEventArgs e)
        {
            // Application is running
            // Process command line args
            
            if (e.Args.Length == 1 && e.Args[0] == "TEST")
            {
                //SE inician los TEST
                baseDatos = "GestMoney_Test";
                SQLConecction conection = new SQLConecction();
                conection.ConnectToSql(baseDatos);

                frmTests test = new frmTests();
                test.Show();
            }
            else {
                // Inicia la aplicacion de forma normal
                baseDatos = "GestMoney";
                SQLConecction conection = new SQLConecction();
                conection.ConnectToSql(baseDatos);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }

            
        }
    }
}
