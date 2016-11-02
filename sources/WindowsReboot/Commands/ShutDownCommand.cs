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

namespace DustInTheWind.WindowsReboot.Commands
{
    internal class ShutDownCommand : CommandBase
    {
        private readonly IRebootUtil rebootUtil;

        public ShutDownCommand(IUserInterface userInterface, IRebootUtil rebootUtil)
            : base(userInterface)
        {
            if (rebootUtil == null) throw new ArgumentNullException("rebootUtil");

            this.rebootUtil = rebootUtil;
        }

        protected override void DoExecute()
        {
            bool allowToContinue = userInterface.Confirm("Do you want to shut down the sysyem?\n\nObs! From WinXP SP1 this command will also power off the system.");

            if (allowToContinue)
                rebootUtil.ShutDown(false);
        }
    }
}