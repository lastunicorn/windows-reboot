using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetScheduleType
{
    internal class SetScheduleTypeUseCase : IRequestHandler<SetScheduleTypeRequest>
    {
        private readonly ExecutionTimer executionTimer;

        public SetScheduleTypeUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task Handle(SetScheduleTypeRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime.Type = request.ScheduleType;

            return Task.CompletedTask;
        }
    }
}