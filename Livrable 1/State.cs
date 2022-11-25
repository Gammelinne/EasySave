using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Livrable_1
{
    internal class State
    {
        private string name;
        private string fileSource;
        private string fileDestination;
        private string stateType;
        private int totalFileToTransfer;
        private int fileLeftToTransfer;
        private int progression;
        private string status;
        private int totalFilesSize;

        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }
        public string StateType { get => stateType; set => stateType = value; }
        public int TotalFileToTransfer { get => totalFileToTransfer; set => totalFileToTransfer = value; }
        public int FileLeftToTransfer { get => fileLeftToTransfer; set => fileLeftToTransfer = value; }
        public int Progression { get => progression; set => progression = value; }
        public string Status { get => status; set => status = value; }
        public int TotalFilesSize { get => totalFilesSize; set => totalFilesSize = value; }

        public State(string name, string fileSource, string fileDestination, string stateType, int totalFileToTransfer, int fileLeftToTransfer, int progression, string status, int totalFilesSize)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            StateType = stateType;
            TotalFileToTransfer = totalFileToTransfer;
            FileLeftToTransfer = fileLeftToTransfer;
            Progression = progression;
            Status = status;
            TotalFilesSize = totalFilesSize;
        }

        public void SaveState()
        {
            string PathState = Directory.GetCurrentDirectory() + @"\State\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\State\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\State\");
            }

            //Create a valid Json
            #region
            if (!File.Exists(PathState))
            {
                File.WriteAllText(PathState, "[]");
            }
            string data = File.ReadAllText(PathState);
            data = data.Remove(data.LastIndexOf("]"), 1);
            if (data.LastIndexOf("}") != -1)
            {
                data = data.Insert(data.LastIndexOf("}") + 1, ",\n");
                File.WriteAllText(PathState, string.Empty);
                File.WriteAllText(PathState, data);
            }
            File.WriteAllText(PathState, data + JsonSerializer.Serialize(GetAll()) + "]");
            #endregion
        }

        //Get all element of the class and place in a dictonary
        public Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> state = new Dictionary<string, object>
            {
                { "Name", Name },
                { "FileSource", FileSource },
                { "FileDestination", FileDestination },
                { "StateType", StateType },
                { "TotalFileToTransfer", TotalFileToTransfer },
                { "FileLeftToTransfer", FileLeftToTransfer },
                { "Progression", Progression },
                { "Status", Status },
                { "TotalFilesSize", TotalFilesSize }
            };
            return state;
        }

        public static void ReadStateOfTheDay()
        {
            string PathState = Directory.GetCurrentDirectory() + @"\State\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (File.Exists(PathState))
            {
                string[] lines = File.ReadAllLines(PathState);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No state for today");
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
