// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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

namespace DustInTheWind.WindowsReboot.Core
{
    public class TaskTime
    {
        public JobTimeType Type { get; set; }

        public DateTime DateTime { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public DateTime CalculateTimeFrom(DateTime now)
        {
            switch (Type)
            {
                case JobTimeType.FixedDate:
                    return DateTime;

                case JobTimeType.Delay:
                    return now + new TimeSpan(Hours, Minutes, Seconds);

                case JobTimeType.Immediate:
                    return now;

                default:
                    throw new Exception("Invalid action type.");
            }
        }

        public TimeSpan CalculateIntervalFrom(DateTime now)
        {
            switch (Type)
            {
                case JobTimeType.FixedDate:
                    return now - DateTime;

                case JobTimeType.Delay:
                    return new TimeSpan(Hours, Minutes, Seconds);

                case JobTimeType.Immediate:
                    return TimeSpan.Zero;

                default:
                    throw new Exception("Invalid action type.");
            }
        }
    }
}