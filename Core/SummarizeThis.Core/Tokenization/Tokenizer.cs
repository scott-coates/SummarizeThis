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
        private const string _ender = @".!?\r\n";
        private readonly string _breakOnSentencesPattern = string.Format(@"(\S.+?[{0}])(?=\s+|$)", _ender);

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
            //if the user types in some words but does not have an ending punctuation for the last sentence,
            //temporarily add a period. then remove.
            //for example, they might forget to add an ending punctuation. Or they might be pasting
            //a bulleted list and the last item might not have a crlf
            bool validEnding = true;
            if (!_ender.Contains(input.Last()))
            {
                input += ".";
                validEnding = false;
            }

            // split on a ".", a "!", a "?" followed by a space or EOL.
            IList<string> retVal =
                Regex.Matches(input, _breakOnSentencesPattern).OfType<Match>().Select(x => x.Value).ToList();

            if (!validEnding && retVal.Count > 0)
            {
                string last = retVal[retVal.Count - 1];

                if (!string.IsNullOrEmpty(last))
                {
                    int position = last.Length - 1;
                    last = last.Remove(position);
                    retVal[retVal.Count - 1] = last;
                }
            }

            return retVal;
        }
    }
}