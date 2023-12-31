﻿// Windows Reboot
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
using DustInTheWind.WindowsReboot.Domain.Scheduling;

namespace DustInTheWind.WindowsReboot.Application
{
    public class ScheduleChangedEvent
    {
        public ScheduleType Type { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan TimeOfDay { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        internal ScheduleChangedEvent(ISchedule schedule)
        {
            switch (schedule)
            {
                case FixedDateSchedule fixedDateSchedule:
                    DateTime = fixedDateSchedule.DateTime;
                    Type = ScheduleType.FixedDate;
                    break;

                case DailySchedule dailySchedule:
                    TimeOfDay = dailySchedule.TimeOfDay;
                    Type = ScheduleType.Daily;
                    break;

                case DelaySchedule delaySchedule:
                    Hours = delaySchedule.Hours;
                    Minutes = delaySchedule.Minutes;
                    Seconds = delaySchedule.Seconds;

                    Type = ScheduleType.Delay;
                    break;

                case ImmediateSchedule _:
                    Type = ScheduleType.Immediate;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}