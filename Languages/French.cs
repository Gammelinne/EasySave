using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveApp.Languages
{
    internal class French : AppLanguage
    {
        public French()
        {
            HomeTitle = "Bienvenue dans EasySave";
            Slogan = "L'application de sauvegarde la plus facile à utiliser";
            Description = "Bienvenue dans EasySave, l'application de sauvegarde la plus simple à utiliser. Pour sauvegarder vos données, allez simplement sur la page Sauvegarde, remplissez les différents champs et cliquer sur Sauvegarder. Si vous rencontrez le moindre problème, vous pouvez nous contacter à l'adresse mail suivante: easysupport@lahyra.com";
            Home = "Accueil";
            Save = "Sauvegarder";
            Settings = "Paramètres";
            SaveName = "Nom";
            Search = "Rechercher";
            Source = "Source";
            Destination = "Destination";
            TypeOfBackup = "Type de sauvegarde";
            CompletBackup = "Sauvegarde complete";
            DifferentialBackup = "Sauvegarde différentielle";
            language = "Langage";
        }
    }
}
