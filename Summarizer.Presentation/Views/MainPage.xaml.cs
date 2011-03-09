using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using Summarizer.Presentation.Messaging;
using System.Text;

namespace Summarizer.Presentation
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<GoToPageMessage>(this, (action) => ReceiveMessage(action));
        }
        private object ReceiveMessage(GoToPageMessage action)
        {
            var sb = new StringBuilder("/Views/");
            sb.Append(action.PageName);
            sb.Append(".xaml");
            NavigationService.Navigate(new System.Uri(sb.ToString(), System.UriKind.Relative));
            return null;
        }
    }
}
