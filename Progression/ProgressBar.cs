using System;
using System.Collections.Generic;
using System.Linq;

namespace Progression
{
    public class ProgressBar
    {
        private readonly ConsoleWrapper consoleWrapper;

        public ProgressBar(int total, string title, int width = 30)
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

        public double PercentComplete
        {
            get { return Math.Round((double)Status / Total, 2, MidpointRounding.AwayFromZero); }
        }

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
            Status = Math.Max(Math.Min(status, Total), 0);
            PrintStatus();
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

        public static void For<T>(IEnumerable<T> elements, string title, Action<T> action)
        {
            var progressBar = new ProgressBar(elements.Count(), title);

            foreach (T element in elements)
            {
                action(element);
                progressBar.Bump();
            }
        }
    }
}
