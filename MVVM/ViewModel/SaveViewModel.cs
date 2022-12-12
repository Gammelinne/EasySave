using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using EasySaveApp.Core;
using EasySaveApp.MVVM.Model;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class SaveViewModel : INotifyPropertyChanged
    {
        private Save _save;

        public SaveViewModel(){ 
            _save = new Save();
            _save.AddSaveChange(OnSaveChanged);

        }
        public string FileName
        {
            get { return _save.Name; }
            set 
            { 
                _save.Name = value;
                OnPropertyChanged("FileName");
            }
        }

        public string SaveType
        {
            get { return _save.SaveType; }
            set
            {
                string[] name = value.Split(':');
                string[] type = name[1].Split(' ');
                _save.SaveType = type[1];
                OnPropertyChanged("SaveType");
            }
        }

        public string PathSource
        {
            get { return _save.PathSource; }
            set
            {
                _save.PathSource = value;
                OnPropertyChanged("PathSource");
            }

        }

        public string PathDestination
        {
            get { return _save.PathDestination; }
            set
            {
                _save.PathDestination = value;
                OnPropertyChanged("PathDestination");
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
                return _browseSourceCommand ??= new RelayCommand(
                    o =>
                    {
                        var dialog = new System.Windows.Forms.FolderBrowserDialog();
                        System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            _save.PathSource = dialog.SelectedPath;
                            OnPropertyChanged("PathSource");
                        }
                    });
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
                            _save.PathDestination = dialog.SelectedPath;
                            OnPropertyChanged("PathDestination");
                        }
                    });
            }
        }

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand => _saveCommand ??= new RelayCommand(
                    async o =>
                    {
                        MainViewModel.ProgressionViewModelCommand.Execute(null);
                        await Task.Run(() => _save.SaveSave());
                    });

        public void OnSaveChanged(State state)
        {
            MainViewModel.SetProgressionCommand.Execute(state.Progression);
            
        }
    }
}
