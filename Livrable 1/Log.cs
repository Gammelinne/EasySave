using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Livrable_1
{
    internal class Log
    {
        public string Name;
        public string FileSource;
        public string FileDestination;
        public int FileSize;
        public double FileTransfertTime; // in ms.
        public DateTime Time;

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
            string PathLog = @"C:\ProjetCsFT\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
           
            if (!File.Exists(PathLog))
            {
                StreamWriter newFile = new StreamWriter(PathLog, true);
                newFile.WriteLine("[]");
                newFile.Close();
            }

            string data = File.ReadAllText(PathLog);
            data = data.Remove(data.Length - 3, 1);
            File.WriteAllText(PathLog, string.Empty);
            StreamWriter file = new StreamWriter(PathLog, true);
            file.WriteLine(data + JsonSerializer.Serialize(GetAll()) + ",\n]");
            file.Close();
        }

        //Get all element of the class and place in a dictonary 
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

        //Open log file and read the content
        public static void ReadLogOfTheDay()
        {
            string PathLog = @"C:\ProjetCsFT\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
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
        }
    }
}
