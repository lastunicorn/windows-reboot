using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.GoToTray
{
    internal class GoToTrayUseCase : IRequestHandler<GoToTrayRequest>
    {
        private readonly EventBus eventBus;

        public GoToTrayUseCase(EventBus eventBus)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task Handle(GoToTrayRequest request, CancellationToken cancellationToken)
        {
            ApplicationStateChangedEvent ev = new ApplicationStateChangedEvent
            {
                ApplicationState = ApplicationState.Tray
            };
            eventBus.Publish(ev);

            return Task.CompletedTask;
        }
    }
}