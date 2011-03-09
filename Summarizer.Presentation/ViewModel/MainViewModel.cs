using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Summarizer.Presentation.Messaging;
using GalaSoft.MvvmLight.Messaging;

namespace Summarizer.Presentation.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public string ApplicationTitle
        {
            get
            {
                return "Summarize This!";
            }
        }

        public string PageName
        {
            get
            {
                return "Enter Text:";
            }
        }

        public string Welcome
        {
            get
            {
                return "Welcome to MVVM Light";
            }
        }

        public RelayCommand Page2Command
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Page2Command = new RelayCommand(() => GoToPage2());

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        private object GoToPage2()
        {
            var msg = new GoToPageMessage() { PageName = "Page2" };
            Messenger.Default.Send<GoToPageMessage>(msg);
            return null;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}