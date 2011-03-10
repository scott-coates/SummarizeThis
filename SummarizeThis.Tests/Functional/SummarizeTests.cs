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
    }
}