using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Livrable_1
{
    internal class State
    {
        public string Name;
        public string FileSource;
        public string FileDestination;
        public string StateType;
        public int TotalFileToTransfer;
        public int FileLeftToTransfer;
        public int Progression;
        
        public State(string name, string fileSource, string fileDestination, string stateType, int totalFileToTransfer, int fileLeftToTransfer, int progression)
        {
            this.Name = name;
            this.FileSource = fileSource;
            this.FileDestination = fileDestination;
            this.StateType = stateType;
            this.TotalFileToTransfer = totalFileToTransfer;
            this.FileLeftToTransfer = fileLeftToTransfer;
            this.Progression = progression;
        }
        
        public void SaveState(){
            string PathState = "C:\\ProjetCsFT\\State\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
            System.IO.StreamWriter file = new System.IO.StreamWriter(PathState, true); // true = append file for multiple logs in the same day
            file.WriteLine(JsonSerializer.Serialize(this.GetAll()) + ","); // add a comma to separate each log
            file.Close();
        }

        public Dictionary<string, object> GetAll() //Get all element of the class and place in a dictonary 
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            state.Add("Name", this.Name);
            state.Add("FileSource", this.FileSource);
            state.Add("FileDestination", this.FileDestination);
            state.Add("StateType", this.StateType);
            state.Add("TotalFileToTransfer", this.TotalFileToTransfer);
            state.Add("FileLeftToTransfer", this.FileLeftToTransfer);
            state.Add("Progression", this.Progression);
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
