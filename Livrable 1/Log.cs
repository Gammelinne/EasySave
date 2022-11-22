using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        //open the file and read the content
        
        public static void ReadLogOfTheDay()
        {
            string PathLog = "C:\\ProjetCsFT\\Log\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (System.IO.File.Exists(PathLog))
            {
                string[] lines = System.IO.File.ReadAllLines(PathLog);
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
