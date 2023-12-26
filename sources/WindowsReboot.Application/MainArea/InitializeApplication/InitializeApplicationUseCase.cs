using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WorkerEngine;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication
{
    internal class InitializeApplicationUseCase : IRequestHandler<InitializeApplicationRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly ExecutionTimer executionTimer;
        private readonly WorkersContainer workersContainer;
        private readonly IConfigStorage configStorage;

        public InitializeApplicationUseCase(ExecutionPlan executionPlan, ExecutionTimer executionTimer, WorkersContainer workersContainer, IConfigStorage configStorage)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.workersContainer = workersContainer ?? throw new ArgumentNullException(nameof(workersContainer));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
        }

        public Task Handle(InitializeApplicationRequest request, CancellationToken cancellationToken)
        {
            executionTimer.ScheduleTime = configStorage.ActionTime;
            executionPlan.ActionType = configStorage.ActionType;
            executionPlan.ForceOption = configStorage.ForceClosingPrograms
                ? ForceOption.Yes
                : ForceOption.No;

            if (configStorage.StartTimerAtApplicationStart)
                executionTimer.Start();

            workersContainer.Start();

            return Task.CompletedTask;
        }
    }
}