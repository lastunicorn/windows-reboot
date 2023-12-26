using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.RestoreFromTray
{
    internal class RestoreFromTrayUseCase : IRequestHandler<RestoreFromTrayRequest>
    {
        private readonly EventBus eventBus;

        public RestoreFromTrayUseCase(EventBus eventBus)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task Handle(RestoreFromTrayRequest request, CancellationToken cancellationToken)
        {
            ApplicationStateChangedEvent ev = new ApplicationStateChangedEvent
            {
                ApplicationState = ApplicationState.Normal
            };
            eventBus.Publish(ev);

            return Task.CompletedTask;
        }
    }
}