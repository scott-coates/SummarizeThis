using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SummarizeThis.Core.Tokenization.Interfaces
{
    public class Tokenizer : ITokenizer
    {
        private const string _breakOnWordsPattern = "\\W+";

        public IEnumerable<string> Tokenize(string input)
        {
            //return value;
            return Regex.Split(input, _breakOnWordsPattern).Where(x => !string.IsNullOrEmpty(x));
        }
    }
}