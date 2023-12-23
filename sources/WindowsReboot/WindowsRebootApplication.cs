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
using Autofac;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.ConfigAccess;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.SystemAccess;
using DustInTheWind.WindowsReboot.UserAccess;
using DustInTheWind.WorkersEngine.Setup.Autofac;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootApplication
    {
        private WindowsRebootForm mainWindow;
        private TrayIcon trayIcon;

        public WindowsRebootApplication()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            ConfigureServices(containerBuilder);

            IContainer container = containerBuilder.Build();

            InitializeApplication(container);
        }

        private static void ConfigureServices(ContainerBuilder containerBuilder)
        {
            // External Services

            containerBuilder.RegisterType<ConfigStorage>().As<IConfigStorage>().SingleInstance();
            containerBuilder.RegisterType<UserInterface>().AsSelf().As<IUserInterface>().SingleInstance();
            containerBuilder.RegisterType<OperatingSystem>().As<IOperatingSystem>().SingleInstance();

            // Internal State

            containerBuilder.RegisterType<ExecutionTimer>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<ExecutionPlan>().AsSelf().SingleInstance();

            // Workers

            containerBuilder.RegisterWorkers();

            // Event Bus

            containerBuilder.RegisterType<EventBus>().AsSelf().SingleInstance();

            // Presentation

            containerBuilder.RegisterType<UiDispatcher>().As<IUiDispatcher>().SingleInstance();

            containerBuilder.RegisterType<WindowsRebootForm>().AsSelf();
            containerBuilder.RegisterType<WindowsRebootViewModel>().AsSelf();

            containerBuilder.RegisterType<TrayIcon>().AsSelf();
            containerBuilder.RegisterType<TrayIconViewModel>().AsSelf();

            containerBuilder.RegisterType<ApplicationEnvironment>().AsSelf().SingleInstance();
        }

        private void InitializeApplication(IComponentContext context)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainWindow = context.Resolve<WindowsRebootForm>();

            UserInterface userInterface = context.Resolve<UserInterface>();
            userInterface.MainForm = mainWindow;

            ApplicationEnvironment applicationEnvironment = context.Resolve<ApplicationEnvironment>();
            applicationEnvironment.Initialize();

            mainWindow.ViewModel = context.Resolve<WindowsRebootViewModel>();

            trayIcon = context.Resolve<TrayIcon>();
            trayIcon.ViewModel = context.Resolve<TrayIconViewModel>();
        }

        public void Run()
        {
            Application.Run(mainWindow);
        }
    }
}