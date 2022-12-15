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

        public ObservableCollection<string> ListPriorityExtensions { get; set; }
        public ObservableCollection<string> PriorityExtensions { get; set; }

        public RelayCommand AddPriorityExtensions { get; set; }
        public RelayCommand RemovePriorityExtensions { get; set; }

        public RelayCommand AddExtensionToEncrypt { get; set; }
        public RelayCommand RemoveExtensionToEncrypt { get; set; }

        public RelayCommand AddProcess { get; set; }
        public RelayCommand RemoveProcess { get; set; }

        public object ExtensionSelected { get; set; }
        public object ProcessSelected { get; set; }
        public object PriorityExtensionsSelected { get; set; }

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
            #region
            List<string> listOfExtensionToCrypt = new List<string>{
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
            List<string> extensionToCryp = new List<string>(Application.Current.Properties["ExtensionToCrypt"].ToString().Split(" "));
            foreach(string extension in extensionToCryp)
            {
                listOfExtensionToCrypt.Remove(extension);
            }
            MyListOfExtension = new ObservableCollection<string>(listOfExtensionToCrypt);
            ExtensionToEncrypt = new ObservableCollection<string>(extensionToCryp);

            AddExtensionToEncrypt = new RelayCommand(o =>
            {
                if (ExtensionSelected != null)
                {
                    string extensionSelected = ExtensionSelected.ToString();
                    ExtensionToEncrypt.Add(extensionSelected);
                    MyListOfExtension.Remove(extensionSelected);
                    string json = File.ReadAllText("../../../Settings.json");
                    Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    setting["ExtensionToCrypt"] += extensionSelected + " ";
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["ExtensionToCrypt"] += extensionSelected + " ";
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
                    setting["ExtensionToCrypt"] = setting["ExtensionToCrypt"].Replace(extensionSelected + " ", "");
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["ExtensionToCrypt"] = Application.Current.Properties["ExtensionToCrypt"].ToString().Replace(extensionSelected + " ", "");
                }
            });
            #endregion

            // Process
            #region
            ProcessList = new ObservableCollection<string>();
            List<string> processToStop = new List<string>(Application.Current.Properties["ProcessToStop"].ToString().Split(" "));
            foreach (var process in Process.GetProcesses())
            {
                if (!processToStop.Contains(process.ProcessName))
                {
                    ProcessList.Add(process.ProcessName);
                }
            }

            ProcessListStop = new ObservableCollection<string>(processToStop);
            
            AddProcess = new RelayCommand(o =>
            {
                if (ProcessSelected != null)
                {
                    string processSelected = ProcessSelected.ToString();
                    ProcessListStop.Add(processSelected);
                    ProcessList.Remove(processSelected);
                    string json = File.ReadAllText("../../../Settings.json");
                    Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    setting["ProcessToStop"] += processSelected + " ";
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["ProcessToStop"] += processSelected + " ";
                }
            });

            RemoveProcess = new RelayCommand(o =>
            {
                if (ProcessSelected != null)
                {
                    string processSelected = ProcessSelected.ToString();
                    ProcessList.Add(processSelected);
                    ProcessListStop.Remove(processSelected);
                    string json = File.ReadAllText("../../../Settings.json");
                    Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    setting["ProcessToStop"] = setting["ProcessToStop"].Replace(processSelected + " ", "");
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["ProcessToStop"] = Application.Current.Properties["ProcessToStop"].ToString().Replace(processSelected + " ", "");
                }
            });
            #endregion

            //Priority Extensions
            List<string> listOfExtensionPriority = new List<string>{
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
            List<string> extensionPriority = new List<string>(Application.Current.Properties["PriorityFiles"].ToString().Split(" "));
            foreach (string extension in extensionPriority)
            {
                listOfExtensionPriority.Remove(extension);
            }

            ListPriorityExtensions = new ObservableCollection<string>(listOfExtensionPriority);
            PriorityExtensions = new ObservableCollection<string>(extensionPriority);

            AddPriorityExtensions = new RelayCommand(o =>
            {
                if (PriorityExtensionsSelected != null)
                {
                    string priorityExtensionsSelected = PriorityExtensionsSelected.ToString();
                    PriorityExtensions.Add(priorityExtensionsSelected);
                    ListPriorityExtensions.Remove(priorityExtensionsSelected);
                    string json = File.ReadAllText("../../../Settings.json");
                    Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    setting["PriorityFiles"] += priorityExtensionsSelected + " ";
                    File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                    Application.Current.Properties["PriorityFiles"] += priorityExtensionsSelected + " ";
                }
            });

            RemovePriorityExtensions = new RelayCommand(o =>
            {
                string priorityExtensionsSelected = PriorityExtensionsSelected.ToString();
                ListPriorityExtensions.Add(priorityExtensionsSelected);
                PriorityExtensions.Remove(priorityExtensionsSelected);
                string json = File.ReadAllText("../../../Settings.json");
                Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                setting["PriorityFiles"] = setting["PriorityFiles"].Replace(priorityExtensionsSelected + " ", "");
                File.WriteAllText("../../../Settings.json", JsonSerializer.Serialize(setting));
                Application.Current.Properties["PriorityFiles"] = Application.Current.Properties["PriorityFiles"].ToString().Replace(PriorityExtensionsSelected + " ", "");
            });
        }
        
    }
}