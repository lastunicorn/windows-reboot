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
using DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.PlanArea.LoadThePlan
{
    internal class LoadThePlanUseCase : IRequestHandler<LoadThePlanRequest>
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private readonly IConfigStorage configuration;

        public LoadThePlanUseCase(ExecutionTimer executionTimer, ExecutionPlan executionPlan, IConfigStorage configuration)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task Handle(LoadThePlanRequest request, CancellationToken cancellationToken)
        {
            if (executionTimer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            executionTimer.Schedule = configuration.Schedule.ToDomain();
            executionPlan.ActionType = configuration.ActionType.ToDomain();
            executionPlan.ForceOption = configuration.ForceClosingPrograms
                ? ForceOption.Yes
                : ForceOption.No;

            return Task.CompletedTask;
        }
    }
}