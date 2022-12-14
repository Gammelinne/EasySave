using EasySaveApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
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
        public static bool is_first_save = true;

        public string Name { get => name; set => name = value; }
        public string PathSource { get => pathSource; set => pathSource = value; }
        public string PathDestination { get => pathDestination; set => pathDestination = value; }
        public string SaveType { get => saveType; set => saveType = value; }
        public State state { get; set; }
        public static bool pause = false;
        public static ManualResetEvent pauseEvent = new ManualResetEvent(false);
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public SaveChange SaveChangeEvent;

        public Save()
        {
            Name = "Save";
            PathSource = @"C:\";
            PathDestination = @"E:\";
            SaveType = "Complete";
            state = new State();
        }

        public Save(string name, string pathSource, string pathDestination, string saveType)
        {

            Name = name;
            PathSource = pathSource;
            PathDestination = pathDestination;
            SaveType = saveType;
            state = new State(name, pathSource, pathDestination, saveType, 0, 0, 0, "END", 0);
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

        public static void Stop()
        {
            cts.Cancel();
            MessageBox.Show("Please wait few second");
            GC.Collect();
        }

        public static void Pause()
        {
            pause = !pause;
            if (pause)
            {
                pauseEvent.Set();
                watch.Stop();
                if (!is_first_save)
                {
                    MessageBox.Show("Save resumed");
                }
            }
            else
            {
                watch.Start();
                pauseEvent.Reset();
                MessageBox.Show("Save paused");
            }
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

        static void Crypt(string pathSource, string pathDestination)
        {
            if (pathDestination.Contains(".crypt"))
            {
                pathDestination = pathDestination.Replace(".crypt", "");
            }
            else
            {
                pathDestination += ".crypt";
            }
            MessageBox.Show(pathDestination);


            string key = Application.Current.Properties["CryptKey"].ToString();
            Process cryptosoft;
            cryptosoft = new Process();
            cryptosoft.StartInfo.FileName = @"../../../CryptoSoft/Cryptosoft.exe";
            cryptosoft.StartInfo.Arguments = pathSource + " " + pathDestination + " " + key;
            cryptosoft.StartInfo.CreateNoWindow = true;
            cryptosoft.Start();
            cryptosoft.WaitForExit();
            cryptosoft.Kill();
        }

        public void SaveSave()
        {
            try
            {
                Pause();
                string status = "ACTIVE";
                string[] listOfPathFile = { };
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
                    FileInfo fileInfo = new FileInfo(oldPath);
                    string extension = fileInfo.Extension;
                    string[] extensionToCrypt = Application.Current.Properties["ExtensionToCrypt"].ToString().Split(" ");

                    if (cts.IsCancellationRequested) { return; }

                    pauseEvent.WaitOne();

                    string newPath = oldPath.Replace(PathSource, PathDestination + @"\" + Name);

                    if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                    }

                    if (SaveType == "Complete")
                    {
                        if (extensionToCrypt.Contains(extension) || extension == ".crypt")
                        {
                            Crypt(oldPath, newPath);
                        }
                        else
                        {
                            File.Copy(oldPath, newPath, true);
                        }
                    }
                    else if (SaveType == "Differential")
                    {
                        if (File.Exists(newPath))
                        {
                            FileInfo fileSource = new FileInfo(oldPath);
                            FileInfo fileDestination = new FileInfo(newPath);
                            if (fileSource.GetHashCode() != fileDestination.GetHashCode())
                            {
                                if (extensionToCrypt.Contains(extension))
                                {
                                    Crypt(oldPath, newPath);
                                }
                                else
                                {
                                    File.Copy(oldPath, newPath, true);
                                }
                            }
                        }
                        else
                        {
                            if (extensionToCrypt.Contains(extension))
                            {
                                Crypt(oldPath, newPath);
                            }
                            else
                            {
                                File.Copy(oldPath, newPath, true);
                            }
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
                    if (Application.Current.Properties["Socket"] != null)
                    {
                        string StatetoSend = "SaveName:" + state.Name + "\n PathSource:" + state.PathSource + "\n PathDestination:" + state.PathDestination + "\n StateType:" + state.StateType + "\n TotalFileToTransfer:" + state.TotalFileToTransfer + "\n FileLeftToTransfer:" + state.FileLeftToTransfer + "\n Progression:" + state.Progression + "\n Status:" + state.Status + "\n TotalFilesSize:" + state.TotalFilesSize;
                        Progression.SendMessage((Socket)Application.Current.Properties["Socket"], StatetoSend);

                    }
                }
                #endregion

                watch.Stop();
                MessageBox.Show("Save finished");

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

                is_first_save = false;
                MainViewModel.SaveHomeViewModelCommand.Execute(null);
                if (Application.Current.Properties["Socket"] != null)
                {
                    Progression.SendMessage((Socket)Application.Current.Properties["Socket"], "<END>");

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}