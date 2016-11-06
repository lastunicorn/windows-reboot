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
using DustInTheWind.WindowsReboot.Core.Services;

namespace DustInTheWind.WindowsReboot.Core
{
    public class Timer
    {
        private readonly ITicker ticker;

        private volatile bool isRunning;

        public ScheduleTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnTimeChanged();
            }
        }

        private bool warningWasRaised;

        private DateTime startTime;
        private TimeSpan? warningTime;
        public TimeSpan TimeUntilAction { get; private set; }

        public event EventHandler Started;
        public event EventHandler Stoped;
        public event EventHandler Tick;
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

        public DateTime ActionTime { get; private set; }

        public readonly TimeSpan? DefaultWarningTime = TimeSpan.FromSeconds(30);
        private ScheduleTime time;

        public Timer(ITicker ticker)
        {
            if (ticker == null) throw new ArgumentNullException("ticker");

            this.ticker = ticker;

            WarningTime = TimeSpan.FromSeconds(30);
        }

        public void Start()
        {
            startTime = DateTime.Now;
            ActionTime = Time.CalculateTimeFrom(startTime);

            if (ActionTime < startTime)
            {
                string currentTimeString = string.Format("{0} : {1}", startTime.ToLongDateString(), startTime.ToLongTimeString());
                string actionTimeString = string.Format("{0} : {1}", ActionTime.ToLongDateString(), ActionTime.ToLongTimeString());

                string message = string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString);
                throw new WindowsRebootException(message);
            }
            
            warningWasRaised = warningTime == null || warningTime > ActionTime - startTime;
            isRunning = true;

            ticker.Tick += HandleTickerTick;

            OnStarted();
        }

        private void HandleTickerTick(object sender, EventArgs eventArgs)
        {
            if (!isRunning)
                return;

            DateTime now = DateTime.Now;

            // todo: refactor this to get rid of the ticker and use instead the system timer.

            CalculateRemainingTime(now);
            RaiseWarningIfNeeded(now);
            DoActionIfNeeded(now);
        }

        private void CalculateRemainingTime(DateTime now)
        {
            TimeUntilAction = ActionTime - now;

            OnTick();
        }

        private void RaiseWarningIfNeeded(DateTime now)
        {
            if (warningTime == null || warningWasRaised || warningTime < ActionTime - now)
                return;

            warningWasRaised = true;

            OnWarning();
        }

        private void DoActionIfNeeded(DateTime now)
        {
            if (ActionTime > now)
                return;

            DateTime? nextRunTime = CalculateNextRunTime(ActionTime + TimeSpan.FromTicks(1));

            if (nextRunTime == null)
            {
                Stop();
            }
            else
            {
                ActionTime = nextRunTime.Value;
            }

            OnRing();
        }

        private DateTime? CalculateNextRunTime(DateTime now)
        {
            switch (Time.Type)
            {
                case TaskTimeType.FixedDate:
                    {
                        DateTime runTime = Time.CalculateTimeFrom(startTime);
                        return runTime < now ? null as DateTime? : runTime;
                    }

                case TaskTimeType.Daily:
                    {
                        return Time.CalculateTimeFrom(now);
                    }

                case TaskTimeType.Delay:
                    {
                        DateTime runTime = Time.CalculateTimeFrom(startTime);
                        return runTime < now ? null as DateTime? : runTime;
                    }

                case TaskTimeType.Immediate:
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
            ticker.Tick -= HandleTickerTick;

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

        protected virtual void OnTick()
        {
            EventHandler handler = Tick;

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
    }
}