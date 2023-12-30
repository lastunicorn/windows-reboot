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
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.PlanExecutionArea.ExecutePlannedAction
{
    internal class ExecutePlannedActionUseCase : IRequestHandler<ExecutePlannedActionRequest>
    {
        private readonly ExecutionPlan executionPlan;
        private readonly IUserInterface userInterface;
        private readonly IOperatingSystem operatingSystem;
        private readonly EventBus eventBus;

        public ExecutePlannedActionUseCase(ExecutionPlan executionPlan, IUserInterface userInterface, IOperatingSystem operatingSystem, EventBus eventBus)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task Handle(ExecutePlannedActionRequest request, CancellationToken cancellationToken)
        {
            Execute();
            RaiseTimerStoppedEvent();

            return Task.CompletedTask;
        }

        public void Execute()
        {
            switch (executionPlan.ActionType)
            {
                case ActionType.Ring:
                    userInterface.DisplayNotification();
                    break;

                case ActionType.LockWorkstation:
                    operatingSystem.Lock();
                    break;

                case ActionType.LogOff:
                    operatingSystem.LogOff(executionPlan.ForceOption == ForceOption.Yes);
                    break;

                case ActionType.Sleep:
                    operatingSystem.Sleep(executionPlan.ForceOption == ForceOption.Yes);
                    break;

                case ActionType.Hibernate:
                    operatingSystem.Hibernate(executionPlan.ForceOption == ForceOption.Yes);
                    break;

                case ActionType.Reboot:
                    operatingSystem.Reboot(executionPlan.ForceOption == ForceOption.Yes);
                    break;

                case ActionType.ShutDown:
                    operatingSystem.ShutDown(executionPlan.ForceOption == ForceOption.Yes);
                    break;

                case ActionType.PowerOff:
                    operatingSystem.PowerOff(executionPlan.ForceOption == ForceOption.Yes);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RaiseTimerStoppedEvent()
        {
            TimerStoppedEvent ev = new TimerStoppedEvent();
            eventBus.Publish(ev);
        }
    }
}