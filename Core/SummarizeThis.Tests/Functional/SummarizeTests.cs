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

        #region BigInput

        private const string _bigInput =
            @"What is a help desk?

In general, a help desk is where end-users go to for support when they can't solve an issue themselves or through the help of others. A help desk system provides IT with work flow that supports the process of providing structured service and support to the end-users.

Why is this important?

The IT department sees two very important issues being resolved by using a formal Helpdesk process.  The first is issue tracking. By appropriate tracking we may find that many other people experience the same type of issues and we will be able to diagnose the problem more efficiently and with greater speed.  

The second issue is user confidence.  From a management point of view we need to provide confidence for our user base so that when you submit a particular inquiry you will know how long it will take to hear back from IT.  The best way to do this is through Service Level Agreements or (SLA's).  This will better help us prioritize our work to control response times, increase efficiency and enforce standards across our entire operation.

Attached to this communication is a printable reference sheet that will help you choose your priority when you submit your ticket to the help desk.  

What can you do to help?

The success of the Helpdesk is based on your adoption. We are asking you whenever possible to please submit your issue through the online portal rather than make a phone call.  In the case of a system outage or inability to login you will need to call and a ticket will be created for you. You can call the help desk at 920-471-4655 or ext. 7122. Keep in mind if you have a working system and call the help desk they will ask you to submit a ticket through the portal.  To send a ticket visit: helpdesk.nationalaudit.com

You can submit a ticket to the help desk 24 hours a day 7 days a week.  The help desk core hours will be from Monday – Friday 8am to 4:30 pm cst.  If it is an urgent issue, please see your supervisor/manager and they have means to contact the Helpdesk staff.  Non-urgent matters outside of those hours will be answered the next business day.  

As always, if you have any questions please do not hesitate to ask.";

        #endregion

        [SetUp]
        public void Setup()
        {
            _summarizer = new Summarizer();
        }

        [Test]
        public void OneSentenceReturnShortInput()
        {
            const string input =
                "NClassifier is a dotnet assembly for working with text.  NClassifier includes a summarizer.";
            const string expectedResult = "NClassifier is a dotnet assembly for working with text.";
            string result = _summarizer.Summarize(input, 1).SummarizedText;
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MostFrequentWord()
        {
            const string input =
                "NClassifier is a dotnet assembly for working with text.  NClassifier includes a summarizer.";
            const string expectedResult = "nclassifi";
            string result = _summarizer.Summarize(input, 1).HighestRankingWordFrequency.First().Key;
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TwoSentenceReturnLongerInput()
        {
            const string input =
                "NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer. A Summarizer allows the summary of text. A Summarizer is really cool. I don't think there are any other dotnet summarizers.";
            const string expectedResult =
                "NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer.";

            string result = _summarizer.Summarize(input, 2).SummarizedText;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CorrectSummarized()
        {
            const string input =
                "NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer. A Summarizer allows the summary of text. A Summarizer is really cool. I don't think there are any other dotnet summarizers.";

            string result = _summarizer.Summarize(input, 2).HighestRankingWordFrequency.First().Key;

            Assert.AreEqual("summar", result);
        }

        [Test]
        public void OneSentenceReturnLongerInput()
        {
            const string input =
                "NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer. A Summarizer allows the summary of text. A Summarizer is really cool. I don't think there are any other dotnet summarizers.";
            const string expectedResult = "NClassifier includes a summarizer.";

            var result = _summarizer.Summarize(input, 1);

            Assert.AreEqual(expectedResult, result.SummarizedText);
        }

        [Test]
        public void DupesIgnored()
        {
            const string input =
                "NClassifier is a dotnet assembly for working with text. NClassifier is a dotnet assembly for working with text. NClassifier includes a summarizer. A Summarizer allows the summary of text. A Summarizer is really cool. I don't think there are any other dotnet summarizers. NClassifier is a dotnet assembly for working with text.";
            const string expectedResult =
                "NClassifier is a dotnet assembly for working with text. I don't think there are any other dotnet summarizers.";


            string result = _summarizer.Summarize(input, 2).SummarizedText;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CorrectOrder()
        {
            const string expected =
                "A help desk system provides IT with work flow that supports the process of providing structured service and support to the end-users. Attached to this communication is a printable reference sheet that will help you choose your priority when you submit your ticket to the help desk. Keep in mind if you have a working system and call the help desk they will ask you to submit a ticket through the portal.";


            TextSummary result = _summarizer.Summarize(_bigInput, 3);

            Assert.AreEqual(result.SummarizedText, expected);
        }

        [Test]
        public void CaseNotImportant()
        {
            const string input =
                "What is a help desk? WHAT IS A HELP DESK? In general, a help desk is where end-users go to for support when they can't solve an issue themselves or through the help of others. By appropriate tracking we may find that many other people experience the same type of issues and we will be able to diagnose the problem more efficiently and with greater speed.";
            const string expected =
                "In general, a help desk is where end-users go to for support when they can't solve an issue themselves or through the help of others. By appropriate tracking we may find that many other people experience the same type of issues and we will be able to diagnose the problem more efficiently and with greater speed.";

            TextSummary result = _summarizer.Summarize(input, 2);

            Assert.AreEqual(result.SummarizedText, expected);
        }

        [Test]
        public void RequestedOutputMoreThanInput()
        {
            const string input = "Test. Hi. Test World. Hello.";

            TextSummary result = _summarizer.Summarize(input, 5);

            Assert.AreEqual(input, result.SummarizedText);
        }
    }
}