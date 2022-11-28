using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace EasySaveApp.MVVM.Model
{
    class Log
    {
        //Attributes

        public string Name;
        public string FileSource;
        public string FileDestination;
        public int FileSize;
        public double FileTransfertTime;
        public DateTime Time;

        //Constructor
        public Log(string name, string fileSource, string fileDestination, int fileSize, double fileTransfertTime, DateTime time)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            FileSize = fileSize;
            FileTransfertTime = fileTransfertTime;
            Time = time;
        }

        // Method
        public void SaveLog()
        {
            //Check if all directory exist
            string PathLog = Directory.GetCurrentDirectory() + @"\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Log\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Log\");
            }

            //Create a valid Json
            #region
            if (!File.Exists(PathLog))
            {
                File.WriteAllText(PathLog, "[]");
            }
            string data = File.ReadAllText(PathLog);
            data = data.Remove(data.LastIndexOf("]"), 1);
            if (data.LastIndexOf("}") != -1)
            {
                data = data.Insert(data.LastIndexOf("}") + 1, ",\n");
                File.WriteAllText(PathLog, string.Empty);
                File.WriteAllText(PathLog, data);
            }
            File.WriteAllText(PathLog, data + JsonSerializer.Serialize(GetAll()) + "]");
            #endregion
        }

        // Get all element of the class and place in a dictonary
        public Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "Name", Name },
                { "FileSource", FileSource },
                { "FileDestination", FileDestination },
                { "FileSize", FileSize },
                { "FileTransfertTime", FileTransfertTime },
                { "Time", Time }
            };
            return log;
        }

        //  log file and read the content
        public static void ReadLogOfTheDay()
        {
            string PathLog = Directory.GetCurrentDirectory() + @"\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (File.Exists(PathLog))
            {
                string[] lines = File.ReadAllLines(PathLog);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No log for today");
            }
            Console.WriteLine("");
            Console.WriteLine("Would you like to go back to the menu? / Voulez-vous revenir au menu ? (y/n)");
            string answer = Console.ReadLine();
            if (answer == "y" || answer == "Y")
            {
                Console.Clear();
                //Program.SaveData();
            }
            else if (answer == "n" || answer == "N")
            {
                Environment.Exit(0);
            }
        }
    }
}
