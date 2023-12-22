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
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class StatusControlViewModel : ViewModelBase, IDisposable
    {
        private readonly Timer ticker;
        private readonly Core.ExecutionTimer executionTimer;
        private readonly IUiDispatcher uiDispatcher;
        private DateTime currentTime;
        private DateTime? actionTime;
        private TimeSpan? timerTime;

        public DateTime CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        public DateTime? ActionTime
        {
            get => actionTime;
            set
            {
                actionTime = value;
                OnPropertyChanged("ActionTime");
            }
        }

        public TimeSpan? TimerTime
        {
            get => timerTime;
            set
            {
                timerTime = value;
                OnPropertyChanged("TimerTime");
            }
        }

        public StatusControlViewModel(Core.ExecutionTimer executionTimer, IUiDispatcher uiDispatcher)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.uiDispatcher = uiDispatcher ?? throw new ArgumentNullException(nameof(uiDispatcher));

            ticker = new Timer(HandleTickerTick, null, 0, 100);

            executionTimer.Started += HandleTimerStarted;
            executionTimer.Stopped += HandleTimerStopped;
        }

        private void HandleTickerTick(object state)
        {
            uiDispatcher.Dispatch(() =>
            {
                CurrentTime = DateTime.Now;

                if (executionTimer.IsRunning)
                    TimerTime = executionTimer.TimeUntilAction;
            });
        }

        private void HandleTimerStarted(object sender, EventArgs eventArgs)
        {
            uiDispatcher.Dispatch(() => { ActionTime = executionTimer.ActionTime; });
        }

        private void HandleTimerStopped(object sender, EventArgs eventArgs)
        {
            uiDispatcher.Dispatch(() =>
            {
                ActionTime = null;
                TimerTime = null;
            });
        }

        public void Dispose()
        {
            ticker.Dispose();
        }
    }
}