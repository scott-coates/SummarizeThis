using System.Collections.Generic;

namespace SummarizeThis.Core.Frequency.Interfaces
{
    public interface IFrequencer
    {
        Dictionary<string, int> GetWordFrequency(string input);
        Dictionary<string, int> GetMostFrequentWords(int howManyWords, Dictionary<string, int> wordFrequencies);
        IEnumerable<SentenceFrequency> GetSentencesWithMostFrequentWords(int numberOfSentences, string input, Dictionary<string, int> mostFrequentWords);
    }
}
