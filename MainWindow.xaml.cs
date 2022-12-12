using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace EasySaveApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SwitchLanguage("en");
            string json = File.ReadAllText("../../../Settings.json");
            Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            Application.Current.Properties["TypeOfLog"] = setting["TypeOfLog"];
            Application.Current.Properties["ExtensionToCrypt"] = ".txt .exe";
            Application.Current.Properties["CryptKey"] = "100";
            Application.Current.Properties["Software"] = "vlc notepad";
            Application.Current.Properties["PriorityFiles"] = ".ben .txt ";
        }

        private void FrenchButton_Checked(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("fr");
        }

        private void EnglishButton_Checked(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("en");
        }

        private void SwitchLanguage(string languageCode)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (languageCode)
            {
                case "en":
                    dict.Source = new Uri("..\\Languages\\StringResources_en.xaml", UriKind.Relative);
                    break;

                case "fr":
                    dict.Source = new Uri("..\\Languages\\StringResources_fr.xaml", UriKind.Relative);
                    break;

                default:
                    dict.Source = new Uri("..\\Languages\\StringResources_en.xaml", UriKind.Relative);
                    break;
            };
            this.Resources.MergedDictionaries.Add(dict);
        }
    }
}
