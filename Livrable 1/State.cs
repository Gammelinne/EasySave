using System;
using System.Collections.Generic;
using System.Text;
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

        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }
        public string StateType { get => stateType; set => stateType = value; }
        public int TotalFileToTransfer { get => totalFileToTransfer; set => totalFileToTransfer = value; }
        public int FileLeftToTransfer { get => fileLeftToTransfer; set => fileLeftToTransfer = value; }
        public int Progression { get => progression; set => progression = value; }

        public State(string name, string fileSource, string fileDestination, string stateType, int totalFileToTransfer, int fileLeftToTransfer, int progression)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            StateType = stateType;
            TotalFileToTransfer = totalFileToTransfer;
            FileLeftToTransfer = fileLeftToTransfer;
            Progression = progression;
        }
        
        public void SaveState(){
            string PathState = "C:\\ProjetCsFT\\State\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            System.IO.StreamWriter file = new System.IO.StreamWriter(PathState, true); // true = append file for multiple logs in the same day
            file.WriteLine(JsonSerializer.Serialize(this.GetAll()) + ","); // add a comma to separate each log
            file.Close();
        }

        public Dictionary<string, object> GetAll() //Get all element of the class and place in a dictonary 
        {
            Dictionary<string, object> state = new Dictionary<string, object>
            {
                { "Name", this.Name },
                { "FileSource", this.FileSource },
                { "FileDestination", this.FileDestination },
                { "StateType", this.StateType },
                { "TotalFileToTransfer", this.TotalFileToTransfer },
                { "FileLeftToTransfer", this.FileLeftToTransfer },
                { "Progression", this.Progression }
            };
            return state;
        }

        public static void ReadStateOfTheDay()
        {
            string PathState = "C:\\ProjetCsFT\\State\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            if (System.IO.File.Exists(PathState))
            {
                string[] lines = System.IO.File.ReadAllLines(PathState);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No state file found for today");
            }
        }
    }
}
