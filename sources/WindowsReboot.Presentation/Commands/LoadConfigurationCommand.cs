// Windows Reboot
// Copyright (C) 2009-2015 Dust in the Wind
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
using System.Threading.Tasks;
using System.Threading;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class LoadConfigurationCommand : CommandBase
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private readonly IConfigStorage configuration;

        public override bool CanExecute => !executionTimer.IsRunning;

        public LoadConfigurationCommand(IUserInterface userInterface, ExecutionTimer executionTimer, ExecutionPlan executionPlan, IConfigStorage configuration, EventBus eventBus)
            : base(userInterface)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            
            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);
        }

        private Task HandleTimerStartedEvent(TimerStartedEvent ev, CancellationToken cancellationToken)
        {
            OnCanExecuteChanged();
            return Task.CompletedTask;
        }

        private Task HandleTimerStoppedEvent(TimerStoppedEvent ev, CancellationToken cancellationToken)
        {
            Dispatch(OnCanExecuteChanged);
            return Task.CompletedTask;
        }

        protected override void DoExecute()
        {
            if (executionTimer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            executionTimer.ScheduleTime = configuration.ActionTime;
            executionPlan.ActionType = configuration.ActionType;
            executionPlan.ForceOption = configuration.ForceClosingPrograms
                ? ForceOption.Yes
                : ForceOption.No;
        }
    }
}