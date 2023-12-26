using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.DirectActionsArea.ExecutePowerOff
{
    internal class ExecutePowerOffUseCase : IRequestHandler<ExecutePowerOffRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly IOperatingSystem operatingSystem;

        public ExecutePowerOffUseCase(IUserInterface userInterface, IOperatingSystem operatingSystem)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        }

        public Task Handle(ExecutePowerOffRequest request, CancellationToken cancellationToken)
        {
            bool allowToContinue = userInterface.ConfirmDirectPowerOff();

            if (allowToContinue)
                operatingSystem.PowerOff(false);

            return Task.CompletedTask;
        }
    }
}