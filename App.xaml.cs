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

        private static Mutex mutex = new Mutex(true, "MyMonoInstanceApp");
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Another instance of the application is already running!");
                Environment.Exit(0);
            }
            base.OnStartup(e);
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var _mainWindow = new MainWindow();
            _mainWindow.ShowDialog();
        }
    }
}
