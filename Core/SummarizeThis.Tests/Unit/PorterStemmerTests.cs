using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using NUnit.Framework;

namespace SummarizeThis.Tests.Unit
{
    [TestFixture]
    class PorterStemmerTests
    {
        [Test]
        public void LessThan2Chars()
        {
            var at = "at";
            var stemmer = new PorterStemmer();

            Assert.AreEqual(at, stemmer.Stem(at));
        }
    }
}
