﻿// Windows Reboot
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
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Application.MainArea.RestoreFromTray;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class RestoreMainWindowCommand : CommandBase
    {
        private readonly IMediator mediator;
        private ApplicationState applicationState;

        public override bool CanExecute => applicationState != ApplicationState.Normal;

        public RestoreMainWindowCommand(IUserInterface userInterface, IMediator mediator, EventBus eventBus)
            : base(userInterface)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<ApplicationStateChangedEvent>(HandleApplicationStateChangedEvent);
        }

        private void HandleApplicationStateChangedEvent(ApplicationStateChangedEvent ev)
        {
            applicationState = ev.ApplicationState;
            OnCanExecuteChanged();
        }

        protected override void DoExecute()
        {
            RestoreFromTrayRequest request = new RestoreFromTrayRequest();
            _ = mediator.Send(request);
        }
    }
}