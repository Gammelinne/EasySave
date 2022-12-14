using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

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
            Application.Current.Properties["ExtensionToCrypt"] = setting["ExtensionToCrypt"];
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
            ResourceDictionary dict = new ResourceDictionary
            {
                Source = languageCode switch
                {
                    "en" => new Uri(@"..\Languages\StringResources_en.xaml", UriKind.Relative),
                    "fr" => new Uri(@"..\Languages\StringResources_fr.xaml", UriKind.Relative),
                    _ => new Uri(@"..\Languages\StringResources_en.xaml", UriKind.Relative),
                }
            };
            Resources.MergedDictionaries.Add(dict);
        }
    }
}