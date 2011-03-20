using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Summarizer.Phone.Model;
using SummarizerService = SummarizeThis.Core.Summarization.Summarizer;
using SummarizeThis.Core.Summarization;
namespace Summarizer.Phone.ViewModel
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
            SummarizeCommand = new RelayCommand(() => Summarize()
                , () => !string.IsNullOrEmpty(InputText));

            string text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In a diam magna, et vulputate turpis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed purus mi, malesuada eu congue fringilla, interdum at sapien. Suspendisse potenti. Praesent egestas massa felis. Ut vel ipsum lectus, sit amet sollicitudin nunc. Integer at augue leo. In ultrices tristique placerat. Nunc id ullamcorper dui. Fusce ac porttitor elit. Nullam in commodo mi. In gravida blandit dapibus. Donec tincidunt luctus rhoncus. Curabitur sit amet porta erat. Etiam congue cursus felis dignissim adipiscing.

Ut ac pharetra est. Mauris quis ligula tellus, sed vehicula nisi. Duis tincidunt augue non est cursus placerat. Suspendisse congue, metus ut sagittis ultricies, sem nulla sodales mauris, ut pulvinar erat orci sit amet eros. Mauris lobortis consequat tincidunt. Etiam quis neque nulla, ut posuere ipsum. Maecenas nec arcu at neque posuere luctus id vel odio. Nullam sit amet enim urna. Cras eu condimentum augue. Nullam vel mi at enim egestas eleifend. Maecenas sed lectus sed lacus ornare lobortis non et ligula. Etiam at lectus odio, sed cursus arcu. Pellentesque pretium varius ligula, at commodo sapien varius sodales. Vivamus accumsan velit in lectus dictum dictum. Quisque pretium tincidunt ultricies. Sed faucibus, mauris eu iaculis pharetra, metus ligula fermentum metus, non mollis est dolor hendrerit mauris.

Curabitur enim metus, tincidunt eu blandit id, accumsan a urna. Maecenas nec est a ante tincidunt tincidunt. Donec at dolor ligula. Donec non elit tellus, at placerat neque. Cras mattis, leo ut eleifend tincidunt, ante metus fringilla quam, vel hendrerit metus ante nec ipsum. Phasellus pharetra massa quis metus adipiscing at viverra dui feugiat. Integer mi felis, blandit sit amet suscipit nec, ultricies a nisl. Vivamus imperdiet augue sed nibh congue hendrerit. Suspendisse sagittis mi eget augue varius non lobortis enim tempor. Aliquam mollis eleifend dolor, ut gravida erat malesuada non. Donec odio turpis, commodo nec interdum at, luctus vitae urna. Donec ac magna lacus, vitae vehicula erat. Suspendisse lacinia posuere elit, eu rutrum ligula pharetra et. Fusce turpis lorem, pulvinar non ornare in, fringilla nec libero. Etiam vel justo vitae elit viverra imperdiet eget sit amet lacus. Donec eu diam neque.";

            InputText = text;
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
            if (NumberOfReturnedSentences < 1)
            {
                NumberOfReturnedSentences = 1;
            }

            TextSummary textSummary = _summarizer.Summarize(InputText, NumberOfReturnedSentences);
            string summarizedText = textSummary.SummarizedText;

            var summary = new Summary(summarizedText, NumberOfReturnedSentences,textSummary.AllWordFrequency);

            Messenger.Default.Send(summary);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}