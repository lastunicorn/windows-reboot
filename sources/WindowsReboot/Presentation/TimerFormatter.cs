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

namespace DustInTheWind.WindowsReboot.Presentation
{
    static class TimerFormatter
    {
        /// <summary>
        /// The template used to display the time left until action.
        /// </summary>
        private const string TimeTemplate = "{0:00} : {1:00} : {2:00} . {3:0}";

        /// <summary>
        /// The text displayed when the timer is stopped.
        /// </summary>
        private const string TimeTemplateEmpty = "--  :  --  :  --  .  -";

        public static string Format(TimeSpan? time)
        {
            return time.HasValue
                ? FormatTime(time.Value)
                : TimeTemplateEmpty;
        }

        private static string FormatTime(TimeSpan time)
        {
            string tmp;

            int d = time.Days;
            int h = time.Hours;
            int m = time.Minutes;
            int s = time.Seconds;
            int f = Convert.ToInt32(Math.Round((double)(time.Milliseconds / 100)));

            if (d == 1)
            {
                tmp = "1 day . ";
            }
            else if (d > 1)
            {
                tmp = d + " days . ";
            }
            else
            {
                tmp = string.Empty;
            }

            tmp += string.Format(TimeTemplate, h, m, s, f);
            return tmp;
        }
    }
}