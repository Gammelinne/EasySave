using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace EasySaveApp.MVVM.Model
{
    class Save
    {
        public delegate void SaveChange(State state);  
        public static Stopwatch watch = new Stopwatch();

        private string name;
        private string pathSource;
        private string pathDestination;
        private string saveType;

        public string Name { get => name; set => name = value; }
        public string PathSource { get => pathSource; set => pathSource = value; }
        public string PathDestination { get => pathDestination; set => pathDestination = value; }
        public string SaveType { get => saveType; set => saveType = value; }
        public State state { get; set; }

        public SaveChange SaveChangeEvent;

        
        public Save()
        {
            //Name = "Save";
            //PathSource = @"C:\";
            //PathDestination = @"E:\";
            //SaveType = "Complete";
            Name = "SavePriority";
            PathSource = @"C:\Users\lefra\Documents\Save\In";
            PathDestination = @"C:\Users\lefra\Documents\Save\Out";
            SaveType = "Complete";
            state = new State();
        }

        public Save(string name, string pathSource, string pathDestination, string saveType)
        {
            Name = name;
            PathSource = pathSource;
            PathDestination = pathDestination;
            SaveType = saveType;
            state = new State(name, pathSource, pathDestination, saveType, 0,0,0,"END", 0);
        }

        public void AddSaveChange(SaveChange listener)
        {
            SaveChangeEvent += listener;
        }

        // Get directory size
        static int GetDirectorySize(string path)
        {
            string[] filesName = Directory.GetFiles(path, "*.*");
            long numberOfFileByte = 0;
            foreach (string name in filesName)
            {
                FileInfo info = new FileInfo(name);
                numberOfFileByte += info.Length;
            }
            return (int)numberOfFileByte;
        }

        public List<string> CheckPriority(string[] oldList)
        {
            List<string> files = new List<string>(oldList);
            List<string> extensions = Application.Current.Properties["PriorityFiles"].ToString().Split(" ").ToList();
            List<string> newList = new List<string>();
            foreach (string extension in extensions)
            {
                newList.AddRange(files.Where(f => f.EndsWith(extension)));
                files = files.Where(f => !f.EndsWith(extension)).ToList();
            }
            newList.AddRange(files);
            return newList;
        }

        public void SaveSave()
        {
            try
            {
                string status = "ACTIVE";
                string[] listOfPathFile = {};
                int size = GetDirectorySize(PathSource);
                Directory.CreateDirectory(PathDestination + @"\" + Name);
                listOfPathFile = Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories);
                listOfPathFile = CheckPriority(listOfPathFile).ToArray();
                int fileLeft = listOfPathFile.Length;
                watch.Start();

                //Create a save and a state per file
                #region
                foreach (string oldPath in listOfPathFile)
                {
                    MessageBox.Show(oldPath);
                    string newPath = oldPath.Replace(PathSource, PathDestination + @"\" + Name);

                    if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                    }

                    if (SaveType == "Complete")
                    {
                        File.Copy(oldPath, newPath, true);
                    }
                    else if (SaveType == "Differential")
                    {
                        if (File.Exists(newPath))
                        {
                            FileInfo fileSource = new FileInfo(oldPath);
                            FileInfo fileDestination = new FileInfo(newPath);
                            if (fileSource.GetHashCode() != fileDestination.GetHashCode())
                            {
                                File.Copy(oldPath, newPath, true);
                            }
                        }
                        else
                        {
                            File.Copy(oldPath, newPath, true);
                        }
                    }

                    fileLeft--;

                    if (fileLeft == 0)
                    {
                        status = "END";
                    }
                    state.PathSource = PathSource;
                    state.PathDestination = PathDestination;
                    state.StateType = SaveType;
                    state.TotalFileToTransfer = listOfPathFile.Length;
                    state.FileLeftToTransfer = fileLeft;
                    state.Progression = (int)((1.0 - ((double)fileLeft / (double)listOfPathFile.Length)) * 100);
                    state.Status = status;
                    state.TotalFilesSize = size;

                    state.SaveState(Application.Current.Properties["TypeOfLog"].ToString());
                    SaveChangeEvent(state);
                    
                }
                #endregion

                watch.Stop();

                //Create a log
                #region
                Log log = new Log(
                    Name, 
                    PathSource, 
                    PathDestination, 
                    (int)size,
                    watch.ElapsedMilliseconds, 
                    DateTime.Now);
                log.SaveLog(Application.Current.Properties["TypeOfLog"].ToString());
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}