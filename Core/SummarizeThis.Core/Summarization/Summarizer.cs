using System;
using System.Collections.Generic;
using System.Linq;
using SummarizeThis.Core.Frequency;
using SummarizeThis.Core.Frequency.Interfaces;
using SummarizeThis.Core.Stem;
using SummarizeThis.Core.StopWord;
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

            Dictionary<string, int> mostFrequentWords =
                wordFrequency.OrderByDescending(x => x.Value).Take(100).ToDictionary(x => x.Key,
                                                                                     x => x.Value);

            IEnumerable<SentenceFrequency> sentencesScores =
                _frequencer.GetSentencesWithMostFrequentWords(input, mostFrequentWords);

            var highestRankedSentences =
                sentencesScores.OrderByDescending(x => x.Score).Take(numberOfSentences).OrderBy(x => x.SentenceNumber);

            //replacing space and eol if someone is passing in bullet points with no ending
            //puncuation
            var summarizedText = string.Join(" ", highestRankedSentences.Select(x => x.Sentence).ToArray())
                .Replace("\r ", "\r");

            if(summarizedText.EndsWith("\r"))
            {
                summarizedText = summarizedText.Remove(summarizedText.Length - 1);
            }

            return new TextSummary(sentencesScores, highestRankedSentences, wordFrequency, mostFrequentWords,
                                   numberOfSentences, summarizedText);
        }
    }
}