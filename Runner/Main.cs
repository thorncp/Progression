using System.Threading;
using Progression;

namespace Runner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var progressBar = new ProgressBar(50000, "Herp Derp", 30);
            for (int i = 0; i < 50000; i++) {
                Thread.Sleep(1);
                progressBar.Bump();
            }
        }
    }
}
