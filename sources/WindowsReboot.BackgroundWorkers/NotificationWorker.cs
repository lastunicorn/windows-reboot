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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WorkersEngine;

namespace WindowsReboot.BackgroundWorkers
{
    public class NotificationWorker : IWorker
    {
        private readonly IUserInterface userInterface;
        private readonly ExecutionPlan executionPlan;

        public NotificationWorker(IUserInterface userInterface, ExecutionPlan executionPlan)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
        }

        public void Start()
        {
            executionPlan.NotificationRaised += HandleExecutionPlanNotificationRaised;
        }

        public void Stop()
        {
            executionPlan.NotificationRaised -= HandleExecutionPlanNotificationRaised;
        }

        private void HandleExecutionPlanNotificationRaised(object sender, EventArgs e)
        {
            userInterface.DisplayNotification();
        }
    }
}