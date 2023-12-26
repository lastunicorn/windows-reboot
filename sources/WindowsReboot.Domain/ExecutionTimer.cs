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
using DustInTheWind.EventBusEngine;

namespace DustInTheWind.WindowsReboot.Domain
{
    public class ExecutionTimer : IDisposable
    {
        private readonly EventBus eventBus;
        private readonly System.Threading.Timer timer;

        public readonly TimeSpan? DefaultWarningTime = TimeSpan.FromSeconds(30);
        private volatile bool isRunning;
        private ScheduleTime scheduleTime = ScheduleTime.Immediate;
        private TimeSpan? warningTime;
        private bool shouldRaiseWarning;
        private DateTime startTime;

        public event EventHandler Warning;
        public event EventHandler Ring;

        public bool IsRunning => isRunning;

        public ScheduleTime ScheduleTime
        {
            get => scheduleTime;
            set
            {
                if (value == null)
                    scheduleTime = ScheduleTime.Immediate;

                scheduleTime = value;

                OnTimeChanges();
            }
        }

        public TimeSpan? WarningTime
        {
            get => warningTime;
            set
            {
                if (isRunning)
                    throw new InvalidOperationException();

                warningTime = value;

                OnWarningTimeChanged();
            }
        }

        public TimeSpan TimeUntilAction => ActionTime - DateTime.Now;

        public DateTime ActionTime { get; private set; }

        public ExecutionTimer(EventBus eventBus)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            timer = new System.Threading.Timer(TimerElapsed);

            WarningTime = TimeSpan.FromSeconds(30);
        }

        private void TimerElapsed(object state)
        {
            if (shouldRaiseWarning)
            {
                shouldRaiseWarning = false;

                TimeSpan interval = ActionTime - DateTime.Now;

                StartTimer(interval);

                OnWarning();
            }
            else
            {
                DateTime? nextRunTime = CalculateNextRunTime(ActionTime + TimeSpan.FromTicks(1));

                if (nextRunTime == null)
                    Stop();
                else
                    RestartInternal(nextRunTime.Value);

                OnRing();
            }
        }

        public void Start()
        {
            startTime = DateTime.Now;
            DateTime? nextRunTime = CalculateNextRunTime(startTime);

            if (nextRunTime == null)
                throw new ActionTimeInThePastException(ActionTime, startTime);

            RestartInternal(nextRunTime.Value);

            OnStarted();
        }

        private DateTime? CalculateNextRunTime(DateTime now)
        {
            switch (ScheduleTime.Type)
            {
                case ScheduleTimeType.FixedDate:
                {
                    DateTime runTime = ScheduleTime.CalculateTimeFrom(startTime);
                    return runTime < now ? null as DateTime? : runTime;
                }

                case ScheduleTimeType.Daily:
                {
                    return ScheduleTime.CalculateTimeFrom(now);
                }

                case ScheduleTimeType.Delay:
                {
                    DateTime runTime = ScheduleTime.CalculateTimeFrom(startTime);
                    return runTime < now ? null as DateTime? : runTime;
                }

                case ScheduleTimeType.Immediate:
                {
                    DateTime runTime = ScheduleTime.CalculateTimeFrom(startTime);
                    return runTime < now ? null as DateTime? : runTime;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RestartInternal(DateTime nextRunTime)
        {
            ActionTime = nextRunTime;

            shouldRaiseWarning = warningTime != null && warningTime < ActionTime - startTime;

            TimeSpan interval = shouldRaiseWarning
                ? ActionTime - DateTime.Now - warningTime.Value
                : ActionTime - DateTime.Now;

            isRunning = true;

            StartTimer(interval);
        }

        private void StartTimer(TimeSpan interval)
        {
            if (interval < TimeSpan.Zero)
                interval = TimeSpan.Zero;

            timer.Change((long)interval.TotalMilliseconds, -1);
        }

        public void Stop()
        {
            StopTimer();
            isRunning = false;

            OnStopped();
        }

        private void StopTimer()
        {
            timer.Change(-1, -1);
        }

        public void ActivateWarning()
        {
            WarningTime = DefaultWarningTime;
        }

        public void DeactivateWarning()
        {
            WarningTime = null;
        }

        protected virtual void OnStarted()
        {
            TimerStartedEvent ev = new TimerStartedEvent
            {
                ActionTime = ActionTime
            };
            eventBus.Publish(ev);
        }

        protected virtual void OnStopped()
        {
            TimerStoppedEvent ev = new TimerStoppedEvent();
            eventBus.Publish(ev);
        }

        protected virtual void OnWarning()
        {
            Warning?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRing()
        {
            Ring?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnWarningTimeChanged()
        {
            WarningTimeChangedEvent ev = new WarningTimeChangedEvent
            {
                Time = WarningTime
            };

            eventBus.Publish(ev);
        }

        protected virtual void OnTimeChanges()
        {
            ScheduleChangedEvent ev = new ScheduleChangedEvent
            {
                DateTime = scheduleTime.DateTime,
                TimeOfDay = scheduleTime.TimeOfDay,

                Hours = scheduleTime.Hours,
                Minutes = scheduleTime.Minutes,
                Seconds = scheduleTime.Seconds,

                Type = scheduleTime.Type,

                IsAllowedToChange = !IsRunning
            };

            eventBus.Publish(ev);
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}