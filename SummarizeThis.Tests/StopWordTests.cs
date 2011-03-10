using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SummarizeThis.Core.StopWord.Interfaces;

namespace SummarizeThis.Tests
{
    [TestFixture]
    public class StopWordTests
    {
        private Mock<IStopWordProvider> _provider;
        private IStopWordService _service;
        private IEnumerable<string> _stopWords;

        [SetUp]
        public void Setup()
        {
            _provider = new Mock<IStopWordProvider>();
            _service = new StopWordService(_provider.Object);
            _stopWords = new[] { "I", "a", "about", "an" };

            _provider.Setup(x => x.StopWords).Returns(_stopWords);
        }

        [Test]
        public void AllStopWordsReturnEmpty()
        {
            IEnumerable<string> retVal = _service.CleanStopWords(_stopWords);

            Assert.That(retVal.Count() == 0);
        }

        [Test]
        public void NoStopWordsReturnAll()
        {
            _provider.Setup(x => x.StopWords).Returns(new string[] { });
            IEnumerable<string> retVal = _service.CleanStopWords(_stopWords);

            Assert.That(retVal.Count() == _stopWords.Count());
        }

        [Test]
        public void OneValidReturnValue()
        {
            var input = new List<string>(_stopWords) { "Valid Value" };

            IEnumerable<string> retVal = _service.CleanStopWords(input);

            Assert.That(retVal.Count() == 1);
        }
    }
}
