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
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.UiCommon;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTimeControlViewModel : ViewModelBase
    {
        private readonly ExecutionTimer executionTimer;
        private readonly IUserInterface userInterface;
        private bool updateFromBusiness;

        private ScheduleTimeType scheduleTimeType;
        private DateTime fixedDate;
        private TimeSpan fixedTime;
        private int delayHours;
        private int delayMinutes;
        private int delaySeconds;
        private TimeSpan dailyTime;
        private bool enabled;

        public ScheduleTimeType ScheduleTimeType
        {
            get { return scheduleTimeType; }
            set
            {
                scheduleTimeType = value;
                OnPropertyChanged("ScheduleTimeType");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public DateTime FixedDate
        {
            get { return fixedDate; }
            set
            {
                fixedDate = value;
                OnPropertyChanged("FixedDate");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public TimeSpan FixedTime
        {
            get { return fixedTime; }
            set
            {
                fixedTime = value;
                OnPropertyChanged("FixedTime");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelayHours
        {
            get { return delayHours; }
            set
            {
                delayHours = value;
                OnPropertyChanged("DelayHours");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelayMinutes
        {
            get { return delayMinutes; }
            set
            {
                delayMinutes = value;
                OnPropertyChanged("DelayMinutes");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelaySeconds
        {
            get { return delaySeconds; }
            set
            {
                delaySeconds = value;
                OnPropertyChanged("DelaySeconds");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public TimeSpan DailyTime
        {
            get { return dailyTime; }
            set
            {
                dailyTime = value;
                OnPropertyChanged("DailyTime");

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public ActionTimeControlViewModel(ExecutionTimer executionTimer, IUserInterface userInterface)
        {
            if (executionTimer == null) throw new ArgumentNullException("executionTimer");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.executionTimer = executionTimer;
            this.userInterface = userInterface;

            Enabled = true;

            UpdateFromTimer();

            executionTimer.TimeChanged += HandleTimerTimeChanged;
            executionTimer.Started += HandleTimerStarted;
            executionTimer.Stopped += HandleTimerStopped;
        }

        private void HandleTimerStarted(object sender, EventArgs eventArgs)
        {
            Enabled = false;
        }

        private void HandleTimerStopped(object sender, EventArgs eventArgs)
        {
            userInterface.Dispatch(() => Enabled = true);
        }

        private void HandleTimerTimeChanged(object sender, EventArgs e)
        {
            UpdateFromTimer();
        }

        private void UpdateFromTimer()
        {
            updateFromBusiness = true;

            try
            {
                if (executionTimer.Time == null)
                    Clear();
                else
                {
                    FixedDate = executionTimer.Time.DateTime.Date;
                    FixedTime = executionTimer.Time.DateTime.TimeOfDay;
                    DelayHours = executionTimer.Time.Hours;
                    DelayMinutes = executionTimer.Time.Minutes;
                    DelaySeconds = executionTimer.Time.Seconds;
                    DailyTime = executionTimer.Time.TimeOfDay;

                    ScheduleTimeType = executionTimer.Time.Type;
                }
            }
            finally
            {
                updateFromBusiness = false;
            }
        }

        private void Clear()
        {
            FixedDate = DateTime.Today;
            FixedTime = DateTime.Now.TimeOfDay;
            DelayHours = 0;
            DelayMinutes = 0;
            DelaySeconds = 0;
            DailyTime = TimeSpan.Zero;

            ScheduleTimeType = ScheduleTimeType.Immediate;
        }

        private ScheduleTime GetActionTime()
        {
            return new ScheduleTime
            {
                Type = scheduleTimeType,
                DateTime = fixedDate.Date + fixedTime,
                TimeOfDay = dailyTime,
                Hours = delayHours,
                Minutes = delayMinutes,
                Seconds = delaySeconds
            };
        }
    }
}