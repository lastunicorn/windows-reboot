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
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.WorkerModel;
using Action = DustInTheWind.WindowsReboot.Core.Action;

namespace DustInTheWind.WindowsReboot.Workers
{
    internal class NotificationWorker : IWorker
    {
        private readonly IUserInterface userInterface;
        private readonly Action action;

        public NotificationWorker(IUserInterface userInterface, Action action)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (action == null) throw new ArgumentNullException("action");

            this.userInterface = userInterface;
            this.action = action;
        }

        public void Start()
        {
            action.NotificationRaised += HandleActionNotificationRaised;
        }

        public void Stop()
        {
            action.NotificationRaised -= HandleActionNotificationRaised;
        }

        private void HandleActionNotificationRaised(object sender, EventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                userInterface.DisplayMessage("Ring-ring!");
            });
        }
    }
}