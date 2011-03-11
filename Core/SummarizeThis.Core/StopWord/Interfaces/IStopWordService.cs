using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummarizeThis.Core.StopWord.Interfaces
{
    public interface IStopWordService
    {
        IEnumerable<string> CleanStopWords(IEnumerable<string> input);
    }
}
