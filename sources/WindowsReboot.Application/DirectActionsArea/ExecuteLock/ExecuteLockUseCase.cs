using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.DirectActionsArea.ExecuteLock
{
    internal class ExecuteLockUseCase : IRequestHandler<ExecuteLockRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly IOperatingSystem operatingSystem;

        public ExecuteLockUseCase(IUserInterface userInterface, IOperatingSystem operatingSystem)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        }

        public Task Handle(ExecuteLockRequest request, CancellationToken cancellationToken)
        {
            bool allowToContinue = userInterface.ConfirmDirectLock();

            if (allowToContinue)
                operatingSystem.Lock();

            return Task.CompletedTask;
        }
    }
}