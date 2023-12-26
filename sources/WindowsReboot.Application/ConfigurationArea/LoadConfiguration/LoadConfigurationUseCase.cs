using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ConfigurationArea.LoadConfiguration
{
    internal class LoadConfigurationUseCase : IRequestHandler<LoadConfigurationRequest>
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private readonly IConfigStorage configuration;

        public LoadConfigurationUseCase(ExecutionTimer executionTimer, ExecutionPlan executionPlan, IConfigStorage configuration)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task Handle(LoadConfigurationRequest request, CancellationToken cancellationToken)
        {
            if (executionTimer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            executionTimer.ScheduleTime = configuration.ActionTime;
            executionPlan.ActionType = configuration.ActionType;
            executionPlan.ForceOption = configuration.ForceClosingPrograms
                ? ForceOption.Yes
                : ForceOption.No;

            return Task.CompletedTask;
        }
    }
}