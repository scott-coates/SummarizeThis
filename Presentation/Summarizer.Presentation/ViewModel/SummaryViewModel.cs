using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Summarizer.Phone.Model;

namespace Summarizer.Phone.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
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
    public class SummaryViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the Page2ViewModel class.
        /// </summary>
        public SummaryViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}
            Messenger.Default.Register<Summary>(this, FillSummary);
        }
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
                return "Summary";
            }
        }

        public string SummarizedText { get; set; }
        public int NumberOfSentences { get; set; }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}

        private void FillSummary(Summary summary)
        {
            SummarizedText = summary.SummarizedText;
            NumberOfSentences = summary.ReturnedSentences;
        }
    }
}