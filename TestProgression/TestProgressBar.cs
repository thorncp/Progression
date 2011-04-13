using System;
using NUnit.Framework;
using Progression;

namespace TestProgression
{
    [TestFixture]
    public class TestProgressBar
    {
        private ProgressBar progressBar;

        [SetUp]
        public void Init()
        {
            progressBar = new ProgressBar(100, "Yo Dawg");
        }

        [Test]
        public void TestNewProgressBarHasZeroStatus()
        {
            Assert.AreEqual(0, progressBar.Status);
        }

        [Test]
        public void TestBumpShouldIncreaseTheStatusByOne()
        {
            progressBar.Bump();
            Assert.AreEqual(1, progressBar.Status);
        }

        [Test]
        public void TestUpdateStatusShouldUpdateTheCurrentStatus()
        {
            progressBar.UpdateStatus(50);
            Assert.AreEqual(50, progressBar.Status);
        }

        [Test]
        public void TestUpdatingStatusHigherThanTotalForcesStatusToTotal()
        {
            progressBar.UpdateStatus(150);
            Assert.AreEqual(100, progressBar.Status);
        }

        [Test]
        public void TestUpdatingStatusBelowZeroForcesStatusToZero()
        {
            progressBar.UpdateStatus(-50);
            Assert.AreEqual(0, progressBar.Status);
        }

        [Test]
        public void TestBumpDoesNotUpdateStatusHigherThanTotal()
        {
            progressBar.UpdateStatus(100);
            progressBar.Bump();
            Assert.AreEqual(100, progressBar.Status);
        }

        [Test]
        public void TestPecentCompleteShouldBeZeroForZeroStatus()
        {
            Assert.AreEqual(0, progressBar.PercentComplete);
        }

        [Test]
        public void TestPecentCompleteShouldBeFiftyForHalfStatus()
        {
            progressBar.UpdateStatus(50);
            Assert.AreEqual(0.5, progressBar.PercentComplete);
        }

        [Test]
        public void TestGeneratedStatusStringUsesProgessBarWidth()
        {
            progressBar.Width = 30;

            string statusString = progressBar.GenerateStatusString();
            int leftBracket = statusString.IndexOf('[');
            int rightBracket = statusString.IndexOf(']');

            // the extra 1 is because we're comparing the indices on the outer side of the "important" bits
            Assert.AreEqual(31, rightBracket - leftBracket);
        }

        [Test]
        public void TestZeroStatusGeneratesStatusStringRepresentingZeroState()
        {
            progressBar.Width = 30;
            Assert.AreEqual("Yo Dawg [                              ] 0%", progressBar.GenerateStatusString());
        }

        [Test]
        public void TestHalfStatusGeneratesStatusStringRepresentingHalfState()
        {
            progressBar.Width = 30;
            progressBar.UpdateStatus(50);
            Assert.AreEqual("Yo Dawg [===============               ] 50%", progressBar.GenerateStatusString());
        }

        [Test]
        public void TestFullStatusGeneratesStatusStringRepresentingCompletedState()
        {
            progressBar.Width = 30;
            progressBar.UpdateStatus(100);
            Assert.AreEqual("Yo Dawg [==============================] 100%", progressBar.GenerateStatusString());
        }
    }
}

