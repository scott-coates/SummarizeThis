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

            var input = new[] { "HI", "BYE" };
            _tokenizer.Setup(x => x.TokenizeWords(It.IsAny<string>())).Returns(input);

            _tokenizer.Setup(x => x.TokenizeSentences(It.IsAny<string>())).Returns(new[] { "Hi" });
        }

        [Test]
        public void OneFrequencyWords()
        {
            _tokenizer.Setup(x => x.TokenizeWords(It.IsAny<string>())).Returns(new[] { "hi" });

            Dictionary<string, int> frequenices = _frequencer.GetWordFrequency(It.IsAny<string>());

            Assert.That(frequenices.Count == 1);
        }

        [Test]
        public void CaseInsensitiveWords()
        {
            var input = new[] { "HI", "hi", "HI", "Hi", "hI" };

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

        [Test]
        public void TestHowManyWordsToReturn()
        {
            var input = new Dictionary<string, int> { { "Foo", 1 }, { "Bar", 1 } };

            var frequentWords = _frequencer.GetMostFrequentWords(0, input);

            Assert.That(frequentWords.Count() == 0);
        }

        [Test]
        public void OrderOfInputMaintainedWhenTakingSeveralBack()
        {
            var input = new Dictionary<string, int> { { "Hello", 1 }, { "Foo", 5 }, { "Scott", 3 }, { "Bar", 5 }, { "World", 3 } };

            var frequentWords = _frequencer.GetMostFrequentWords(1, input);

            Assert.That(frequentWords.Count() == 1);
            Assert.That(frequentWords.First() == "Foo");
        }

        [Test]
        public void OrderByCount()
        {
            var input = new Dictionary<string, int> { { "Foo", 2 }, { "Bar", 5 } };

            var frequentWords = _frequencer.GetMostFrequentWords(1, input);

            Assert.That(frequentWords.Count() == 1);
            Assert.That(frequentWords.First() == "Bar");
        }

        [Test]
        public void OneSentenceMostFrequentNoWords()
        {
            var sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(1, It.IsAny<string>(),
                                                                                               new string[] { });

            Assert.That(sentencesWithMostFrequentWords.Count() == 0);
        }

        [Test]
        public void OneSentenceOneFrequentWord()
        {
            var sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(0, It.IsAny<string>(),
                                                                                               new[] { "Hi" });

            Assert.That(sentencesWithMostFrequentWords.Count() == 0);
        }

        [Test]
        public void ReturnOneSentence()
        {
            var sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(1, It.IsAny<string>(),
                                                                                               new[] { "Hi" });

            Assert.That(sentencesWithMostFrequentWords.Count() == 1);
        }

        [Test]
        public void MultipleSentences()
        {
            var output = new[]
                             {
                                 "A desk is a great thing"
                                 , "Why",
                                 "Because a desk rock"
                             };
            _tokenizer.Setup(x => x.TokenizeSentences(It.IsAny<string>())).Returns(output);

            var sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(1, It.IsAny<string>(),
                                                                                               new[] { "desk" });

            Assert.That(sentencesWithMostFrequentWords.Count() == 1);

            Assert.That(sentencesWithMostFrequentWords.First() == "A desk is a great thing");
        }

        [Test]
        public void DuplicatesNotReturned()
        {
            var output = new[]
                             {
                                 "A desk is a great thing",
                                 "Because it's rockin",
                                 "A desk is a great thing"
                             };
            _tokenizer.Setup(x => x.TokenizeSentences(It.IsAny<string>())).Returns(output);

            var sentencesWithMostFrequentWords = _frequencer.GetSentencesWithMostFrequentWords(2, It.IsAny<string>(),
                                                                                               new[] { "desk" });

            Assert.That(sentencesWithMostFrequentWords.Count() == 2);

            Assert.That(sentencesWithMostFrequentWords.First() == "A desk is a great thing");
            Assert.That(sentencesWithMostFrequentWords.Last() == "Because it's rockin");
        }
    }
}