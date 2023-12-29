using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.TimerArea.WarnTheUser
{
    internal class WarnTheUserUseCase : IRequestHandler<WarnTheUserRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly IUserInterface userInterface;

        public WarnTheUserUseCase(ExecutionPlan executionPlan, IUserInterface userInterface)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public Task Handle(WarnTheUserRequest request, CancellationToken cancellationToken)
        {
            string actionName = executionPlan.ActionType.ToString();
            userInterface.DisplayExecutionWarning(actionName);

            return Task.CompletedTask;
        }
    }
}