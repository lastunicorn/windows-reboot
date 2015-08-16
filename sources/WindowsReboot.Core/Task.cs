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
    public class Task
    {
        public TaskType Type { get; set; }
        public TaskTime Time { get; set; }

        public bool ForceAction { get; set; }
        public bool DisplayWarningMessage { get; set; }

        public Task()
        {
            ForceAction = true;
            DisplayWarningMessage = true;
        }

        public DateTime CalculateTimeToRun(DateTime now)
        {
            return Time.CalculateTimeFrom(now);
        }

        public TimeSpan CalculateTimeUntilRun(DateTime now)
        {
            return Time.CalculateIntervalFrom(now);
        }
    }
}