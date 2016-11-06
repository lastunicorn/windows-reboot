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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.MainWindow;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.Setup;
using DustInTheWind.WindowsReboot.WorkerModel;
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
        private static WorkerModel.Workers workers;
        private static ApplicationEnvironment applicationEnvironment;
        private static MainWindowCloseBehaviour mainWindowCloseBehaviour;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WindowsRebootForm mainWindow = CreateMainWindow();
            WindowsRebootPresenter presenter = CreatePresenter(mainWindow);

            mainWindow.Presenter = presenter;

            Application.Run(mainWindow);
        }

        private static WindowsRebootForm CreateMainWindow()
        {
            return new WindowsRebootForm();
        }

        private static WindowsRebootPresenter CreatePresenter(WindowsRebootForm mainWindow)
        {
            uiDispatcher = new UiDispatcher();

            userInterface = new UserInterface(uiDispatcher)
            {
                MainForm = mainWindow
            };

            windowsRebootConfiguration = new WindowsRebootConfiguration();

            rebootUtil = new RebootUtil();
            timer = new Core.Timer();
            action = new Core.Action(timer, rebootUtil);

            IWorkerProvider workerProvider = new WorkerProvider(userInterface, timer, action);
            workers = new WorkerModel.Workers(workerProvider);

            applicationEnvironment = new ApplicationEnvironment(action, timer, workers, windowsRebootConfiguration);
            applicationEnvironment.Initialize();

            mainWindowCloseBehaviour = new MainWindowCloseBehaviour(mainWindow, applicationEnvironment, windowsRebootConfiguration, timer, userInterface);

            return new WindowsRebootPresenter(mainWindow, userInterface, action, timer, rebootUtil, windowsRebootConfiguration, applicationEnvironment);
        }
    }
}