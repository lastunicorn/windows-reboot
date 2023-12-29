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
    public sealed class ExecutionWorker : IWorker, IDisposable
    {
        private readonly IMediator mediator;

        private readonly InternalExecutionTimer timer;

        public bool IsRunning => timer.IsRunning;

        public TimeSpan TimeUntilAction => timer.ActionTime - DateTime.Now;

        public DateTime ActionTime => timer.ActionTime;

        public ExecutionWorker(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            timer = new InternalExecutionTimer();
        }

        public void Start()
        {
            timer.Warning += TimerWarning;
            timer.Ring += TimerRing;
        }

        public void Stop()
        {
            timer.Warning -= TimerWarning;
            timer.Ring -= TimerRing;
        }

        public void StartTimer(DateTime actionTime, TimeSpan? warningInterval = null)
        {
            timer.ActionTime = actionTime;
            timer.WarningInterval = warningInterval;
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        private void TimerWarning(object sender, EventArgs e)
        {
            WarnTheUserRequest request = new WarnTheUserRequest();
            _ = mediator.Send(request);
        }

        private void TimerRing(object sender, EventArgs eventArgs)
        {
            ExecutePlanRequest request = new ExecutePlanRequest();
            _ = mediator.Send(request);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}