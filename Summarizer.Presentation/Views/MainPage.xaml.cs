using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using Summarizer.Presentation.Messaging;
using System.Text;
using System;
using Summarizer.Presentation.Extensions;
using Summarizer.Presentation.Model;
using Summarizer.Presentation.ViewModel;

namespace Summarizer.Presentation
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<GoToPageMessage<Summary>>(this, (action) =>
                {
                    ViewModelLocator.SummaryStatic.SummarizedText = action.Data.SummarizedText;
                    
                    ViewModelLocator.SummaryStatic.NumberOfSentences = action.Data.ReturnedSentences;

                    action.NavigateToPage(NavigationService);
                });
        }
    }
}
