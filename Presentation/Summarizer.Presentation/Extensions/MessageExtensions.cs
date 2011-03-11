using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Summarizer.Presentation.Messaging;
using System.Text;
using System.Windows.Navigation;

namespace Summarizer.Presentation.Extensions
{
    public static class MessageExtensions
    {
        public static void NavigateToPage(this GoToPageMessage message, NavigationService nav)
        {
            //putting in this extension because I don't believe
            //the logic for navigation should exist in the message class
            var sb = new StringBuilder("/Views/");
            sb.Append(message.PageName);
            sb.Append(".xaml");
            nav.Navigate(new Uri(sb.ToString(), UriKind.Relative));
        }
    }
}
