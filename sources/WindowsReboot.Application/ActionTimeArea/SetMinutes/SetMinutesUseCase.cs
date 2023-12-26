using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetMinutes
{
    internal class SetMinutesUseCase : IRequestHandler<SetMinutesRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetMinutesUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetMinutesRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.Minutes = request.Minutes;

            return Task.CompletedTask;
        }
    }
}