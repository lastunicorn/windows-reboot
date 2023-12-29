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
using MediatR;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootApplication
    {
        private WindowsRebootForm mainWindow;
        private TrayIcon trayIcon;

        public WindowsRebootApplication()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            ServicesSetup.Execute(containerBuilder);
            IContainer container = containerBuilder.Build();

            InitializeApplication(container);
        }

        private void InitializeApplication(IComponentContext context)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Force the initialization of the Windows Forms environment.
            using (Form form = new Form())
            {
            }

            IMediator mediator = context.Resolve<IMediator>();
            InitializeApplicationRequest request = new InitializeApplicationRequest();
            mediator.Send(request).Wait();

            mainWindow = context.Resolve<WindowsRebootForm>();
            mainWindow.ViewModel = context.Resolve<WindowsRebootViewModel>();

            MainWindowCloseBehaviour mainWindowCloseBehaviour = context.Resolve<MainWindowCloseBehaviour>();
            mainWindow.AddBehavior(mainWindowCloseBehaviour);

            MainWindowMinimizeBehavior mainWindowMinimizeBehavior = context.Resolve<MainWindowMinimizeBehavior>();
            mainWindow.AddBehavior(mainWindowMinimizeBehavior);

            trayIcon = context.Resolve<TrayIcon>();
            trayIcon.ViewModel = context.Resolve<TrayIconViewModel>();
        }

        public void Run()
        {
            System.Windows.Forms.Application.Run(mainWindow);
        }
    }
}