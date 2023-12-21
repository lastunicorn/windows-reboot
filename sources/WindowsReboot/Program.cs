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
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.ConfigAccess;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Presentation;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Presentation.WorkerModel;
using DustInTheWind.WindowsReboot.Setup;
using DustInTheWind.WindowsReboot.UserAccess;
using WindowsReboot.SystemAccess;
using Action = DustInTheWind.WindowsReboot.Core.Action;
using Timer = DustInTheWind.WindowsReboot.Core.Timer;

namespace DustInTheWind.WindowsReboot
{
    internal static class Program
    {
        private static UiDispatcher uiDispatcher;
        private static UserInterface userInterface;
        private static WindowsRebootConfiguration windowsRebootConfiguration;
        private static IRebootUtil rebootUtil;
        private static Timer timer;
        private static Action action;
        private static Workers workers;
        private static ApplicationEnvironment applicationEnvironment;
        private static MainWindowCloseBehaviour mainWindowCloseBehaviour;
        private static MainWindowStateBehaviour mainWindowStateBehaviour;
        private static TrayIcon trayIcon;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WindowsRebootForm mainWindow = new WindowsRebootForm();
            uiDispatcher = new UiDispatcher();
            windowsRebootConfiguration = new WindowsRebootConfiguration();

            userInterface = new UserInterface(uiDispatcher, windowsRebootConfiguration)
            {
                MainForm = mainWindow
            };

            rebootUtil = new RebootUtil();
            timer = new Timer();
            action = new Action(timer, rebootUtil);

            IWorkerProvider workerProvider = new WorkerProvider(userInterface, timer, action);
            workers = new Workers(workerProvider);

            applicationEnvironment = new ApplicationEnvironment(action, timer, workers, windowsRebootConfiguration);
            applicationEnvironment.Initialize();

            mainWindowCloseBehaviour = new MainWindowCloseBehaviour(mainWindow, applicationEnvironment, windowsRebootConfiguration, timer, userInterface);
            mainWindowStateBehaviour = new MainWindowStateBehaviour(mainWindow, userInterface, windowsRebootConfiguration);

            mainWindow.ViewModel = new WindowsRebootViewModel(userInterface, action, timer, windowsRebootConfiguration, applicationEnvironment);

            trayIcon = new TrayIcon
            {
                ViewModel = new TrayIconViewModel(userInterface, rebootUtil, timer, applicationEnvironment)
            };

            Application.Run(mainWindow);
        }
    }
}