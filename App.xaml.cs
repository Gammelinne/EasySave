using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Diagnostics;
using System.IO;

namespace EasySaveApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            string json = File.ReadAllText(@"C:\Users\lefra\OneDrive - Association Cesi Viacesi mail\CESI\3eme Année\Programmation Système\Projet\EasySave\Livrable 2\Settings\Settings.json");
            Dictionary<string, string> settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            Current.Properties["TypeOfLog"] = settings["TypeOfLog"];
            //MessageBox.Show(Current.Properties["TypeOfLog"].ToString());
        }
    }
}
