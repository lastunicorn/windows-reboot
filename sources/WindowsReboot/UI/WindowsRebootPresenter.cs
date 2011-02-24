// Windows Reboot
// Copyright (C) 2009 Dust in the Wind
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
using MVCSharp.Core;
using DustInTheWind.WindowsReboot.UI;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootPresenter : ControllerBase<MainTask, IWindowsRebootView>
    {
        //public override MainTask Task
        //{
        //    get            {                return base.Task;            }
        //    set
        //    {
        //        base.Task = value;
        //        if (value != null)
        //        {
        //            value.Model = 
        //        }
        //    }
        //}

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

        //private Configuration config;

        //private WindowsRebootConfigSection configSection;

        /// <summary>
        /// A value indicating if the exit of the application was requested chosing the menu item.
        /// </summary>
        private bool exitRequested = false;

        private bool displayWarningMessage = false;
        private TimeSpan warningMessageTime = TimeSpan.FromSeconds(30);

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootPresenter"/> class.
        /// </summary>
        public WindowsRebootPresenter()
        {
            //config = GetConfiguration();
            //configSection = WindowsRebootConfigSection.GetOrCreateSection(config);
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
            View.LabelCurrentTime = now.ToLongDateString() + "  :  " + now.ToLongTimeString();

            if (actionIsSet)
            {
                // Refresh the timer label.

                TimeSpan timeUntilAction = new TimeSpan(actionTime.Ticks - now.Ticks);
                View.LabelTimer = ToTimerString(timeUntilAction);


                if (displayWarningMessage && this.actionTime - this.warningMessageTime <= now)
                {
                    displayWarningMessage = false;
                    View.DisplayMessage("Warning");
                }

                // Check if it is the time to do the action.
                if (actionTime <= now)
                {
                    actionIsSet = false;
                    DoAction(View.ActionType.Value);
                    EnableInterface(true);
                    View.LabelActionTime = string.Empty;
                    View.LabelTimer = TIME_TEMPLATE_EMPTY;
                }
            }
        }

        private string ToTimerString(TimeSpan time)
        {
            string tmp;

            int d = time.Days;
            int h = time.Hours;
            int m = time.Minutes;
            int s = time.Seconds;
            int f = Convert.ToInt32(Math.Round((double)(time.Milliseconds / 100)));

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
                    View.DisplayMessage(string.Format("Ring-ring! It is {0}", DateTime.Now.ToShortTimeString()));
                    break;

                case ActionType.LockWorkstation:
                    RebootUtil.Lock();
                    break;

                case ActionType.LogOff:
                    RebootUtil.LogOff(View.ForceAction);
                    break;

                case ActionType.Sleep:
                    RebootUtil.Sleep(View.ForceAction);
                    break;

                case ActionType.Hibernate:
                    RebootUtil.Hibernate(View.ForceAction);
                    break;

                case ActionType.Reboot:
                    RebootUtil.Reboot(View.ForceAction);
                    break;

                case ActionType.ShutDown:
                    RebootUtil.ShutDown(View.ForceAction);
                    break;

                case ActionType.PowerOff:
                    RebootUtil.PowerOff(View.ForceAction);
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

            if (View.FixedTimeGroupSelected)
            {
                time = View.FixedDate.Add(View.FixedTime);
            }
            else if (View.DelayGroupSelected)
            {
                time = now.Add(new TimeSpan(View.Hours, View.Minutes, View.Seconds));
            }
            else if (View.ImmediateGroupSelected)
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
        internal void StartTimerClicked()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime actionTime = CalculateActionTime(now);

                if (actionTime < now)
                {
                    string currentTimeString = now.ToLongDateString() + " : " + now.ToLongTimeString();
                    string actionTimeString = actionTime.ToLongDateString() + " : " + actionTime.ToLongTimeString();
                    View.DisplayErrorMessage(string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString));
                }
                else
                {
                    this.actionTime = actionTime;
                    startTime = now;
                    EnableInterface(false);
                    View.LabelActionTime = this.actionTime.ToLongDateString() + "  :  " + actionTime.ToLongTimeString();

                    if (View.DisplayActionWarning && actionTime - now > warningMessageTime)
                        displayWarningMessage = true;

                    actionIsSet = true;
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Stop timer" button is clicked.
        /// </summary>
        internal void StopTimerClicked()
        {
            try
            {
                actionIsSet = false;
                EnableInterface(true);
                View.LabelActionTime = string.Empty;
                View.LabelTimer = TIME_TEMPLATE_EMPTY;
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        #endregion


        #region Load/Close Form events

        /// <summary>
        /// Method called when the form is loaded.
        /// </summary>
        internal void ViewLoaded()
        {
            try
            {
                string title = Application.ProductName + " " + VersionUtil.GetVersionToString();
                View.Title = title;
                View.NotifyIconText = title;

                Array values = Enum.GetValues(typeof(ActionType));

                ActionTypeItem[] items = new ActionTypeItem[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    items[i] = new ActionTypeItem((ActionType)values.GetValue(i));
                }

                View.ActionTypes = items;

                this.LoadInitialConfiguration();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }

            if (this.startAtStartUp)
                this.StartTimerClicked();
        }

        /// <summary>
        /// Method called before the form is closed. It has to decide if the form is allowed to be closed or not.
        /// </summary>
        /// <returns>true if the form is allowed to be closed; false otherwise.</returns>
        internal bool ViewClosing()
        {
            bool allowClosing = false;

            if (!exitRequested && Task.Settings.CloseToTray)
            {
                // Minimize to tray
                View.Hide();
                View.NotifyIconVisible = true;
            }
            else
            {
                if (this.actionIsSet)
                {
                    // If timer is started ask if realy want to close.
                    allowClosing = View.AskToClose("The timer is started. Are you sure you want to close the application?");
                }
                else
                {
                    // If timer is stopped, close.
                    allowClosing = true;
                }
            }


            if (allowClosing)
                View.NotifyIconVisible = false;


            this.exitRequested = false;

            return allowClosing;
        }

        /// <summary>
        /// Method called when the form is minimized.
        /// </summary>
        internal void ViewMinimized()
        {
            try
            {
                if (Task.Settings.MinimizeToTray)
                {
                    View.Hide();
                    View.NotifyIconVisible = true;
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
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
                if (actionIsSet)
                {
                    View.NotifyIconText = View.LabelTimer;
                }
                else
                {
                    View.NotifyIconText = View.Title;
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the notify icon is clicked with the left mouse button.
        /// </summary>
        internal void OnNotifyIconMouseClicked()
        {
            try
            {
                View.Show();
                View.WindowState = FormWindowState.Normal;
                View.NotifyIconVisible = false;
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Show" item of the notify icon menu was clicked.
        /// </summary>
        internal void OnNotifyIconShowClicked()
        {
            try
            {
                View.Show();
                View.WindowState = FormWindowState.Normal;
                View.NotifyIconVisible = false;
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the tray icon is clicked by the user.
        /// </summary>
        internal void OnNotifyIconLockComputerClicked()
        {
            try
            {
                if (View.Confirm("Do you want to lock the workstation?"))
                {
                    RebootUtil.Lock();
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }


        /// <summary>
        /// Method clicked when the user choose "Log Off" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconLogOffClicked()
        {
            try
            {
                if (View.Confirm("Do you want to log off the current user?\nThe current logged in user is '" + Environment.UserDomainName + "'"))
                {
                    RebootUtil.LogOff(false);
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Sleep" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconSleepClicked()
        {
            try
            {
                if (View.Confirm("Do you want to put the system in 'Stand By' state?"))
                {
                    RebootUtil.Sleep(false);
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Hibernate" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconHibernateClicked()
        {
            try
            {
                if (View.Confirm("Do you want to put the system in 'Hibernate' state?"))
                {
                    RebootUtil.Hibernate(false);
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Reboot" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconRebootClicked()
        {
            try
            {
                if (View.Confirm("Do you want to reboot the system?"))
                {
                    RebootUtil.Reboot(false);
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Shut Down" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconShutDownClicked()
        {
            try
            {
                if (View.Confirm("Do you want to shut down the sysyem?\n\nObs! From WinXP SP1 this command will also power off the system."))
                {
                    RebootUtil.ShutDown(false);
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method clicked when the user choose "Power Off" from the tray icon menu.
        /// </summary>
        internal void OnNotifyIconPowerOffClicked()
        {
            try
            {
                if (View.Confirm("Do you want to power off the system?\n\nObs! Only if the hardware supports 'Power Off'. Otherwise just a 'Shut Down' will be performed."))
                {
                    RebootUtil.PowerOff(false);
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }


        /// <summary>
        /// Method called when the "Exit" item of the notify icon menu was clicked.
        /// </summary>
        internal void OnNotifyIconExitClicked()
        {
            try
            {
                this.exitRequested = true;
                View.Close();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

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
                View.Hide();
                View.NotifyIconVisible = true;
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        #endregion

        /// <summary>
        /// Method called when the "Exit" item from the "File" menu is clicked.
        /// </summary>
        internal void MenuItemExitClicked()
        {
            try
            {
                this.exitRequested = true;
                View.Close();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }

        /// <summary>
        /// Method called when the "Options" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void MenuItemOptionsClicked()
        {
            Task.TasksManager.StartTask(typeof(OptionsTask), new object[] { Task.Settings });

            //Task.Navigator.Navigate(MainTask.Options);

            //if (View.DisplayOptions(this.configSection))
            //{
            //    // Save the option to the file.
            //    this.config.Save(ConfigurationSaveMode.Modified);
            //}
        }

        #region internal void OnMenuItemSaveCurrentSettingsClicked()

        /// <summary>
        /// Method called when the "Save Current Settings" item from the "Configuration" menu is clicked.
        /// </summary>
        internal void OnMenuItemSaveCurrentSettingsClicked()
        {
            try
            {
                this.SaveConfiguration();
                View.DisplayMessage("The configuration was saved.");
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
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
                    View.DisplayErrorMessage("Cannot complete the task while the timer is started.");
                }
                else
                {
                    this.ClearInterface();
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
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
                    View.DisplayErrorMessage("Cannot complete the task while the timer is started.");
                }
                else
                {
                    this.LoadInitialConfiguration();
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
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
                View.DisplayLicense();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
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
                View.DisplayAbout();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
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
            View.ActionTimeGroupEnabled = value;
            View.ActionTypeGroupEnabled = value;
            View.MenuItem_LoadInitialSettingsEnabled = value;
            View.MenuItem_LoadDefaultSettingsEnabled = value;
        }

        #endregion

        #region private void ClearInterface()

        /// <summary>
        /// Clears the interface and displayed the default values.
        /// </summary>
        private void ClearInterface()
        {
            DateTime now = DateTime.Now;

            View.FixedDate = now.Date;
            View.FixedTime = now.TimeOfDay;

            View.Hours = 0;
            View.Minutes = 0;
            View.Seconds = 0;

            View.DelayGroupSelected = true;

            View.ActionType = new ActionTypeItem(ActionType.PowerOff);

            View.ForceAction = true;
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
                    View.FixedDate = Task.DuckSettings.ActionTime_DateTime.Date;
                    View.FixedTime = Task.DuckSettings.ActionTime_DateTime.TimeOfDay;
                    View.FixedTimeGroupSelected = true;
                    break;

                case ActionTimeType.Delay:
                    View.Hours = Task.DuckSettings.ActionTime_Hours;
                    View.Minutes = Task.DuckSettings.ActionTime_Minutes;
                    View.Seconds = Task.DuckSettings.ActionTime_Seconds;
                    View.DelayGroupSelected = true;
                    break;

                case ActionTimeType.Immediate:
                    View.ImmediateGroupSelected = true;
                    break;

                default:
                    break;
            }

            View.ActionType = new ActionTypeItem(Task.DuckSettings.ActionType);

            View.ForceAction = Task.DuckSettings.ForceClosingPrograms;

            this.startAtStartUp = Task.Settings.TimerInitiallyStarted;
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

            if (View.FixedTimeGroupSelected)
            {
                Task.DuckSettings.ActionTime_Type = ActionTimeType.FixedDate;
                Task.DuckSettings.ActionTime_DateTime = View.FixedDate.Date.Add(View.FixedTime);
                Task.DuckSettings.ActionTime_Hours = 0;
                Task.DuckSettings.ActionTime_Minutes = 0;
                Task.DuckSettings.ActionTime_Seconds = 0;
            }
            else if (View.DelayGroupSelected)
            {
                Task.DuckSettings.ActionTime_Type = ActionTimeType.Delay;
                Task.DuckSettings.ActionTime_Hours = View.Hours;
                Task.DuckSettings.ActionTime_Minutes = View.Minutes;
                Task.DuckSettings.ActionTime_Seconds = View.Seconds;
                Task.DuckSettings.ActionTime_DateTime = DateTime.Now;
            }
            else if (View.ImmediateGroupSelected)
            {
                Task.DuckSettings.ActionTime_Type = ActionTimeType.Immediate;
                Task.DuckSettings.ActionTime_DateTime = DateTime.Now;
                Task.DuckSettings.ActionTime_Hours = 0;
                Task.DuckSettings.ActionTime_Minutes = 0;
                Task.DuckSettings.ActionTime_Seconds = 0;
            }

            Task.DuckSettings.ActionType = View.ActionType.Value;

            Task.DuckSettings.ForceClosingPrograms = View.ForceAction;

            //config.Save();
            Task.DuckSettings.Save();
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
            switch (View.ActionType.Value)
            {
                case ActionType.Ring:
                    View.ForceActionVisible = false;
                    break;

                case ActionType.LockWorkstation:
                    View.ForceActionVisible = false;
                    break;

                case ActionType.LogOff:
                    View.ForceActionVisible = true;
                    break;

                case ActionType.Sleep:
                    View.ForceActionVisible = true;
                    break;

                case ActionType.Hibernate:
                    View.ForceActionVisible = true;
                    break;

                case ActionType.Reboot:
                    View.ForceActionVisible = true;
                    break;

                case ActionType.ShutDown:
                    View.ForceActionVisible = true;
                    break;

                case ActionType.PowerOff:
                    View.ForceActionVisible = true;
                    break;

                default:
                    View.ForceActionVisible = false;
                    break;
            }
        }
    }
}
