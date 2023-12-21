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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.Presentation.Commands;
using DustInTheWind.WindowsReboot.Presentation.UiCommon;
using DustInTheWind.WindowsReboot.Services;
using Action = DustInTheWind.WindowsReboot.Core.Action;
using Timer = DustInTheWind.WindowsReboot.Core.Timer;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class WindowsRebootViewModel : ViewModelBase
    {
        private string title;

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

        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootViewModel"/> class with
        /// the view used to interact with the user.
        /// </summary>
        public WindowsRebootViewModel(IUserInterface userInterface, Action action, Timer timer,
            WindowsRebootConfiguration windowsRebootConfiguration, ApplicationEnvironment applicationEnvironment)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (timer == null) throw new ArgumentNullException(nameof(timer));
            if (windowsRebootConfiguration == null) throw new ArgumentNullException(nameof(windowsRebootConfiguration));
            if (applicationEnvironment == null) throw new ArgumentNullException(nameof(applicationEnvironment));

            ActionTimeControlViewModel = new ActionTimeControlViewModel(timer, userInterface);
            ActionTypeControlViewModel = new ActionTypeControlViewModel(timer, action, userInterface);
            ActionControlViewModel = new ActionControlViewModel(timer, userInterface);
            StatusControlViewModel = new StatusControlViewModel(timer, userInterface);

            GoToTrayCommand = new GoToTrayCommand(userInterface);
            LoadDefaultConfigurationCommand = new LoadDefaultConfigurationCommand(userInterface, timer, action);
            LoadConfigurationCommand = new LoadConfigurationCommand(userInterface, timer, action, windowsRebootConfiguration);
            SaveConfigurationCommand = new SaveConfigurationCommand(userInterface, timer, action, windowsRebootConfiguration);
            OptionsCommand = new OptionsCommand(userInterface);
            LicenseCommand = new LicenseCommand(userInterface);
            AboutCommand = new AboutCommand(userInterface);
            ExitCommand = new ExitCommand(userInterface, applicationEnvironment);

            Title = string.Format("{0} {1}", Application.ProductName, VersionUtil.GetVersionToString());
        }
    }
}