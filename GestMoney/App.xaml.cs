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

        void Init(object sender, StartupEventArgs e)
        {
            // Application is running
            // Process command line args
            if (e.Args.Length == 1 && e.Args[0] == "TEST")
            {
                //SE inician los TEST
                frmTests test = new frmTests();
                test.Show();
            }
            else {
                // Inicia la aplicacion de forma normal
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }

            
        }
    }
}
