using System;

namespace Lucene.Net.Analysis
{
    public interface IPorterStemmer
    {
        /// <summary> Stem a word provided as a String.  Returns the result as a String.</summary>
        String Stem(String s);
    }
}