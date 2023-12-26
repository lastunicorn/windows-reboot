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
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.ConfigurationArea.LoadConfiguration;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class LoadConfigurationCommand : CommandBase
    {
        private readonly IMediator mediator;

        public LoadConfigurationCommand(IUserInterface userInterface, EventBus eventBus, IMediator mediator)
            : base(userInterface)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);
        }

        private void HandleTimerStartedEvent(TimerStartedEvent ev)
        {
            CanExecute = false;
        }

        private void HandleTimerStoppedEvent(TimerStoppedEvent ev)
        {
            CanExecute = true;
        }

        protected override void DoExecute()
        {
            LoadConfigurationRequest request = new LoadConfigurationRequest();
            _ = mediator.Send(request);
        }
    }
}