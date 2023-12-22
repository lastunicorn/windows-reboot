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
using Autofac;
using DustInTheWind.WindowsReboot.ConfigAccess;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Presentation.Workers;
using DustInTheWind.WindowsReboot.SystemAccess;
using DustInTheWind.WindowsReboot.UserAccess;
using DustInTheWind.WorkersEngine.Setup.Autofac;
using OperatingSystem = DustInTheWind.WindowsReboot.SystemAccess.OperatingSystem;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootApplication
    {
        //private readonly MainWindowCloseBehaviour mainWindowCloseBehaviour;
        //private readonly MainWindowStateBehaviour mainWindowStateBehaviour;

        private readonly WindowsRebootForm mainWindow;
        private readonly TrayIcon trayIcon;

        public WindowsRebootApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ContainerBuilder containerBuilder = new ContainerBuilder();
            ConfigureServices(containerBuilder);

            IContainer container = containerBuilder.Build();

            // Initialize Application

            mainWindow = container.Resolve<WindowsRebootForm>();

            UserInterface userInterface = container.Resolve<UserInterface>();
            userInterface.MainForm = mainWindow;

            ApplicationEnvironment applicationEnvironment = container.Resolve<ApplicationEnvironment>();
            applicationEnvironment.Initialize();

            mainWindow.ViewModel = container.Resolve<WindowsRebootViewModel>();
            
            trayIcon = container.Resolve<TrayIcon>();
            trayIcon.ViewModel = container.Resolve<TrayIconViewModel>();



            //mainWindow = new WindowsRebootForm();
            //UiDispatcher uiDispatcher = new UiDispatcher();

            //// External Services

            //ConfigStorage configStorage = new ConfigStorage();

            //UserInterface userInterface = new UserInterface(uiDispatcher, configStorage)
            //{
            //    MainForm = mainWindow
            //};

            //OperatingSystem operatingSystem = new OperatingSystem();

            //// Internal State

            //ExecutionTimer executionTimer = new ExecutionTimer();
            //ExecutionPlan executionPlan = new ExecutionPlan(operatingSystem);

            //WorkerProvider workerProvider = new WorkerProvider(userInterface, executionTimer, executionPlan);
            //Workers workers = new Workers(workerProvider);

            //ApplicationEnvironment applicationEnvironment = new ApplicationEnvironment(executionPlan, executionTimer, workers, configStorage);
            //applicationEnvironment.Initialize();

            //mainWindowCloseBehaviour = new MainWindowCloseBehaviour(mainWindow, applicationEnvironment, configStorage, executionTimer, userInterface);
            //mainWindowStateBehaviour = new MainWindowStateBehaviour(mainWindow, userInterface, configStorage);

            //mainWindow.ViewModel = new WindowsRebootViewModel(userInterface, executionPlan, executionTimer, configStorage, applicationEnvironment);

            //trayIcon = new TrayIcon
            //{
            //    ViewModel = new TrayIconViewModel(userInterface, operatingSystem, executionTimer, applicationEnvironment)
            //};
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

            //containerBuilder.RegisterType<WorkersContainer>().AsSelf().SingleInstance();
            //containerBuilder.RegisterType<WorkerProvider>().As<IWorkerProvider>().SingleInstance();

            //containerBuilder.RegisterType<TimerWorker>().AsSelf().As<IWorker>().SingleInstance();
            //containerBuilder.RegisterType<WarningWorker>().AsSelf().As<IWorker>().SingleInstance();
            //containerBuilder.RegisterType<NotificationWorker>().AsSelf().As<IWorker>().SingleInstance();
            //containerBuilder.RegisterType<MainWindowCloseWorker>().AsSelf().As<IWorker>().SingleInstance();
            //containerBuilder.RegisterType<MainWindowStateWorker>().AsSelf().As<IWorker>().SingleInstance();

            // Presentation

            containerBuilder.RegisterType<UiDispatcher>().As<IUiDispatcher>().SingleInstance();

            containerBuilder.RegisterType<WindowsRebootForm>().AsSelf();
            containerBuilder.RegisterType<WindowsRebootViewModel>().AsSelf();

            containerBuilder.RegisterType<TrayIcon>().AsSelf();
            containerBuilder.RegisterType<TrayIconViewModel>().AsSelf();

            containerBuilder.RegisterType<ApplicationEnvironment>().AsSelf().SingleInstance();
        }

        public void Run()
        {
            Application.Run(mainWindow);
        }
    }
}