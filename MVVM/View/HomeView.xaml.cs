using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace EasySaveApp.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
