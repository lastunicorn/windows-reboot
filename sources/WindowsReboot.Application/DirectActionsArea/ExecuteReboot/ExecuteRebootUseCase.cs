using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.DirectActionsArea.ExecuteReboot
{
    internal class ExecuteRebootUseCase : IRequestHandler<ExecuteRebootRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly IOperatingSystem operatingSystem;

        public ExecuteRebootUseCase(IUserInterface userInterface, IOperatingSystem operatingSystem)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        }

        public Task Handle(ExecuteRebootRequest request, CancellationToken cancellationToken)
        {
            bool allowToContinue = userInterface.ConfirmDirectReboot();

            if (allowToContinue)
                operatingSystem.Reboot(false);

            return Task.CompletedTask;
        }
    }
}