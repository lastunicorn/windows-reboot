using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetDailyTime
{
    internal class SetDailyTimeUseCase : IRequestHandler<SetDailyTimeRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetDailyTimeUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetDailyTimeRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.TimeOfDay = request.Time;

            return Task.CompletedTask;
        }
    }
}