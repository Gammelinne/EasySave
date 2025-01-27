﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace EasySaveApp.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SaveHomeView.xaml
    /// </summary>
    public partial class SaveHomeView : UserControl
    {
        public SaveHomeView()
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
