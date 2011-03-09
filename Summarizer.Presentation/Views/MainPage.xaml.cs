using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using Summarizer.Presentation.Messaging;
using System.Text;
using System;
using Summarizer.Presentation.Extensions;

namespace Summarizer.Presentation
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<GoToPageMessage>(this, (action) => action.NavigateToPage(NavigationService));
        }
    }
}
