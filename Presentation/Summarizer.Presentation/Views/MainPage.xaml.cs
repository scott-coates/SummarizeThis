using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using System.Text;
using System;
using Summarizer.Presentation.Model;
using Summarizer.Presentation.ViewModel;
using Summarizer.Presentation.Views;

namespace Summarizer.Presentation
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
