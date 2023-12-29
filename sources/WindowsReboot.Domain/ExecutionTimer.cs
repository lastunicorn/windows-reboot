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
        private readonly InternalExecutionTimer timer;

        private static readonly ImmediateSchedule DefaultSchedule = new ImmediateSchedule();
        public static TimeSpan? DefaultWarningTime = TimeSpan.FromSeconds(30);
        private volatile bool isRunning;
        private ISchedule schedule = DefaultSchedule;
        private TimeSpan? warningInterval = DefaultWarningTime;
        private DateTime startTime;

        public event EventHandler Warning;

        public event EventHandler Ring;

        public bool IsRunning => isRunning;

        public ISchedule Schedule
        {
            get => schedule;
            set
            {
                if (value == null)
                    schedule = DefaultSchedule;

                schedule = value;

                OnScheduleChangedChanges();
            }
        }

        public TimeSpan? WarningInterval
        {
            get => warningInterval;
            set
            {
                if (isRunning)
                    throw new InvalidOperationException();

                warningInterval = value;

                OnWarningIntervalChanged();
            }
        }

        public TimeSpan TimeUntilAction => timer.ActionTime - DateTime.Now;

        public DateTime ActionTime => timer.ActionTime;

        public ExecutionTimer(EventBus eventBus)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            timer = new InternalExecutionTimer();
            timer.Warning += TimerWarning;
            timer.Ring += TimerRing;
        }

        private void TimerWarning(object sender, EventArgs e)
        {
            OnWarning();
        }

        private void TimerRing(object sender, EventArgs e)
        {
            OnRing();
            isRunning = false;

            OnStopped();
        }

        public void Start()
        {
            startTime = DateTime.Now;
            DateTime? nextRunTime = CalculateNextRunTime(startTime);

            if (nextRunTime == null)
                throw new ActionTimeInThePastException(ActionTime, startTime);

            timer.ActionTime = nextRunTime.Value;
            timer.WarningInterval = warningInterval;
            timer.Start();

            OnStarted();
        }

        private DateTime? CalculateNextRunTime(DateTime dateTime)
        {
            DateTime runTime = Schedule.CalculateTimeFrom(dateTime);

            return runTime < dateTime
                ? null as DateTime?
                : runTime;
        }

        public void Stop()
        {
            timer.Stop();
            isRunning = false;

            OnStopped();
        }

        public void ActivateWarning()
        {
            WarningInterval = DefaultWarningTime;
        }

        public void DeactivateWarning()
        {
            WarningInterval = null;
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

        protected virtual void OnWarningIntervalChanged()
        {
            WarningIntervalChangedEvent ev = new WarningIntervalChangedEvent
            {
                Interval = warningInterval
            };

            eventBus.Publish(ev);
        }

        protected virtual void OnScheduleChangedChanges()
        {
            ScheduleChangedEvent ev = new ScheduleChangedEvent(schedule)
            {
                IsAllowedToChange = !isRunning
            };
            eventBus.Publish(ev);
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}