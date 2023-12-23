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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTimeControlViewModel : ViewModelBase
    {
        private readonly ExecutionTimer executionTimer;

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
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public DateTime FixedDate
        {
            get => fixedDate;
            set
            {
                fixedDate = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public TimeSpan FixedTime
        {
            get => fixedTime;
            set
            {
                fixedTime = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelayHours
        {
            get => delayHours;
            set
            {
                delayHours = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelayMinutes
        {
            get => delayMinutes;
            set
            {
                delayMinutes = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public int DelaySeconds
        {
            get => delaySeconds;
            set
            {
                delaySeconds = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public TimeSpan DailyTime
        {
            get => dailyTime;
            set
            {
                dailyTime = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.Time = GetActionTime();
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        public ActionTimeControlViewModel(ExecutionTimer executionTimer, EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));

            Enabled = true;

            UpdateGui(executionTimer.Time);

            eventBus.Subscribe<TimerTimeChangedEvent>(HandleTimerTimeChangedEvent);
            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);
        }

        private Task HandleTimerTimeChangedEvent(TimerTimeChangedEvent ev, CancellationToken cancellationToken)
        {
            UpdateGui(ev.Time);
            return Task.CompletedTask;
        }

        private Task HandleTimerStartedEvent(TimerStartedEvent ev, CancellationToken cancellationToken)
        {
            Dispatch(() => Enabled = false);
            return Task.CompletedTask;
        }

        private Task HandleTimerStoppedEvent(TimerStoppedEvent ev, CancellationToken cancellationToken)
        {
            Dispatch(() => Enabled = true);
            return Task.CompletedTask;
        }

        private void UpdateGui(ScheduleTime scheduleTime)
        {
            RunInInitializeMode(() =>
            {
                if (scheduleTime == null)
                {
                    Clear();
                    return;
                }
                
                FixedDate = scheduleTime.DateTime.Date;
                FixedTime = scheduleTime.DateTime.TimeOfDay;
                DelayHours = scheduleTime.Hours;
                DelayMinutes = scheduleTime.Minutes;
                DelaySeconds = scheduleTime.Seconds;
                DailyTime = scheduleTime.TimeOfDay;

                ScheduleTimeType = scheduleTime.Type;

            });
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