using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedDate
{
    internal class SetFixedDateUseCase : IRequestHandler<SetFixedDateRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetFixedDateUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetFixedDateRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.DateTime = request.Date;

            return Task.CompletedTask;
        }
    }
}