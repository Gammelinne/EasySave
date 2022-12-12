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
        private int fileTotal;

        public int ProgressionValue { get => progressionValue; set => progressionValue = value; }
        public int FileLeftToTransfert { get => fileLeftToTransfert; set => fileLeftToTransfert = value; }
        public int FileTotal { get => fileTotal; set => fileTotal = value; }

        public Progression()
        {
            ProgressionValue = 0;
            FileLeftToTransfert = 0;
            FileTotal = 0;
        }
    }
}
