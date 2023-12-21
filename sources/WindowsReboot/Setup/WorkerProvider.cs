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
using System.Collections.Generic;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.WorkerModel;
using DustInTheWind.WindowsReboot.Presentation.Workers;
using Action = DustInTheWind.WindowsReboot.Core.Action;

namespace DustInTheWind.WindowsReboot.Setup
{
    internal class WorkerProvider : IWorkerProvider
    {
        private readonly IUserInterface userInterface;
        private readonly Timer timer;
        private readonly Action action;

        public WorkerProvider(IUserInterface userInterface, Timer timer, Action action)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public IEnumerable<IWorker> GetNewWorkers()
        {
            yield return new WarningWorker(userInterface, timer, action);
            yield return new NotificationWorker(userInterface, action);
        }
    }
}