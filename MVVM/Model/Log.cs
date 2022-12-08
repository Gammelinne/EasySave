using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EasySaveApp.MVVM.Model
{
    internal class Log
    {
        public string Name;
        public string PathSource;
        public string PathDestination;
        public int FileSize;
        public double FileTransfertTime;
        public DateTime Time;

        public Log(string name, string pathSource, string pathDestination, int fileSize, double fileTransfertTime, DateTime time)
        {
            Name = name;
            PathSource = pathSource;
            PathDestination = pathDestination;
            FileSize = fileSize;
            FileTransfertTime = fileTransfertTime;
            Time = time;
        }

        public void SaveLog(string extension)
        {
            //Check if all directory exist
            string PathLog = Directory.GetCurrentDirectory() + @"\Log\" + DateTime.Now.ToString("dd-MM-yyyy") + "." + extension;
            Console.WriteLine(PathLog);
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Log\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Log\");
            }

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
                File.WriteAllText(PathLog, data + JsonSerializer.Serialize(GetAllJson()) + "]");

            }
            else if (extension == "xml")
            {
                if (!File.Exists(PathLog))
                {
                    File.WriteAllText(PathLog, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Log>\n</Log>");
                }

                string data = File.ReadAllText(PathLog);
                data = data.Remove(data.LastIndexOf("</Log>"), 6);
                if (data.LastIndexOf("</Log>") != -1)
                {
                    data = data.Insert(data.LastIndexOf("</Log>") + 6, "\n");
                    File.WriteAllText(PathLog, string.Empty);
                    File.WriteAllText(PathLog, data);
                }
                File.WriteAllText(PathLog, data + GetAllXML() + "\n</Log>");
            }
            #endregion
        }

        //Get all element of the class and place in a dictonary 
        public Dictionary<string, object> GetAllJson()
        {
            Dictionary<string, object> log = new Dictionary<string, object>
            {
                { "Name", Name },
                { "PathSource", PathSource },
                { "PathDestination", PathDestination },
                { "FileSize", FileSize },
                { "FileTransfertTime", FileTransfertTime },
                { "Time", Time }
            };
            return log;
        }

        public string GetAllXML()
        {
            string log = "\t<Log>\n";
            log += "\t\t<Name>" + Name + "</Name>\n";
            log += "\t\t<PathSource>" + PathSource + "</PathSource>\n";
            log += "\t\t<PathDestination>" + PathDestination + "</PathDestination>\n";
            log += "\t\t<FileSize>" + FileSize + "</FileSize>\n";
            log += "\t\t<FileTransfertTime>" + FileTransfertTime + "</FileTransfertTime>\n";
            log += "\t\t<Time>" + Time + "</Time>\n";
            log += "\t</Log>";
            return log;
        }
    }
}