using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SummarizeThis.Core.Stem.Interfaces;
using SummarizeThis.Core.StopWord.Interfaces;
using SummarizeThis.Core.Tokenization.Interfaces;

namespace SummarizeThis.Core.Tokenization
{
    public class Tokenizer : ITokenizer
    {
        private readonly IStopWordService _stopWordService;
        private readonly IStemmer _stemmer;

        private const string _breakOnWordsPattern = "\\W+";
        //http://stackoverflow.com/questions/1936388/what-is-a-regular-expression-for-parsing-out-individual-sentences
        private const string _breakOnSentencesPattern = @"(\S.+?[.!?])(?=\s+|$)";

        public Tokenizer(IStopWordService stopWordService, IStemmer stemmer)
        {
            _stopWordService = stopWordService;
            _stemmer = stemmer;
        }

        public IEnumerable<string> TokenizeWords(string input)
        {
            //using string is null or empty check.
            //for some reason, "Hi." was returning two values. [0, "Hi"]; [1,""];??
            IEnumerable<string> tokenizeWords =
                Regex.Split(input, _breakOnWordsPattern)
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => _stemmer.Stem(x));

            return _stopWordService.CleanStopWords(tokenizeWords);
        }

        public IEnumerable<string> TokenizeSentences(string input)
        {
            // split on a ".", a "!", a "?" followed by a space or EOL.
            return Regex.Matches(input, _breakOnSentencesPattern).OfType<Match>().Select(x => x.Value);
        }
    }
}