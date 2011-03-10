using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SummarizeThis.Core.Tokenization.Interfaces;

namespace SummarizeThis.Core.Tokenization
{
    public class Tokenizer : ITokenizer
    {
        private const string _breakOnWordsPattern = "\\W+";
        private const string _breakOnSentencesPattern = @"(?:\.|!|\?)+(?:\s+|\z)";

        public IEnumerable<string> TokenizeWords(string input)
        {
            //using string is null or empty check.
            //for some reason, "Hi." was returning two values. [0, "Hi"]; [1,""];??
            return Regex.Split(input, _breakOnWordsPattern).Where(x => !string.IsNullOrEmpty(x));
        }

        public IEnumerable<string> TokenizeSentences(string input)
        {
            // split on a ".", a "!", a "?" followed by a space or EOL.
            //Using the hack (above) to remove empty.
            return Regex.Split(input, _breakOnSentencesPattern).Where(x => !string.IsNullOrEmpty(x)); ;
        }
    }
}