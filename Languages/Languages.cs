using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveApp.Languages
{
    public class AppLanguage
    {
        private string homeTitle;
        private string slogan;
        private string description;
        private string home;
        private string save;
        private string settings;
        private string saveName;
        private string search;
        private string source;
        private string destination;
        private string typeOfBackup;
        private string completBackup;
        private string differentialBackup;
        private string _language;

        public string HomeTitle { get => homeTitle; set => homeTitle = value; }
        public string Slogan { get => slogan; set => slogan = value; }
        public string Description { get => description; set => description = value; }
        public string Home { get => home; set => home = value; }
        public string Save { get => save; set => save = value; }
        public string Settings { get => settings; set => settings = value; }
        public string SaveName { get => saveName; set => saveName = value; }
        public string Search { get => search; set => search = value; }
        public string Source { get => source; set => source = value; }
        public string Destination { get => destination; set => destination = value; }
        public string TypeOfBackup { get => typeOfBackup; set => typeOfBackup = value; }
        public string CompletBackup { get => completBackup; set => completBackup = value; }
        public string DifferentialBackup { get => differentialBackup; set => differentialBackup = value; }
        public string language { get => _language; set => _language = value; }

        public AppLanguage(string homeTitle, string slogan, string description, string home, string save, string settings, string saveName, string search, string source, string destination, string typeOfBackup, string completBackup, string differentialBackup, string _language)
        {
            HomeTitle = homeTitle;
            Slogan = slogan;
            Description = description;
            Home = home;
            Save = save;
            Settings = settings;
            SaveName = saveName;
            Search = search;
            Source = source;
            Destination = destination;
            TypeOfBackup = typeOfBackup;
            CompletBackup = completBackup;
            DifferentialBackup = differentialBackup;
            language = _language;
        }
        public AppLanguage()
        {
            HomeTitle = "";
            Slogan = "";
            Description = "";
            Home = "This is a test";
            Save = "";
            Settings = "";
            SaveName = "";
            Search = "";
            Source = "";
            Destination = "";
            TypeOfBackup = "";
            CompletBackup = "";
            DifferentialBackup = "";
            language = "Language";
        }
    }
}
