using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummarizeThis.Core.Summarization
{
    public class TextSummary
    {
        public string SummarizedText { get; private set; }
        public Dictionary<string, int> WordFrequency { get; private set; }
        public IEnumerable<string> MostFrequentWords { get; private set; }
        public IEnumerable<string> SentencesWithMostFrequentWords { get; private set; }
        public int ReturnedSentences { get; private set; }

        public TextSummary(string summarizedText, Dictionary<string, int> wordFrequency,
                           IEnumerable<string> mostFrequentWords, IEnumerable<string> sentencesWithMostFrequentWords,
                           int returnedSentences)
        {
            SummarizedText = summarizedText;
            WordFrequency = wordFrequency;
            MostFrequentWords = mostFrequentWords;
            SentencesWithMostFrequentWords = sentencesWithMostFrequentWords;
            ReturnedSentences = returnedSentences;
        }
    }
}