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
        public double FileTransfertTime;
        public DateTime Time;

        public Log(string name, string fileSource, string fileDestination, int fileSize,  double fileTransfertTime, DateTime time)
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
            //Check if all directory exist
            string PathLog = @"C:\EasySave\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (!Directory.Exists(@"C:\EasySave"))
            {
                Directory.CreateDirectory(@"C:\EasySave");
            }
            if (!Directory.Exists(@"C:\EasySave\Log\"))
            {
                Directory.CreateDirectory(@"C:\EasySave\Log\");
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
            string PathLog = @"C:\EasySave\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
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
