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
using System.ComponentModel;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.MainWindow;
using DustInTheWind.WindowsReboot.Services;

namespace DustInTheWind.WindowsReboot
{
    internal class MainWindowCloseBehaviour
    {
        private readonly WindowsRebootForm mainWindow;
        private readonly ApplicationEnvironment applicationEnvironment;
        private readonly WindowsRebootConfiguration windowsRebootConfiguration;
        private readonly Timer timer;
        private readonly IUserInterface userInterface;

        private volatile bool closingFromBusiness;

        public MainWindowCloseBehaviour(WindowsRebootForm mainWindow, ApplicationEnvironment applicationEnvironment,
            WindowsRebootConfiguration windowsRebootConfiguration, Timer timer, IUserInterface userInterface)
        {
            if (mainWindow == null) throw new ArgumentNullException("mainWindow");
            if (applicationEnvironment == null) throw new ArgumentNullException("applicationEnvironment");
            if (windowsRebootConfiguration == null) throw new ArgumentNullException("windowsRebootConfiguration");
            if (timer == null) throw new ArgumentNullException("timer");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.mainWindow = mainWindow;
            this.applicationEnvironment = applicationEnvironment;
            this.windowsRebootConfiguration = windowsRebootConfiguration;
            this.timer = timer;
            this.userInterface = userInterface;

            mainWindow.Closing += HandleMainWindowClosing;

            applicationEnvironment.Closing += HandleApplicationEnvironmentClosing;
            applicationEnvironment.CloseRevoked += HandleApplicationEnvironmentCloseRevoked;
        }

        private void HandleApplicationEnvironmentClosing(object sender, CancelEventArgs e)
        {
            bool allowToClose = !timer.IsRunning || userInterface.AskToClose("The timer is started. Are you sure you want to close the application?");

            if (!allowToClose)
                e.Cancel = true;
            else
                closingFromBusiness = true;
        }

        private void HandleApplicationEnvironmentCloseRevoked(object sender, EventArgs e)
        {
            closingFromBusiness = false;
        }

        private void HandleMainWindowClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (closingFromBusiness)
                {
                    if (windowsRebootConfiguration.CloseToTray)
                    {
                        userInterface.MainWindowState = MainWindowState.Tray;
                        e.Cancel = true;
                    }

                    return;
                }

                e.Cancel = true;
                applicationEnvironment.Close();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}