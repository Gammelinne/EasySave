using EasySaveApp.MVVM.Model;
using System;
using System.Collections.Generic;
using EasySaveApp.Core;
using EasySaveApp.MVVM.View;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;

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

        public string FileLeftToTransfert
        {
            get
            {
                return _progression.FileLeftToTransfert + " files left to transfert of " + _progression.FileTotal.ToString() + " files ( " + _progression.ProgressionValue.ToString() + "% )";
            }
            set
            {
                _progression.FileLeftToTransfert = Convert.ToInt32(value);
                OnPropertyChanged("FileLeftToTransfert");
            }
        }

        public int FileTotal
        {
            get { return _progression.FileTotal; }
            set
            {
                _progression.FileTotal = value;
                OnPropertyChanged("FileTotal");
            }
        }

        private RelayCommand _stopCommand;

        public RelayCommand StopCommand => _stopCommand ??= new RelayCommand(
                    async o =>
                    {
                        await Task.Run(() => Save.Stop());
                        ProgressionValue = 0;
                        FileLeftToTransfert = "0";
                        FileTotal = 0;
                        MainViewModel.SaveHomeViewModelCommand.Execute(null);
                    });

        private RelayCommand _pauseCommand;

        public RelayCommand PauseCommand => _pauseCommand ??= new RelayCommand(
                    async o =>
                    {
                        await Task.Run(() => Save.Pause());
                    });

    }
}
