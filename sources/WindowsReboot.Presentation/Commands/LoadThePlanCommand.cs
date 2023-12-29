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
using DustInTheWind.WindowsReboot.Application.PlanStorageArea.LoadThePlan;
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class LoadThePlanCommand : CommandBase
    {
        private readonly IMediator mediator;

        public LoadThePlanCommand(EventBus eventBus, IMediator mediator)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);
        }

        private void HandleTimerStartedEvent(TimerStartedEvent ev)
        {
            Dispatch(() =>
            {
                CanExecute = false;
            });
        }

        private void HandleTimerStoppedEvent(TimerStoppedEvent ev)
        {
            Dispatch(() =>
            {
                CanExecute = true;
            });
        }

        protected override void DoExecute()
        {
            LoadThePlanRequest request = new LoadThePlanRequest();
            _ = mediator.Send(request);
        }
    }
}