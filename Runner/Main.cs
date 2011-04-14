using System;
using System.Threading;
using Progression;
using TestProgression;

namespace Runner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var progressBar = new ProgressBar(100, "Herp Derp");
            for (int i = 0; i < 100; i++) {
                Thread.Sleep(50);
                progressBar.Bump();
                Console.Out.WriteLine(i.ToString());
            }
        }
    }
}
