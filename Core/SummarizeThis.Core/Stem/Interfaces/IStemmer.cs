using System;

namespace SummarizeThis.Core.Stem.Interfaces
{
    public interface IStemmer
    {
        /// <summary> Stem a word provided as a String.  Returns the result as a String.</summary>
        String Stem(String s);
    }
}