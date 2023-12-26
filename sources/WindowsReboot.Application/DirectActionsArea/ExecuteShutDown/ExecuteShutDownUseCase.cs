using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.DirectActionsArea.ExecuteShutDown
{
    internal class ExecuteShutDownUseCase : IRequestHandler<ExecuteShutDownRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly IOperatingSystem operatingSystem;

        public ExecuteShutDownUseCase(IUserInterface userInterface, IOperatingSystem operatingSystem)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        }

        public Task Handle(ExecuteShutDownRequest request, CancellationToken cancellationToken)
        {
            bool allowToContinue = userInterface.ConfirmDirectShutDown();

            if (allowToContinue)
                operatingSystem.ShutDown(false);

            return Task.CompletedTask;
        }
    }
}