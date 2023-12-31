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
using DustInTheWind.WindowsReboot.Application.PlanExecutionArea.ExecutePlannedAction;
using DustInTheWind.WindowsReboot.Application.PlanExecutionArea.WarnTheUser;
using DustInTheWind.WorkerEngine;
using MediatR;

namespace DustInTheWind.WindowsReboot.Workers
{
    public sealed class ExecutionWorker : WorkerBase, IDisposable
    {
        private readonly IMediator mediator;
        private readonly CustomTimer timer;
        private Guid currentRequestId = Guid.Empty;

        public bool IsTimerRunning => timer.IsRunning;

        public DateTime ActionTime => timer.ActionTime;

        public TimeSpan TimeUntilAction => timer.ActionTime - DateTime.Now;

        public ExecutionWorker(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            timer = new CustomTimer();
        }

        protected override void DoStart()
        {
            timer.Warning += TimerWarning;
            timer.Ring += TimerRing;
        }

        protected override void DoStop()
        {
            timer.Warning -= TimerWarning;
            timer.Ring -= TimerRing;
        }

        public void StartTimer(ExecutionRequest executionRequest)
        {
            if (!IsStarted)
                throw new WorkerNotRunningException();

            currentRequestId = executionRequest.Id;

            timer.ActionTime = executionRequest.ActionTime;
            timer.WarningInterval = executionRequest.WarningInterval;
            timer.Start();
        }

        public void StopTimer()
        {
            if (!IsStarted)
                throw new WorkerNotRunningException();

            timer.Stop();
        }

        private void TimerWarning(object sender, EventArgs e)
        {
            WarnTheUserRequest request = new WarnTheUserRequest();
            _ = mediator.Send(request);
        }

        private void TimerRing(object sender, EventArgs eventArgs)
        {
            ExecutePlannedActionRequest request = new ExecutePlannedActionRequest
            {
                RequestId = currentRequestId
            };

            currentRequestId = Guid.Empty;

            _ = mediator.Send(request);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}