using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummarizeThis.Core.StopWord.Interfaces
{
    public interface IStopWordProvider
    {
        IEnumerable<string> StopWords { get; }
    }
}
