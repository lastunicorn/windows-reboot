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
using DustInTheWind.WindowsReboot.Commands;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.UiCommon;
using Action = DustInTheWind.WindowsReboot.Core.Action;
using Timer = DustInTheWind.WindowsReboot.Core.Timer;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal class WindowsRebootPresenter : ViewModelBase
    {
        /// <summary>
        /// The view used to interact with the user.
        /// </summary>
        private readonly IWindowsRebootView view;

        private readonly IUserInterface userInterface;

        private readonly Timer timer;
        private string title;
        private readonly WindowsRebootConfiguration configuration;

        public GoToTrayCommand GoToTrayCommand { get; private set; }
        public LoadDefaultConfigurationCommand LoadDefaultConfigurationCommand { get; private set; }
        public LoadConfigurationCommand LoadConfigurationCommand { get; private set; }
        public SaveConfigurationCommand SaveConfigurationCommand { get; private set; }
        public OptionsCommand OptionsCommand { get; private set; }
        public LicenseCommand LicenseCommand { get; private set; }
        public AboutCommand AboutCommand { get; private set; }
        public ExitCommand ExitCommand { get; private set; }

        public LockComputerCommand LockComputerCommand { get; private set; }
        public LogOffCommand LogOffCommand { get; private set; }
        public SleepCommand SleepCommand { get; private set; }
        public HibernateCommand HibernateCommand { get; private set; }
        public RebootCommand RebootCommand { get; private set; }
        public ShutDownCommand ShutDownCommand { get; private set; }
        public PowerOffCommand PowerOffCommand { get; private set; }

        public ActionTimeControlViewModel ActionTimeControlViewModel { get; private set; }
        public ActionTypeControlViewModel ActionTypeControlViewModel { get; private set; }
        public ActionControlViewModel ActionControlViewModel { get; private set; }
        public StatusControlViewModel StatusControlViewModel { get; private set; }

        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootPresenter"/> class with
        /// the view used to interact with the user.
        /// </summary>
        public WindowsRebootPresenter(IWindowsRebootView view, IUserInterface userInterface, Action action, Timer timer,
            IRebootUtil rebootUtil, WindowsRebootConfiguration windowsRebootConfiguration, ApplicationEnvironment applicationEnvironment)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (action == null) throw new ArgumentNullException("action");
            if (timer == null) throw new ArgumentNullException("timer");
            if (rebootUtil == null) throw new ArgumentNullException("rebootUtil");
            if (windowsRebootConfiguration == null) throw new ArgumentNullException("windowsRebootConfiguration");
            if (applicationEnvironment == null) throw new ArgumentNullException("applicationEnvironment");

            this.view = view;
            this.userInterface = userInterface;
            this.timer = timer;
            this.configuration = windowsRebootConfiguration;

            ActionTimeControlViewModel = new ActionTimeControlViewModel(timer, userInterface);
            ActionTypeControlViewModel = new ActionTypeControlViewModel(timer, action, userInterface);
            ActionControlViewModel = new ActionControlViewModel(timer, userInterface);
            StatusControlViewModel = new StatusControlViewModel(timer, userInterface);

            GoToTrayCommand = new GoToTrayCommand(userInterface);
            LoadDefaultConfigurationCommand = new LoadDefaultConfigurationCommand(userInterface, timer, action);
            LoadConfigurationCommand = new LoadConfigurationCommand(userInterface, timer, action, configuration);
            SaveConfigurationCommand = new SaveConfigurationCommand(userInterface, timer, action, configuration);
            OptionsCommand = new OptionsCommand(userInterface, configuration);
            LicenseCommand = new LicenseCommand(userInterface);
            AboutCommand = new AboutCommand(userInterface);
            ExitCommand = new ExitCommand(userInterface, applicationEnvironment);

            LockComputerCommand = new LockComputerCommand(userInterface, rebootUtil);
            LogOffCommand = new LogOffCommand(userInterface, rebootUtil);
            SleepCommand = new SleepCommand(userInterface, rebootUtil);
            HibernateCommand = new HibernateCommand(userInterface, rebootUtil);
            RebootCommand = new RebootCommand(userInterface, rebootUtil);
            ShutDownCommand = new ShutDownCommand(userInterface, rebootUtil);
            PowerOffCommand = new PowerOffCommand(userInterface, rebootUtil);
        }

        /// <summary>
        /// Method called when the form is loaded.
        /// </summary>
        internal void OnFormLoad()
        {
            try
            {
                string title = string.Format("{0} {1}", Application.ProductName, VersionUtil.GetVersionToString());

                Title = title;
                view.NotifyIconText = title;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the mouse is moved over the notify icon.
        /// </summary>
        internal void OnNotifyIconMouseMove()
        {
            try
            {
                view.NotifyIconText = timer.IsRunning
                    ? TimerFormatter.Format(timer.TimeUntilAction)
                    : Title;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the notify icon is clicked with the left mouse button.
        /// </summary>
        internal void OnNotifyIconMouseClicked()
        {
            try
            {
                userInterface.MainWindowState = MainWindowState.Normal;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Show" item of the notify icon menu was clicked.
        /// </summary>
        internal void OnNotifyIconShowClicked()
        {
            try
            {
                userInterface.MainWindowState = MainWindowState.Normal;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}