using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummarizeThis.Core.Frequency;

namespace SummarizeThis.Core.Summarization
{
    public class TextSummary
    {
        public string SummarizedText { get; private set; }
        public Dictionary<string, int> AllWordFrequency { get; private set; }
        public Dictionary<string, int> HighestRankingWordFrequency { get; private set; }
        public int ReturnedSentences { get; private set; }
        public IEnumerable<SentenceFrequency> SentenceScores { get; private set; }

        public TextSummary(string summarizedText, IEnumerable<SentenceFrequency> sentenceScores,
                           Dictionary<string, int> allWordFrequency,
                           Dictionary<string, int> highestRankingWordFrequency,
                           int returnedSentences)
        {
            SummarizedText = summarizedText;
            SentenceScores = sentenceScores;
            AllWordFrequency = allWordFrequency;
            HighestRankingWordFrequency = highestRankingWordFrequency;
            ReturnedSentences = returnedSentences;
        }
    }
}