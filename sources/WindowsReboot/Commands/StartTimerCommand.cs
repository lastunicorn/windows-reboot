﻿// Windows Reboot
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

namespace DustInTheWind.WindowsReboot.Commands
{
    internal class StartTimerCommand : CommandBase
    {
        private readonly Timer timer;

        public override bool CanExecute
        {
            get { return !timer.IsRunning; }
        }

        public StartTimerCommand(Timer timer, IUserInterface userInterface)
            : base(userInterface)
        {
            if (timer == null) throw new ArgumentNullException("timer");

            this.timer = timer;

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
            timer.Start();
        }
    }
}