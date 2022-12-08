using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EasySaveApp.MVVM.Model
{
    internal class Progression
    {
        private int progressionValue;
        private int fileLeftToTransfert;
        private int fileTransfered;

        public int ProgressionValue { get => progressionValue; set => progressionValue = value; }
        public int FileLeftToTransfert { get => fileLeftToTransfert; set => fileLeftToTransfert = value; }
        public int FileTransfered { get => fileTransfered; set => fileTransfered = value; }

        public Progression()
        {
            //int filelefttotransfert = (int)Application.Current.Properties["FileLeft"];
            //int filetransfered = (int)Application.Current.Properties["TotalFile"];
            //ProgressionValue = (filetransfered * 100) / filelefttotransfert;
            //FileLeftToTransfert = filelefttotransfert;
            //FileTransfered = filetransfered - filelefttotransfert;
        }
       
    }
}
