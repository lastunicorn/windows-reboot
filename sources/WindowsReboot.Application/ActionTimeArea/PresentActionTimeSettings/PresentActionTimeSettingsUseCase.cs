using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.PresentActionTimeSettings
{
    internal class PresentActionTimeSettingsUseCase : IRequestHandler<PresentActionTimeSettingsRequest, PresentActionTimeSettingsResponse>
    {
        private readonly ExecutionTimer executionTimer;

        public PresentActionTimeSettingsUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task<PresentActionTimeSettingsResponse> Handle(PresentActionTimeSettingsRequest request, CancellationToken cancellationToken)
        {
            PresentActionTimeSettingsResponse response = new PresentActionTimeSettingsResponse
            {
                DateTime = executionTimer.ScheduleTime.DateTime,
                TimeOfDay = executionTimer.ScheduleTime.TimeOfDay,
                
                Hours = executionTimer.ScheduleTime.Hours,
                Minutes = executionTimer.ScheduleTime.Minutes,
                Seconds = executionTimer.ScheduleTime.Seconds,
                
                Type = executionTimer.ScheduleTime.Type,
                IsAllowedToChange = !executionTimer.IsRunning
            };

            return Task.FromResult(response);
        }
    }
}