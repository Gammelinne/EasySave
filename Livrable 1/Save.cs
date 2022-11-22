using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace Livrable_1
{
    internal class Save
    {
        private string name;
        private string fileSource;
        private string fileDestination;
        private string fileType;

        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }
        public string FileType { get => fileType; set => fileType = value; }

        public Save(string name, string fileSource, string fileDestination, string fileType)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            FileType = fileType;
        }

        public void SaveSave()
        {
            //start timer
            Timer timer = new Timer();
            timer.Interval = 1000;
            int count = 0;
            foreach (string newPath in Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories)) // on récupère les fichiers du dossier source
            {
                
                if (!Directory.Exists(Path.GetDirectoryName(newPath.Replace(FileSource, FileDestination + "\\" + Name)))) //Si le dossier n'existe pas
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newPath.Replace(FileSource, FileDestination + "\\" + Name))); //on le créer
                }
                
                if (!File.Exists(newPath.Replace(FileSource, FileDestination + "\\" + Name)) && FileType == "2") //si on est en sauvegarde diferentiel et que le fichier n'existe pas
                {
                    File.Copy(newPath, newPath.Replace(FileSource, FileDestination + "\\" + Name), true); //on copie
                    count++; //on incrémente le compteur
                    Console.WriteLine(count + " out of " + Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length + " files copied"); //on affiche le nombre de fichier sauvegarder en plus
                }
                
                else if(File.Exists(newPath.Replace(FileSource, FileDestination + "\\" + Name)) && FileType == "2") //si il existe et qu'on est en sauvegarde differenti
                {
                    FileInfo file1 = new FileInfo(newPath); //on récupère les infos du fichier source

                    FileInfo file2 = new FileInfo(newPath.Replace(FileSource, FileDestination + "\\" + Name)); //on recupère les infos du fichier destination

                    if (file1.Length != file2.Length) //si la taille du fichier source est différente de la taille du fichier destination
                    {
                        File.Copy(newPath, newPath.Replace(FileSource, FileDestination + "\\" + Name), true); //on copie
                        count++; //on incrémente le compteur
                        Console.WriteLine(count + " out of " + Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length + " files copied"); //on affiche le nombre de fichier sauvegarder en plus
                    }
                }
                else if (FileType == "1") //si on est en sauvegarde complète
                {
                    File.Copy(newPath, newPath.Replace(FileSource, FileDestination + "\\" + Name), true); //on copie
                    count++; //on incrémente le compteur
                    Console.WriteLine(count + " out of " + Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length + " files copied"); //on affiche le nombre de fichier sauvegarder en plus
                }
                //create object state
                State state = new State(Name, FileSource, FileDestination, FileType, Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length, count, Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length - count); //reste à sauvegarder
                //save state
                state.SaveState();
            }
            //create log
            Log log = new Log(Name, FileSource, FileDestination, Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length, timer.Interval, DateTime.Now);
            log.SaveLog(); //save log

            Console.WriteLine("Do you want to do another save ? / Voulez vous faire une autre sauvegarde ? (y)");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                Program.SaveData();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
