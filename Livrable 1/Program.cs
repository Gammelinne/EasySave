using System;
using System.Threading;

namespace Livrable_1
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, welcome to EasySave file transfer program / Bonjour, bienvenue dans le programme de transfert de fichier EasySave\n");
            SaveData();
        }

        public static void SaveData()
        {
            //Start choice
            Console.WriteLine("Please enter what you want to do / Veuillez entrer ce que vous voulez faire\n");
            Console.WriteLine("1. Create a backup / Créer une sauvegarde");
            Console.WriteLine("2. Get log of the day / Obtenir le log du jour");
            Console.WriteLine("3. Get state of the day / Obtenir l'etat des sauvegardes du jour");
            Console.WriteLine("4. Exit / Quitter\n");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    //Make a save
                    Console.WriteLine("Please enter the name of the save / Veuillez entrer le nom de la sauvegarde");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please enter directory source / Veuillez entrer le dossier source");
                    string fileSource = Console.ReadLine();
                    Console.WriteLine("Please enter directory destination / Veuillez entrer le dossier de destination");
                    string fileDestination = Console.ReadLine();
                    Console.WriteLine("Please enter the type of the save / Veuillez entrer le type du sauvegarde");
                    Console.WriteLine("1. Complete / Complet");
                    Console.WriteLine("2. Differentiel / Differentiel");
                    string fileType = Console.ReadLine();
                    Console.WriteLine("which extension do you want for log file and state file / Quelle extension voulez-vous pour le fichier log et le fichier d'etat");
                    Console.WriteLine("1. Json / Json");
                    Console.WriteLine("2. Xml / Xml");
                    string extension = Console.ReadLine();
                    while (fileType != "1" && fileType != "2" && extension != "1" && extension != "2")
                    {
                        Console.WriteLine("Please enter a valid choice for type / Veuillez entrer un choix valide pour le type: ");
                        fileType = Console.ReadLine();
                        Console.WriteLine("Please enter a valid choice for extension / Veuillez entrer un choix valide pour l'extension: ");
                        extension = Console.ReadLine();
                    }
                    //try
                    //{
                        Save save = new Save(name, fileSource, fileDestination, fileType);
                        save.SaveSave(extension);
                    //}
                    //catch (Exception e)
                    //{
                        //Console.Clear();
                        //Console.WriteLine(e.Message);
                        //Console.WriteLine("\n------------------------------------------------------------------------------------------\n");
                        //SaveData();
                    //}
                    break;

                case "2":
                    try
                    {
                        Log.ReadLogOfTheDay();
                    }
                    catch
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
                    }
                    catch
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