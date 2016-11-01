// Windows Reboot
// Copyright (C) 2009-2015 Dust in the Wind
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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal class ActionTimeControlViewModel : ViewModelBase
    {
        private TaskTimeType taskTimeType;
        private DateTime fixedDateTime;
        private int delayHours;
        private int delayMinutes;
        private int delaySeconds;
        private TimeSpan dailyTime;

        public TaskTimeType TaskTimeType
        {
            get { return taskTimeType; }
            set
            {
                taskTimeType = value;
                OnPropertyChanged("TaskTimeType");
            }
        }

        public DateTime FixedDateTime
        {
            get { return fixedDateTime; }
            set
            {
                fixedDateTime = value;
                OnPropertyChanged("FixedDateTime");
            }
        }

        public int DelayHours
        {
            get { return delayHours; }
            set
            {
                delayHours = value;
                OnPropertyChanged("DelayHours");
            }
        }

        public int DelayMinutes
        {
            get { return delayMinutes; }
            set
            {
                delayMinutes = value;
                OnPropertyChanged("DelayMinutes");
            }
        }

        public int DelaySeconds
        {
            get { return delaySeconds; }
            set
            {
                delaySeconds = value;
                OnPropertyChanged("DelaySeconds");
            }
        }

        public TimeSpan DailyTime
        {
            get { return dailyTime; }
            set
            {
                dailyTime = value;
                OnPropertyChanged("DailyTime");
            }
        }

        public ActionTimeControlViewModel()
        {
            fixedDateTime = DateTime.Now;
            delayHours = 0;
            delayMinutes = 0;
            delaySeconds = 0;
            dailyTime = TimeSpan.Zero;
        }

        public void Clear()
        {
            FixedDateTime = DateTime.Now;
            DelayHours = 0;
            DelayMinutes = 0;
            DelaySeconds = 0;
            DailyTime = TimeSpan.Zero;
        }
    }
}
