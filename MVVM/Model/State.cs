using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EasySaveApp.MVVM.Model
{
    internal class State
    {
        private string name;
        private string pathSource;
        private string pathDestination;
        private string stateType;
        private int totalFileToTransfer;
        private int fileLeftToTransfer;
        private int progression;
        private string status;
        private int totalFilesSize;

        public string Name { get => name; set => name = value; }
        public string PathSource { get => pathSource; set => pathSource = value; }
        public string PathDestination { get => pathDestination; set => pathDestination = value; }
        public string StateType { get => stateType; set => stateType = value; }
        public int TotalFileToTransfer { get => totalFileToTransfer; set => totalFileToTransfer = value; }
        public int FileLeftToTransfer { get => fileLeftToTransfer; set => fileLeftToTransfer = value; }
        public int Progression { get => progression; set => progression = value; }
        public string Status { get => status; set => status = value; }
        public int TotalFilesSize { get => totalFilesSize; set => totalFilesSize = value; }

        public State(string name, string pathSource, string pathDestination, string stateType, int totalFileToTransfer, int fileLeftToTransfer, int progression, string status, int totalFilesSize)
        {
            Name = name;
            PathSource = pathSource;
            PathDestination = pathDestination;
            StateType = stateType;
            TotalFileToTransfer = totalFileToTransfer;
            FileLeftToTransfer = fileLeftToTransfer;
            Progression = progression;
            Status = status;
            TotalFilesSize = totalFilesSize;
        }

        public State() {}

        public void SaveState(string extension)
        {
            string PathState = Directory.GetCurrentDirectory() + @"\State\" + DateTime.Now.ToString("dd-MM-yyyy") + "." + extension;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\State\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\State\");
            }

            //Create a valid Json
            #region
            if (extension == "json")
            {
                if (!File.Exists(PathState))
                {
                    File.WriteAllText(PathState, "[]");
                }
                {
                    string data = File.ReadAllText(PathState);
                    data = data.Remove(data.LastIndexOf("]"), 1);
                    if (data.LastIndexOf("}") != -1)
                    {
                        data = data.Insert(data.LastIndexOf("}") + 1, ",\n");
                        File.WriteAllText(PathState, string.Empty);
                        File.WriteAllText(PathState, data);
                    }
                    File.WriteAllText(PathState, data + JsonSerializer.Serialize(GetAllJson()) + "]");
                }
            }
            else if (extension == "xml")
            {
                if (!File.Exists(PathState))
                {
                    File.WriteAllText(PathState, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<State>\n</State>");
                }

                string data = File.ReadAllText(PathState);
                data = data.Remove(data.LastIndexOf("</State>"), 6);
                if (data.LastIndexOf("</State>") != -1)
                {
                    data = data.Insert(data.LastIndexOf("</State>") + 6, "\n");
                    File.WriteAllText(PathState, string.Empty);
                    File.WriteAllText(PathState, data);
                }
                File.WriteAllText(PathState, data + GetAllXML() + "\n</State>");
            }
            #endregion
        }

        //Get all element of the class and place in a dictonary
        public Dictionary<string, object> GetAllJson()
        {
            Dictionary<string, object> state = new Dictionary<string, object>
            {
                { "Name", Name },
                { "PathSource", PathSource },
                { "PathDestination", PathDestination },
                { "StateType", StateType },
                { "TotalFileToTransfer", TotalFileToTransfer },
                { "FileLeftToTransfer", FileLeftToTransfer },
                { "Progression", Progression },
                { "Status", Status },
                { "TotalFilesSize", TotalFilesSize }
            };
            return state;
        }
        public string GetAllXML()
        {
            string state = "<State>\n";
            state += "\t<Name>" + Name + "</Name>\n";
            state += "\t<PathSource>" + PathSource + "</PathSource>\n";
            state += "\t<PathDestination>" + PathDestination + "</PathDestination>\n";
            state += "\t<StateType>" + StateType + "</StateType>\n";
            state += "\t<TotalFileToTransfer>" + TotalFileToTransfer + "</TotalFileToTransfer>\n";
            state += "\t<FileLeftToTransfer>" + FileLeftToTransfer + "</FileLeftToTransfer>\n";
            state += "\t<Progression>" + Progression + "</Progression>\n";
            state += "\t<Status>" + Status + "</Status>\n";
            state += "\t<TotalFilesSize>" + TotalFilesSize + "</TotalFilesSize>\n";
            state += "</State>\n";
            return state;
        }
    }
}