using EasySaveApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveApp.Languages
{
    public class English : AppLanguage
    {
        public English()
        {
            HomeTitle = "Welcome in EasySave";
            Slogan = "Easiest backup application";
            Description = "Welcome in EasySave, the easiest to use backup application. To save data, simply go to the Save section, fill in the different field and select Save. If you have any problems, please contact us at this mail address: easysupport@lahyra.com";
            Home = "Home";
            Save = "Save";
            Settings = "Settings";
            SaveName = "Name";
            Search = "Search";
            Source = "Source";
            Destination = "Destination";
            TypeOfBackup = "Type of Backup";
            CompletBackup = "Complet Backup";
            DifferentialBackup = "Differential Backup";
            language = "Language";
        }
    }
}
