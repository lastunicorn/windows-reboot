using System;

namespace DustInTheWind.WindowsReboot.Application.StatusArea.PresentTimerStatus
{
    public class PresentTimerStatusResponse
    {
        public DateTime CurrentTime { get; set; }

        public DateTime? ActionTime { get; set; }
    }
}