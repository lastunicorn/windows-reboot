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

        private readonly IRebootUtil rebootUtil;
        private readonly Timer timer;
        private string title;
        private readonly WindowsRebootConfiguration configuration;

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
            this.rebootUtil = rebootUtil;

            ActionTimeControlViewModel = new ActionTimeControlViewModel(timer, userInterface);
            ActionTypeControlViewModel = new ActionTypeControlViewModel(timer, action, userInterface);
            ActionControlViewModel = new ActionControlViewModel(timer, userInterface);
            StatusControlViewModel = new StatusControlViewModel(ticker, timer, userInterface);

            configuration = new WindowsRebootConfiguration();

            timer.Started += HandlePerformerStarted;
            timer.Stoped += HandlePerformerStoped;
            timer.Warning += HandleTimerWarning;
        }

        private void HandleTimerWarning(object sender, EventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds WindowsReboot will perform the action:\n\n{0}.", action.Type);
                userInterface.DisplayMessage(message);
            });
        }

        private void HandlePerformerStarted(object sender, EventArgs e)
        {
            EnableInterface(false);
        }

        private void HandlePerformerStoped(object sender, EventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                EnableInterface(true);
            });
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

        /// <summary>
        /// Method called when the tray icon is clicked by the user.
        /// </summary>
        internal void OnNotifyIconLockComputerClicked()
        {
            try
            {
                bool allowToContinue = userInterface.Confirm("Do you want to lock the workstation?");

                if (allowToContinue)
                    rebootUtil.Lock();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Log Off" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconLogOffClicked()
        {
            try
            {
                string question = string.Format("Do you want to log off the current user?\nThe current logged in user is '{0}'", Environment.UserDomainName);
                bool allowToContinue = userInterface.Confirm(question);

                if (allowToContinue)
                    rebootUtil.LogOff(false);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Sleep" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconSleepClicked()
        {
            try
            {
                bool allowToContinue = userInterface.Confirm("Do you want to put the system in 'Stand By' state?");

                if (allowToContinue)
                    rebootUtil.Sleep(false);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #region internal void OnNotifyIconHibernateClicked()

        /// <summary>
        /// Method clicked when the user choose "Hibernate" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconHibernateClicked()
        {
            try
            {
                bool allowToContinue = userInterface.Confirm("Do you want to put the system in 'Hibernate' state?");

                if (allowToContinue)
                    rebootUtil.Hibernate(false);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnNotifyIconRebootClicked()

        /// <summary>
        /// Method clicked when the user choose "Reboot" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconRebootClicked()
        {
            try
            {
                bool allowToContinue = userInterface.Confirm("Do you want to reboot the system?");

                if (allowToContinue)
                    rebootUtil.Reboot(false);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnNotifyIconShutDownClicked()

        /// <summary>
        /// Method clicked when the user choose "Shut Down" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconShutDownClicked()
        {
            try
            {
                bool allowToContinue = userInterface.Confirm("Do you want to shut down the sysyem?\n\nObs! From WinXP SP1 this command will also power off the system.");

                if (allowToContinue)
                    rebootUtil.ShutDown(false);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnNotifyIconPowerOffClicked()

        /// <summary>
        /// Method clicked when the user choose "Power Off" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconPowerOffClicked()
        {
            try
            {
                bool allowToContinue = userInterface.Confirm("Do you want to power off the system?\n\nObs! Only if the hardware supports 'Power Off'. Otherwise just a 'Shut Down' will be performed.");

                if (allowToContinue)
                    rebootUtil.PowerOff(false);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

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

        /// <summary>
        /// Method called when the "Options" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemOptionsClicked()
        {
            userInterface.DisplayOptions(configuration);
        }

        /// <summary>
        /// Method called when the "Save Current Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemSaveCurrentSettingsClicked()
        {
            SaveConfigurationCommand command = new SaveConfigurationCommand(userInterface, timer, action, configuration);
            command.Execute();
        }

        /// <summary>
        /// Method called when the "Load Default Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemLoadDefaultSettingsClicked()
        {
            LoadDefaultConfigurationCommand command = new LoadDefaultConfigurationCommand(userInterface, timer, action);
            command.Execute();
        }

        /// <summary>
        /// Method called when the "Load Initial Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemLoadInitialSettingsClicked()
        {
            LoadConfigurationCommand command = new LoadConfigurationCommand(userInterface, timer, action, configuration);
            command.Execute();
        }

        /// <summary>
        /// Method called when the "License" item from the "Help" menu is clicked.
        /// </summary>
        internal void OnMenuItemLicenseClicked()
        {
            try
            {
                userInterface.DisplayLicense();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "About" item from the "Help" menu is clicked.
        /// </summary>
        internal void OnMenuItemAboutClicked()
        {
            try
            {
                userInterface.DisplayAbout();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        /// <summary>
        /// Enables or disables the interface.
        /// </summary>
        /// <param name="value">A value that specifies if the interface should be enabled or disabled.</param>
        private void EnableInterface(bool value)
        {
            view.MenuItem_LoadInitialSettingsEnabled = value;
            view.MenuItem_LoadDefaultSettingsEnabled = value;
        }

        /// <summary>
        /// Clears the interface and displayed the default values.
        /// </summary>
        private void ClearInterface()
        {
            timer.Time = new ScheduleTime();

            action.Type = TaskType.PowerOff;
            action.Force = true;

            ActionTimeControlViewModel.TaskTimeType = TaskTimeType.Delay;
        }

        /// <summary>
        /// Loads the values from the configuration file and populates the interface with them.
        /// </summary>
        private void LoadConfiguration()
        {
            ClearInterface();

            timer.Time = configuration.ActionTime;
            action.Type = configuration.ActionType;
            action.Force = configuration.ForceClosingPrograms;
        }
    }
}