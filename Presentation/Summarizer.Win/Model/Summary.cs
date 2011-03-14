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

namespace Summarizer.Win.Model
{
    public class Summary
    {
        public string SummarizedText { get; private set; }
        public int ReturnedSentences { get; private set; }

        public Summary(string summarizedText, int returnedSentences)
        {
            SummarizedText = summarizedText;
            ReturnedSentences = returnedSentences;
        }
    }
}
