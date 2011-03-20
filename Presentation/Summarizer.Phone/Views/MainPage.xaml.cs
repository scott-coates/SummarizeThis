using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using System.Text;
using System;
using Summarizer.Phone.Model;
using Summarizer.Phone.ViewModel;

namespace Summarizer.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Messenger.Default.Register<Summary>(this, (x) => NavigationService.Navigate(new Uri("/Views/SummaryPage.xaml", UriKind.Relative)));
        }
    }
}
