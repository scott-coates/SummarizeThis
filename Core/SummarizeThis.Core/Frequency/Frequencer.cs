using System.Collections.Generic;
using System.Linq;
using SummarizeThis.Core.Frequency.Interfaces;
using SummarizeThis.Core.Tokenization.Interfaces;
using System.Text.RegularExpressions;

namespace SummarizeThis.Core.Frequency
{
    public class Frequencer : IFrequencer
    {
        private readonly ITokenizer _tokenizer;

        public Frequencer(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public Dictionary<string, int> GetWordFrequency(string input)
        {
            IEnumerable<string> tokens = _tokenizer.TokenizeWords(input);

            var group = (from t in tokens
                         group t by t.ToLower()
                         into g
                         select g);

            var groupAsDictionary = group.ToDictionary(x => x.Key, x => x.Count());

            return groupAsDictionary;
        }


        public IEnumerable<SentenceFrequency> GetSentencesWithMostFrequentWords(string input,
                                                                                Dictionary<string, int>
                                                                                    mostFrequentWords)
        {
            IEnumerable<SentenceFrequency> retVal = null;
            IEnumerable<string> convertedSentences = _tokenizer.TokenizeSentences(input);

            retVal = SearchSentencesForKeyWords(convertedSentences.
                                                    ToList(),
                                                mostFrequentWords);

            return retVal;
        }

        private IEnumerable<SentenceFrequency> SearchSentencesForKeyWords(IList<string> sentences,
                                                                          Dictionary<string, int> mostFrequentWords)
        {
            var retVal = new List<SentenceFrequency>();
            string sentence = null;

            for (int i = 0; i < sentences.Count; i++)
            {
                sentence = sentences[i];
                if (!retVal.Any(x => x.Sentence.ToLower() == sentence.ToLower()))
                {
                    retVal.Add(GetScore(sentence, i, mostFrequentWords));
                }
            }

            return retVal;
        }

        private SentenceFrequency GetScore(string sentence, int sentenceNumber,
                                           Dictionary<string, int> mostFrequentWords)
        {
            int score = (from word in
                             mostFrequentWords
                         let occurence = Regex.Matches(sentence, word.Key).Count
                         where occurence > 0
                         select word.Value*occurence)
                .Sum(x => x);

            return new SentenceFrequency(sentence, score, sentenceNumber);
        }
    }
}