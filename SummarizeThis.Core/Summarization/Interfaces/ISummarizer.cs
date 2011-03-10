namespace SummarizeThis.Core.Summarization.Interfaces
{
    public interface ISummarizer
    {
        string Summarize(string input, int numberOfSentences);
    }
}
