// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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

namespace DustInTheWind.WindowsReboot.Presentation
{
    class StatusControlViewModel : ViewModelBase
    {
        private readonly Performer performer;
        private readonly UiDispatcher uiDispatcher;
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

        public StatusControlViewModel(ITicker ticker, Performer performer, UiDispatcher uiDispatcher)
        {
            if (ticker == null) throw new ArgumentNullException("ticker");
            if (performer == null) throw new ArgumentNullException("performer");
            if (uiDispatcher == null) throw new ArgumentNullException("uiDispatcher");

            this.performer = performer;
            this.uiDispatcher = uiDispatcher;

            ticker.Tick += HandleTickerTick;

            performer.Started += HandlePerformerStarted;
            performer.Stoped += HandlePerformerStoped;
            performer.Tick += HandlePerformerTick;
        }

        private void HandleTickerTick(object sender, EventArgs eventArgs)
        {
            uiDispatcher.Dispatch(() =>
            {
                CurrentTime = DateTime.Now;
            });
        }

        private void HandlePerformerStarted(object sender, EventArgs eventArgs)
        {
            uiDispatcher.Dispatch(() =>
            {
                ActionTime = performer.ActionTime;
            });
        }

        private void HandlePerformerStoped(object sender, EventArgs eventArgs)
        {
            uiDispatcher.Dispatch(() =>
            {
                ActionTime = null;
                TimerTime = null;
            });
        }

        private void HandlePerformerTick(object sender, TickEventArgs e)
        {
            uiDispatcher.Dispatch(() =>
            {
                TimerTime = e.TimeUntilAction;
            });
        }
    }
}
