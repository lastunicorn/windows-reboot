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
using DustInTheWind.WindowsReboot.Core.Config;

namespace DustInTheWind.WindowsReboot.Commands
{
    internal class OptionsCommand : CommandBase
    {
        private readonly WindowsRebootConfiguration configuration;

        public OptionsCommand(IUserInterface userInterface, WindowsRebootConfiguration configuration)
            : base(userInterface)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.configuration = configuration;
        }

        protected override void DoExecute()
        {
            userInterface.DisplayOptions(configuration);
        }
    }
}
