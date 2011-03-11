using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummarizeThis.Core.Frequency
{
    internal class SentenceFrequency
    {
        public string Sentence { get; private set; }
        public int SentenceNumber { get; private set; }

        public SentenceFrequency(string sentence, int sentenceNumber)
        {
            if (sentence == null) throw new ArgumentNullException("sentence");

            Sentence = sentence;
            SentenceNumber = sentenceNumber;
        }
    }
}
