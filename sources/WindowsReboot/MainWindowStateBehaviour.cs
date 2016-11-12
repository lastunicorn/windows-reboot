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
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.MainWindow;
using DustInTheWind.WindowsReboot.Services;

namespace DustInTheWind.WindowsReboot
{
    internal class MainWindowStateBehaviour
    {
        private readonly WindowsRebootForm mainWindow;
        private readonly IUserInterface userInterface;

        public MainWindowStateBehaviour(WindowsRebootForm mainWindow, IUserInterface userInterface)
        {
            if (mainWindow == null) throw new ArgumentNullException("mainWindow");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.mainWindow = mainWindow;
            this.userInterface = userInterface;

            userInterface.MainWindowStateChanged += HandleUserInterfaceMainWindowStateChanged;
        }

        private void HandleUserInterfaceMainWindowStateChanged(object sender, EventArgs e)
        {
            try
            {
                switch (userInterface.MainWindowState)
                {
                    case MainWindowState.Normal:
                        mainWindow.Show();
                        mainWindow.WindowState = FormWindowState.Normal;
                        mainWindow.NotifyIconVisible = false;
                        break;

                    case MainWindowState.Tray:
                        mainWindow.Hide();
                        mainWindow.NotifyIconVisible = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}