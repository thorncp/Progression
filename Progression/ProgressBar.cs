using System;

namespace Progression
{
    public class ProgressBar
    {
        public ProgressBar (int total)
        {
            Total = total;
        }

        public int Total { get; set; }

        public int Status { get; set; }
    }
}

