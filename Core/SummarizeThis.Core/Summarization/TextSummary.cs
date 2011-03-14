using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummarizeThis.Core.Frequency;

namespace SummarizeThis.Core.Summarization
{
    public class TextSummary
    {
        private string _summarizedText;

        public IEnumerable<SentenceFrequency> SentenceScores { get; private set; }
        public Dictionary<string, int> AllWordFrequency { get; private set; }
        public Dictionary<string, int> HighestRankingWordFrequency { get; private set; }
        public int ReturnedSentences { get; private set; }

        public IEnumerable<SentenceFrequency> HighestRankedSentences
        {
            get { return SentenceScores.Take(ReturnedSentences); }
        }

        public string SummarizedText
        {
            get
            {
                if (string.IsNullOrEmpty(_summarizedText))
                {
                    _summarizedText = string.Join(" ", HighestRankedSentences.Select(x => x.Sentence).ToArray());
                }
                return _summarizedText;
            }
        }

        public TextSummary(IEnumerable<SentenceFrequency> sentenceScores,
                           Dictionary<string, int> allWordFrequency,
                           Dictionary<string, int> highestRankingWordFrequency,
                           int returnedSentences)
        {
            SentenceScores = sentenceScores;
            AllWordFrequency = allWordFrequency;
            HighestRankingWordFrequency = highestRankingWordFrequency;
            ReturnedSentences = returnedSentences;
        }
    }
}