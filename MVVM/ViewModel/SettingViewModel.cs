using EasySaveApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace EasySaveApp.MVVM.ViewModel
{
    class SettingViewModel : ObservableObject
    {

        public ObservableCollection<string> ProcessList { get; set; }
        public ObservableCollection<string> ProcessListStop { get; set; }

        public ObservableCollection<string> MyListOfExtension { get; set; }
        public ObservableCollection<string> ExtensionToEncrypt { get; set; }
        public RelayCommand AddExtensionToEncrypt { get; set; }
        public RelayCommand RemoveExtensionToEncrypt { get; set; }
        public RelayCommand AddProcess { get; set; }
        public RelayCommand RemoveProcess { get; set; }
        public object ExtensionSelected { get; set; }
        public object ProcessSelected { get; set; }

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
        public SettingViewModel()
        {

            // Extensions
            MyListOfExtension = new ObservableCollection<string>
            {
                "txt",
                "json",
                "xml",
                "doc",
                "docx",
                "pdf",
                "png",
                "jpg",
                "jpeg",
                "gif",
                "mp3",
                "mp4",
                "avi",
                "mkv",
                "exe",
                "dll",
                "zip",
                "rar",
                "7z",
                "tar",
                "iso",
                "bin",
                "dat",
                "db",
                "dbf",
                "log",
                "mdb",
                "sav",
                "sql",
                "tar",
                "xml",
                "csv",
                "apk",
                "bat",
                "bin",
                "cgi",
                "com",
                "gadget",
                "jar",
                "wsf",
                "msi",
                "msu",
                "msp",
                "vb",
                "vbs",
                "vbe",
                "ps1",
                "ps1xml",
                "ps2",
                "ps2xml",
                "psc1",
                "psc2",
                "msh",
                "msh1",
                "msh2",
                "mshxml",
                "msh1xml",
                "msh2xml",
                "scf",
                "lnk",
                "inf",
                "reg",
                "docm",
                "dotm",
                "xlsm",
                "xltm",
                "xlam",
                "pptm",
                "potm",
                "ppam",
                "ppsm",
                "sldm",
                "thmx",
                "pub",
                "odt",
                "ott",
                "odp",
                "otp",
                "ods",
                "ots",
                "odg",
                "otg"
            };

            ExtensionToEncrypt = new ObservableCollection<string>();

            AddExtensionToEncrypt = new RelayCommand(o =>
            {
                if (ExtensionSelected != null)
                {
                    string extensionSelected = ExtensionSelected.ToString();
                    ExtensionToEncrypt.Add(extensionSelected);
                    MyListOfExtension.Remove(extensionSelected);
                    string json = File.ReadAllText("../../../Settings.json");
                    Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    setting["PriorityFiles"] += "." + extensionSelected + " ";
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["PriorityFiles"] += "." + extensionSelected + " ";
                }
            });

            RemoveExtensionToEncrypt = new RelayCommand(o =>
            {
                if (ExtensionSelected != null)
                {
                    string extensionSelected = ExtensionSelected.ToString();
                    MyListOfExtension.Add(extensionSelected);
                    ExtensionToEncrypt.Remove(extensionSelected);
                    string json = File.ReadAllText("../../../Settings.json");
                    Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    setting["PriorityFiles"] = setting["PriorityFiles"].Replace("." + extensionSelected + " ", "");
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["PriorityFiles"] = Application.Current.Properties["PriorityFiles"].ToString().Replace("." + extensionSelected + " ", "");
                    MessageBox.Show(Application.Current.Properties["PriorityFiles"].ToString());
                }
            });

            // Process
            ProcessList = new ObservableCollection<string>();
            
            foreach (var process in Process.GetProcesses())
            {
                ProcessList.Add(process.ProcessName);
            }

            ProcessListStop = new ObservableCollection<string>();
            
            AddProcess = new RelayCommand(o =>
            {
                if (ProcessSelected != null)
                {
                    ProcessListStop.Add(ProcessSelected.ToString());
                    ProcessList.Remove(ProcessSelected.ToString());
                }
            });

            RemoveProcess = new RelayCommand(o =>
            {
                if (ProcessSelected != null)
                {
                    ProcessList.Add(ProcessSelected.ToString());
                    ProcessListStop.Remove(ProcessSelected.ToString());
                }
            });
        }
    }
}