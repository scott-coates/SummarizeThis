using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummarizeThis.Core.Frequency
{
    internal class SentenceFrequency
    {
        public string Sentence { get; private set; }
        public int Score { get; private set; }

        public SentenceFrequency(string sentence, int score)
        {
            if (sentence == null) throw new ArgumentNullException("score");

            Sentence = sentence;
            Score = score;
        }
    }
}
