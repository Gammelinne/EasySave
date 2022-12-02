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
                string[] lines = File.ReadAllLines("../../../Settings.json");
                
                
            }
        }
    }
}
