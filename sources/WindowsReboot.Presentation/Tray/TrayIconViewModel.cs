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
using System.ComponentModel;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.Commands;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Tray
{
    public class TrayIconViewModel : ViewModelBase
    {
        private readonly IUserInterface userInterface;
        private readonly ExecutionTimer executionTimer;
        private readonly string defaultText;
        private string text;
        private bool isVisible;
        private bool wasVisibleBeforeClosing;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged();
            }
        }

        public RestoreMainWindowCommand RestoreMainWindowCommand { get; private set; }
        public LockComputerCommand LockComputerCommand { get; private set; }
        public LogOffCommand LogOffCommand { get; private set; }
        public SleepCommand SleepCommand { get; private set; }
        public HibernateCommand HibernateCommand { get; private set; }
        public RebootCommand RebootCommand { get; private set; }
        public ShutDownCommand ShutDownCommand { get; private set; }
        public PowerOffCommand PowerOffCommand { get; private set; }
        public ExitCommand ExitCommand { get; private set; }

        public TrayIconViewModel(IUserInterface userInterface, IOperatingSystem operatingSystem, ExecutionTimer executionTimer,
            ApplicationEnvironment applicationEnvironment, EventBus eventBus, IMediator mediator)
        {
            if (applicationEnvironment == null) throw new ArgumentNullException(nameof(applicationEnvironment));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            if (mediator == null) throw new ArgumentNullException(nameof(mediator));

            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));

            RestoreMainWindowCommand = new RestoreMainWindowCommand(userInterface, mediator, eventBus);
            LockComputerCommand = new LockComputerCommand(userInterface, operatingSystem);
            LogOffCommand = new LogOffCommand(userInterface, operatingSystem);
            SleepCommand = new SleepCommand(userInterface, operatingSystem);
            HibernateCommand = new HibernateCommand(userInterface, operatingSystem);
            RebootCommand = new RebootCommand(userInterface, operatingSystem);
            ShutDownCommand = new ShutDownCommand(userInterface, operatingSystem);
            PowerOffCommand = new PowerOffCommand(userInterface, operatingSystem);
            ExitCommand = new ExitCommand(userInterface, applicationEnvironment);

            defaultText = string.Format("{0} {1}", System.Windows.Forms.Application.ProductName, VersionUtil.GetVersionToString());

            Text = defaultText;

            eventBus.Subscribe<GuiStateChangedEvent>(HandleGuiStateChangedEvent);

            applicationEnvironment.Closing += HandleApplicationEnvironmentClosing;
            applicationEnvironment.CloseRevoked += HandleApplicationEnvironmentCloseRevoked;
        }

        private void HandleGuiStateChangedEvent(GuiStateChangedEvent ev)
        {
            try
            {
                switch (ev.MainWindowState)
                {
                    case MainWindowState.Normal:
                        IsVisible = false;
                        break;

                    case MainWindowState.Tray:
                        IsVisible = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        private void HandleApplicationEnvironmentClosing(object sender, CancelEventArgs e)
        {
            wasVisibleBeforeClosing = isVisible;
            IsVisible = false;
        }

        private void HandleApplicationEnvironmentCloseRevoked(object sender, EventArgs e)
        {
            IsVisible = wasVisibleBeforeClosing;
        }

        public void OnNotifyIconMouseMove()
        {
            try
            {
                Text = executionTimer.IsRunning
                    ? (TimerText)executionTimer.TimeUntilAction
                    : defaultText;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}