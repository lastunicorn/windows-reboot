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
using DustInTheWind.WindowsReboot.CustomControls;

namespace DustInTheWind.WindowsReboot.Commands
{
    internal class StopTimerCommand : ICommand
    {
        private readonly Timer timer;
        private readonly IUserInterface userInterface;

        public bool CanExecute
        {
            get { return timer.IsRunning; }
        }

        public event EventHandler CanExecuteChanged;

        public StopTimerCommand(Timer timer, IUserInterface userInterface)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.timer = timer;
            this.userInterface = userInterface;

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

        public void Execute()
        {
            try
            {
                timer.Stop();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}