using EasySaveApp.Core;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
		public RelayCommand HomeViewModelCommand { get; set; }
		public RelayCommand SaveViewModelCommand { get; set; }
        public RelayCommand SettingViewModelCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SaveViewModel SaveVM { get; set; }
        public SettingViewModel SettingVM { get; set; }

        private object _currentView;

		public object CurrentView
		{
			get { return _currentView; }
			set 
			{ 
			_currentView = value;
			OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
            HomeVM = new HomeViewModel();
            SaveVM = new SaveViewModel();
            SettingVM = new SettingViewModel();

            CurrentView = HomeVM;

            HomeViewModelCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            SaveViewModelCommand = new RelayCommand(o => { CurrentView = SaveVM; });
            SettingViewModelCommand = new RelayCommand(o => { CurrentView = SettingVM; });
        }
    }
}
