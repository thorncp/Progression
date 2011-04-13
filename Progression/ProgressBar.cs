using System;

namespace Progression
{
    public class ProgressBar
    {
        public ProgressBar (int total, string title = null)
        {
            Total = total;
            Title = title;
        }

        public int Total { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }
    }
}

