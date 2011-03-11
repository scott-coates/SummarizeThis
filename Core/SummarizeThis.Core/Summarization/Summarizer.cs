using System;
using System.Collections.Generic;
using System.Linq;
using SummarizeThis.Core.Frequency;
using SummarizeThis.Core.Frequency.Interfaces;
using SummarizeThis.Core.StopWord.Interfaces;
using SummarizeThis.Core.Summarization.Interfaces;
using SummarizeThis.Core.Tokenization;

namespace SummarizeThis.Core.Summarization
{
    /*
     * A lot of the logic came from http://nclassifier.sourceforge.net/
     */
    public class Summarizer : ISummarizer
    {
        private readonly IFrequencer _frequencer;

        public Summarizer(IFrequencer frequencer)
        {
            _frequencer = frequencer;
        }

        public Summarizer()
            : this(new Frequencer(new Tokenizer(new StopWordService(new StopWordProvider()))))
        {
        }

        public TextSummary Summarize(string input, int numberOfSentences)
        {
            Dictionary<string, int> wordFrequency = _frequencer.GetWordFrequency(input);
            IEnumerable<string> mostFrequentWords = _frequencer.GetMostFrequentWords(100, wordFrequency);
            IEnumerable<string> sentencesWithMostFrequentWords =
                _frequencer.GetSentencesWithMostFrequentWords(numberOfSentences, input, mostFrequentWords);

            string summarizedText = string.Join(" ", sentencesWithMostFrequentWords.ToArray());

            return new TextSummary(summarizedText, wordFrequency, mostFrequentWords, numberOfSentences);
        }
    }
}