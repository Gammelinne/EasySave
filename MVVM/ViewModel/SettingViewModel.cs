using EasySaveApp.Core;
using EasySaveApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
