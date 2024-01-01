using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.AutoStart.AutoStart
{
    internal class AutoStartUseCase : IRequestHandler<AutoStartRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly IConfigStorage configStorage;
        private readonly IExecutionTimer executionTimer;
        private readonly EventBus eventBus;
        private readonly ISystemClock systemClock;

        public AutoStartUseCase(ExecutionPlan executionPlan, IConfigStorage configStorage,
            IExecutionTimer executionTimer, EventBus eventBus, ISystemClock systemClock)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.systemClock = systemClock ?? throw new ArgumentNullException(nameof(systemClock));
        }

        public Task Handle(AutoStartRequest request, CancellationToken cancellationToken)
        {
            if (configStorage.StartTimerAtApplicationStart)
                Start();

            return Task.CompletedTask;
        }

        private void Start()
        {
            DateTime startTime = systemClock.GetCurrentTime();
            DateTime actionTime = executionPlan.Schedule.ComputeActionTimeRelativeTo(startTime);

            if (actionTime < startTime)
                throw new ActionTimeInThePastException(actionTime, startTime);

            ExecutionRequest executionRequest = new ExecutionRequest
            {
                Id = Guid.NewGuid(),
                ActionTime = actionTime,
                WarningInterval = executionPlan.WarningInterval
            };

            executionTimer.Start(executionRequest);

            RaiseTimerStartedEvent(actionTime, executionRequest.Id);
        }

        private void RaiseTimerStartedEvent(DateTime nextRunTime, Guid requestId)
        {
            TimerStartedEvent ev = new TimerStartedEvent
            {
                RequestId = requestId,
                ActionTime = nextRunTime
            };
            eventBus.Publish(ev);
        }
    }
}