// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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
using System.Configuration;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;

namespace DustInTheWind.WindowsReboot.Presentation
{
    internal class WindowsRebootPresenter
    {
        /// <summary>
        /// The view used to interact with the user.
        /// </summary>
        private readonly IWindowsRebootView view;

        private readonly UserInterface userInterface;
        private readonly UiDispatcher uiDispatcher;

        /// <summary>
        /// The template used to display the time left until action.
        /// </summary>
        private const string TIME_TEMPLATE = "{0:00} : {1:00} : {2:00} . {3:0}";

        /// <summary>
        /// The text displayed when the timer is stopped.
        /// </summary>
        private const string TIME_TEMPLATE_EMPTY = "--  :  --  :  --  .  -";

        /// <summary>
        /// Indicates if the timer was started.
        /// </summary>
        private volatile bool actionIsSet;

        /// <summary>
        /// The time when the timer was started.
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// The time when the action should be executed.
        /// </summary>
        private DateTime actionTime;

        /// <summary>
        /// A value that specifies if the form should be opened with the timer started or not.
        /// </summary>
        private bool startAtStartUp;

        private readonly Configuration config;

        private readonly WindowsRebootConfigSection configSection;

        /// <summary>
        /// A value indicationg if the exit of the application was requested chosing the menu item.
        /// </summary>
        private bool exitRequested;

        private bool displayWarningMessage;
        private readonly TimeSpan warningMessageTime = TimeSpan.FromSeconds(30);

        private readonly IRebootUtil rebootUtil;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootPresenter"/> class with
        /// the view used to interact with the user.
        /// </summary>
        /// <param name="view">The view used to interact with the user.</param>
        /// <param name="userInterface"></param>
        /// <param name="uiDispatcher"></param>
        public WindowsRebootPresenter(IWindowsRebootView view, UserInterface userInterface, UiDispatcher uiDispatcher)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.view = view;
            this.userInterface = userInterface;
            this.uiDispatcher = uiDispatcher;

            rebootUtil = new RebootUtil();

            config = GetConfiguration();
            configSection = WindowsRebootConfigSection.GetOrCreateSection(config);
        }

        #endregion

        #region internal void OnTimerElapsed()

        /// <summary>
        /// Method called by the timer. It updates the interface and, if necesary, it executes the chosen action.
        /// </summary>
        internal void OnTimerElapsed()
        {
            DateTime now = DateTime.Now;

            RefreshCurentTimeLabel(now);

            if (actionIsSet)
            {
                RefreshTimerLabel(now);
                DisplayWarningIfNeeded(now);
                DoActionIfNeeded(now);
            }
        }

        private void RefreshCurentTimeLabel(DateTime now)
        {
            view.LabelCurrentTime = string.Format("{0}  :  {1}", now.ToLongDateString(), now.ToLongTimeString());
        }

        private void RefreshTimerLabel(DateTime now)
        {
            TimeSpan timeUntilAction = actionTime - now;
            view.LabelTimer = ConstructTimerLabel(timeUntilAction);
        }

        private void DisplayWarningIfNeeded(DateTime now)
        {
            if (!displayWarningMessage || actionTime - warningMessageTime > now)
                return;

            displayWarningMessage = false;

            uiDispatcher.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds WindowsReboot will perform {0} action.", view.ActionType);
                userInterface.DisplayMessage(message);
            });
        }

        private void DoActionIfNeeded(DateTime now)
        {
            if (actionTime <= now)
            {
                actionIsSet = false;
                DoAction(view.ActionType.Value);
                EnableInterface(true);
                view.LabelActionTime = string.Empty;
                view.LabelTimer = TIME_TEMPLATE_EMPTY;
            }
        }

        private static string ConstructTimerLabel(TimeSpan timeUntilAction)
        {
            string tmp;

            int d = timeUntilAction.Days;
            int h = timeUntilAction.Hours;
            int m = timeUntilAction.Minutes;
            int s = timeUntilAction.Seconds;
            int f = Convert.ToInt32(Math.Round((double)(timeUntilAction.Milliseconds / 100)));

            if (d == 1)
            {
                tmp = "1 day . ";
            }
            else if (d > 1)
            {
                tmp = d + " days . ";
            }
            else
            {
                tmp = string.Empty;
            }

            tmp += string.Format(TIME_TEMPLATE, h, m, s, f);
            return tmp;
        }

        #endregion

        #region private void DoAction(ActionType actionType)

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="actionType">The action to be executes.</param>
        private void DoAction(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Ring:
                    uiDispatcher.Dispatch(() =>
                    {
                        userInterface.DisplayMessage("Ring-ring!");
                    });
                    break;

                case ActionType.LockWorkstation:
                    rebootUtil.Lock();
                    break;

                case ActionType.LogOff:
                    rebootUtil.LogOff(view.ForceAction);
                    break;

                case ActionType.Sleep:
                    rebootUtil.Sleep(view.ForceAction);
                    break;

                case ActionType.Hibernate:
                    rebootUtil.Hibernate(view.ForceAction);
                    break;

                case ActionType.Reboot:
                    rebootUtil.Reboot(view.ForceAction);
                    break;

                case ActionType.ShutDown:
                    rebootUtil.ShutDown(view.ForceAction);
                    break;

                case ActionType.PowerOff:
                    rebootUtil.PowerOff(view.ForceAction);
                    break;
            }
        }

        #endregion

        #region private DateTime CalculateActionTime(DateTime now)

        /// <summary>
        /// Calculates the time of the action based on the values provided by the user.
        /// </summary>
        /// <param name="now">The current time.</param>
        /// <returns>The time of the action.</returns>
        private DateTime CalculateActionTime(DateTime now)
        {
            DateTime time;

            if (view.FixedTimeGroupSelected)
            {
                time = view.FixedDate.Add(view.FixedTime);
            }
            else if (view.DelayGroupSelected)
            {
                time = now.Add(new TimeSpan(view.Hours, view.Minutes, view.Seconds));
            }
            else if (view.ImmediateGroupSelected)
            {
                time = now;
            }
            else
            {
                throw new Exception("No action time was chosen.");
            }

            return time;
        }

        #endregion

        #region Start/Stop timer

        /// <summary>
        /// Method called when the "Start timer" button is clicked.
        /// </summary>
        internal void OnStartTimerClicked()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime actionTime = CalculateActionTime(now);

                if (actionTime < now)
                {
                    string currentTimeString = now.ToLongDateString() + " : " + now.ToLongTimeString();
                    string actionTimeString = actionTime.ToLongDateString() + " : " + actionTime.ToLongTimeString();

                    string message = string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString);
                    userInterface.DisplayErrorMessage(message);
                }
                else
                {
                    this.actionTime = actionTime;
                    startTime = now;
                    EnableInterface(false);
                    view.LabelActionTime = this.actionTime.ToLongDateString() + "  :  " + this.actionTime.ToLongTimeString();

                    if (view.DisplayActionWarning && actionTime - now > warningMessageTime)
                        displayWarningMessage = true;

                    actionIsSet = true;
                }
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Stop timer" button is clicked.
        /// </summary>
        internal void OnStopTimerClicked()
        {
            try
            {
                actionIsSet = false;
                EnableInterface(true);
                view.LabelActionTime = string.Empty;
                view.LabelTimer = TIME_TEMPLATE_EMPTY;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region Load/Close Form events

        /// <summary>
        /// Method called when the form is loaded.
        /// </summary>
        internal void OnFormLoad()
        {
            try
            {
                string title = Application.ProductName + " " + VersionUtil.GetVersionToString();
                view.Title = title;
                view.NotifyIconText = title;

                Array values = Enum.GetValues(typeof(ActionType));

                ActionTypeItem[] items = new ActionTypeItem[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    items[i] = new ActionTypeItem((ActionType)values.GetValue(i));
                }

                view.ActionTypes = items;

                LoadInitialConfiguration();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }

            if (startAtStartUp)
                OnStartTimerClicked();
        }

        /// <summary>
        /// Method called before the form is closed. It has to decide if the form is allowed to be closed or not.
        /// </summary>
        /// <returns>true if the form is allowed to be closed; false otherwise.</returns>
        internal bool OnFormClosing()
        {
            bool allowToCLose = false;

            if (!exitRequested && configSection.CloseToTray.Value)
            {
                // Minimize to tray
                view.Hide();
                view.NotifyIconVisible = true;
            }
            else
            {
                allowToCLose = !actionIsSet || userInterface.AskToClose("The timer is started. Are you sure you want to close the application?");
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
                if (configSection.MinimizeToTray.Value)
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

        #region internal void OnNotifyIconMouseMove()

        /// <summary>
        /// Method called when the mouse is moved over the notify icon.
        /// </summary>
        internal void OnNotifyIconMouseMove()
        {
            try
            {
                view.NotifyIconText = actionIsSet 
                    ? view.LabelTimer
                    : view.Title;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnNotifyIconMouseClicked()

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

        #endregion

        #region internal void OnNotifyIconShowClicked()

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

        #endregion

        #region internal void OnNotifyIconLockComputerClicked()

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

        #endregion

        #region internal void OnNotifyIconLogOffClicked()

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

        #endregion

        #region internal void OnNotifyIconSleepClicked()

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

        #endregion

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
            if (userInterface.DisplayOptions(configSection))
            {
                // Save the option to the file.
                config.Save(ConfigurationSaveMode.Modified);
            }
        }

        /// <summary>
        /// Method called when the "Save Current Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemSaveCurrentSettingsClicked()
        {
            try
            {
                SaveConfiguration();
                userInterface.DisplayMessage("The configuration was saved.");
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Load Default Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemLoadDefaultSettingsClicked()
        {
            try
            {
                if (actionIsSet)
                    userInterface.DisplayErrorMessage("Cannot complete the task while the timer is started.");
                else
                    ClearInterface();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Load Initial Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemLoadInitialSettingsClicked()
        {
            try
            {
                if (actionIsSet)
                    userInterface.DisplayErrorMessage("Cannot complete the task while the timer is started.");
                else
                    LoadInitialConfiguration();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
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

        #region private void EnableInterface(bool value)

        /// <summary>
        /// Enables or disables the interface.
        /// </summary>
        /// <param name="value">A value that specifies if the interface should be enabled or disabled.</param>
        private void EnableInterface(bool value)
        {
            view.ActionTimeGroupEnabled = value;
            view.ActionTypeGroupEnabled = value;
            view.MenuItem_LoadInitialSettingsEnabled = value;
            view.MenuItem_LoadDefaultSettingsEnabled = value;
        }

        #endregion

        #region private void ClearInterface()

        /// <summary>
        /// Clears the interface and displayed the default values.
        /// </summary>
        private void ClearInterface()
        {
            DateTime now = DateTime.Now;

            view.FixedDate = now.Date;
            view.FixedTime = now.TimeOfDay;

            view.Hours = 0;
            view.Minutes = 0;
            view.Seconds = 0;

            view.DelayGroupSelected = true;

            view.ActionType = new ActionTypeItem(ActionType.PowerOff);

            view.ForceAction = true;
        }

        #endregion

        #region private void LoadInitialConfiguration()

        /// <summary>
        /// Loads the values from the configuration file and populates the interface with them.
        /// </summary>
        private void LoadInitialConfiguration()
        {
            WindowsRebootConfigSection configSection = GetConfigurationSection();

            ClearInterface();

            switch (configSection.ActionTime.Type)
            {
                case ActionTimeType.FixedDate:
                    view.FixedDate = this.configSection.ActionTime.DateTime.Date;
                    view.FixedTime = this.configSection.ActionTime.DateTime.TimeOfDay;
                    view.FixedTimeGroupSelected = true;
                    break;

                case ActionTimeType.Delay:
                    view.Hours = this.configSection.ActionTime.Hours;
                    view.Minutes = this.configSection.ActionTime.Minutes;
                    view.Seconds = this.configSection.ActionTime.Seconds;
                    view.DelayGroupSelected = true;
                    break;

                case ActionTimeType.Immediate:
                    view.ImmediateGroupSelected = true;
                    break;
            }

            view.ActionType = new ActionTypeItem(this.configSection.ActionType.Value);

            view.ForceAction = this.configSection.ForceClosingPrograms.Value;

            startAtStartUp = this.configSection.StartTimerAtApplicationStart.Value;
        }

        #endregion

        #region private void SaveConfiguration()

        /// <summary>
        /// Saves the current values from the interface into the configuration file.
        /// </summary>
        private void SaveConfiguration()
        {
            //Configuration config = this.GetConfiguration();
            //WindowsRebootConfigSection configSection = WindowsRebootConfigSection.GetOrCreateSection(config);

            if (view.FixedTimeGroupSelected)
            {
                configSection.ActionTime.Type = ActionTimeType.FixedDate;
                configSection.ActionTime.DateTime = view.FixedDate.Date.Add(view.FixedTime);
                configSection.ActionTime.Hours = 0;
                configSection.ActionTime.Minutes = 0;
                configSection.ActionTime.Seconds = 0;
            }
            else if (view.DelayGroupSelected)
            {
                configSection.ActionTime.Type = ActionTimeType.Delay;
                configSection.ActionTime.Hours = view.Hours;
                configSection.ActionTime.Minutes = view.Minutes;
                configSection.ActionTime.Seconds = view.Seconds;
                configSection.ActionTime.DateTime = DateTime.Now;
            }
            else if (view.ImmediateGroupSelected)
            {
                configSection.ActionTime.Type = ActionTimeType.Immediate;
                configSection.ActionTime.DateTime = DateTime.Now;
                configSection.ActionTime.Hours = 0;
                configSection.ActionTime.Minutes = 0;
                configSection.ActionTime.Seconds = 0;
            }

            configSection.ActionType.Value = view.ActionType.Value;

            configSection.ForceClosingPrograms.Value = view.ForceAction;

            //config.Save();
            config.Save(ConfigurationSaveMode.Modified);
        }

        #endregion

        #region Configuration

        private static Configuration GetConfiguration()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        private static WindowsRebootConfigSection GetConfigurationSection()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return WindowsRebootConfigSection.GetOrCreateSection(config);
        }

        #endregion

        internal void OnActionTypeChanged()
        {
            switch (view.ActionType.Value)
            {
                case ActionType.LogOff:
                case ActionType.Sleep:
                case ActionType.Hibernate:
                case ActionType.Reboot:
                case ActionType.ShutDown:
                case ActionType.PowerOff:
                    view.ForceActionEnabled = true;
                    view.ForceAction = true;
                    break;

                default:
                    view.ForceActionEnabled = false;
                    view.ForceAction = false;
                    break;
            }
        }
    }
}