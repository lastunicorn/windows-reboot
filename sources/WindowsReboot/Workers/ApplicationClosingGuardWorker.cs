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
using System.ComponentModel;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.WorkerModel;

namespace DustInTheWind.WindowsReboot.Workers
{
    internal class ApplicationClosingGuardWorker : IWorker
    {
        private readonly IUserInterface userInterface;
        private readonly Timer timer;
        private readonly ApplicationEnvironment applicationEnvironment;

        public ApplicationClosingGuardWorker(IUserInterface userInterface, Timer timer, ApplicationEnvironment applicationEnvironment)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (timer == null) throw new ArgumentNullException("timer");
            if (applicationEnvironment == null) throw new ArgumentNullException("applicationEnvironment");

            this.userInterface = userInterface;
            this.timer = timer;
            this.applicationEnvironment = applicationEnvironment;
        }

        public void Start()
        {
            applicationEnvironment.Closing += HandleApplicationEnvironmentClosing;
        }

        public void Stop()
        {
            applicationEnvironment.Closing -= HandleApplicationEnvironmentClosing;
        }

        private void HandleApplicationEnvironmentClosing(object sender, CancelEventArgs e)
        {
            if (!timer.IsRunning)
                return;

            userInterface.Dispatch(() =>
            {
                bool allowToClose = userInterface.AskToClose("The timer is started. Are you sure you want to close the application?");
                e.Cancel = !allowToClose;
            });
        }
    }
}