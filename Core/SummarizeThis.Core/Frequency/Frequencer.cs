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
            IEnumerable<string> convertedSentences = _tokenizer.TokenizeSentences(input);

            IEnumerable<SentenceFrequency> sentenceFrequencies = SearchSentencesForKeyWords(numberOfSentences,
                                                                                            convertedSentences.ToList(),
                                                                                            mostFrequentWords.ToList())
                .OrderBy(x => x.SentenceNumber);

            IEnumerable<string> sentences = sentenceFrequencies.Select(x => x.Sentence);

            return sentences;
        }

        private IEnumerable<SentenceFrequency> SearchSentencesForKeyWords(int numberOfSentences, List<string> sentences,
                                                                          IList<string> mostFrequentWords)
        {
            var alreadProcessed = new List<string>();

            for (int i = 0; i < numberOfSentences; i++)
            {
                for (int j = 0; j < sentences.Count; j++)
                {
                    string sentenceToLower = sentences[j].ToLower();
                    if (sentenceToLower.Contains(mostFrequentWords[i].ToLower()) && !alreadProcessed.Contains(sentenceToLower))
                    {
                        var sentFreq = new SentenceFrequency(sentences[j], j);
                        alreadProcessed.Add(sentenceToLower);
                        yield return sentFreq;
                        break; //this word is found. move to next most frequent word.
                    }
                }
            }
        }
    }
}