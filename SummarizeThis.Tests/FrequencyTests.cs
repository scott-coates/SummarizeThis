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
    }
}
