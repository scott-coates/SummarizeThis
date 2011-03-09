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

namespace Summarizer.Presentation.Messaging
{
    public class GoToPageMessage
    {
        public string PageName { get; set; }
    }

    public class GoToPageMessage<T> : GoToPageMessage
    {
        public T Data { get; private set; }

        public GoToPageMessage(T data)
        {
            Data = data;
        }
    }
}