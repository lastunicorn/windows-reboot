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
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication;
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Domain;
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

        public TrayIconViewModel(IUserInterface userInterface, ExecutionTimer executionTimer,
            EventBus eventBus, IMediator mediator,
            RestoreMainWindowCommand restoreMainWindowCommand,
            LockComputerCommand lockComputerCommand,
            LogOffCommand logOffCommand,
            SleepCommand sleepCommand,
            HibernateCommand hibernateCommand,
            RebootCommand rebootCommand,
            ShutDownCommand shutDownCommand,
            PowerOffCommand powerOffCommand,
            ExitCommand exitCommand)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            if (mediator == null) throw new ArgumentNullException(nameof(mediator));

            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));

            RestoreMainWindowCommand = restoreMainWindowCommand ?? throw new ArgumentNullException(nameof(restoreMainWindowCommand));
            LockComputerCommand = lockComputerCommand ?? throw new ArgumentNullException(nameof(lockComputerCommand));
            LogOffCommand = logOffCommand ?? throw new ArgumentNullException(nameof(logOffCommand));
            SleepCommand = sleepCommand ?? throw new ArgumentNullException(nameof(sleepCommand));
            HibernateCommand = hibernateCommand ?? throw new ArgumentNullException(nameof(hibernateCommand));
            RebootCommand = rebootCommand ?? throw new ArgumentNullException(nameof(rebootCommand));
            ShutDownCommand = shutDownCommand ?? throw new ArgumentNullException(nameof(shutDownCommand));
            PowerOffCommand = powerOffCommand ?? throw new ArgumentNullException(nameof(powerOffCommand));
            ExitCommand = exitCommand ?? throw new ArgumentNullException(nameof(exitCommand));

            defaultText = string.Format("{0} {1}", System.Windows.Forms.Application.ProductName, VersionUtil.GetVersionToString());

            Text = defaultText;

            eventBus.Subscribe<ApplicationStateChangedEvent>(HandleApplicationStateChangedEvent);
            eventBus.Subscribe<ApplicationClosingEvent>(HandleApplicationClosingEvent);
            eventBus.Subscribe<ApplicationCloseRevokedEvent>(HandleApplicationCloseRevokedEvent);
        }

        private void HandleApplicationStateChangedEvent(ApplicationStateChangedEvent ev)
        {
            try
            {
                switch (ev.ApplicationState)
                {
                    case ApplicationState.Normal:
                        IsVisible = false;
                        break;

                    case ApplicationState.Tray:
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

        private void HandleApplicationClosingEvent(ApplicationClosingEvent ev)
        {
            wasVisibleBeforeClosing = isVisible;
            IsVisible = false;
        }

        private void HandleApplicationCloseRevokedEvent(ApplicationCloseRevokedEvent ev)
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