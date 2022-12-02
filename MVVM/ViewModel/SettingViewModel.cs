using EasySaveApp.Core;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;


namespace EasySaveApp.MVVM.ViewModel
{
    class SettingViewModel : ObservableObject
    {
        public string SelectedExtension
        {
            set
            {
                string newValue = value.Replace("System.Windows.Controls.ComboBoxItem\u00A0: ", "");
                Application.Current.Properties["TypeOfLog"] = newValue;
                string json = File.ReadAllText("../../../Settings.json");
                Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                setting["TypeOfLog"] = newValue;
                File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
            }
        }
    }
}
