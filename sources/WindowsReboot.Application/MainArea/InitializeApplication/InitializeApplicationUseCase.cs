// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Domain.Scheduling;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using DustInTheWind.WorkerEngine;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication
{
    internal class InitializeApplicationUseCase : IRequestHandler<InitializeApplicationRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly WorkersContainer workersContainer;
        private readonly IConfigStorage configStorage;
        private readonly IExecutionTimer executionTimer;
        private readonly EventBus eventBus;
        private readonly ISystemClock systemClock;

        public InitializeApplicationUseCase(ExecutionPlan executionPlan, WorkersContainer workersContainer, IConfigStorage configStorage,
            IExecutionTimer executionTimer, EventBus eventBus, ISystemClock systemClock)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.workersContainer = workersContainer ?? throw new ArgumentNullException(nameof(workersContainer));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.systemClock = systemClock ?? throw new ArgumentNullException(nameof(systemClock));
        }

        public Task Handle(InitializeApplicationRequest request, CancellationToken cancellationToken)
        {
            ISchedule schedule = configStorage.Schedule.ToDomain();
            SetSchedule(schedule);

            executionPlan.ActionType = configStorage.ActionType.ToDomain();
            executionPlan.ForceOption = configStorage.ForceClosingPrograms
                ? ForceOption.Yes
                : ForceOption.No;

            workersContainer.Start();

            // todo: implement auto-start
            //if (configStorage.StartTimerAtApplicationStart)
            //    Start();

            return Task.CompletedTask;
        }

        private void SetSchedule(ISchedule schedule)
        {
            executionPlan.Schedule = schedule;

            RaiseScheduleChangedEvent(schedule);
        }

        private void Start()
        {
            DateTime startTime = systemClock.GetCurrentTime();
            DateTime actionTime = executionPlan.Schedule.ComputeActionTimeRelativeTo(startTime);

            if (actionTime < startTime)
                throw new ActionTimeInThePastException(actionTime, startTime);

            ExecutionRequest executionRequest = new ExecutionRequest
            {
                ActionTime = actionTime,
                WarningInterval = executionPlan.WarningInterval
            };

            executionTimer.Start(executionRequest);

            RaiseTimerStartedEvent(actionTime);
        }

        private void RaiseScheduleChangedEvent(ISchedule schedule)
        {
            ScheduleChangedEvent ev = new ScheduleChangedEvent(schedule);
            eventBus.Publish(ev);
        }

        private void RaiseTimerStartedEvent(DateTime nextRunTime)
        {
            TimerStartedEvent ev = new TimerStartedEvent
            {
                ActionTime = nextRunTime
            };

            eventBus.Publish(ev);
        }
    }
}