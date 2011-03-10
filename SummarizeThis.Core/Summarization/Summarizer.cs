using System;
using System.Collections.Generic;
using SummarizeThis.Core.Frequency.Interfaces;
using SummarizeThis.Core.Summarization.Interfaces;

namespace SummarizeThis.Core.Summarization
{
    public class Summarizer : ISummarizer
    {
        private IFrequencer _frequencer;

        public Summarizer(IFrequencer frequencer)
        {
            _frequencer = frequencer;
        }


        public TextSummary Summarize(string input, int numberOfSentences)
        {
            Dictionary<string, int> wordFrequency = _frequencer.GetWordFrequency(input);
            IEnumerable<string> mostFrequentWords = _frequencer.GetMostFrequentWords(100, wordFrequency);
            //IEnumerable<string> sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(numberOfSentences,
            return null;
        }
    }
}