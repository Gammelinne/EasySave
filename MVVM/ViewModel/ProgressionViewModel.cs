using EasySaveApp.MVVM.Model;
using System;
using System.Collections.Generic;
using EasySaveApp.Core;
using EasySaveApp.MVVM.View;
using System.Text;
using System.Windows;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class ProgressionViewModel : ObservableObject
    {
        private Progression _progression;

        public ProgressionViewModel()
        {
            _progression = new Progression();
        }

        public int ProgressionValue
        {
            get { return _progression.ProgressionValue; }
            set
            {
                _progression.ProgressionValue = value;
                OnPropertyChanged("ProgressionValue");
            }
        }
    }
}
