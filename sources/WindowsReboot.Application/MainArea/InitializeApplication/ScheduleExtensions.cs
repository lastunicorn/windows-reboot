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
using DustInTheWind.WindowsReboot.Domain.Scheduling;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using ScheduleType = DustInTheWind.WindowsReboot.Ports.ConfigAccess.ScheduleType;

namespace DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication
{
    internal static class ScheduleExtensions
    {
        public static ISchedule ToDomain(this Schedule schedule)
        {
            switch (schedule.Type)
            {
                case ScheduleType.FixedDate:
                    return new FixedDateSchedule
                    {
                        DateTime = schedule.DateTime
                    };

                case ScheduleType.Daily:
                    return new DailySchedule
                    {
                        TimeOfDay = schedule.TimeOfDay
                    };

                case ScheduleType.Delay:
                    return new DelaySchedule
                    {
                        Hours = schedule.Hours,
                        Minutes = schedule.Minutes,
                        Seconds = schedule.Seconds
                    };

                case ScheduleType.Immediate:
                    return new ImmediateSchedule();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Schedule ToConfigModel(this ISchedule schedule)
        {
            if (schedule is FixedDateSchedule fixedDateSchedule)
            {
                return new Schedule
                {
                    DateTime = fixedDateSchedule.DateTime,
                    Type = ScheduleType.FixedDate
                };
            }

            if (schedule is DailySchedule dailySchedule)
            {
                return new Schedule
                {
                    TimeOfDay = dailySchedule.TimeOfDay,
                    Type = ScheduleType.Daily
                };
            }

            if (schedule is DelaySchedule delaySchedule)
            {
                return new Schedule
                {
                    Hours = delaySchedule.Hours,
                    Minutes = delaySchedule.Minutes,
                    Seconds = delaySchedule.Seconds,
                    Type = ScheduleType.Delay
                };
            }

            if (schedule is ImmediateSchedule immediateSchedule)
            {
                return new Schedule
                {
                    Type = ScheduleType.Immediate
                };
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}