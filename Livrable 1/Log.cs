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

        public Log(string name, string fileSource, string fileDestination, int fileSize, double fileTransfertTime, DateTime time)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            FileSize = fileSize;
            FileTransfertTime = fileTransfertTime;
            Time = time;
        }

        public void SaveLog(string extension)
        {
            Console.WriteLine(extension);
            if (extension == "1")
            {
                extension = "json";
            }
            else if (extension == "2")
            {
                extension = "xml";
            }
            Console.WriteLine(extension);
            //Check if all directory exist
            string PathLog = Directory.GetCurrentDirectory() + @"\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + "." + extension;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Log\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Log\");
            }

            //Create a valid Json
            #region
            if (extension == "json")
            {
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

            }
            else if (extension == "xml")
            {
                if (!File.Exists(PathLog))
                {
                    File.WriteAllText(PathLog, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<State>\n</State>");
                }
                string data = File.ReadAllText(PathLog);
                data = data.Remove(data.LastIndexOf("</Log>"), 1);
                if (data.LastIndexOf("</Log>") != -1)
                {
                    data = data.Insert(data.LastIndexOf("</Log>") + 1, ",\n");
                    File.WriteAllText(PathLog, string.Empty);
                    File.WriteAllText(PathLog, data);
                }
                File.WriteAllText(PathLog, data + GetAll() + "</Log>");
            }
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
                Program.SaveData();
            }
            else if (answer == "n" || answer == "N")
            {
                Environment.Exit(0);
            }
        }
    }
}
