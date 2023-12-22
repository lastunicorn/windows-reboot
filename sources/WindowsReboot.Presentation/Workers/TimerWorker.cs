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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WorkersEngine;

namespace DustInTheWind.WindowsReboot.Presentation.Workers
{
    public class TimerWorker : IWorker
    {
        private readonly IUserInterface userInterface;
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;

        public TimerWorker(IUserInterface userInterface, ExecutionTimer executionTimer, ExecutionPlan executionPlan)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
        }

        public void Start()
        {
            executionTimer.Warning += HandleExecutionTimerWarning;
            executionTimer.Ring += HandleExecutionTimerRing;
        }

        public void Stop()
        {
            executionTimer.Warning -= HandleExecutionTimerWarning;
            executionTimer.Ring -= HandleExecutionTimerRing;
        }

        private void HandleExecutionTimerWarning(object sender, EventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds WindowsReboot will perform the action:\n\n{0}.", executionPlan.ActionType);
                userInterface.DisplayMessage(message);
            });
        }

        private void HandleExecutionTimerRing(object sender, EventArgs eventArgs)
        {
            executionPlan.Execute();
        }
    }
}