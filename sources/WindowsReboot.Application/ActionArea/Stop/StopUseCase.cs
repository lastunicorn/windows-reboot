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
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionArea.Stop
{
    internal class StopUseCase : IRequestHandler<StopRequest>
    {
        private readonly IExecutionProcess executionProcess;
        private readonly EventBus eventBus;

        public StopUseCase(IExecutionProcess executionProcess, EventBus eventBus)
        {
            this.executionProcess = executionProcess ?? throw new ArgumentNullException(nameof(executionProcess));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task Handle(StopRequest request, CancellationToken cancellationToken)
        {
            executionProcess.Stop();
            RaiseTimerStoppedEvent();

            return Task.CompletedTask;
        }

        private void RaiseTimerStoppedEvent()
        {
            TimerStoppedEvent ev = new TimerStoppedEvent();
            eventBus.Publish(ev);
        }
    }
}