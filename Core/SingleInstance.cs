using System;
using System.Threading;
using System.Windows;

namespace EasySaveApp.Core
{
    internal class SingleInstance
    {
        public partial class Application : App
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
        }
    }
}
