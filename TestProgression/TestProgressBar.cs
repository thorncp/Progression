using System;
using NUnit.Framework;
using Progression;

namespace TestProgression
{
	[TestFixture]
	public class TestProgressBar
	{
		[Test]
		public void TestNewProgressBarHasZeroStatus()
		{
			var progressBar = new ProgressBar();
			Assert.AreEqual(0, progressBar.Status);
		}
	}
}

