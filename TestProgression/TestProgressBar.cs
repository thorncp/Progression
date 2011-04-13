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
    }
}

