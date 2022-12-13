using EasySaveApp.MVVM.ViewModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
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

        public string Name { get => name; set => name = value; }
        public string PathSource { get => pathSource; set => pathSource = value; }
        public string PathDestination { get => pathDestination; set => pathDestination = value; }
        public string SaveType { get => saveType; set => saveType = value; }
        public State state { get; set; }
        public static bool pause = false;
        public static bool is_first_save = true;
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

        public void SaveSave()
        {
            try
            {
                if (is_first_save)
                {
                    Save.Pause();
                }
                string status = "ACTIVE";
                string[] listOfPathFile = { };
                int size = GetDirectorySize(PathSource);
                Directory.CreateDirectory(PathDestination + @"\" + Name);
                listOfPathFile = Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories);
                int fileLeft = listOfPathFile.Length;
                watch.Start();

                //Create a save and a state per file
                #region
                foreach (string oldPath in listOfPathFile)
                {
                    if (cts.IsCancellationRequested)
                    {
                        //reset save

                        return;
                    }
                    pauseEvent.WaitOne();

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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}