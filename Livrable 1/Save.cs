using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Livrable_1
{
    internal class Save
    {
        //Timer class
        public static int secondsCount = 0;
        public static Timer timer = new Timer(1000);

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
            int count = 0;
            string[] directory = { };

            //Start timer
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
            secondsCount++;

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

            long size = GetDirectorySize(FileSource);

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

                //Create a state
                string Status = "ACTIVES";
                State state = new State(Name, FileSource, FileDestination, FileType,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length - count,
                    count,
                    Status,
                    (int)size);

                //Save state and change status
                if (state.FileLeftToTransfer > 30 && state.FileLeftToTransfer % 10 == 0)
                {
                    state.SaveState();
                }

                if (state.FileLeftToTransfer < 30 && state.FileLeftToTransfer % 10 != 0)
                {
                    state.SaveState();
                }

                if (state.FileLeftToTransfer == 0)
                {
                    Status = "END";
                    State endState = new State(Name, FileSource, FileDestination, FileType,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length - count,
                    count,
                    Status,
                    (int)size);

                    endState.SaveState();
                }
            }
            
            //Create a log
            Log log = new Log(Name, FileSource, FileDestination, (int)size, secondsCount, DateTime.Now);
            log.SaveLog(); //Save log

            Console.WriteLine("Would you like to go back to the menu? / Voulez-vous revenir au menu ? (y)");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                Console.Clear();
                Program.SaveData();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        static long GetDirectorySize(string path) // Get Directory Size
        {
            // Get array of all file names.
            string[] a = Directory.GetFiles(path, "*.*");

            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // Return total size
            return b;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            secondsCount++;
            // throw new NotImplementedException();
        }
    }
}
