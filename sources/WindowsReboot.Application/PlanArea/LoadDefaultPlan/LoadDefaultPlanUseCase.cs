﻿// Windows Reboot
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
using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.PlanArea.LoadDefaultPlan
{
    internal class LoadDefaultPlanUseCase : IRequestHandler<LoadDefaultPlanRequest>
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;

        public LoadDefaultPlanUseCase(ExecutionTimer executionTimer, ExecutionPlan executionPlan)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
        }

        public Task Handle(LoadDefaultPlanRequest request, CancellationToken cancellationToken)
        {
            if (executionTimer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            executionTimer.Schedule = new Schedule
            {
                Type = ScheduleType.Delay
            };

            executionPlan.ActionType = ActionType.PowerOff;
            executionPlan.ForceOption = ForceOption.Yes;
            executionTimer.WarningTime = null;

            return Task.CompletedTask;
        }
    }
}