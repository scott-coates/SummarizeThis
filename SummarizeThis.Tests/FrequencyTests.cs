using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SummarizeThis.Core.Tokenization.Interfaces;
using SummarizeThis.Core.Frequency.Interfaces;
using Moq;

namespace SummarizeThis.Tests
{
    [TestFixture]
    public class FrequencyTests
    {
        private Mock<ITokenizer> _tokenizer;
        private IFrequencer _frequencer;

        [SetUp]
        public void Setup()
        {
            _tokenizer = new Mock<ITokenizer>();
            _frequencer = new Frequencer(_tokenizer.Object);
        }

        [Test]
        public void OneFrequencyWords()
        {
            _tokenizer.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[] { "hi" });

            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.Count == 1);
        }

        [Test]
        public void CaseInsensitiveWords()
        {
            var input = new[] { "HI", "hi", "HI", "Hi", "hI" };

            _tokenizer.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(input);

            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.First().Value == input.Length);
        }

        [Test]
        public void Counter2Words()
        {
            var input = new[] { "HI", "BYE" };

            _tokenizer.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(input);

            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.Count == 2);
        }

        [Test]
        public void TestHowManyWordsToReturn()
        {
            var input = new Dictionary<string, int> { { "Foo", 1 }, { "Bar", 1 } };

            var frequentWords = _frequencer.GetMostFrequentWords(0, input);

            Assert.That(frequentWords.Count() == 0);
        }
    }
}
