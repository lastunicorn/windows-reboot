// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
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
using Autofac;
using DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication;
using DustInTheWind.WindowsReboot.Presentation.Behaviors;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Presentation.Tray;
using DustInTheWind.WindowsReboot.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootApplicationContext : ApplicationContext
    {
        private TrayIcon trayIcon;
        private static IContainer container;

        public WindowsRebootApplicationContext()
        {
            InitializeServiceContainer();
            InitializeBusiness();
            InitializeGui();
        }

        private static void InitializeServiceContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            ServicesSetup.Execute(containerBuilder);
            container = containerBuilder.Build();
        }

        private static void InitializeBusiness()
        {
            IMediator mediator = container.Resolve<IMediator>();
            InitializeApplicationRequest request = new InitializeApplicationRequest();
            mediator.Send(request).Wait();
        }

        private void InitializeGui()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Force the Windows Forms initialization.
            using (Form form = new Form())
            {
            }

            // Main Window

            WindowsRebootForm mainForm = container.Resolve<WindowsRebootForm>();

            MainWindowCloseBehaviour mainWindowCloseBehaviour = container.Resolve<MainWindowCloseBehaviour>();
            mainForm.AddBehavior(mainWindowCloseBehaviour);

            MainWindowMinimizeBehavior mainWindowMinimizeBehavior = container.Resolve<MainWindowMinimizeBehavior>();
            mainForm.AddBehavior(mainWindowMinimizeBehavior);

            MainForm = mainForm;

            // Tray Icon

            trayIcon = container.Resolve<TrayIcon>();

            // UI Dispatcher

            UiDispatcher.Initialize();
        }
    }
}