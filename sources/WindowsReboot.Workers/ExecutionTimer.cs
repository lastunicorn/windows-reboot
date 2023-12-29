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
    internal sealed class ExecutionTimer : IDisposable
    {
        private readonly Timer timer;
        private TimerStep step;
        private volatile bool isRunning;

        public DateTime ActionTime { get; set; }

        public TimeSpan? WarningInterval { get; set; }

        public DateTime? WarningTime => ActionTime - WarningInterval;

        public bool IsRunning => isRunning;

        public event EventHandler Warning;

        public event EventHandler Ring;

        public ExecutionTimer()
        {
            timer = new Timer(TimerElapsed);
        }

        private void TimerElapsed(object state)
        {
            switch (step)
            {
                case TimerStep.None:
                    StopTimer();
                    break;

                case TimerStep.Warn:
                    StartInternal();
                    OnWarning();
                    break;

                case TimerStep.Ring:
                    StartInternal();
                    OnRing();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Start()
        {
            StartInternal();
        }

        private void StartInternal()
        {
            DateTime now = DateTime.Now;

            if (now <= WarningTime)
            {
                step = TimerStep.Warn;

                TimeSpan interval = WarningTime.Value - now;
                StartTimer(interval);
            }
            else if (now <= ActionTime)
            {
                step = TimerStep.Ring;

                TimeSpan interval = ActionTime - now;
                StartTimer(interval);
            }
            else
            {
                step = TimerStep.None;

                StopTimer();
            }
        }

        public void Stop()
        {
            StopTimer();
        }

        private void StartTimer(TimeSpan interval)
        {
            if (interval < TimeSpan.Zero)
                interval = TimeSpan.Zero;

            timer.Change((long)interval.TotalMilliseconds, -1);
            isRunning = true;
        }

        private void StopTimer()
        {
            timer.Change(-1, -1);
            isRunning = false;
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