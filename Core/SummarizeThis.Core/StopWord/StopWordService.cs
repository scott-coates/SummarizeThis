using System;
using System.Collections.Generic;
using System.Linq;

namespace SummarizeThis.Core.StopWord.Interfaces
{
    public class StopWordService : IStopWordService
    {
        private IStopWordProvider _provider;

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