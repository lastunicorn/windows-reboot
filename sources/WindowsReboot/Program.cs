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
using DustInTheWind.WindowsReboot.MainWindow;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.Setup;
using DustInTheWind.WindowsReboot.WorkerModel;

namespace DustInTheWind.WindowsReboot
{
    internal static class Program
    {
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
            UiDispatcher uiDispatcher = new UiDispatcher();

            UserInterface userInterface = new UserInterface(uiDispatcher)
            {
                MainForm = mainWindow
            };

            IRebootUtil rebootUtil = new RebootUtil();
            Core.Timer timer = new Core.Timer();
            Core.Action action = new Core.Action(timer, rebootUtil);

            IWorkerProvider workerProvider = new WorkerProvider(userInterface, timer, action);
            WorkerModel.Workers workers = new WorkerModel.Workers(workerProvider);

            return new WindowsRebootPresenter(mainWindow, userInterface, action, timer, rebootUtil, workers);
        }
    }
}