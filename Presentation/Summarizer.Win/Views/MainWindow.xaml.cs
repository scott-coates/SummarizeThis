using System.Windows;
using Summarizer.Win.ViewModel;
using System;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;

namespace Summarizer.Win
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}