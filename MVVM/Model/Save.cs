using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace EasySaveApp.MVVM.Model
{
    class Save
    {
        public static Stopwatch watch = new Stopwatch();

        private string name;
        private string pathSource;
        private string pathDestination;
        private string saveType;

        public string Name { get => name; set => name = value; }
        public string PathSource { get => pathSource; set => pathSource = value; }
        public string PathDestination { get => pathDestination; set => pathDestination = value; }
        public string SaveType { get => saveType; set => saveType = value; }

        public Save()
        {
            Name = "Save";
            PathSource = @"C:\";
            PathDestination = @"E:\";
            SaveType = "Complete";
        }

        public Save(string name, string pathSource, string pathDestination, string saveType)
        {
            Name = name;
            PathSource = pathSource;
            PathDestination = pathDestination;
            SaveType = saveType;
        }

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

        public void SaveSave()
        {
            try
            {
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
                    FileInfo fileInfo = new FileInfo(oldPath);
                    string extension = fileInfo.Extension;
                    string[] extensionToCrypt = Application.Current.Properties["ExtensionToCrypt"].ToString().Split(" ");
                    string newPath = oldPath.Replace(PathSource, PathDestination + @"\" + Name);

                    if (!Directory.Exists(Path.GetDirectoryName(newPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                    }

                    if (SaveType == "Complete")
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

                    State state = new State(
                        Name,
                        PathSource,
                        PathDestination,
                        SaveType,
                        listOfPathFile.Length,
                        fileLeft,
                        listOfPathFile.Length - fileLeft,
                        status,
                        size);

                    state.SaveState(Application.Current.Properties["TypeOfLog"].ToString());
                    Application.Current.Properties["FileLeft"] = fileLeft;
                    Application.Current.Properties["TotalFile"] = listOfPathFile.Length;
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
    
        static void Crypt(string pathSource, string pathDestination)
        {
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
    
    }
}