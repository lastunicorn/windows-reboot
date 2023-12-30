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
using DustInTheWind.WindowsReboot.Application.MainArea.HideApplication;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using DustInTheWind.WorkerEngine;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication
{
    internal class CloseApplicationUseCase : IRequestHandler<CloseApplicationRequest, CloseApplicationResponse>
    {
        private readonly EventBus eventBus;
        private readonly IUserInterface userInterface;
        private readonly IConfigStorage configStorage;
        private readonly IExecutionTimer executionTimer;
        private readonly WorkersContainer workersContainer;
        private CloseApplicationResponse response;

        public CloseApplicationUseCase(EventBus eventBus, IUserInterface userInterface, IConfigStorage configStorage,
            IExecutionTimer executionTimer, WorkersContainer workersContainer)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.workersContainer = workersContainer ?? throw new ArgumentNullException(nameof(workersContainer));
        }

        public Task<CloseApplicationResponse> Handle(CloseApplicationRequest request, CancellationToken cancellationToken)
        {
            response = new CloseApplicationResponse();

            CloseApplication(request.Force);

            return Task.FromResult(response);
        }

        private void CloseApplication(bool force)
        {
            bool shouldHideInsteadOfClose = configStorage.CloseToTray && !force;

            if (shouldHideInsteadOfClose)
            {
                RaiseApplicationStateChangedEvent(ApplicationState.Hidden);
                return;
            }

            bool allowToCloseWhileTimerIsRunning = !executionTimer.IsTimerRunning() || userInterface.ConfirmClosingWhileTimerIsRunning();

            if (!allowToCloseWhileTimerIsRunning)
                return;

            RaiseApplicationClosingEvent();

            userInterface.CloseUserInterface();
            workersContainer.Stop();

            response.CloseSuccessfullyCompleted = true;
        }

        private void RaiseApplicationStateChangedEvent(ApplicationState applicationState)
        {
            ApplicationStateChangedEvent ev = new ApplicationStateChangedEvent
            {
                ApplicationState = applicationState
            };
            eventBus.Publish(ev);
        }

        private void RaiseApplicationClosingEvent()
        {
            ApplicationClosingEvent ev = new ApplicationClosingEvent();
            eventBus.Publish(ev);
        }
    }
}