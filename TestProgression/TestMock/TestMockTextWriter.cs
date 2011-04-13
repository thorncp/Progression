using System;
using NUnit.Framework;

namespace TestProgression.TestMock
{
    [TestFixture]
    public class TestMockTextWriter
    {
        [Test]
        public void TestWriteAppendsTextToOutputBuffer()
        {
            var buffer = new MockTextWriter();

            buffer.Write("Yo Dawg");
            Assert.AreEqual("Yo Dawg", buffer.Text);

            buffer.Write(", I heard you liked tests so I wrote some tests for your tests.");
            Assert.AreEqual("Yo Dawg, I heard you liked tests so I wrote some tests for your tests.", buffer.Text);
        }

        [Test]
        public void TestWriteMovesTheCursorPositionBackToZeroWhenTextStartsWithCarriageReturn()
        {
            var buffer = new MockTextWriter();
            buffer.Write("Yo Dawg");
            buffer.Write("\rHi");
            Assert.AreEqual("Hi Dawg", buffer.Text);
        }
    }
}

