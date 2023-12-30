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
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application;
using DustInTheWind.WindowsReboot.Application.PlanStateArea.PresentTimerStatus;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class StatusControlViewModel : ViewModelBase, IDisposable
    {
        private readonly IMediator mediator;
        private Timer ticker;
        private DateTime currentTime;
        private DateTime? actionTime;
        private TimeSpan? timerTime;

        public DateTime CurrentTime
        {
            get => currentTime;
            private set
            {
                currentTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ActionTime
        {
            get => actionTime;
            private set
            {
                actionTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? TimerTime
        {
            get => timerTime;
            private set
            {
                timerTime = value;
                OnPropertyChanged();
            }
        }

        public StatusControlViewModel(IMediator mediator, EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);

            Initialize();
        }

        private async void Initialize()
        {
            PresentTimerStatusRequest request = new PresentTimerStatusRequest();
            PresentTimerStatusResponse response = await mediator.Send(request);

            CurrentTime = response.CurrentTime;
            ActionTime = response.ActionTime;
            TimerTime = null;

            ticker = new Timer(HandleTickerTick, null, 0, 100);
        }

        private void HandleTickerTick(object state)
        {
            Dispatch(() =>
            {
                DateTime now = DateTime.Now;

                CurrentTime = now;

                if (ActionTime == null)
                {
                    TimerTime = null;
                }
                else
                {
                    TimeSpan? newTimerTime = ActionTime - now;

                    TimerTime = newTimerTime < TimeSpan.Zero
                        ? TimeSpan.Zero
                        : newTimerTime;
                }
            });
        }

        private void HandleTimerStartedEvent(TimerStartedEvent ev)
        {
            Dispatch(() =>
            {
                ActionTime = ev.ActionTime;
            });
        }

        private void HandleTimerStoppedEvent(TimerStoppedEvent ev)
        {
            Dispatch(() =>
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