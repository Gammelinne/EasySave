using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Livrable_1
{
    internal class Log
    {
        public string Name;
        public string FileSource;
        public string FileDestination;
        public int FileSize; // in bytes
        public double FileTransfertTime; // in ms.
        public DateTime Time;

        public Log(string name, string fileSource, string fileDestination, int fileSize, double fileTransfertTime, DateTime Time)
        {
            this.Name = name;
            this.FileSource = fileSource;
            this.FileDestination = fileDestination;
            this.FileSize = fileSize;
            this.FileTransfertTime = fileTransfertTime;
            this.Time = Time;
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
            Dictionary<string, object> log = new Dictionary<string, object>();
            log.Add("Name", this.Name);
            log.Add("FileSource", this.FileSource);
            log.Add("FileDestination", this.FileDestination);
            log.Add("FileSize", this.FileSize);
            log.Add("FileTransfertTime", this.FileTransfertTime);
            log.Add("Time", this.Time);
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
