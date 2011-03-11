using System.Linq;
using NUnit.Framework;
using SummarizeThis.Core.Summarization;
using SummarizeThis.Core.Summarization.Interfaces;

namespace SummarizeThis.Tests.Functional
{
    [TestFixture]
    public class SummarizeTests
    {
        private ISummarizer _summarizer;

        [SetUp]
        public void Setup()
        {
            _summarizer = new Summarizer();
        }

        [Test]
        public void OneSentenceReturnShortInput()
        {
            const string input = "NClassifier is a dotnet assembly for working with text.  NClassifier includes a summarizer.";
            const string expectedResult = "NClassifier is a dotnet assembly for working with text.";
            string result = _summarizer.Summarize(input, 1).SummarizedText;
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MostFrequentWord()
        {
            const string input = "NClassifier is a dotnet assembly for working with text.  NClassifier includes a summarizer.";
            const string expectedResult = "nclassifier";
            string result = _summarizer.Summarize(input, 1).MostFrequentWords.First();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TwoSentenceReturnLongerInput()
        {
            const string input = "NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer. A Summarizer allows the summary of text. A Summarizer is really cool. I don't think there are any other dotnet summarizers.";
            const string expectedResult = "NClassifier includes a summarizer. A Summarizer allows the summary of text.";

            string result = _summarizer.Summarize(input, 2).SummarizedText;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OneSentenceReturnLongerInput()
        {
            const string input = "NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer. A Summarizer allows the summary of text. A Summarizer is really cool. I don't think there are any other dotnet summarizers.";
            const string expectedResult = "NClassifier includes a summarizer.";

            string result = _summarizer.Summarize(input, 1).SummarizedText;

            Assert.AreEqual(expectedResult, result);
        }
    }
}