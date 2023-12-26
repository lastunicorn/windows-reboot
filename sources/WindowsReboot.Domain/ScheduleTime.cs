// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace DustInTheWind.WindowsReboot.Domain
{
    public class ScheduleTime
    {
        public ScheduleTimeType Type { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan TimeOfDay { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public static ScheduleTime Immediate { get; } = new ScheduleTime
        {
            Type = ScheduleTimeType.Immediate
        };

        public ScheduleTime()
        {
            Type = ScheduleTimeType.Immediate;
            DateTime = DateTime.Now;
        }

        public DateTime CalculateTimeFrom(DateTime now)
        {
            switch (Type)
            {
                case ScheduleTimeType.FixedDate:
                    return DateTime;

                case ScheduleTimeType.Daily:
                {
                    DateTime potentialTime = now.Date + TimeOfDay;

                    while (potentialTime < now)
                        potentialTime += TimeSpan.FromDays(1);

                    // todo: check if reached DateTime.Max

                    return potentialTime;
                }

                case ScheduleTimeType.Delay:
                    return now + new TimeSpan(Hours, Minutes, Seconds);

                case ScheduleTimeType.Immediate:
                    return now;

                default:
                    throw new Exception("Invalid action type.");
            }
        }
    }
}