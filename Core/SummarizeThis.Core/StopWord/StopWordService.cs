using System.Collections.Generic;
using System.Linq;
using SummarizeThis.Core.StopWord.Interfaces;

namespace SummarizeThis.Core.StopWord
{
    public class StopWordService : IStopWordService
    {
        private readonly IStopWordProvider _provider;

        public StopWordService(IStopWordProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<string> CleanStopWords(IEnumerable<string> input)
        {
            //take input and lower - makes for easier comparison

            IEnumerable<string> stopWords = _provider.StopWords.Select(x => x.ToLower());

            return input.Where(x => !stopWords.Contains(x.ToLower()));
        }
    }
}