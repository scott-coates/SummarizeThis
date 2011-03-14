using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SummarizeThis.Core.Frequency;
using SummarizeThis.Core.Tokenization.Interfaces;
using SummarizeThis.Core.Frequency.Interfaces;
using Moq;

namespace SummarizeThis.Tests.Unit
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

            var input = new[] {"HI", "BYE"};
            _tokenizer.Setup(x => x.TokenizeWords(It.IsAny<string>())).Returns(input);
            _tokenizer.Setup(x => x.TokenizeSentences(It.IsAny<string>())).Returns(new[] {"Hi"});
        }

        [Test]
        public void OneFrequencyWords()
        {
            _tokenizer.Setup(x => x.TokenizeWords(It.IsAny<string>())).Returns(new[] {"hi"});

            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.Count == 1);
        }

        [Test]
        public void CaseInsensitiveWords()
        {
            var input = new[] {"HI", "hi", "HI", "Hi", "hI"};

            _tokenizer.Setup(x => x.TokenizeWords(It.IsAny<string>())).Returns(input);

            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.First().Value == input.Length);
        }

        [Test]
        public void Counter2Words()
        {
            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.Count == 2);
        }

        [Test]
        public void ResultsToLowerForAccurateCounting()
        {
            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.First().Key == "hi");
            Assert.That(frequenices.Last().Key == "bye");
        }
    }
}