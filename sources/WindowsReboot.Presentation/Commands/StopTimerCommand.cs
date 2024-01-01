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
using DustInTheWind.WindowsReboot.Application;
using DustInTheWind.WindowsReboot.Application.PlanExecutionArea.StopTimer;
using DustInTheWind.WindowsReboot.Application.PlanStateArea.PresentStopAbility;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class StopTimerCommand : CommandBase
    {
        private readonly IMediator mediator;

        public StopTimerCommand(IMediator mediator, EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);

            Initialize();
        }

        private async void Initialize()
        {
            PresentStopAbilityRequest request = new PresentStopAbilityRequest();
            PresentStopAbilityResponse response = await mediator.Send(request);

            Dispatch(() =>
            {
                CanExecute = response.CanStop;
            });
        }

        private void HandleTimerStartedEvent(TimerStartedEvent ev)
        {
            Dispatch(() =>
            {
                CanExecute = true;
            });
        }

        private void HandleTimerStoppedEvent(TimerStoppedEvent ev)
        {
            Dispatch(() =>
            {
                CanExecute = false;
            });
        }

        protected override void DoExecute()
        {
            StopTimerRequest request = new StopTimerRequest();
            _ = mediator.Send(request);
        }
    }
}