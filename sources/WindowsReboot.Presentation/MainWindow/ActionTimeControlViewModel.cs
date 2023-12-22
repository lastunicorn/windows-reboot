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
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTimeControlViewModel : ViewModelBase
    {
        private readonly ExecutionTimer executionTimer;
        private readonly IUiDispatcher uiDispatcher;
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
            get => scheduleTimeType;
            set
            {
                scheduleTimeType = value;
                OnPropertyChanged(nameof(ScheduleTimeType));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public DateTime FixedDate
        {
            get => fixedDate;
            set
            {
                fixedDate = value;
                OnPropertyChanged(nameof(FixedDate));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public TimeSpan FixedTime
        {
            get => fixedTime;
            set
            {
                fixedTime = value;
                OnPropertyChanged(nameof(FixedTime));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelayHours
        {
            get => delayHours;
            set
            {
                delayHours = value;
                OnPropertyChanged(nameof(DelayHours));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelayMinutes
        {
            get => delayMinutes;
            set
            {
                delayMinutes = value;
                OnPropertyChanged(nameof(DelayMinutes));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelaySeconds
        {
            get => delaySeconds;
            set
            {
                delaySeconds = value;
                OnPropertyChanged(nameof(DelaySeconds));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public TimeSpan DailyTime
        {
            get => dailyTime;
            set
            {
                dailyTime = value;
                OnPropertyChanged(nameof(DailyTime));

                if (!updateFromBusiness)
                    executionTimer.Time = GetActionTime();
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }

        public ActionTimeControlViewModel(ExecutionTimer executionTimer, IUiDispatcher uiDispatcher)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.uiDispatcher = uiDispatcher ?? throw new ArgumentNullException(nameof(uiDispatcher));

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
            uiDispatcher.Dispatch(() => Enabled = true);
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