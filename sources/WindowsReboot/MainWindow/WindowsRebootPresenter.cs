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
using DustInTheWind.WindowsReboot.Core.Services;
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

        private readonly UserInterface userInterface;
        private readonly Action action;

        /// <summary>
        /// A value indicationg if the exit of the application was requested chosing the menu item.
        /// </summary>
        private bool exitRequested;

        private readonly Timer timer;
        private string title;
        private readonly WindowsRebootConfiguration configuration;

        public LoadDefaultConfigurationCommand LoadDefaultConfigurationCommand { get; private set; }
        public LoadConfigurationCommand LoadConfigurationCommand { get; private set; }
        public SaveConfigurationCommand SaveConfigurationCommand { get; private set; }
        public OptionsCommand OptionsCommand { get; private set; }
        public LicenseCommand LicenseCommand { get; private set; }
        public AboutCommand AboutCommand { get; private set; }

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
        public WindowsRebootPresenter(IWindowsRebootView view, UserInterface userInterface, ITicker ticker, Action action, Timer timer, IRebootUtil rebootUtil)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (action == null) throw new ArgumentNullException("action");
            if (timer == null) throw new ArgumentNullException("timer");
            if (rebootUtil == null) throw new ArgumentNullException("rebootUtil");

            this.view = view;
            this.userInterface = userInterface;
            this.action = action;
            this.timer = timer;

            ActionTimeControlViewModel = new ActionTimeControlViewModel(timer, userInterface);
            ActionTypeControlViewModel = new ActionTypeControlViewModel(timer, action, userInterface);
            ActionControlViewModel = new ActionControlViewModel(timer, userInterface);
            StatusControlViewModel = new StatusControlViewModel(ticker, timer, userInterface);

            configuration = new WindowsRebootConfiguration();

            LoadDefaultConfigurationCommand = new LoadDefaultConfigurationCommand(userInterface, timer, action);
            LoadConfigurationCommand = new LoadConfigurationCommand(userInterface, timer, action, configuration);
            SaveConfigurationCommand = new SaveConfigurationCommand(userInterface, timer, action, configuration);
            OptionsCommand = new OptionsCommand(userInterface, configuration);
            LicenseCommand = new LicenseCommand(userInterface);
            AboutCommand = new AboutCommand(userInterface);

            LockComputerCommand = new LockComputerCommand(userInterface, rebootUtil);
            LogOffCommand = new LogOffCommand(userInterface, rebootUtil);
            SleepCommand = new SleepCommand(userInterface, rebootUtil);
            HibernateCommand = new HibernateCommand(userInterface, rebootUtil);
            RebootCommand = new RebootCommand(userInterface,rebootUtil);
            ShutDownCommand = new ShutDownCommand(userInterface, rebootUtil);
            PowerOffCommand = new PowerOffCommand(userInterface, rebootUtil);
        }

        #region Load/Close Form events

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

                LoadConfiguration();

                if (configuration.StartTimerAtApplicationStart)
                    timer.Start();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Loads the values from the configuration file and populates the interface with them.
        /// </summary>
        private void LoadConfiguration()
        {
            timer.Time = configuration.ActionTime;
            action.Type = configuration.ActionType;
            action.Force = configuration.ForceClosingPrograms;
        }

        /// <summary>
        /// Method called before the form is closed. It has to decide if the form is allowed to be closed or not.
        /// </summary>
        /// <returns>true if the form is allowed to be closed; false otherwise.</returns>
        internal bool OnFormClosing()
        {
            bool allowToCLose = false;

            if (!exitRequested && configuration.CloseToTray)
            {
                // Minimize to tray
                view.Hide();
                view.NotifyIconVisible = true;
            }
            else
            {
                allowToCLose = !timer.IsRunning || userInterface.AskToClose("The timer is started. Are you sure you want to close the application?");
            }


            if (allowToCLose)
                view.NotifyIconVisible = false;


            exitRequested = false;

            return allowToCLose;
        }

        /// <summary>
        /// Method called when the form is minimized.
        /// </summary>
        internal void OnFormMinimized()
        {
            try
            {
                if (configuration.MinimizeToTray)
                {
                    view.Hide();
                    view.NotifyIconVisible = true;
                }
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region Notify icon events

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
                view.Show();
                view.WindowState = FormWindowState.Normal;
                view.NotifyIconVisible = false;
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
                view.Show();
                view.WindowState = FormWindowState.Normal;
                view.NotifyIconVisible = false;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #region internal void OnNotifyIconExitClicked()

        /// <summary>
        /// Method called when the "Exit" item of the notify icon menu was clicked.
        /// </summary>
        internal void OnNotifyIconExitClicked()
        {
            try
            {
                exitRequested = true;
                view.Close();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #endregion

        #region Menu item events

        /// <summary>
        /// Method called when the "Go To Tray" item from the "File" menu is clicked.
        /// </summary>
        internal void OnMenuItemGoToTrayClicked()
        {
            try
            {
                view.Hide();
                view.NotifyIconVisible = true;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Exit" item from the "File" menu is clicked.
        /// </summary>
        internal void OnMenuItemExitClicked()
        {
            try
            {
                exitRequested = true;
                view.Close();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion
    }
}