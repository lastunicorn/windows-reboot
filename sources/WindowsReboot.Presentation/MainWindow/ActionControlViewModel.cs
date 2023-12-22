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

using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.CommandModel;
using DustInTheWind.WindowsReboot.Presentation.Commands;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionControlViewModel
    {
        public ICommand StartTimerCommand { get; set; }
        public ICommand StopTimerCommand { get; set; }

        public ActionControlViewModel(ExecutionTimer executionTimer, IUserInterface userInterface)
        {
            StartTimerCommand = new StartTimerCommand(executionTimer, userInterface);
            StopTimerCommand = new StopTimerCommand(executionTimer, userInterface);
        }
    }
}