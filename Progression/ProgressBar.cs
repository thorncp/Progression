using System;
using System.Collections.Generic;
using System.Linq;

namespace Progression
{
    public class ProgressBar
    {
        private readonly ConsoleWrapper consoleWrapper;

        public ProgressBar(int total, string title, int width)
        {
            Total = total;
            Title = title;
            Width = width;

            consoleWrapper = new ConsoleWrapper(Console.Out);
            Console.SetOut(consoleWrapper);

            consoleWrapper.PreWrite += ClearStatus;
            consoleWrapper.PostWrite += PrintStatus;

            PrintStatus();
        }

        public int Total { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }

        public int Width { get; set; }

        public double PercentComplete { get; private set; }

        public bool IsRunning
        {
            get { return Status < Total; }
        }

        public void Bump()
        {
            UpdateStatus(Status + 1);
        }

        public void UpdateStatus(int status)
        {
            if (status > Total) throw new ArgumentException("Cannot update status greater than total", "status");
            if (status < 0) throw new ArgumentException("Cannot update status less than zero", "status");

            Status = Math.Max(Math.Min(status, Total), 0);
            double previousPercentComplete = PercentComplete;
            PercentComplete = Math.Round((double)Status / Total, 2, MidpointRounding.AwayFromZero);

            // long running progress bars exhibit a flickering behavior when printed to the console
            // on every cycle. to avoid that, we only print when there is a change significant enough
            // to change text that will be printed.
            if (PercentComplete > previousPercentComplete) PrintStatus();

            if (Status == Total)
            {
                consoleWrapper.BypassWrite(" done");
                consoleWrapper.WriteLine();
            }
        }

        private string GenerateStatusString()
        {
            int ticksCompleted = (int)(PercentComplete * Width);

            string ticks = string.Empty.PadLeft(ticksCompleted, '=');
            string spaces = string.Empty.PadLeft(Width - ticksCompleted, ' ');

            return string.Format("{0} [{1}{2}] {3:0}%", Title, ticks, spaces, PercentComplete * 100);
        }

        private void PrintStatus()
        {
            consoleWrapper.BypassWrite("\r" + GenerateStatusString());
        }

        private void ClearStatus()
        {
            int size = GenerateStatusString().Length;
            consoleWrapper.BypassWrite("\r".PadRight(size + 1) + "\r");
        }

        public static void ForEach<T>(IEnumerable<T> elements, string title, int width, Action<T> action)
        {
            var array = elements.ToArray();
            var progressBar = new ProgressBar(array.Length, title, width);

            foreach (T element in array)
            {
                action(element);
                progressBar.Bump();
            }
        }
    }
}
