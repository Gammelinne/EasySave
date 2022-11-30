using EasySaveApp.MVVM.Model;
using System.Windows;
using System.Windows.Controls;


namespace EasySaveApp.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SaveView.xaml
    /// </summary>
    public partial class SaveView : UserControl
    {
        public SaveView()
        {
            InitializeComponent();
        }

        private void SourceBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                saveSourceTextBox.Text = dialog.SelectedPath;
            }
        }

        private void DestinationBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                saveDestinationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var save = Save.SaveSave();
        }
    }
}
