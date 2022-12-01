using EasySaveApp.MVVM.Model;
using System;
using System.IO;
using System.Timers;

namespace EasySaveApp.MVVM.Model
{
    class Save
    {
        //Timer class
        public static int secondsCount = 0;
        public static Timer timer = new Timer(1000);

        private string name;
        private string fileSource;
        private string fileDestination;
        private string fileType;

        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }
        public string SaveType { get => fileType; set => fileType = value; }

        public Save(){
            Name = "Save";
            FileSource = "C:\\";
            FileDestination = "E:\\";
            SaveType = "1";
        }

        public Save(string name, string fileSource, string fileDestination, string saveType)
        {
            Name = name;
            FileSource = fileSource;
            FileDestination = fileDestination;
            SaveType = saveType;
        }

        public void SaveSave()
        {
            int count = 0;
            string[] directory = { };

            //Start timer
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
            secondsCount++;

            try
            {
                directory = Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            long size = GetDirectorySize(FileSource);

            // We get the files from the source folder
            foreach (string newPath in directory)
            {
                //Check if the file exist
                if (!Directory.Exists(Path.GetDirectoryName(newPath.Replace(FileSource, FileDestination + @"\" + Name))))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newPath.Replace(FileSource, FileDestination + @"\" + Name)));
                }

                //If we are in differential backup and the file does not exist, we copy the files
                if (!File.Exists(newPath.Replace(FileSource, FileDestination + @"\" + Name)) && SaveType == "Differential")
                {
                    File.Copy(newPath, newPath.Replace(FileSource, FileDestination + @"\" + Name), true);
                    count++;
                    Console.WriteLine(count + " out of " + directory.Length + " files copied");
                }

                //If we are in differential backup and the file exist, we check if the size of the source file is different from the size of the destination file
                else if (File.Exists(newPath.Replace(FileSource, FileDestination + @"\" + Name)) && SaveType == "Complete")
                {
                    FileInfo file1 = new FileInfo(newPath);
                    //we get the information from the destination file
                    FileInfo file2 = new FileInfo(newPath.Replace(FileSource, FileDestination + @"\" + Name));

                    if (file1.Length != file2.Length)
                    {
                        File.Copy(newPath, newPath.Replace(FileSource, FileDestination + @"\" + Name), true);
                        count++;
                        Console.WriteLine(count + " out of " + directory.Length + " files copied");
                    }
                }

                //If we are in full backup,we copy all files
                else if (SaveType == "1")
                {
                    File.Copy(newPath, newPath.Replace(FileSource, FileDestination + @"\" + Name), true);
                    count++;
                    Console.WriteLine(count + " out of " + directory.Length + " files copied");
                }

                //Create a state
                string Status = "ACTIVES";
                State state = new State(Name, FileSource, FileDestination, SaveType,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length - count, count, Status, (int)size);

                //Save state and change status
                if (state.FileLeftToTransfer > 30 && state.FileLeftToTransfer % 10 == 0)
                {
                    state.SaveState();
                }

                if (state.FileLeftToTransfer < 30 && state.FileLeftToTransfer % 10 != 0)
                {
                    state.SaveState();
                }

                if (state.FileLeftToTransfer == 0)
                {
                    Status = "END";
                    State endState = new State(Name, FileSource, FileDestination, SaveType,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length,
                    Directory.GetFiles(FileSource, "*.*", SearchOption.AllDirectories).Length - count, count, Status, (int)size);
                    endState.SaveState();
                }
            }

            //Create a log
            Log log = new Log(Name, FileSource, FileDestination, (int)size, secondsCount, DateTime.Now);
            log.SaveLog();

            // Get directory size
            static long GetDirectorySize(string path)
            {
                // Get array of all file names.
                string[] filesName = Directory.GetFiles(path, "*.*");

                // Calculate total bytes of all files
                long countByte = 0;
                foreach (string name in filesName)
                {
                    FileInfo info = new FileInfo(name);
                    countByte += info.Length;
                }
                return countByte;
            }

            void Timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                secondsCount++;
            }
        }
    }
}

