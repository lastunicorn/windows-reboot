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
using System.Collections.Generic;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Services;
using DustInTheWind.WindowsReboot.MainWindow;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.WorkerModel;
using DustInTheWind.WindowsReboot.Workers;

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

            ITicker ticker = new Ticker100();
            IRebootUtil rebootUtil = new RebootUtil();
            Core.Timer timer = new Core.Timer(ticker);
            Core.Action action = new Core.Action(timer, rebootUtil);
            
            WorkerModel.Workers workers = new WorkerModel.Workers(new List<IWorker>
            {
                new WarningWorker(userInterface, timer, action)
            });

            return new WindowsRebootPresenter(mainWindow, userInterface, ticker, action, timer, rebootUtil, workers);
        }
    }
}
