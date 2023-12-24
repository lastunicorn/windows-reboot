using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedTime
{
    internal class SetFixedTimeUseCase : IRequestHandler<SetFixedTimeRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetFixedTimeUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetFixedTimeRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.TimeOfDay = request.Time;

            return Task.CompletedTask;
        }
    }
}