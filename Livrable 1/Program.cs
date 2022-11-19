using System;

namespace Livrable_1
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to the File Transfer Program / Bonjour, Bienvenue dans le programme de transfert de fichier");
            SaveData();
        }

        
        public static void SaveData()
        {
            Console.WriteLine("Please enter what you want to do / Veuillez entrer ce que vous voulez faire");
            Console.WriteLine("1. Transfer a folder / 1. Transférer un folder");
            Console.WriteLine("2. get log of the day / 2. obtenir le log du jour");
            Console.WriteLine("3. get state of the day / 3. obtenir l'etat des sauvegardes du jour");

            Log log = new Log("test", "test", "test", 12, 14, DateTime.Now);
            log.SaveLog();

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Please enter the name of the save / Veuillez entrer le nom de la sauvegarde");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please enter the source of the file / Veuillez entrer la source du fichier");
                    string fileSource = Console.ReadLine();
                    Console.WriteLine("Please enter the destination of the file / Veuillez entrer la destination du fichier");
                    string fileDestination = Console.ReadLine();
                    Console.WriteLine("Please enter the type of the file / Veuillez entrer le type du fichier");
                    Console.WriteLine("1. Complete / 1. Complet");
                    Console.WriteLine("2. Differentiel / 2. Differentiel");
                    string fileType = Console.ReadLine();
                    if (fileType == "1" || fileType == "2")
                    {
                        Save save = new Save(name, fileSource, fileDestination, fileType);
                        save.SaveSave();
                    }
                    break;
                case "2":
                    Log.ReadLogOfTheDay();
                    break;
                case "3":
                    State.ReadStateOfTheDay();
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice / Veuillez entrer un choix valide");
                    break;
            }
        }
    }
}
