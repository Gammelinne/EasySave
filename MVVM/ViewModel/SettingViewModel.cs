using EasySaveApp.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace EasySaveApp.MVVM.ViewModel
{
    class SettingViewModel : ObservableObject
    {
        public ObservableCollection<string> ProcessList { get; set; }
        public ObservableCollection<string> ProcessListStop { get; set; }

        public ObservableCollection<string> MyListOfExetension { get; set; }
        public ObservableCollection<string> ExtensionToEncrypt { get; set; }
        public RelayCommand AddExtensionToEncrypt { get; set; }
        public RelayCommand RemoveExtensionToEncrypt { get; set; }
        public RelayCommand AddProcess { get; set; }
        public RelayCommand RemoveProcess { get; set; }
        public object ExtensionSelected { get; set; }
        public object ProcessSelected { get; set; }
        public SettingViewModel()
        {
            
            // Extensions

            MyListOfExetension = new ObservableCollection<string>();
            MyListOfExetension.Add("txt");
            MyListOfExetension.Add("json");
            MyListOfExetension.Add("xml");
            MyListOfExetension.Add("doc");
            MyListOfExetension.Add("docx");
            MyListOfExetension.Add("pdf");
            MyListOfExetension.Add("png");
            MyListOfExetension.Add("jpg");
            MyListOfExetension.Add("jpeg");
            MyListOfExetension.Add("gif");
            MyListOfExetension.Add("mp3");
            MyListOfExetension.Add("mp4");
            MyListOfExetension.Add("avi");
            MyListOfExetension.Add("mkv");
            MyListOfExetension.Add("exe");
            MyListOfExetension.Add("dll");
            MyListOfExetension.Add("zip");
            MyListOfExetension.Add("rar");
            MyListOfExetension.Add("7z");
            MyListOfExetension.Add("tar");
            MyListOfExetension.Add("iso");
            MyListOfExetension.Add("bin");
            MyListOfExetension.Add("dat");
            MyListOfExetension.Add("db");
            MyListOfExetension.Add("dbf");
            MyListOfExetension.Add("log");
            MyListOfExetension.Add("mdb");
            MyListOfExetension.Add("sav");
            MyListOfExetension.Add("sql");
            MyListOfExetension.Add("tar");
            MyListOfExetension.Add("xml");
            MyListOfExetension.Add("csv");
            MyListOfExetension.Add("apk");
            MyListOfExetension.Add("bat");
            MyListOfExetension.Add("bin");
            MyListOfExetension.Add("cgi");
            MyListOfExetension.Add("com");
            MyListOfExetension.Add("gadget");
            MyListOfExetension.Add("jar");
            MyListOfExetension.Add("wsf");
            MyListOfExetension.Add("msi");
            MyListOfExetension.Add("msu");
            MyListOfExetension.Add("msp");
            MyListOfExetension.Add("vb");
            MyListOfExetension.Add("vbs");
            MyListOfExetension.Add("vbe");
            MyListOfExetension.Add("ps1");
            MyListOfExetension.Add("ps1xml");
            MyListOfExetension.Add("ps2");
            MyListOfExetension.Add("ps2xml");
            MyListOfExetension.Add("psc1");
            MyListOfExetension.Add("psc2");
            MyListOfExetension.Add("msh");
            MyListOfExetension.Add("msh1");
            MyListOfExetension.Add("msh2");
            MyListOfExetension.Add("mshxml");
            MyListOfExetension.Add("msh1xml");
            MyListOfExetension.Add("msh2xml");
            MyListOfExetension.Add("scf");
            MyListOfExetension.Add("lnk");
            MyListOfExetension.Add("inf");
            MyListOfExetension.Add("reg");
            MyListOfExetension.Add("docm");
            MyListOfExetension.Add("dotm");
            MyListOfExetension.Add("xlsm");
            MyListOfExetension.Add("xltm");
            MyListOfExetension.Add("xlam");
            MyListOfExetension.Add("pptm");
            MyListOfExetension.Add("potm");
            MyListOfExetension.Add("ppam");
            MyListOfExetension.Add("ppsm");
            MyListOfExetension.Add("sldm");
            MyListOfExetension.Add("thmx");
            MyListOfExetension.Add("pub");
            MyListOfExetension.Add("odt");
            MyListOfExetension.Add("ott");
            MyListOfExetension.Add("odp");
            MyListOfExetension.Add("otp");
            MyListOfExetension.Add("ods");
            MyListOfExetension.Add("ots");
            MyListOfExetension.Add("odg");
            MyListOfExetension.Add("otg");

            ExtensionToEncrypt = new ObservableCollection<string>();

            AddExtensionToEncrypt = new RelayCommand(o =>
            {
                if (ExtensionSelected != null)
                {
                    ExtensionToEncrypt.Add(ExtensionSelected.ToString());
                    MyListOfExetension.Remove(ExtensionSelected.ToString());
                }
            });

            RemoveExtensionToEncrypt = new RelayCommand(o =>
            {
                if (ExtensionSelected != null)
                {
                    MyListOfExetension.Add(ExtensionSelected.ToString());
                    ExtensionToEncrypt.Remove(ExtensionSelected.ToString());
                }
            });

            // Process

            ProcessList = new ObservableCollection<string>();
            
            foreach (var process in Process.GetProcesses())
            {
                ProcessList.Add(process.ProcessName + ".exe");
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