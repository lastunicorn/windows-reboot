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
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication
{
    internal class CloseApplicationUseCase : IRequestHandler<CloseApplicationRequest, CloseApplicationResponse>
    {
        private readonly EventBus eventBus;
        private readonly IUserInterface userInterface;
        private readonly IConfigStorage configStorage;
        private readonly IExecutionProcess executionProcess;

        public CloseApplicationUseCase(EventBus eventBus, IUserInterface userInterface, IConfigStorage configStorage, IExecutionProcess executionProcess)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.executionProcess = executionProcess ?? throw new ArgumentNullException(nameof(executionProcess));
        }

        public Task<CloseApplicationResponse> Handle(CloseApplicationRequest request, CancellationToken cancellationToken)
        {
            CloseApplicationResponse response = new CloseApplicationResponse();

            if (configStorage.CloseToTray && !request.Force)
            {
                OnGoToTray();
                response.CloseSuccessfullyCompleted = false;
            }
            else
            {
                bool allowToClose = !executionProcess.IsTimerRunning() || userInterface.ConfirmClosingWhileTimerIsRunning();

                if (allowToClose)
                {
                    bool success = OnApplicationClosing();

                    if (success)
                    {
                        userInterface.CloseUserInterface();
                        response.CloseSuccessfullyCompleted = true;
                    }
                    else
                    {
                        OnApplicationCloseRevoked();
                        response.CloseSuccessfullyCompleted = false;
                    }
                }
                else
                {
                    response.CloseSuccessfullyCompleted = false;
                }
            }

            return Task.FromResult(response);
        }

        private void OnGoToTray()
        {
            ApplicationStateChangedEvent ev = new ApplicationStateChangedEvent
            {
                ApplicationState = ApplicationState.Tray
            };
            eventBus.Publish(ev);
        }

        private bool OnApplicationClosing()
        {
            ApplicationClosingEvent ev = new ApplicationClosingEvent();
            eventBus.Publish(ev);
            return !ev.IsCanceled;
        }

        private void OnApplicationCloseRevoked()
        {
            ApplicationCloseRevokedEvent ev = new ApplicationCloseRevokedEvent();
            eventBus.Publish(ev);
        }
    }
}