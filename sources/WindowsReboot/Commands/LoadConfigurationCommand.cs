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
using DustInTheWind.WindowsReboot.CommandModel;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.Services;
using Action = DustInTheWind.WindowsReboot.Core.Action;

namespace DustInTheWind.WindowsReboot.Commands
{
    internal class LoadConfigurationCommand : CommandBase
    {
        private readonly Timer timer;
        private readonly Action action;
        private readonly WindowsRebootConfiguration configuration;

        public override bool CanExecute
        {
            get { return !timer.IsRunning; }
        }

        public LoadConfigurationCommand(IUserInterface userInterface, Timer timer, Action action, WindowsRebootConfiguration configuration)
            : base(userInterface)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (action == null) throw new ArgumentNullException("action");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.timer = timer;
            this.action = action;
            this.configuration = configuration;

            timer.Started += HandleTimerStarted;
            timer.Stopped += HandleTimerStopped;
        }

        private void HandleTimerStarted(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        private void HandleTimerStopped(object sender, EventArgs e)
        {
            userInterface.Dispatch(OnCanExecuteChanged);
        }

        protected override void DoExecute()
        {
            if (timer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            timer.Time = configuration.ActionTime;
            action.Type = configuration.ActionType;
            action.Force = configuration.ForceClosingPrograms;
        }
    }
}