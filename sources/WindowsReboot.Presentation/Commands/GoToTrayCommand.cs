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
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class GoToTrayCommand : CommandBase
    {
        private readonly IMediator mediator;
        private MainWindowState mainWindowState;

        public override bool CanExecute => mainWindowState != MainWindowState.Tray;

        public GoToTrayCommand(IUserInterface userInterface, IMediator mediator, EventBus eventBus)
            : base(userInterface)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<GuiStateChangedEvent>(HandleGuiStateChangedEvent);
        }

        private void HandleGuiStateChangedEvent(GuiStateChangedEvent ev)
        {
            mainWindowState = ev.MainWindowState;
            OnCanExecuteChanged();
        }

        protected override void DoExecute()
        {
            GoToTrayRequest goToTrayRequest = new GoToTrayRequest();
            _ = mediator.Send(goToTrayRequest);
        }
    }
}