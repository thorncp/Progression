using System;
using NUnit.Framework;
using Progression;

namespace TestProgression
{
    [TestFixture]
    public class TestProgressBar
    {
        [Test]
        public void TestNewProgressBarHasCorrectTotal()
        {
            var progressBar = new ProgressBar(100);
            Assert.AreEqual(100, progressBar.Total);
        }

        [Test]
        public void TestNewProgressBarHasZeroStatus()
        {
            var progressBar = new ProgressBar(100);
            Assert.AreEqual(0, progressBar.Status);
        }

        [Test]
        public void TestNewProgressBarWithTitleShouldRetainTitle()
        {
            var progressBar = new ProgressBar(100, "Yo Dawg");
            Assert.AreEqual("Yo Dawg", progressBar.Title);
        }

        [Test]
        public void TestNewProgressBarWithoutTitleShouldHaveNullTitle()
        {
            var progressBar = new ProgressBar(100);
            Assert.IsNull(progressBar.Title);
        }

        [Test]
        public void TestBumpingTheProgressBarShouldIncreaseTheStatusByOne()
        {
            var progressBar = new ProgressBar(100);
            progressBar.Bump();
            Assert.AreEqual(1, progressBar.Status);
        }

        [Test]
        public void TestSetStatusShouldUpdateTheCurrentStatus()
        {
            var progressBar = new ProgressBar(100);
            progressBar.UpdateStatus(50);
            Assert.AreEqual(50, progressBar.Status);
        }

        [Test]
        public void TestUpdatingStatusHigherThanTotalForcesStatusToTotal()
        {
            var progressBar = new ProgressBar(100);
            progressBar.UpdateStatus(150);
            Assert.AreEqual(100, progressBar.Status);
        }

        [Test]
        public void TestUpdatingStatusBelowZeroForcesStatusToZero()
        {
            var progressBar = new ProgressBar(100);
            progressBar.UpdateStatus(-50);
            Assert.AreEqual(0, progressBar.Status);
        }

        [Test]
        public void TestBumpDoesNotUpdateStatusHigherThanTotal()
        {
            var progressBar = new ProgressBar(100);
            progressBar.UpdateStatus(100);
            progressBar.Bump();
            Assert.AreEqual(100, progressBar.Status);
        }

        [Test]
        public void TestPecentCompleteShouldBeZeroForZeroStatus()
        {
            var progressBar = new ProgressBar(100);
            Assert.AreEqual(0, progressBar.PercentComplete);
        }

        [Test]
        public void TestPecentCompleteShouldBeFiftyForHalfStatus()
        {
            var progressBar = new ProgressBar(100);
            progressBar.UpdateStatus(50);
            Assert.AreEqual(0.5, progressBar.PercentComplete);
        }

        [Test]
        public void TestGeneratedStatusStringUsesProgessBarWidth()
        {
            var progressBar = new ProgressBar(100, "Yo Dawg");
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
            var progressBar = new ProgressBar(100, "Yo Dawg");
            progressBar.Width = 30;
            Assert.AreEqual("Yo Dawg [                              ] 0%", progressBar.GenerateStatusString());
        }

        [Test]
        public void TestHalfStatusGeneratesStatusStringRepresentingHalfState()
        {
            var progressBar = new ProgressBar(100, "Yo Dawg");
            progressBar.Width = 30;
            progressBar.UpdateStatus(50);
            Assert.AreEqual("Yo Dawg [===============               ] 50%", progressBar.GenerateStatusString());
        }

        [Test]
        public void TestFullStatusGeneratesStatusStringRepresentingCompletedState()
        {
            var progressBar = new ProgressBar(100, "Yo Dawg");
            progressBar.Width = 30;
            progressBar.UpdateStatus(100);
            Assert.AreEqual("Yo Dawg [==============================] 100%", progressBar.GenerateStatusString());
        }
    }
}

