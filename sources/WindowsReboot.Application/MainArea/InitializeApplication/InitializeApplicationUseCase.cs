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
            executionTimer.Schedule = configStorage.Schedule.ToDomain();
            executionPlan.ActionType = configStorage.ActionType.ToDomain();
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