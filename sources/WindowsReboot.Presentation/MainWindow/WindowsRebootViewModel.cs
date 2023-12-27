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
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Presentation.Commands;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class WindowsRebootViewModel : ViewModelBase
    {
        private string title;
        private bool isVisible;

        public GoToTrayCommand GoToTrayCommand { get; private set; }

        public LoadDefaultPlanCommand LoadDefaultPlanCommand { get; private set; }

        public LoadThePlanCommand LoadThePlanCommand { get; private set; }

        public SaveThePlanCommand SaveThePlanCommand { get; private set; }

        public OptionsCommand OptionsCommand { get; private set; }

        public LicenseCommand LicenseCommand { get; private set; }

        public AboutCommand AboutCommand { get; private set; }

        public ExitCommand ExitCommand { get; private set; }

        public ActionTimeControlViewModel ActionTimeControlViewModel { get; private set; }

        public ActionTypeControlViewModel ActionTypeControlViewModel { get; private set; }

        public ActionControlViewModel ActionControlViewModel { get; private set; }

        public StatusControlViewModel StatusControlViewModel { get; private set; }

        public string Title
        {
            get => title;
            set
            {
                title = value;
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

        public WindowsRebootViewModel(EventBus eventBus,
            ActionTimeControlViewModel actionTimeControlViewModel, ActionTypeControlViewModel actionTypeControlViewModel,
            ActionControlViewModel actionControlViewModel, StatusControlViewModel statusControlViewModel,
            GoToTrayCommand goToTrayCommand, LoadDefaultPlanCommand loadDefaultPlanCommand,
            LoadThePlanCommand loadThePlanCommand, SaveThePlanCommand saveThePlanCommand,
            OptionsCommand optionsCommand, LicenseCommand licenseCommand, AboutCommand aboutCommand, ExitCommand exitCommand)
        {
            ActionTimeControlViewModel = actionTimeControlViewModel ?? throw new ArgumentNullException(nameof(actionTimeControlViewModel));
            ActionTypeControlViewModel = actionTypeControlViewModel ?? throw new ArgumentNullException(nameof(actionTypeControlViewModel));
            ActionControlViewModel = actionControlViewModel ?? throw new ArgumentNullException(nameof(actionControlViewModel));
            StatusControlViewModel = statusControlViewModel ?? throw new ArgumentNullException(nameof(statusControlViewModel));

            GoToTrayCommand = goToTrayCommand ?? throw new ArgumentNullException(nameof(goToTrayCommand));
            LoadDefaultPlanCommand = loadDefaultPlanCommand ?? throw new ArgumentNullException(nameof(loadDefaultPlanCommand));
            LoadThePlanCommand = loadThePlanCommand ?? throw new ArgumentNullException(nameof(loadThePlanCommand));
            SaveThePlanCommand = saveThePlanCommand ?? throw new ArgumentNullException(nameof(saveThePlanCommand));
            OptionsCommand = optionsCommand ?? throw new ArgumentNullException(nameof(optionsCommand));
            LicenseCommand = licenseCommand ?? throw new ArgumentNullException(nameof(licenseCommand));
            AboutCommand = aboutCommand ?? throw new ArgumentNullException(nameof(aboutCommand));
            ExitCommand = exitCommand ?? throw new ArgumentNullException(nameof(exitCommand));

            string productName = System.Windows.Forms.Application.ProductName;
            string versionToString = VersionUtil.GetVersionToString();

            Title = $"{productName} {versionToString}";

            eventBus.Subscribe<ApplicationStateChangedEvent>(HandleApplicationStateChangedEvent);
        }

        private void HandleApplicationStateChangedEvent(ApplicationStateChangedEvent ev)
        {
            try
            {
                switch (ev.ApplicationState)
                {
                    case ApplicationState.Normal:
                        IsVisible = true;
                        break;

                    case ApplicationState.Tray:
                        IsVisible = false;
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
    }
}