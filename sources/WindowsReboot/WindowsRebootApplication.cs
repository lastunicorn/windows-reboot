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

using System.Windows.Forms;
using DustInTheWind.WindowsReboot.ConfigAccess;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Presentation;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Presentation.WorkerModel;
using DustInTheWind.WindowsReboot.Setup;
using DustInTheWind.WindowsReboot.SystemAccess;
using DustInTheWind.WindowsReboot.UserAccess;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootApplication
    {
        private readonly MainWindowCloseBehaviour mainWindowCloseBehaviour;
        private readonly MainWindowStateBehaviour mainWindowStateBehaviour;
        private readonly TrayIcon trayIcon;

        private readonly WindowsRebootForm mainWindow;

        public WindowsRebootApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainWindow = new WindowsRebootForm();
            UiDispatcher uiDispatcher = new UiDispatcher();
            WindowsRebootConfiguration windowsRebootConfiguration = new WindowsRebootConfiguration();

            UserInterface userInterface = new UserInterface(uiDispatcher, windowsRebootConfiguration)
            {
                MainForm = mainWindow
            };

            OperatingSystem operatingSystem = new OperatingSystem();
            ExecutionTimer executionTimer = new ExecutionTimer();
            ExecutionPlan executionPlan = new ExecutionPlan(operatingSystem);

            WorkerProvider workerProvider = new WorkerProvider(userInterface, executionTimer, executionPlan);
            Workers workers = new Workers(workerProvider);

            ApplicationEnvironment applicationEnvironment = new ApplicationEnvironment(executionPlan, executionTimer, workers, windowsRebootConfiguration);
            applicationEnvironment.Initialize();

            mainWindowCloseBehaviour = new MainWindowCloseBehaviour(mainWindow, applicationEnvironment, windowsRebootConfiguration, executionTimer, userInterface);
            mainWindowStateBehaviour = new MainWindowStateBehaviour(mainWindow, userInterface, windowsRebootConfiguration);

            mainWindow.ViewModel = new WindowsRebootViewModel(userInterface, executionPlan, executionTimer, windowsRebootConfiguration, applicationEnvironment);

            trayIcon = new TrayIcon
            {
                ViewModel = new TrayIconViewModel(userInterface, operatingSystem, executionTimer, applicationEnvironment)
            };
        }

        public void Run()
        {
            Application.Run(mainWindow);
        }
    }
}