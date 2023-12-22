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
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.CommandModel;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class PowerOffCommand : CommandBase
    {
        private readonly IOperatingSystem operatingSystem;

        public PowerOffCommand(IUserInterface userInterface, IOperatingSystem operatingSystem)
            : base(userInterface)
        {
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        }

        protected override void DoExecute()
        {
            bool allowToContinue = UserInterface.Confirm("Do you want to power off the system?\n\nObs! Only if the hardware supports 'Power Off'. Otherwise just a 'Shut Down' will be performed.");

            if (allowToContinue)
                operatingSystem.PowerOff(false);
        }
    }
}