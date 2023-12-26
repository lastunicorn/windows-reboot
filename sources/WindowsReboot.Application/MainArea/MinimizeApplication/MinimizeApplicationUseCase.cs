using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.MinimizeApplication
{
    internal class MinimizeApplicationUseCase : IRequestHandler<MinimizeApplicationRequest>
    {
        private readonly IConfigStorage configStorage;
        private readonly EventBus eventBus;

        public MinimizeApplicationUseCase(IConfigStorage configStorage, EventBus eventBus)
        {
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task Handle(MinimizeApplicationRequest request, CancellationToken cancellationToken)
        {
            if (configStorage.MinimizeToTray)
            {
                ApplicationStateChangedEvent ev = new ApplicationStateChangedEvent
                {
                    ApplicationState = ApplicationState.Tray
                };

                eventBus.Publish(ev);
            }

            return Task.CompletedTask;
        }
    }
}