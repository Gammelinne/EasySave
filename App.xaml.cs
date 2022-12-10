using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EasySaveApp
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var _mainWindow = new MainWindow();
            _mainWindow.ShowDialog();
        }
    }
}
