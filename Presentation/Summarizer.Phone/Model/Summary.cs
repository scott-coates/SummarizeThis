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
using System.Collections.Generic;

namespace Summarizer.Phone.Model
{
    public class Summary
    {
        public string SummarizedText { get; private set; }
        public int ReturnedSentences { get; private set; }
        public Dictionary<string, int> WordFrequency { get; private set; }

        public Summary(string summarizedText, int returnedSentences, Dictionary<string, int> wordFrequency)
        {
            SummarizedText = summarizedText;
            ReturnedSentences = returnedSentences;
            WordFrequency = wordFrequency;
        }
    }
}
