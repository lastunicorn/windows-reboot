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

namespace DustInTheWind.WindowsReboot.Core
{
    public class Timer : IDisposable
    {
        private readonly System.Threading.Timer timer;

        public readonly TimeSpan? DefaultWarningTime = TimeSpan.FromSeconds(30);
        private volatile bool isRunning;
        private ScheduleTime time;
        private TimeSpan? warningTime;
        private bool shouldRaiseWarning;
        private DateTime startTime;

        public event EventHandler Started;
        public event EventHandler Stoped;
        public event EventHandler Warning;
        public event EventHandler Ring;
        public event EventHandler WarningTimeChanged;
        public event EventHandler TimeChanged;

        /// <summary>
        /// Indicates if the timer was started.
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        public ScheduleTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnTimeChanged();
            }
        }

        public TimeSpan? WarningTime
        {
            get { return warningTime; }
            set
            {
                if (isRunning)
                    throw new InvalidOperationException();

                warningTime = value;

                OnWarningTimeChanged();
            }
        }

        public TimeSpan TimeUntilAction
        {
            get { return ActionTime - DateTime.Now; }
        }

        public DateTime ActionTime { get; private set; }

        public Timer()
        {
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
            {
                string currentTimeString = string.Format("{0} : {1}", startTime.ToLongDateString(), startTime.ToLongTimeString());
                string actionTimeString = string.Format("{0} : {1}", ActionTime.ToLongDateString(), ActionTime.ToLongTimeString());

                string message = string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString);
                throw new WindowsRebootException(message);
            }

            RestartInternal(nextRunTime.Value);

            OnStarted();
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

        private void StopTimer()
        {
            timer.Change(-1, -1);
        }

        private DateTime? CalculateNextRunTime(DateTime now)
        {
            switch (Time.Type)
            {
                case ScheduleTimeType.FixedDate:
                    {
                        DateTime runTime = Time.CalculateTimeFrom(startTime);
                        return runTime < now ? null as DateTime? : runTime;
                    }

                case ScheduleTimeType.Daily:
                    {
                        return Time.CalculateTimeFrom(now);
                    }

                case ScheduleTimeType.Delay:
                    {
                        DateTime runTime = Time.CalculateTimeFrom(startTime);
                        return runTime < now ? null as DateTime? : runTime;
                    }

                case ScheduleTimeType.Immediate:
                    {
                        DateTime runTime = Time.CalculateTimeFrom(startTime);
                        return runTime < now ? null as DateTime? : runTime;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Stop()
        {
            StopTimer();
            isRunning = false;

            OnStoped();
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnStoped()
        {
            EventHandler handler = Stoped;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnWarning()
        {
            EventHandler handler = Warning;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnRing()
        {
            EventHandler handler = Ring;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnWarningTimeChanged()
        {
            EventHandler handler = WarningTimeChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnTimeChanged()
        {
            EventHandler handler = TimeChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}