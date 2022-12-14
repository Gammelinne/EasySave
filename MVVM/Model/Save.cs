using EasySaveApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

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
            System.Windows.MessageBox.Show("Please wait few second");
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
                    System.Windows.MessageBox.Show("Save resumed");
                }
            }
            else
            {
                watch.Start();
                pauseEvent.Reset();
                System.Windows.MessageBox.Show("Save paused");
            }
        }
        public void ChangeProgression()
        {

        }

        public List<string> CheckPriority(string[] oldList)
        {
            List<string> files = new List<string>(oldList);
            List<string> extensions = System.Windows.Application.Current.Properties["PriorityFiles"].ToString().Split(" ").ToList();
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
            System.Windows.MessageBox.Show(pathDestination);


            string key = System.Windows.Application.Current.Properties["CryptKey"].ToString();
            Process cryptosoft;
            cryptosoft = new Process();
            cryptosoft.StartInfo.FileName = @"../../../CryptoSoft/Cryptosoft.exe";
            cryptosoft.StartInfo.Arguments = pathSource + " " + pathDestination + " " + key;
            cryptosoft.StartInfo.CreateNoWindow = true;
            cryptosoft.Start();
            cryptosoft.WaitForExit();
            cryptosoft.Kill();
        }

        public bool CheckSize(int Size, string Name)
        {
            int SizeMax = Convert.ToInt32(System.Windows.Application.Current.Properties["FileSizeMax"]) * 1000000;
            if (SizeMax > Size)
            {
                return true;
            }
            else
            {
                if (System.Windows.Forms.MessageBox.Show("The file " + Name + " is heavier than the limit. Do you still want to back it up? ", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }
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
                    //get size of the file
                    FileInfo info = new FileInfo(oldPath);
                    int sizeFile = (int)info.Length;
                    string Name = info.Name;
                    bool is_save = CheckSize(sizeFile, Name);
                    if (is_save)
                    {
                        FileInfo fileInfo = new FileInfo(oldPath);
                        string extension = fileInfo.Extension;
                        string[] extensionToCrypt = System.Windows.Application.Current.Properties["ExtensionToCrypt"].ToString().Split(" ");

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


                    }
                    if (fileLeft == 0)
                    {
                        status = "END";
                    }
                    fileLeft--;

                    state.PathSource = PathSource;
                    state.PathDestination = PathDestination;
                    state.StateType = SaveType;
                    state.TotalFileToTransfer = listOfPathFile.Length;
                    state.FileLeftToTransfer = fileLeft;
                    state.Progression = (int)((1.0 - ((double)fileLeft / (double)listOfPathFile.Length)) * 100);
                    state.Status = status;
                    state.TotalFilesSize = size;

                    state.SaveState(System.Windows.Application.Current.Properties["TypeOfLog"].ToString());
                    SaveChangeEvent(state);
                    if (System.Windows.Application.Current.Properties["Socket"] != null)
                    {
                        string StatetoSend = "SaveName:" + state.Name + "\n PathSource:" + state.PathSource + "\n PathDestination:" + state.PathDestination + "\n StateType:" + state.StateType + "\n TotalFileToTransfer:" + state.TotalFileToTransfer + "\n FileLeftToTransfer:" + state.FileLeftToTransfer + "\n Progression:" + state.Progression + "\n Status:" + state.Status + "\n TotalFilesSize:" + state.TotalFilesSize;
                        Progression.SendMessage((Socket)System.Windows.Application.Current.Properties["Socket"], StatetoSend);
                    }

                }
                #endregion

                watch.Stop();
                System.Windows.MessageBox.Show("Save finished");

                //Create a log
                #region
                Log log = new Log(
                    Name,
                    PathSource,
                    PathDestination,
                    (int)size,
                    watch.ElapsedMilliseconds,
                    DateTime.Now);
                log.SaveLog(System.Windows.Application.Current.Properties["TypeOfLog"].ToString());
                #endregion

                is_first_save = false;
                MainViewModel.SaveHomeViewModelCommand.Execute(null);
                if (System.Windows.Application.Current.Properties["Socket"] != null)
                {
                    Progression.SendMessage((Socket)System.Windows.Application.Current.Properties["Socket"], "<END>");
                    System.Windows.Application.Current.Properties["Socket"] = null;

                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
    }
}