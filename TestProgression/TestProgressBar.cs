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
            var progressBar = new ProgressBar (100);
            Assert.AreEqual (100, progressBar.Total);
        }

        [Test]
        public void TestNewProgressBarHasZeroStatus()
        {
            var progressBar = new ProgressBar (100);
            Assert.AreEqual (0, progressBar.Status);
        }
    }
}

