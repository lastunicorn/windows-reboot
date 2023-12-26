using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.DirectActionsArea.ExecuteLogOff
{
    internal class ExecuteLogOffUseCase : IRequestHandler<ExecuteLogOffRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly IOperatingSystem operatingSystem;

        public ExecuteLogOffUseCase(IUserInterface userInterface, IOperatingSystem operatingSystem)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        }

        public Task Handle(ExecuteLogOffRequest request, CancellationToken cancellationToken)
        {
            bool allowToContinue = userInterface.ConfirmDirectLogOff(Environment.UserDomainName);

            if (allowToContinue)
                operatingSystem.LogOff(false);
         
            return Task.CompletedTask;
        }
    }
}