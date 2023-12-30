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
using System.Threading;

namespace DustInTheWind.WindowsReboot.Workers
{
    internal sealed class CustomTimer : IDisposable
    {
        private readonly Timer timer;
        private TimerState state = TimerState.NotStarted;
        private DateTime actionTime;
        private TimeSpan? warningInterval;

        public DateTime ActionTime
        {
            get => actionTime;
            set
            {
                StopInternalTimer();
                state = TimerState.NotStarted;

                actionTime = value;
            }
        }

        public TimeSpan? WarningInterval
        {
            get => warningInterval;
            set
            {
                StopInternalTimer();
                state = TimerState.NotStarted;

                warningInterval = value;
            }
        }

        public DateTime? WarningTime => ActionTime - WarningInterval;

        public bool IsRunning => state == TimerState.RunningForWarning || state == TimerState.RunningForRing;

        public event EventHandler Warning;

        public event EventHandler Ring;

        public CustomTimer()
        {
            timer = new Timer(TimerElapsed);
        }

        private void TimerElapsed(object state)
        {
            switch (this.state)
            {
                case TimerState.NotStarted:
                    StopInternalTimer();
                    break;

                case TimerState.RunningForWarning:
                    MoveToNextState();
                    OnWarning();
                    break;

                case TimerState.RunningForRing:
                    MoveToNextState();
                    OnRing();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Start()
        {
            if (state != TimerState.NotStarted)
            {
                StopInternalTimer();
                state = TimerState.NotStarted;
            }

            MoveToNextState();
        }

        private void MoveToNextState()
        {
            DateTime now = DateTime.Now;

            if (state == TimerState.NotStarted)
            {
                if (now <= WarningTime)
                    StartTimerForWarning(now);
                else
                    StartTimerForRing(now);
            }
            else if (state == TimerState.RunningForWarning)
            {
                StartTimerForRing(now);
            }
            else if (state == TimerState.RunningForRing)
            {
                state = TimerState.NotStarted;
                StopInternalTimer();
            }
        }

        private void StartTimerForWarning(DateTime now)
        {
            state = TimerState.RunningForWarning;

            TimeSpan interval = WarningTime.Value - now;
            StartInternalTimer(interval);
        }

        private void StartTimerForRing(DateTime now)
        {
            state = TimerState.RunningForRing;

            TimeSpan interval = ActionTime - now;

            if (interval < TimeSpan.Zero)
                interval = TimeSpan.Zero;

            StartInternalTimer(interval);
        }

        public void Stop()
        {
            StopInternalTimer();
            state = TimerState.NotStarted;
        }

        private void StartInternalTimer(TimeSpan interval)
        {
            if (interval < TimeSpan.Zero)
                interval = TimeSpan.Zero;

            timer.Change((long)interval.TotalMilliseconds, -1);
        }

        private void StopInternalTimer()
        {
            timer.Change(-1, -1);
        }

        private void OnWarning()
        {
            Warning?.Invoke(this, EventArgs.Empty);
        }

        private void OnRing()
        {
            Ring?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}