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

using System;
using System.Diagnostics;
using System.Windows.Forms;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application;
using DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication;
using DustInTheWind.WindowsReboot.Application.MainArea.HideApplication;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using DustInTheWind.WindowsReboot.Presentation.Commands;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.Tray
{
    public class TrayIconViewModel : ViewModelBase
    {
        private readonly IExecutionTimer executionTimer;
        private readonly string defaultText;
        private string text;
        private bool isVisible;

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

        public TrayIconViewModel(IExecutionTimer executionTimer,
            EventBus eventBus,
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

            string productName = System.Windows.Forms.Application.ProductName;
            string versionAsString = VersionUtil.GetVersionToString();
            defaultText = $"{productName} {versionAsString}";

            Text = defaultText;

            eventBus.Subscribe<ApplicationStateChangedEvent>(HandleApplicationStateChangedEvent);
            eventBus.Subscribe<ApplicationClosingEvent>(HandleApplicationClosingEvent);
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

                    case ApplicationState.Hidden:
                        IsVisible = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Form mainForm = (Form)Control.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
                MessageBox.Show(mainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleApplicationClosingEvent(ApplicationClosingEvent ev)
        {
            IsVisible = false;
        }

        public void OnNotifyIconMouseMove()
        {
            try
            {
                Text = executionTimer.IsTimerRunning()
                    ? (TimerText)executionTimer.GetTimeUntilAction()
                    : defaultText;
            }
            catch (Exception ex)
            {
                Form mainForm = (Form)Control.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
                MessageBox.Show(mainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}