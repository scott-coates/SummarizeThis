﻿using System;
using System.Collections.Generic;
using System.Linq;
using SummarizeThis.Core.Tokenization.Interfaces;

namespace SummarizeThis.Core.Frequency.Interfaces
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
            IEnumerable<string> tokens = _tokenizer.Tokenize(input);

            var group = (from t in tokens
                         group t by t.ToLower()
                             into g
                             select g);

            var groupAsDictionary = group.ToDictionary(x => x.Key, x => x.Count());

            return groupAsDictionary;
        }

        public IEnumerable<string> GetMostFrequentWords(int howManyWords, Dictionary<string, int> wordFrequencies)
        {
            return null;
        }
    }
}