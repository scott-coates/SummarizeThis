using System.Windows;
using Summarizer.Win.ViewModel;

namespace Summarizer.Win
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class SummaryWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public SummaryWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}