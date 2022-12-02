using EasySaveApp.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace EasySaveApp.MVVM.ViewModel
{
    class SaveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private RelayCommand _saveSetting;

        public RelayCommand SaveSetting
        {
            get
            {
                return _saveSetting ?? (_saveSetting = new RelayCommand(
                    o =>
                    {
                        //string json = File.ReadAllText(@"C:\Users\lefra\OneDrive - Association Cesi Viacesi mail\CESI\3eme Année\Programmation Système\Projet\EasySave\Livrable 2\Settings\Settings.json");
                        MessageBox.Show("Bonjour");
                    }));
            }
        }
    }
}
