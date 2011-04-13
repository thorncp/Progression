using System;

namespace Progression
{
    public class ProgressBar
    {
        public ProgressBar(int total,string title = null)
        {
            Total = total;
            Title = title;
        }

        public int Total { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }

        public void Bump()
        {
            UpdateStatus(Status + 1);
        }

        public void UpdateStatus(int status)
        {
            Status = Math.Max(Math.Min(status, Total), 0);
        }
    }
}

