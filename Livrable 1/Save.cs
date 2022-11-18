using System;
using System.Collections.Generic;
using System.Text;

namespace Livrable_1
{
    internal class Save
    {
        public string Name;
        public string FileSource;
        public string FileDestination;
        public string FileType;

        public Save(string name, string fileSource, string fileDestination, string fileType)
        {
            this.Name = name;
            this.FileSource = fileSource;
            this.FileDestination = fileDestination;
            this.FileType = fileType;
        }

        public void SaveSave()
        {
            
            //copier le dossier source dans le dossier destination
            

            //créer un state durant le transfert

            //créer un log quand le tranfert est terminé

        }
    }
}
