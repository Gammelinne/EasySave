using System;
using System.Threading;

namespace Livrable_1
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to the File Transfer Program / Bonjour, bienvenue dans le programme de transfert de fichier");
            Thread.Sleep(1500); //The Thread.Sleep() method suspends the current thread for the specified amount of time.;
            Console.Clear();
            SaveData();
        }

        
        public static void SaveData()
        {
            Console.WriteLine("Please enter what you want to do / Veuillez entrer ce que vous voulez faire");
            Console.WriteLine("");
            Console.WriteLine("1. Transfer a folder / 1. Transférer un folder");
            Console.WriteLine("2. get log of the day / 2. obtenir le log du jour");
            Console.WriteLine("3. get state of the day / 3. obtenir l'etat des sauvegardes du jour");
            Console.WriteLine("4. exit / 4. quitter");
            Console.WriteLine("");

            /*
            Log log = new Log("test", "test", "test", 12, 14, DateTime.Now);
            log.SaveLog();
            */

            string choice = Console.ReadLine();
            Console.Clear();

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
                    try
                    {
                        Log.ReadLogOfTheDay();
                    } catch
                    {
                        Console.WriteLine("No log file found for today");
                        Thread.Sleep(2000);
                        Console.Clear();
                        SaveData();
                    }
                    break;
                case "3":
                    try
                    {
                        State.ReadStateOfTheDay();
                    } catch
                    {
                        Console.WriteLine("No state file found for today");
                        Thread.Sleep(2000);
                        Console.Clear();
                        SaveData();
                    }
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter a valid choice / Veuillez entrer un choix valide");
                    Thread.Sleep(2000);
                    Console.Clear();
                    SaveData();
                    break;
            }
        }
    }
}
