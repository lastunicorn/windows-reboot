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
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Services;

namespace DustInTheWind.WindowsReboot.Presentation
{
    public class MainWindowStateBehaviour
    {
        private readonly WindowsRebootForm mainWindow;
        private readonly IUserInterface userInterface;
        private readonly IWindowsRebootConfiguration configuration;

        public MainWindowStateBehaviour(WindowsRebootForm mainWindow, IUserInterface userInterface, IWindowsRebootConfiguration configuration)
        {
            this.mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            userInterface.MainWindowStateChanged += HandleUserInterfaceMainWindowStateChanged;
            mainWindow.SizeChanged += HandleMainWindowSizeChanged;
        }

        private void HandleMainWindowSizeChanged(object sender, EventArgs eventArgs)
        {
            if (mainWindow.WindowState != FormWindowState.Minimized)
                return;

            if (configuration.MinimizeToTray)
                userInterface.MainWindowState = MainWindowState.Tray;
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
                        break;

                    case MainWindowState.Tray:
                        mainWindow.Hide();
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