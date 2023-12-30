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
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using DustInTheWind.WindowsReboot.Workers;
using DustInTheWind.WorkerEngine;
using ExecutionRequest = DustInTheWind.WindowsReboot.Ports.WorkerAccess.ExecutionRequest;

namespace DustInTheWind.WindowsReboot.WorkerAccess
{
    public class ExecutionTimer : IExecutionTimer
    {
        private readonly WorkersContainer workersContainer;

        public ExecutionTimer(WorkersContainer workersContainer)
        {
            this.workersContainer = workersContainer ?? throw new ArgumentNullException(nameof(workersContainer));
        }

        public void Start(ExecutionRequest executionRequest)
        {
            ExecutionWorker executionWorker = workersContainer.GetOne<ExecutionWorker>();

            Workers.ExecutionRequest requestForWorker = new Workers.ExecutionRequest
            {
                ActionTime = executionRequest.ActionTime,
                WarningInterval = executionRequest.WarningInterval
            };

            executionWorker.StartTimer(requestForWorker);
        }

        public void Stop()
        {
            ExecutionWorker executionWorker = workersContainer.GetOne<ExecutionWorker>();
            executionWorker.StopTimer();
        }

        public bool IsTimerRunning()
        {
            ExecutionWorker executionWorker = workersContainer.GetOne<ExecutionWorker>();
            return executionWorker.IsTimerRunning;
        }

        public TimeSpan GetTimeUntilAction()
        {
            ExecutionWorker executionWorker = workersContainer.GetOne<ExecutionWorker>();
            return executionWorker.TimeUntilAction;
        }

        public DateTime GetActionTime()
        {
            ExecutionWorker executionWorker = workersContainer.GetOne<ExecutionWorker>();
            return executionWorker.ActionTime;
        }
    }
}