using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetHours
{
    internal class SetHoursUseCase : IRequestHandler<SetHoursRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetHoursUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetHoursRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.Hours = request.Hours;

            return Task.CompletedTask;
        }
    }
}