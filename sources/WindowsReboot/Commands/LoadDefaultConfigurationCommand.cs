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
using DustInTheWind.WindowsReboot.Services;
using Action = DustInTheWind.WindowsReboot.Core.Action;

namespace DustInTheWind.WindowsReboot.Commands
{
    internal class LoadDefaultConfigurationCommand : CommandBase
    {
        private readonly Timer timer;
        private readonly Action action;

        public override bool CanExecute
        {
            get { return !timer.IsRunning; }
        }

        public LoadDefaultConfigurationCommand(IUserInterface userInterface, Timer timer, Action action)
            : base(userInterface)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (action == null) throw new ArgumentNullException("action");

            this.timer = timer;
            this.action = action;

            timer.Started += HandleTimerStarted;
            timer.Stoped += HandleTimerStoped;
        }

        private void HandleTimerStarted(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        private void HandleTimerStoped(object sender, EventArgs e)
        {
            userInterface.Dispatch(OnCanExecuteChanged);
        }

        protected override void DoExecute()
        {
            if (timer.IsRunning)
                throw new WindowsRebootException("Cannot complete the task while the timer is started.");

            timer.Time = new ScheduleTime
            {
                Type = TaskTimeType.Delay
            };

            action.Type = TaskType.PowerOff;
            action.Force = true;
        }
    }
}