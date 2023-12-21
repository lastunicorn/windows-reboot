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
using DustInTheWind.WindowsReboot.Presentation.CommandModel;
using DustInTheWind.WindowsReboot.Services;

namespace DustInTheWind.WindowsReboot.Presentation.Commands
{
    public class RestoreMainWindowCommand : CommandBase
    {
        public override bool CanExecute => userInterface.MainWindowState == MainWindowState.Tray;

        public RestoreMainWindowCommand(IUserInterface userInterface)
            : base(userInterface)
        {
            userInterface.MainWindowStateChanged += HandleUserInterfaceMainWindowStateChanged;
        }

        private void HandleUserInterfaceMainWindowStateChanged(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        protected override void DoExecute()
        {
            userInterface.MainWindowState = MainWindowState.Normal;
        }
    }
}