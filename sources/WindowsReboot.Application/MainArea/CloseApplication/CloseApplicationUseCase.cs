using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication
{
    internal class CloseApplicationUseCase : IRequestHandler<CloseApplicationRequest, CloseApplicationResponse>
    {
        private readonly EventBus eventBus;
        private readonly IUserInterface userInterface;
        private readonly IConfigStorage configStorage;
        private readonly ExecutionTimer executionTimer;

        public CloseApplicationUseCase(EventBus eventBus, IUserInterface userInterface, IConfigStorage configStorage, ExecutionTimer executionTimer)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
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
                bool allowToClose = !executionTimer.IsRunning || userInterface.ConfirmClosingWhileTimerIsRunning();

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