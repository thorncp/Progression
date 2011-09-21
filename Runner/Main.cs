using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Progression;

namespace Runner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var progressBar = new ProgressBar(5000, "Standard", 30);

            for (int i = 0; i < 5000; i++) {
                Thread.Sleep(1);
                progressBar.Bump();
            }

            ProgressBar.ForEach(Enumerable.Range(0, 5000), "Printing!", 30, i => {
                // print every 100th int, directly to the console
                Thread.Sleep(1);
                if (i % 100 == 0) Console.Out.WriteLine(i);
            });

            // the progress bar is not thread safe, we must take extra steps when multi threading
            var threadedProgressBar = new ProgressBar(5000, "Threading, ZOMG!", 30);
            Parallel.ForEach(Enumerable.Range(0, 5000), i => {
                Thread.Sleep(1);
                // notice the lock!
                lock (threadedProgressBar) threadedProgressBar.Bump();
            });
        }
    }
}
