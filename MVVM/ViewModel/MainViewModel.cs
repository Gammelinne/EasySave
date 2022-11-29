using EasySaveApp.Core;
using EasySaveApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand ChangeViewCommand { get; set; }
		public RelayCommand SaveHomeViewModelCommand { get; set; }
		public RelayCommand SaveViewModelCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SaveViewModel SaveVM { get; set; }

        public ChangeViewModelTest ChangeVM { get; set; }
		public SaveHomeViewModel SaveHomeVM { get; set; }
        
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
            ChangeVM = new ChangeViewModelTest();
            SaveHomeVM = new SaveHomeViewModel();
            SaveVM = new SaveViewModel();

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
        }
    }
}
