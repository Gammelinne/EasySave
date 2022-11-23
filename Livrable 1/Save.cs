using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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
            string[] directory = { };

            try
            {
                directory = Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories);
            } catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Program.SaveData();
            }
                foreach (string newPath in directory) // We get the files from the source folder
            {
                
                if (!Directory.Exists(Path.GetDirectoryName(newPath.Replace(FileSource, FileDestination + "\\" + Name)))) //If the file does not exist
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newPath.Replace(FileSource, FileDestination + "\\" + Name))); //we create it
                }
                
                if (!File.Exists(newPath.Replace(FileSource, FileDestination + "\\" + Name)) && FileType == "2") //if we are in differential backup and the file does not exist
                {
                    File.Copy(newPath, newPath.Replace(FileSource, FileDestination + "\\" + Name), true); //we copy
                    count++; //on incrémente le compteur
                    Console.WriteLine(count + " out of " + directory.Length + " files copied"); //the number of files saved in addition is displayed
                }
                
                else if(File.Exists(newPath.Replace(FileSource, FileDestination + "\\" + Name)) && FileType == "2") //if it exists and we are in differential backup
                {
                    FileInfo file1 = new FileInfo(newPath); //we get the information from the source file

                    FileInfo file2 = new FileInfo(newPath.Replace(FileSource, FileDestination + "\\" + Name)); //we get the information from the destination file

                    if (file1.Length != file2.Length) //if the size of the source file is different from the size of the destination file
                    {
                        File.Copy(newPath, newPath.Replace(FileSource, FileDestination + "\\" + Name), true); //We copy
                        count++; //we increment the counter
                        Console.WriteLine(count + " out of " + directory.Length + " files copied"); //the number of files saved in addition is displayed
                    }
                }
                else if (FileType == "1") //if you are in full backup
                {
                    File.Copy(newPath, newPath.Replace(FileSource, FileDestination + "\\" + Name), true); //We copy
                    count++; //we increment the counter
                    Console.WriteLine(count + " out of " + directory.Length + " files copied"); //
                }
                //create object state
                State state = new State(Name, FileSource, FileDestination, FileType, Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length, Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length - count, count); //reste à sauvegarder
                if(state.Progression % 10 == 0)
                {
                    state.SaveState();
                }
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
