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
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.PlanSetupArea.SetDailySchedule
{
    internal class SetDailyScheduleUseCase : IRequestHandler<SetDailyScheduleRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly IExecutionTimer executionTimer;
        private readonly EventBus eventBus;

        public SetDailyScheduleUseCase(ExecutionPlan executionPlan, IExecutionTimer executionTimer, EventBus eventBus)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task Handle(SetDailyScheduleRequest request, CancellationToken cancellationToken)
        {
            if (executionTimer.IsTimerRunning())
                throw new TimerIsRunningException();

            DailySchedule schedule = new DailySchedule
            {
                TimeOfDay = request.TimeOfDay
            };

            SetSchedule(schedule);

            return Task.CompletedTask;
        }

        private void SetSchedule(ISchedule schedule)
        {
            executionPlan.Schedule = schedule;

            ScheduleChangedEvent ev = new ScheduleChangedEvent(schedule);
            eventBus.Publish(ev);
        }
    }
}