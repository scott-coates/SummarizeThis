using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SummarizerService = SummarizeThis.Core.Summarization.Summarizer;
using GalaSoft.MvvmLight.Messaging;
using SummarizeThis.Core.Summarization;

namespace Summarizer.Win.ViewModel
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
        private SummarizerService _summarizer = new SummarizerService();

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

        public string SummarizedText { get; set; }

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
            TextSummary textSummary = _summarizer.Summarize(InputText, NumberOfReturnedSentences);

            SummarizedText = textSummary.SummarizedText;
            RaisePropertyChanged("SummarizedText");
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}