namespace SummarizeThis.Core.Summarization.Interfaces
{
    public interface ISummarizer
    {
        TextSummary Summarize(string input, int numberOfSentences);
    }
}
