using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis;
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
            : this(new Frequencer(new Tokenizer(new StopWordService(new StopWordProvider()), new PorterStemmer())))
        {
        }

        public TextSummary Summarize(string input, int numberOfSentences)
        {
            Dictionary<string, int> wordFrequency = _frequencer.GetWordFrequency(input);
            Dictionary<string, int> mostFrequentWords = _frequencer.GetMostFrequentWords(100, wordFrequency);
            IEnumerable<SentenceFrequency> sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(numberOfSentences, input, mostFrequentWords);

            string summarizedText = string.Join(" ", sentencesWithMostFrequentWords.Select(x => x.Sentence).ToArray());

            return new TextSummary(summarizedText, sentencesWithMostFrequentWords, wordFrequency, mostFrequentWords, numberOfSentences);
        }
    }
}