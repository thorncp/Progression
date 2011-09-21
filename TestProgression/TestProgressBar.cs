using System;
using NUnit.Framework;
using Progression;
using TestProgression.Mock;

namespace TestProgression
{
    [TestFixture]
    public class TestProgressBar
    {
        private ProgressBar progressBar;
        private MockConsole console;

        [SetUp]
        public void Init()
        {
            console = new MockConsole();
            Console.SetOut(console);
            progressBar = new ProgressBar(100, "Yo Dawg", 30);
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
        public void TestUpdatingStatusHigherThanTotalThrowsException()
        {
            Assert.Throws<ArgumentException>(() => progressBar.UpdateStatus(150));
        }

        [Test]
        public void TestUpdatingStatusBelowZeroThrowsException()
        {
            Assert.Throws<ArgumentException>(() => progressBar.UpdateStatus(-50));
        }

        [Test]
        public void TestBumpingAfterCompleteThrowsException()
        {
            progressBar.UpdateStatus(100);
            Assert.Throws<ArgumentException>(() => progressBar.Bump());
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
        public void TestIsRunningIsTrueWhenStatusIsLessThanTotal()
        {
            Assert.IsTrue(progressBar.IsRunning);
        }

        [Test]
        public void TestIsRunningIsFalseWhenStatusIsEqualToTotal()
        {
            progressBar.UpdateStatus(100);
            Assert.IsFalse(progressBar.IsRunning);
        }

        [Test]
        public void TestGeneratedStatusStringUsesProgessBarWidth()
        {
            string statusString = console.Text;
            int leftBracket = statusString.IndexOf('[');
            int rightBracket = statusString.IndexOf(']');

            // the extra 1 is because we're comparing the indices on the outer side of the "important" bits
            Assert.AreEqual(31, rightBracket - leftBracket);
        }

        [Test]
        public void TestZeroStatusGeneratesStatusStringRepresentingZeroState()
        {
            Assert.AreEqual("Yo Dawg [                              ] 0%", console.Text);
        }

        [Test]
        public void TestHalfStatusGeneratesStatusStringRepresentingHalfState()
        {
            progressBar.UpdateStatus(50);
            Assert.AreEqual("Yo Dawg [===============               ] 50%", console.Text);
        }

        [Test]
        public void TestFullStatusGeneratesStatusStringRepresentingCompletedState()
        {
            progressBar.UpdateStatus(100);
            Assert.AreEqual("Yo Dawg [==============================] 100% done", console.Text);
        }

        [Test]
        public void TestUpdatingStatusOverwirtesOldStatusInConsole()
        {
            // this test is really covered by the previous two, as the status is dumped on progress
            // bar creation, so anything after that would have had to overwrite the old.
            // including it here as a seperate test for clarity.

            Assert.AreEqual("Yo Dawg [                              ] 0%", console.Text);
            progressBar.UpdateStatus(50);
            Assert.AreEqual("Yo Dawg [===============               ] 50%", console.Text);
        }

        [Test]
        public void TestWritingToConsoleDuringProgressBarWritesTheTextOverTheProgressBarAndMovesItToTheNextLine()
        {
            Console.Out.WriteLine("Herp Derp");

            string[] lines = console.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // the spaces make up the length of the progress was overwritten.
            // the \r will of course print the text on top of these spaces in the console
            Assert.AreEqual("                                           \rHerp Derp", lines [0]);

            Assert.AreEqual("Yo Dawg [                              ] 0%", lines [1]);
        }

        [Test]
        public void TestCompletingProgressBarRelinquishesControlOfTheConsole()
        {
            // flush while the progress bar is still active
            Console.Out.Flush();

            // assert our flag hasn't been tripped. this means our console object isn't
            // currently set as Console.Out
            Assert.False(console.Flushed);

            progressBar.UpdateStatus(100);

            // flush after the progress bar has finished
            Console.Out.Flush();

            // this should trip our flag, showing that our console object is set
            // as the current Console.Out
            Assert.True(console.Flushed);
        }

        [Test]
        public void TestForEachYieldsEachElement()
        {
            var array = new[] { 1,2,3,4,5 };
            int index = 0;
            ProgressBar.ForEach(array, "Yo Dawg", 30, element => {
                Assert.AreEqual(array[index++], element);
            });
        }
    }
}

