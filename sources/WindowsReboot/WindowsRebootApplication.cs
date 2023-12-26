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

using System.Reflection;
using Autofac;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.ActionTypeArea.PresentActionTypeConfiguration;
using DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication;
using DustInTheWind.WindowsReboot.ConfigAccess;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation;
using DustInTheWind.WindowsReboot.Presentation.Behaviors;
using DustInTheWind.WindowsReboot.Presentation.Commands;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Presentation.Tray;
using DustInTheWind.WindowsReboot.SystemAccess;
using DustInTheWind.WindowsReboot.UserAccess;
using DustInTheWind.WindowsReboot.Workers;
using DustInTheWind.WorkerEngine.Setup.Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

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

            Assembly[] workerAssemblies = new[]
            {
                typeof(ExecutionWorker).Assembly
            };
            containerBuilder.RegisterWorkers(workerAssemblies);

            // Infrastructure

            containerBuilder.RegisterType<EventBus>().AsSelf().SingleInstance();

            Assembly useCaseAssembly = typeof(PresentActionTypeConfigurationRequest).Assembly;
            MediatRConfiguration mediatRConfiguration = MediatRConfigurationBuilder.Create(useCaseAssembly)
                .WithAllOpenGenericHandlerTypesRegistered()
                .Build();
            containerBuilder.RegisterMediatR(mediatRConfiguration);

            // Presentation

            containerBuilder.RegisterType<UiDispatcher>().As<IUiDispatcher>().SingleInstance();

            containerBuilder.RegisterType<WindowsRebootForm>().AsSelf();
            containerBuilder.RegisterType<WindowsRebootViewModel>().AsSelf();
            containerBuilder.RegisterType<MainWindowCloseBehaviour>().AsSelf();
            containerBuilder.RegisterType<MainWindowMinimizeBehavior>().AsSelf();
            
            containerBuilder.RegisterType<ActionTimeControlViewModel>().AsSelf();
            containerBuilder.RegisterType<ActionTypeControlViewModel>().AsSelf();
            containerBuilder.RegisterType<ActionControlViewModel>().AsSelf();
            containerBuilder.RegisterType<StatusControlViewModel>().AsSelf();
            
            containerBuilder.RegisterType<StartTimerCommand>().AsSelf();
            containerBuilder.RegisterType<StopTimerCommand>().AsSelf();
            
            containerBuilder.RegisterType<GoToTrayCommand>().AsSelf();
            containerBuilder.RegisterType<ExitCommand>().AsSelf();
            containerBuilder.RegisterType<LoadConfigurationCommand>().AsSelf();
            containerBuilder.RegisterType<SaveConfigurationCommand>().AsSelf();
            containerBuilder.RegisterType<LoadDefaultConfigurationCommand>().AsSelf();
            containerBuilder.RegisterType<OptionsCommand>().AsSelf();
            containerBuilder.RegisterType<LicenseCommand>().AsSelf();
            containerBuilder.RegisterType<AboutCommand>().AsSelf();

            containerBuilder.RegisterType<TrayIcon>().AsSelf();
            containerBuilder.RegisterType<TrayIconViewModel>().AsSelf();
            
            containerBuilder.RegisterType<RestoreMainWindowCommand>().AsSelf();
            containerBuilder.RegisterType<LockComputerCommand>().AsSelf();
            containerBuilder.RegisterType<LogOffCommand>().AsSelf();
            containerBuilder.RegisterType<SleepCommand>().AsSelf();
            containerBuilder.RegisterType<HibernateCommand>().AsSelf();
            containerBuilder.RegisterType<RebootCommand>().AsSelf();
            containerBuilder.RegisterType<ShutDownCommand>().AsSelf();
            containerBuilder.RegisterType<PowerOffCommand>().AsSelf();
        }

        private void InitializeApplication(IComponentContext context)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            IMediator mediator = context.Resolve<IMediator>();
            InitializeApplicationRequest request = new InitializeApplicationRequest();
            mediator.Send(request).Wait();

            mainWindow = context.Resolve<WindowsRebootForm>();
            mainWindow.ViewModel = context.Resolve<WindowsRebootViewModel>();

            MainWindowCloseBehaviour mainWindowCloseBehaviour = context.Resolve<MainWindowCloseBehaviour>();
            mainWindow.AddBehavior(mainWindowCloseBehaviour);

            MainWindowMinimizeBehavior mainWindowMinimizeBehavior = context.Resolve<MainWindowMinimizeBehavior>();
            mainWindow.AddBehavior(mainWindowMinimizeBehavior);

            UserInterface userInterface = context.Resolve<UserInterface>();
            userInterface.MainForm = mainWindow;

            trayIcon = context.Resolve<TrayIcon>();
            trayIcon.ViewModel = context.Resolve<TrayIconViewModel>();
        }

        public void Run()
        {
            System.Windows.Forms.Application.Run(mainWindow);
        }
    }
}