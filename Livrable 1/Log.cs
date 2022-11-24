 using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Livrable_1
{
    internal class Log
    {
        private string name;
        private string fileSource;
        private string fileDestination;
        private int fileSize; // in bytes
        private double fileTransfertTime; // in ms.
        private DateTime time;

        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }
        public int FileSize { get => fileSize; set => fileSize = value; }
        public double FileTransfertTime { get => fileTransfertTime; set => fileTransfertTime = value; }
        public DateTime Time { get => time; set => time = value; }

        public Log(string name, string fileSource, string fileDestination, int fileSize, double fileTransfertTime, DateTime time)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            FileSize = fileSize;
            FileTransfertTime = fileTransfertTime;
            Time = time;
        }
        
        public void SaveLog()
        {
            string PathLog = "C:\\ProjetCsFT\\Log\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            System.IO.StreamWriter file = new System.IO.StreamWriter(PathLog, true); // true = append file for multiple logs in the same day        
            file.WriteLine(JsonSerializer.Serialize(this.GetAll()) + ","); // add a comma to separate each log
            file.Close();
        }

        public Dictionary<string, object> GetAll() //Get all element of the class and place in a dictonary 
        {
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "Name", this.Name },
                { "FileSource", this.FileSource },
                { "FileDestination", this.FileDestination },
                { "FileSize", this.FileSize },
                { "FileTransfertTime", this.FileTransfertTime },
                { "Time", this.Time }
            };
            return log;
        }


        //Read log of the day ;
        public static void ReadLogOfTheDay()
        {
            string PathLog = "C:\\ProjetCsFT\\Log\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            string[] lines = System.IO.File.ReadAllLines(PathLog);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("Would you like to go back to the menu? / Voulez-vous revenir au menu ? (y)");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                Program.SaveData();
            } else
            {
                Environment.Exit(0);
            }
        }
    }
}

/*
         public static long GetDirectorySize(string path)
        {
            //Get array of all file names.
            string[] a = Directory.GetFiles(path, "*.*");

            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                //Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            //return total size
            return b;

        }
 */