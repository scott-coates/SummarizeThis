using System.Collections.Generic;

namespace SummarizeThis.Core.Frequency.Interfaces
{
    public interface IFrequencer
    {
        Dictionary<string, int> GetWordFrequency(string input);
    }
}
