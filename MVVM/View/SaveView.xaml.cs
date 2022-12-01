using EasySaveApp.MVVM.ViewModel;
using EasySaveApp.MVVM.Model;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

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
                sourceTextBox.Text = dialog.SelectedPath;
            }
        }

        private void DestinationBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                destinationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text.ToString();
            string fileSource = sourceTextBox.Text.ToString();
            string fileDestination = destinationTextBox.Text.ToString();
            string saveType = "Complete";

            SaveViewModel save = new SaveViewModel(name, fileSource, fileDestination, saveType);
            
            if (!save.CheckInputFill())
            {
                MessageBox.Show("Please fill all data",
                    "Data error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult.Yes);
            }
            else if (!save.CheckPathExist())
            {
                MessageBox.Show("Source or Destination path does not exist",
                    "Path error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult.Yes);
            }
            else
            {
                save.Save();
            }



        }
    }
}
