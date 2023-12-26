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
using DustInTheWind.WindowsReboot.Application.MainArea.GoToTray;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.Presentation.Commands;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class WindowsRebootViewModel : ViewModelBase
    {
        private readonly IUserInterface userInterface;
        private string title;
        private bool isVisible;

        public GoToTrayCommand GoToTrayCommand { get; private set; }
        public LoadDefaultConfigurationCommand LoadDefaultConfigurationCommand { get; private set; }
        public LoadConfigurationCommand LoadConfigurationCommand { get; private set; }
        public SaveConfigurationCommand SaveConfigurationCommand { get; private set; }
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

        public WindowsRebootViewModel(IUserInterface userInterface, EventBus eventBus,
            ActionTimeControlViewModel actionTimeControlViewModel, ActionTypeControlViewModel actionTypeControlViewModel,
            ActionControlViewModel actionControlViewModel, StatusControlViewModel statusControlViewModel,
            GoToTrayCommand goToTrayCommand, LoadDefaultConfigurationCommand loadDefaultConfigurationCommand,
            LoadConfigurationCommand loadConfigurationCommand, SaveConfigurationCommand saveConfigurationCommand,
            OptionsCommand optionsCommand, LicenseCommand licenseCommand, AboutCommand aboutCommand, ExitCommand exitCommand)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));

            ActionTimeControlViewModel = actionTimeControlViewModel ?? throw new ArgumentNullException(nameof(actionTimeControlViewModel));
            ActionTypeControlViewModel = actionTypeControlViewModel ?? throw new ArgumentNullException(nameof(actionTypeControlViewModel));
            ActionControlViewModel = actionControlViewModel ?? throw new ArgumentNullException(nameof(actionControlViewModel));
            StatusControlViewModel = statusControlViewModel ?? throw new ArgumentNullException(nameof(statusControlViewModel));

            GoToTrayCommand = goToTrayCommand ?? throw new ArgumentNullException(nameof(goToTrayCommand));
            LoadDefaultConfigurationCommand = loadDefaultConfigurationCommand ?? throw new ArgumentNullException(nameof(loadDefaultConfigurationCommand));
            LoadConfigurationCommand = loadConfigurationCommand ?? throw new ArgumentNullException(nameof(loadConfigurationCommand));
            SaveConfigurationCommand = saveConfigurationCommand ?? throw new ArgumentNullException(nameof(saveConfigurationCommand));
            OptionsCommand = optionsCommand ?? throw new ArgumentNullException(nameof(optionsCommand));
            LicenseCommand = licenseCommand ?? throw new ArgumentNullException(nameof(licenseCommand));
            AboutCommand = aboutCommand ?? throw new ArgumentNullException(nameof(aboutCommand));
            ExitCommand = exitCommand ?? throw new ArgumentNullException(nameof(exitCommand));

            string productName = System.Windows.Forms.Application.ProductName;
            string versionToString = VersionUtil.GetVersionToString();
            
            Title = $"{productName} {versionToString}";

            eventBus.Subscribe<GuiStateChangedEvent>(HandleGuiStateChangedEvent);
        }

        private void HandleGuiStateChangedEvent(GuiStateChangedEvent ev)
        {
            try
            {
                switch (ev.MainWindowState)
                {
                    case MainWindowState.Normal:
                        IsVisible = true;
                        break;

                    case MainWindowState.Tray:
                        IsVisible = false;
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
    }
}