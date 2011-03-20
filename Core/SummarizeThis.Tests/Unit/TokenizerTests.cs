using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SummarizeThis.Core.Stem.Interfaces;
using SummarizeThis.Core.StopWord.Interfaces;
using SummarizeThis.Core.Tokenization;
using SummarizeThis.Core.Tokenization.Interfaces;

namespace SummarizeThis.Tests.Unit
{
    [TestFixture]
    public class TokenizerTests
    {
        private ITokenizer _tokenizer;
        private Mock<IStopWordService> _stopWordService;
        private Mock<IStemmer> _stemmer;

        [SetUp]
        public void Setup()
        {
            _stopWordService = new Mock<IStopWordService>();
            _stopWordService.Setup(x => x.CleanStopWords(It.IsAny<IEnumerable<string>>())).Returns(
                (IEnumerable<string> input) => input); //just return
            _stemmer = new Mock<IStemmer>();
            _stemmer.Setup(x => x.Stem(It.IsAny<string>())).Returns((string x) => x);
            _tokenizer = new Tokenizer(_stopWordService.Object, _stemmer.Object);
        }

        [Test]
        public void OneValidValueReturned()
        {
            const string input = "Hi";

            IEnumerable<string> output = _tokenizer.TokenizeWords(input);

            Assert.That(output.Count() == 1);
            Assert.AreEqual("Hi", output.ToList()[0]);
        }

        [Test]
        public void OneValidValueReturnedWithPeriod()
        {
            const string input = "Hi. Bye!";

            IEnumerable<string> output = _tokenizer.TokenizeWords(input);

            Assert.That(output.Count() == 2);
            Assert.AreEqual("Hi", output.ToList()[0]);
            Assert.AreEqual("Bye", output.ToList()[1]);
        }

        [Test]
        public void GetSentncesReturnsValid()
        {
            const string input = "Hello World!";

            IEnumerable<string> output = _tokenizer.TokenizeSentences(input);

            Assert.That(output.Count() == 1);
        }

        [Test]
        public void TestGets2Sentences()
        {
            const string input = "Hello World! Bye World?";

            IEnumerable<string> output = _tokenizer.TokenizeSentences(input);

            Assert.That(output.Count() == 2);
        }


        [Test]
        public void TestWithCRLF()
        {
            string input = "Hello World!" + Environment.NewLine + "Bye World?";

            IEnumerable<string> output = _tokenizer.TokenizeSentences(input);

            Assert.That(output.Count() == 2);
        }

        [Test]
        public void TestWithCRLFAndNotEndingPunctuation()
        {
            string input = "Hello World" + Environment.NewLine + "Bye World" + Environment.NewLine;

            IEnumerable<string> output = _tokenizer.TokenizeSentences(input);

            Assert.That(output.Count() == 2);
        }
    }
}