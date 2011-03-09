using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Summarizer.Presentation.Messaging;
using GalaSoft.MvvmLight.Messaging;
using NClassifier.Summarizer;
using Summarizer.Presentation.Model;

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
        private SimpleSummarizer _summarizer = new SimpleSummarizer();

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

        public string InputText { get; set; }

        public int NumberOfReturnedSentences { get; set; }

        public RelayCommand SummarizeCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            SummarizeCommand = new RelayCommand(() => Summarize());

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        private void Summarize()
        {
            string summarizedText = _summarizer.Summarize(InputText, NumberOfReturnedSentences).ToString();
            
            var summary = new Summary(summarizedText, NumberOfReturnedSentences);

            var msg = new GoToPageMessage<Summary>(summary) { PageName = "SummaryPage" };

            Messenger.Default.Send(msg);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}