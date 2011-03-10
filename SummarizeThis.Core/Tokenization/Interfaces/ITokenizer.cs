using System.Collections.Generic;

namespace SummarizeThis.Core.Tokenization.Interfaces
{
    /// <summary>
    /// Defines an interface for the splitting up of strings into tokens.
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// Splits up the input string into tokens which each have individual probabilities.
        /// </summary>
        /// <param name="input">The string to tokenize.</param>
        /// <returns>Returns token collection.</returns>
        IEnumerable<string> Tokenize(string input);
    }
}
