using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetSeconds
{
    internal class SetSecondsUseCase : IRequestHandler<SetSecondsRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetSecondsUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetSecondsRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.Seconds = request.Seconds;

            return Task.CompletedTask;
        }
    }
}