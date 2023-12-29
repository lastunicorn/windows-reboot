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
using DustInTheWind.WindowsReboot.Application.TimerArea.ExecutePlan;
using DustInTheWind.WindowsReboot.Application.TimerArea.WarnTheUser;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WorkerEngine;
using MediatR;

namespace DustInTheWind.WindowsReboot.Workers
{
    public class ExecutionWorker : IWorker
    {
        private readonly IMediator mediator;
        private readonly ExecutionPlan executionPlan;

        public ExecutionWorker(IMediator mediator, ExecutionPlan executionPlan)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
        }

        public void Start()
        {
            executionPlan.Warning += HandleExecutionPlanWarning;
            executionPlan.Ring += HandleExecutionPlanRing;
        }

        public void Stop()
        {
            executionPlan.Warning -= HandleExecutionPlanWarning;
            executionPlan.Ring -= HandleExecutionPlanRing;
        }

        private void HandleExecutionPlanWarning(object sender, EventArgs e)
        {
            WarnTheUserRequest request = new WarnTheUserRequest();
            _ = mediator.Send(request);
        }

        private void HandleExecutionPlanRing(object sender, EventArgs eventArgs)
        {
            ExecutePlanRequest request = new ExecutePlanRequest();
            _ = mediator.Send(request);
        }
    }
}