using DustInTheWind.WindowsReboot.Core;
using System;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.PresentActionTimeSettings
{
    public class PresentActionTimeSettingsResponse
    {
        public ScheduleTimeType Type { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan TimeOfDay { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public bool IsAllowedToChange { get; set; }
    }
}