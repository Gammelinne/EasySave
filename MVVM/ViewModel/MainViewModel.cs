using EasySaveApp.Core;
using EasySaveApp.Languages;
using EasySaveApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand FrenchCommand { get; set; }
        public RelayCommand EnglishCommand { get; set; }
        public RelayCommand ShutdownWindowCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand SaveHomeViewModelCommand { get; set; }
		public RelayCommand SaveViewModelCommand { get; set; }
        public RelayCommand SettingViewModelCommand { get; set; }
        public static RelayCommand ProgressionViewModelCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SaveViewModel SaveVM { get; set; }
		public SaveHomeViewModel SaveHomeVM { get; set; }
        public SettingViewModel SettingVM { get; set; }
        public ProgressionViewModel ProgressionVM { get; set; }

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

        // CurrentLanguage
        private object _currentLanguage;
        public object CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                OnPropertyChanged();
            }
        }

        private French frenchLanguage;
        private English englishLanguage;

        

        public MainViewModel()
		{
            ShutdownWindowCommand = new RelayCommand(o => { App.Current.Shutdown(); });
            HomeVM = new HomeViewModel();
            SaveHomeVM = new SaveHomeViewModel();
            SaveVM = new SaveViewModel();
            SettingVM = new SettingViewModel();
            ProgressionVM = new ProgressionViewModel();

            CurrentView = SaveHomeVM;

            HomeViewCommand = new RelayCommand(o => 
			{
				CurrentView= HomeVM;
			});

            SaveHomeViewModelCommand = new RelayCommand(o =>
            {
                CurrentView = SaveHomeVM;
            });

            SaveViewModelCommand = new RelayCommand(o =>
            {
                CurrentView = SaveVM;
            });

            SettingViewModelCommand = new RelayCommand(o =>
            {
                CurrentView = SettingVM;
            });

            ProgressionViewModelCommand = new RelayCommand(o =>
            {
                CurrentView = ProgressionVM;
            });

            //Language Command

            CurrentLanguage = new English();

            FrenchCommand = new RelayCommand(o =>
            {
                CurrentLanguage = new French();
            });

            EnglishCommand = new RelayCommand(o =>
            {
                CurrentLanguage = new English();
            });
        }
    }
}
