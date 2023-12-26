using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ConfigurationArea.LoadDefaultConfiguration
{
    internal class LoadDefaultConfigurationUseCase : IRequestHandler<LoadDefaultConfigurationRequest>
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;

        public LoadDefaultConfigurationUseCase(ExecutionTimer executionTimer, ExecutionPlan executionPlan)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
        }
        
        public Task Handle(LoadDefaultConfigurationRequest request, CancellationToken cancellationToken)
        {
            if (executionTimer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            executionTimer.ScheduleTime = new ScheduleTime
            {
                Type = ScheduleTimeType.Delay
            };

            executionPlan.ActionType = ActionType.PowerOff;
            executionPlan.ForceOption = ForceOption.Yes;
            executionTimer.WarningTime = null;

            return Task.CompletedTask;
        }
    }
}