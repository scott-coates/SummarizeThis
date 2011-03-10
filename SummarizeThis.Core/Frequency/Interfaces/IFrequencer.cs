using System.Collections.Generic;

namespace SummarizeThis.Core.Frequency.Interfaces
{
    public interface IFrequencer
    {
        Dictionary<string, int> GetWordFrequency(string input);
        IEnumerable<string> GetMostFrequentWords(int howManyWords, Dictionary<string, int> wordFrequencies);
        IEnumerable<string> GetSentencesWithMostFrequentWords(int numberOfSentences, string input, IEnumerable<string> mostFrequentWords);
    }
}
