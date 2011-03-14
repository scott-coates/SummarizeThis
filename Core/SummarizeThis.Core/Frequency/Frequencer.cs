using System.Collections.Generic;
using System.Linq;
using SummarizeThis.Core.Frequency.Interfaces;
using SummarizeThis.Core.Tokenization.Interfaces;

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

        private IEnumerable<SentenceFrequency> SearchSentencesForKeyWords(IEnumerable<string> sentences,
                                                                          Dictionary<string, int> mostFrequentWords)
        {
            var retVal = new List<SentenceFrequency>();

            foreach (
                var sentence in
                    sentences.Where(sentence => !retVal.Any(x => x.Sentence.ToLower() == sentence.ToLower())))
            {
                retVal.Add(GetScore(sentence, mostFrequentWords));
            }

            return retVal;
        }

        private SentenceFrequency GetScore(string sentence, Dictionary<string, int> mostFrequentWords)
        {
            int score = mostFrequentWords
                .Where(word => sentence.Contains(word.Key))
                .Sum(word => word.Value);

            return new SentenceFrequency(sentence, score);
        }
    }
}