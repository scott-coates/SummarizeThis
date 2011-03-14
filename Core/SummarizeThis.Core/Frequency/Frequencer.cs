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

        public Dictionary<string, int> GetMostFrequentWords(int howManyWords, Dictionary<string, int> wordFrequencies)
        {
            //The logic is to grab the top x frequent words. We are not simply ordering by score, we're ordering by the 
            //sequence of words entered by the user and score.

            return wordFrequencies.OrderByDescending(x => x.Value).Take(howManyWords).ToDictionary(x => x.Key,
                                                                                                   x => x.Value);
        }

        public IEnumerable<string> GetSentencesWithMostFrequentWords(int numberOfSentences,
                                                                     string input,
                                                                     Dictionary<string, int> mostFrequentWords)
        {
            IEnumerable<string> retVal = null;
            IEnumerable<string> convertedSentences = _tokenizer.TokenizeSentences(input);

            if (numberOfSentences >= convertedSentences.Count())
            {
                retVal = convertedSentences;
            }
            else
            {
                IEnumerable<SentenceFrequency> sentenceFrequencies = SearchSentencesForKeyWords(numberOfSentences,
                                                                                                convertedSentences.
                                                                                                    ToList(),
                                                                                                mostFrequentWords);
                retVal = sentenceFrequencies.Select(x => x.Sentence);
            }

            return retVal;
        }

        private IEnumerable<SentenceFrequency> SearchSentencesForKeyWords(int numberOfSentences,
                                                                          IEnumerable<string> sentences,
                                                                          Dictionary<string, int> mostFrequentWords)
        {
            var retVal = new List<SentenceFrequency>();

            foreach (
                var sentence in
                    sentences.Where(sentence => !retVal.Any(x => x.Sentence.ToLower() == sentence.ToLower())))
            {
                retVal.Add(GetScore(sentence, mostFrequentWords));
            }

            return retVal.OrderByDescending(x => x.Score).Take(numberOfSentences);
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