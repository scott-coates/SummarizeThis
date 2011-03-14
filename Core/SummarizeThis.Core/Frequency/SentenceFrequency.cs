using System;

namespace SummarizeThis.Core.Frequency
{
    public class SentenceFrequency
    {
        public string Sentence { get; private set; }
        public int Score { get; private set; }
        public int SentenceNumber { get; private set; }

        public SentenceFrequency(string sentence, int score, int sentenceNumber)
        {
            if (sentence == null) throw new ArgumentNullException("score");

            Sentence = sentence;
            Score = score;
            SentenceNumber = sentenceNumber;
        }
    }
}
