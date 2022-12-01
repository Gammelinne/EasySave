using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using EasySaveApp.Core;
using EasySaveApp.MVVM.Model;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class SaveViewModel : INotifyPropertyChanged
    {
        private Save _save;

        public SaveViewModel()
        {
            _save = new Save();
        }
        public string FileName
        {
            get { return _save.Name; }
            set
            {
                {
                    _save.Name = value;
                    OnPropertyChanged("FileName");
                }
            }
        }

        public string FileSource
        {
            get { return _save.FileSource; }
            set
            {
                _save.FileSource = value;
                OnPropertyChanged("FileSource");
            }

        }

        public string FileDestination
        {
            get { return _save.FileDestination; }
            set
            {
                _save.FileDestination = value;
                OnPropertyChanged("FileDestination");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private RelayCommand _browseSourceCommand;

        public RelayCommand BrowseSourceCommand
        {
            get
            {
                return _browseSourceCommand ?? (_browseSourceCommand = new RelayCommand(
                    o =>
                    {
                        var dialog = new System.Windows.Forms.FolderBrowserDialog();
                        System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            _save.FileSource = dialog.SelectedPath;
                            OnPropertyChanged("FileSource");
                        }
                    }));
            }
        }

        private RelayCommand _browseDestinationCommand;

        public RelayCommand BrowseDestinationCommand
        {
            get
            {
                return _browseDestinationCommand ??= new RelayCommand(
                    o =>
                    {
                        var dialog = new System.Windows.Forms.FolderBrowserDialog();
                        System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            _save.FileDestination = dialog.SelectedPath;
                            OnPropertyChanged("FileDestination");
                        }
                    });
            }
        }
    }
}
