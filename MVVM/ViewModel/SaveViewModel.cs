using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using EasySaveApp.MVVM.Model;

namespace EasySaveApp.MVVM.ViewModel
{
    class SaveViewModel
    {

        private string name;
        private string fileSource;
        private string fileDestination;
        private string saveType;

        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }
        public string SaveType { get => saveType; set => saveType = value; }

        public SaveViewModel(string name, string fileSource, string fileDestination, string saveType)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            SaveType = saveType;
        }

        public bool CheckInputFill()
        {
            if (Name == string.Empty || FileSource == string.Empty || FileDestination == string.Empty || SaveType == string.Empty)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }

        public bool CheckPathExist()
        {
            if (Directory.Exists(FileSource) && Directory.Exists(FileDestination))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void Save()
        {

            Save save = new Save(name, fileSource, fileDestination, saveType);
            save.SaveSave();
        }
    }
}
