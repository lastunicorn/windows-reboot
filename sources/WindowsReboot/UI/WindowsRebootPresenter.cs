// Windows Reboot
// Copyright (C) 2009 Iuga Alexandru
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
using DustInTheWind.WindowsReboot.Config;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootPresenter
    {
        /// <summary>
        /// The view used to interact with the user.
        /// </summary>
        private readonly IWindowsRebootView view;

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
        private volatile bool actionIsSet = false;

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
        private bool startAtStartUp = false;

        private Configuration config;

        private WindowsRebootConfigSection configSection;

        /// <summary>
        /// A value indicationg if the exit of the application was requested chosing the menu item.
        /// </summary>
        private bool exitRequested = false;

        private bool displayWarningMessage = false;
        private TimeSpan warningMessageTime = TimeSpan.FromSeconds(30);

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootPresenter"/> class with
        /// the view used to interact with the user.
        /// </summary>
        /// <param name="view">The view used to interact with the user.</param>
        public WindowsRebootPresenter(IWindowsRebootView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            this.view = view;

            this.config = this.GetConfiguration();
            this.configSection = WindowsRebootConfigSection.GetOrCreateSection(config);
        }

        #endregion


        #region internal void OnTimerElapsed()

        /// <summary>
        /// Method called by the timer. It updates the interface and, if necesary, it executes the chosen action.
        /// </summary>
        internal void OnTimerElapsed()
        {
            DateTime now = DateTime.Now;

            // Refresh the current time label.
            this.view.LabelCurrentTime = now.ToLongDateString() + "  :  " + now.ToLongTimeString();

            if (this.actionIsSet)
            {
                // Refresh the timer label.

                TimeSpan timeUntilAction = new TimeSpan(this.actionTime.Ticks - now.Ticks);
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

                this.view.LabelTimer = tmp;


                if (this.displayWarningMessage && this.actionTime - this.warningMessageTime <= now)
                {
                    this.displayWarningMessage = false;
                    this.view.DisplayMessage("Warning");
                }

                // Check if it is the time to do the action.
                if (this.actionTime <= now)
                {
                    this.actionIsSet = false;
                    this.DoAction(this.view.ActionType.Value);
                    this.EnableInterface(true);
                    this.view.LabelActionTime = string.Empty;
                    this.view.LabelTimer = TIME_TEMPLATE_EMPTY;
                }
            }
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
                    this.view.DisplayMessage("Ring-ring!");
                    break;

                case ActionType.LockWorkstation:
                    RebootUtil.Lock();
                    break;

                case ActionType.LogOff:
                    RebootUtil.LogOff(this.view.ForceAction);
                    break;

                case ActionType.Sleep:
                    RebootUtil.Sleep(this.view.ForceAction);
                    break;

                case ActionType.Hibernate:
                    RebootUtil.Hibernate(this.view.ForceAction);
                    break;

                case ActionType.Reboot:
                    RebootUtil.Reboot(this.view.ForceAction);
                    break;

                case ActionType.ShutDown:
                    RebootUtil.ShutDown(this.view.ForceAction);
                    break;

                case ActionType.PowerOff:
                    RebootUtil.PowerOff(this.view.ForceAction);
                    break;

                default:
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

            if (this.view.FixedTimeGroupSelected)
            {
                time = this.view.FixedDate.Add(this.view.FixedTime);
            }
            else if (this.view.DelayGroupSelected)
            {
                time = now.Add(new TimeSpan(this.view.Hours, this.view.Minutes, this.view.Seconds));
            }
            else if (this.view.ImmediateGroupSelected)
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

        #region internal void OnStartTimerClicked()

        /// <summary>
        /// Method called when the "Start timer" button is clicked.
        /// </summary>
        internal void OnStartTimerClicked()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime actionTime = this.CalculateActionTime(now);

                if (actionTime < now)
                {
                    string currentTimeString = now.ToLongDateString() + " : " + now.ToLongTimeString();
                    string actionTimeString = actionTime.ToLongDateString() + " : " + actionTime.ToLongTimeString();
                    this.view.DisplayErrorMessage(string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString));
                }
                else
                {
                    this.actionTime = actionTime;
                    this.startTime = now;
                    this.EnableInterface(false);
                    this.view.LabelActionTime = this.actionTime.ToLongDateString() + "  :  " + this.actionTime.ToLongTimeString();

                    if (this.view.DisplayActionWarning && actionTime - now > this.warningMessageTime)
                        this.displayWarningMessage = true;

                    this.actionIsSet = true;
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnStopTimerClicked()

        /// <summary>
        /// Method called when the "Stop timer" button is clicked.
        /// </summary>
        internal void OnStopTimerClicked()
        {
            try
            {
                this.actionIsSet = false;
                this.EnableInterface(true);
                this.view.LabelActionTime = string.Empty;
                this.view.LabelTimer = TIME_TEMPLATE_EMPTY;
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #endregion


        #region Load/Close Form events

        #region internal void OnFormLoad()

        /// <summary>
        /// Method called when the form is loaded.
        /// </summary>
        internal void OnFormLoad()
        {
            try
            {
                string title = Application.ProductName + " " + VersionUtil.GetVersionToString();
                this.view.Title = title;
                this.view.NotifyIconText = title;

                Array values = Enum.GetValues(typeof(ActionType));

                ActionTypeItem[] items = new ActionTypeItem[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    items[i] = new ActionTypeItem((ActionType)values.GetValue(i));
                }

                this.view.ActionTypes = items;

                this.LoadInitialConfiguration();
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }

            if (this.startAtStartUp)
                this.OnStartTimerClicked();
        }

        #endregion

        #region internal bool OnFormClosing()

        /// <summary>
        /// Method called before the form is closed. It has to decide if the form is allowed to be closed or not.
        /// </summary>
        /// <returns>true if the form is allowed to be closed; false otherwise.</returns>
        internal bool OnFormClosing()
        {
            bool allowClosing = false;

            if (!this.exitRequested && this.configSection.CloseToTray.Value)
            {
                // Minimize to tray
                this.view.Hide();
                this.view.NotifyIconVisible = true;
            }
            else
            {
                if (this.actionIsSet)
                {
                    // If timer is started ask if realy want to close.
                    allowClosing = this.view.AskToClose("The timer is started. Are you sure you want to close the application?");
                }
                else
                {
                    // If timer is stopped, close.
                    allowClosing = true;
                }
            }


            if (allowClosing)
                this.view.NotifyIconVisible = false;


            this.exitRequested = false;

            return allowClosing;
        }

        #endregion


        #region internal void OnFormMinimized()

        /// <summary>
        /// Method called when the form is minimized.
        /// </summary>
        internal void OnFormMinimized()
        {
            try
            {
                if (this.configSection.MinimizeToTray.Value)
                {
                    this.view.Hide();
                    this.view.NotifyIconVisible = true;
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

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
                if (this.actionIsSet)
                {
                    this.view.NotifyIconText = this.view.LabelTimer;
                }
                else
                {
                    this.view.NotifyIconText = this.view.Title;
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                this.view.Show();
                this.view.WindowState = FormWindowState.Normal;
                this.view.NotifyIconVisible = false;
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                this.view.Show();
                this.view.WindowState = FormWindowState.Normal;
                this.view.NotifyIconVisible = false;
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to lock the workstation?"))
                {
                    RebootUtil.Lock();
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to log off the current user?\nThe current logged in user is '" + Environment.UserDomainName + "'"))
                {
                    RebootUtil.LogOff(false);
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to put the system in 'Stand By' state?"))
                {
                    RebootUtil.Sleep(false);
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to put the system in 'Hibernate' state?"))
                {
                    RebootUtil.Hibernate(false);
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to reboot the system?"))
                {
                    RebootUtil.Reboot(false);
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to shut down the sysyem?\n\nObs! From WinXP SP1 this command will also power off the system."))
                {
                    RebootUtil.ShutDown(false);
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                if (this.view.Confirm("Do you want to power off the system?\n\nObs! Only if the hardware supports 'Power Off'. Otherwise just a 'Shut Down' will be performed."))
                {
                    RebootUtil.PowerOff(false);
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
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
                this.exitRequested = true;
                this.view.Close();
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #endregion

        #region Menu item events

        #region internal void OnMenuItemGoToTrayClicked()

        /// <summary>
        /// Method called when the "Go To Tray" item from the "File" menu is clicked.
        /// </summary>
        internal void OnMenuItemGoToTrayClicked()
        {
            try
            {
                this.view.Hide();
                this.view.NotifyIconVisible = true;
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnMenuItemExitClicked()

        /// <summary>
        /// Method called when the "Exit" item from the "File" menu is clicked.
        /// </summary>
        internal void OnMenuItemExitClicked()
        {
            try
            {
                this.exitRequested = true;
                this.view.Close();
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnMenuItemOptionsClicked()

        /// <summary>
        /// Method called when the "Options" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemOptionsClicked()
        {
            if (this.view.DisplayOptions(this.configSection))
            {
                // Save the option to the file.
                this.config.Save(ConfigurationSaveMode.Modified);
            }
        }

        #endregion

        #region internal void OnMenuItemSaveCurrentSettingsClicked()

        /// <summary>
        /// Method called when the "Save Current Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemSaveCurrentSettingsClicked()
        {
            try
            {
                this.SaveConfiguration();
                this.view.DisplayMessage("The configuration was saved.");
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnMenuItemLoadDefaultSettingsClicked()

        /// <summary>
        /// Method called when the "Load Default Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemLoadDefaultSettingsClicked()
        {
            try
            {
                if (this.actionIsSet)
                {
                    this.view.DisplayErrorMessage("Cannot complete the task while the timer is started.");
                }
                else
                {
                    this.ClearInterface();
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnMenuItemLoadInitialSettingsClicked()

        /// <summary>
        /// Method called when the "Load Initial Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemLoadInitialSettingsClicked()
        {
            try
            {
                if (this.actionIsSet)
                {
                    this.view.DisplayErrorMessage("Cannot complete the task while the timer is started.");
                }
                else
                {
                    this.LoadInitialConfiguration();
                }
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnMenuItemLicenseClicked()

        /// <summary>
        /// Method called when the "License" item from the "Help" menu is clicked.
        /// </summary>
        internal void OnMenuItemLicenseClicked()
        {
            try
            {
                this.view.DisplayLicense();
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region internal void OnMenuItemAboutClicked()

        /// <summary>
        /// Method called when the "About" item from the "Help" menu is clicked.
        /// </summary>
        internal void OnMenuItemAboutClicked()
        {
            try
            {
                this.view.DisplayAbout();
            }
            catch (Exception ex)
            {
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #endregion


        #region private void EnableInterface(bool value)

        /// <summary>
        /// Enables or disables the interface.
        /// </summary>
        /// <param name="value">A value that specifies if the interface should be enabled or disabled.</param>
        private void EnableInterface(bool value)
        {
            this.view.ActionTimeGroupEnabled = value;
            this.view.ActionTypeGroupEnabled = value;
            this.view.MenuItem_LoadInitialSettingsEnabled = value;
            this.view.MenuItem_LoadDefaultSettingsEnabled = value;
        }

        #endregion

        #region private void ClearInterface()

        /// <summary>
        /// Clears the interface and displayed the default values.
        /// </summary>
        private void ClearInterface()
        {
            DateTime now = DateTime.Now;

            this.view.FixedDate = now.Date;
            this.view.FixedTime = now.TimeOfDay;

            this.view.Hours = 0;
            this.view.Minutes = 0;
            this.view.Seconds = 0;

            this.view.DelayGroupSelected = true;

            this.view.ActionType = new ActionTypeItem(ActionType.PowerOff);

            this.view.ForceAction = true;
        }

        #endregion

        #region private void LoadInitialConfiguration()

        /// <summary>
        /// Loads the values from the configuration file and populates the interface with them.
        /// </summary>
        private void LoadInitialConfiguration()
        {
            WindowsRebootConfigSection configSection = this.GetConfigurationSection();

            this.ClearInterface();

            switch (configSection.ActionTime.Type)
            {
                case ActionTimeType.FixedDate:
                    this.view.FixedDate = this.configSection.ActionTime.DateTime.Date;
                    this.view.FixedTime = this.configSection.ActionTime.DateTime.TimeOfDay;
                    this.view.FixedTimeGroupSelected = true;
                    break;

                case ActionTimeType.Delay:
                    this.view.Hours = this.configSection.ActionTime.Hours;
                    this.view.Minutes = this.configSection.ActionTime.Minutes;
                    this.view.Seconds = this.configSection.ActionTime.Seconds;
                    this.view.DelayGroupSelected = true;
                    break;

                case ActionTimeType.Immediate:
                    this.view.ImmediateGroupSelected = true;
                    break;

                default:
                    break;
            }

            this.view.ActionType = new ActionTypeItem(this.configSection.ActionType.Value);

            this.view.ForceAction = this.configSection.ForceClosingPrograms.Value;

            this.startAtStartUp = this.configSection.StartTimerAtApplicationStart.Value;
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

            if (this.view.FixedTimeGroupSelected)
            {
                this.configSection.ActionTime.Type = ActionTimeType.FixedDate;
                this.configSection.ActionTime.DateTime = this.view.FixedDate.Date.Add(this.view.FixedTime);
                this.configSection.ActionTime.Hours = 0;
                this.configSection.ActionTime.Minutes = 0;
                this.configSection.ActionTime.Seconds = 0;
            }
            else if (this.view.DelayGroupSelected)
            {
                this.configSection.ActionTime.Type = ActionTimeType.Delay;
                this.configSection.ActionTime.Hours = this.view.Hours;
                this.configSection.ActionTime.Minutes = this.view.Minutes;
                this.configSection.ActionTime.Seconds = this.view.Seconds;
                this.configSection.ActionTime.DateTime = DateTime.Now;
            }
            else if (this.view.ImmediateGroupSelected)
            {
                this.configSection.ActionTime.Type = ActionTimeType.Immediate;
                this.configSection.ActionTime.DateTime = DateTime.Now;
                this.configSection.ActionTime.Hours = 0;
                this.configSection.ActionTime.Minutes = 0;
                this.configSection.ActionTime.Seconds = 0;
            }

            this.configSection.ActionType.Value = this.view.ActionType.Value;

            this.configSection.ForceClosingPrograms.Value = this.view.ForceAction;

            //config.Save();
            this.config.Save(ConfigurationSaveMode.Modified);
        }

        #endregion

        #region Configuration

        private Configuration GetConfiguration()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        private WindowsRebootConfigSection GetConfigurationSection()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return WindowsRebootConfigSection.GetOrCreateSection(config);
        }

        #endregion

        internal void OnActionTypeChanged()
        {
            switch (this.view.ActionType.Value)
            {
                case ActionType.Ring:
                    this.view.ForceActionVisible = false;
                    break;

                case ActionType.LockWorkstation:
                    this.view.ForceActionVisible = false;
                    break;

                case ActionType.LogOff:
                    this.view.ForceActionVisible = true;
                    break;

                case ActionType.Sleep:
                    this.view.ForceActionVisible = true;
                    break;

                case ActionType.Hibernate:
                    this.view.ForceActionVisible = true;
                    break;

                case ActionType.Reboot:
                    this.view.ForceActionVisible = true;
                    break;

                case ActionType.ShutDown:
                    this.view.ForceActionVisible = true;
                    break;

                case ActionType.PowerOff:
                    this.view.ForceActionVisible = true;
                    break;

                default:
                    this.view.ForceActionVisible = false;
                    break;
            }
        }
    }
}
