using System;
using NUnit.Framework;
using TestProgression.Mock;

namespace TestProgression.TestMock
{
    [TestFixture]
    public class TestMockConsole
    {
        [Test]
        public void TestWriteAppendsTextToOutputBuffer()
        {
            var console = new MockConsole();

            console.Write("Yo Dawg");
            Assert.AreEqual("Yo Dawg", console.Text);

            console.Write(", I heard you liked tests so I wrote some tests for your tests.");
            Assert.AreEqual("Yo Dawg, I heard you liked tests so I wrote some tests for your tests.", console.Text);
        }

        [Test]
        public void TestWriteMovesTheCursorPositionBackToZeroWhenTextStartsWithCarriageReturn()
        {
            var console = new MockConsole();
            console.Write("Yo Dawg");
            console.Write("\rHi");
            Assert.AreEqual("Hi Dawg", console.Text);
        }

        [Test]
        public void TestWriteDoesNotResetTheCursorWhenTextStartsWithCarriageReturnLineFeed()
        {
            var console = new MockConsole();
            console.Write("Yo Dawg");
            console.Write(Environment.NewLine + "Hi");
            Assert.AreEqual("Yo Dawg" + Environment.NewLine + "Hi", console.Text);
        }

        [Test]
        public void TestCarriageReturnDoesNotOverwriteNewlines()
        {
            var console = new MockConsole();
            console.WriteLine("Yo Dawg");
            console.Write("\rHi");
            Assert.AreEqual("Yo Dawg" + Environment.NewLine + "Hi", console.Text);
        }
    }
}

