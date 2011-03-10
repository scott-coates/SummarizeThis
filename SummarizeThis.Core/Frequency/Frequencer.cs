using System;
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

        public IEnumerable<string> GetMostFrequentWords(int howManyWords, Dictionary<string, int> wordFrequencies)
        {
            //The logic is to grab the top x frequent words. We are not simply ordering by frequency, we're ordering by the 
            //sequence of words entered by the user and frequency.

            return wordFrequencies.OrderByDescending(x => x.Value).Select(x => x.Key).Take(howManyWords);
        }

        public IEnumerable<string> GetSentencesWithMostFrequentWords(int numberOfSentences,
                                                                     string input,
                                                                     IEnumerable<string> mostFrequentWords)
        {
            IEnumerable<string> sentences = _tokenizer.TokenizeSentences(input);
            return SearchSentencesForKeyWords(sentences, mostFrequentWords).Take(numberOfSentences);
        }

        private IEnumerable<string> SearchSentencesForKeyWords(IEnumerable<string> sentences,
                                                               IEnumerable<string> mostFrequentWords)
        {
            foreach (var word in mostFrequentWords)
            {
                foreach (var sentence in sentences)
                {
                    if (sentence.ToLower().Contains(word.ToLower()))
                    {
                        yield return sentence;
                    }
                }
            }
        }
    }
}