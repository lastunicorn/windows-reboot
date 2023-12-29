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
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.PlanArea.LoadDefaultPlan
{
    internal class LoadDefaultPlanUseCase : IRequestHandler<LoadDefaultPlanRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly IExecutionProcess executionProcess;

        public LoadDefaultPlanUseCase(ExecutionPlan executionPlan, IExecutionProcess executionProcess)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.executionProcess = executionProcess ?? throw new ArgumentNullException(nameof(executionProcess));
        }

        public Task Handle(LoadDefaultPlanRequest request, CancellationToken cancellationToken)
        {
            if (executionProcess.IsTimerRunning())
                throw new TimerIsRunningException();

            executionPlan.Schedule = new DelaySchedule
            {
                Hours = 0,
                Minutes = 10,
                Seconds = 0
            };

            executionPlan.ActionType = ActionType.PowerOff;
            executionPlan.ForceOption = ForceOption.Yes;
            executionPlan.DeactivateWarning();

            return Task.CompletedTask;
        }
    }
}