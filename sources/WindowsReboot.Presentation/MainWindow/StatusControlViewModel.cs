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
using DustInTheWind.WindowsReboot.Presentation.UiCommon;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class StatusControlViewModel : ViewModelBase, IDisposable
    {
        private readonly Timer ticker;
        private readonly Core.Timer timer;
        private readonly IUserInterface userInterface;
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

        public StatusControlViewModel(Core.Timer timer, IUserInterface userInterface)
        {
            this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));

            ticker = new Timer(HandleTickerTick, null, 0, 100);

            timer.Started += HandleTimerStarted;
            timer.Stopped += HandleTimerStopped;
        }

        private void HandleTickerTick(object state)
        {
            userInterface.Dispatch(() =>
            {
                CurrentTime = DateTime.Now;

                if (timer.IsRunning)
                    TimerTime = timer.TimeUntilAction;
            });
        }

        private void HandleTimerStarted(object sender, EventArgs eventArgs)
        {
            userInterface.Dispatch(() => { ActionTime = timer.ActionTime; });
        }

        private void HandleTimerStopped(object sender, EventArgs eventArgs)
        {
            userInterface.Dispatch(() =>
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