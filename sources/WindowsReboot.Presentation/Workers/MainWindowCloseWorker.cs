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
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WorkersEngine;

namespace DustInTheWind.WindowsReboot.Presentation.Workers
{
    public class MainWindowCloseWorker : IWorker
    {
        private readonly WindowsRebootForm mainWindow;
        private readonly ApplicationEnvironment applicationEnvironment;
        private readonly IConfigStorage configStorage;
        private readonly ExecutionTimer executionTimer;
        private readonly IUserInterface userInterface;

        private volatile bool closingFromBusiness;

        public MainWindowCloseWorker(WindowsRebootForm mainWindow, ApplicationEnvironment applicationEnvironment,
            IConfigStorage configStorage, ExecutionTimer executionTimer, IUserInterface userInterface)
        {
            this.mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            this.applicationEnvironment = applicationEnvironment ?? throw new ArgumentNullException(nameof(applicationEnvironment));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public void Start()
        {
            mainWindow.Closing += HandleMainWindowClosing;

            applicationEnvironment.Closing += HandleApplicationEnvironmentClosing;
            applicationEnvironment.CloseRevoked += HandleApplicationEnvironmentCloseRevoked;
        }

        public void Stop()
        {
            mainWindow.Closing -= HandleMainWindowClosing;

            applicationEnvironment.Closing -= HandleApplicationEnvironmentClosing;
            applicationEnvironment.CloseRevoked -= HandleApplicationEnvironmentCloseRevoked;
        }

        private void HandleMainWindowClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (closingFromBusiness)
                {
                    if (configStorage.CloseToTray)
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

        private void HandleApplicationEnvironmentClosing(object sender, CancelEventArgs e)
        {
            bool allowToClose = !executionTimer.IsRunning || userInterface.AskToClose("The timer is started. Are you sure you want to close the application?");

            if (!allowToClose)
                e.Cancel = true;
            else
                closingFromBusiness = true;
        }

        private void HandleApplicationEnvironmentCloseRevoked(object sender, EventArgs e)
        {
            closingFromBusiness = false;
        }
    }
}