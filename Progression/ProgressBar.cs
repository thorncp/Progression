using System;
using System.IO;
using System.Text;

namespace Progression
{
    public class ProgressBar
    {
        public ProgressBar(int total, string title, int width = 30)
        {
            Total = total;
            Title = title;
            Width = width;

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
            Console.Out.Write("\r" +  GenerateStatusString());
        }
    }
}

