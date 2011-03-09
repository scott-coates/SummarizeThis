using GalaSoft.MvvmLight;

namespace Summarizer.Presentation.ViewModel
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
        }
        public string ApplicationTitle
        {
            get
            {
                return "MVVM LIGHT";
            }
        }

        public string PageName
        {
            get
            {
                return "Page 2";
            }
        }

        public string Welcome
        {
            get
            {
                return "Welcome to Page 2!";
            }
        }
        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}