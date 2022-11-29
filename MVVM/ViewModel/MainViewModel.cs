using EasySaveApp.Core;

namespace EasySaveApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand ChangeViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }

		public ChangeViewModelTest ChangeVM { get; set; }

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

            CurrentView = HomeVM;

			HomeViewCommand = new RelayCommand(o => { CurrentView= HomeVM; });

            ChangeViewCommand = new RelayCommand(o =>
            { CurrentView = ChangeVM; });
        }
    }
}
