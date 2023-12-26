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

namespace DustInTheWind.WinFormsAdditions
{
    public readonly struct TimerText
    {
        private const string TimeTemplate = "{0:00} : {1:00} : {2:00} . {3:0}";
        private const string TimeTemplateEmpty = "--  :  --  :  --  .  -";

        private readonly string text;

        public TimerText(TimeSpan? time)
        {
            text = time.HasValue
                ? FormatTime(time.Value)
                : TimeTemplateEmpty;
        }

        public static TimerText Empty { get; } = new TimerText(null);

        private static string FormatTime(TimeSpan time)
        {
            int d = time.Days;
            int h = time.Hours;
            int m = time.Minutes;
            int s = time.Seconds;
            int f = Convert.ToInt32(Math.Round((double)(time.Milliseconds / 100)));

            string daysText;

            if (d == 1)
                daysText = "1 day . ";
            else if (d > 1)
                daysText = d + " days . ";
            else
                daysText = string.Empty;

            return daysText + string.Format(TimeTemplate, h, m, s, f);
        }

        public override string ToString()
        {
            return text;
        }

        public static implicit operator string(TimerText timerText)
        {
            return timerText.ToString();
        }

        public static implicit operator TimerText(TimeSpan? timeSpan)
        {
            return new TimerText(timeSpan);
        }
    }
}