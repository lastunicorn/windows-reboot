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
using DustInTheWind.WindowsReboot.Core.Services;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    class StatusControlViewModel : ViewModelBase
    {
        private readonly Task task;
        private readonly UserInterface userInterface;
        private DateTime currentTime;
        private DateTime? actionTime;
        private TimeSpan? timerTime;

        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        public DateTime? ActionTime
        {
            get { return actionTime; }
            set
            {
                actionTime = value;
                OnPropertyChanged("ActionTime");
            }
        }

        public TimeSpan? TimerTime
        {
            get { return timerTime; }
            set
            {
                timerTime = value;
                OnPropertyChanged("TimerTime");
            }
        }

        public StatusControlViewModel(ITicker ticker, Task task, UserInterface userInterface)
        {
            if (ticker == null) throw new ArgumentNullException("ticker");
            if (task == null) throw new ArgumentNullException("task");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.task = task;
            this.userInterface = userInterface;

            ticker.Tick += HandleTickerTick;

            task.Started += HandlePerformerStarted;
            task.Stoped += HandlePerformerStoped;
            task.Tick += HandlePerformerTick;
        }

        private void HandleTickerTick(object sender, EventArgs eventArgs)
        {
            userInterface.Dispatch(() =>
            {
                CurrentTime = DateTime.Now;
            });
        }

        private void HandlePerformerStarted(object sender, EventArgs eventArgs)
        {
            userInterface.Dispatch(() =>
            {
                ActionTime = task.ActionTime;
            });
        }

        private void HandlePerformerStoped(object sender, EventArgs eventArgs)
        {
            userInterface.Dispatch(() =>
            {
                ActionTime = null;
                TimerTime = null;
            });
        }

        private void HandlePerformerTick(object sender, EventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                TimerTime = task.TimeUntilAction;
            });
        }
    }
}
