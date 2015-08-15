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
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;

namespace DustInTheWind.WindowsReboot.Presentation
{
    internal class WindowsRebootPresenter : ViewModelBase
    {
        /// <summary>
        /// The view used to interact with the user.
        /// </summary>
        private readonly IWindowsRebootView view;

        private readonly UserInterface userInterface;

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

        private readonly IRebootUtil rebootUtil;
        private readonly Performer performer;
        private ActionTypeItem selectedActionType;
        private string title;
        private ActionTypeItem[] actionTypes;

        public FixedDateControlViewModel FixedDateControlViewModel { get; private set; }
        public StatusControlViewModel StatusControlViewModel { get; private set; }
        public DelayTimeControlViewModel DelayTimeControlViewModel { get; private set; }

        /// <summary>
        /// Sets the available values that can be chosed for the action type.
        /// </summary>
        public ActionTypeItem[] ActionTypes
        {
            get { return actionTypes; }
            set
            {
                actionTypes = value;
                OnPropertyChanged("ActionTypes");
            }
        }

        /// <summary>
        /// Gets or sets the action type.
        /// </summary>
        public ActionTypeItem SelectedActionType
        {
            get { return selectedActionType; }
            set
            {
                selectedActionType = value;
                OnPropertyChanged("SelectedActionType");
            }
        }

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

            ITicker ticker = new Ticker100();
            performer = new Performer(userInterface, uiDispatcher, ticker);

            FixedDateControlViewModel = new FixedDateControlViewModel();
            StatusControlViewModel = new StatusControlViewModel(ticker, performer, uiDispatcher);
            DelayTimeControlViewModel = new DelayTimeControlViewModel();

            rebootUtil = new RebootUtil();

            config = GetConfiguration();
            configSection = WindowsRebootConfigSection.GetOrCreateSection(config);

            performer.Started += HandlePerformerStarted;
            performer.Stoped += HandlePerformerStoped;

            ActionTypes = Enum.GetValues(typeof(ActionType))
                .Cast<ActionType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();
        }

        private void HandlePerformerStarted(object sender, EventArgs eventArgs)
        {
            EnableInterface(false);
        }

        private void HandlePerformerStoped(object sender, EventArgs eventArgs)
        {
            EnableInterface(true);
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
            DateTime actionTime;

            if (view.FixedTimeGroupSelected)
            {
                actionTime = FixedDateControlViewModel.GetFullTime();
            }
            else if (view.DelayGroupSelected)
            {
                TimeSpan time = DelayTimeControlViewModel.GetTime();
                actionTime = now.Add(time);
            }
            else if (view.ImmediateGroupSelected)
            {
                actionTime = now;
            }
            else
            {
                throw new Exception("No action time was chosen.");
            }

            return actionTime;
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
                if (SelectedActionType == null)
                    throw new WindowsRebootException("Select an action to be performed.");

                DateTime now = DateTime.Now;
                DateTime actionTime = CalculateActionTime(now);

                performer.DisplayWarningMessage = view.DisplayActionWarning;
                performer.ForceAction = view.ForceAction;
                performer.Start(actionTime, SelectedActionType.Value);
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
                performer.Stop();
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
                string title = string.Format("{0} {1}", Application.ProductName, VersionUtil.GetVersionToString());

                Title = title;
                view.NotifyIconText = title;

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
                allowToCLose = !performer.IsRunning || userInterface.AskToClose("The timer is started. Are you sure you want to close the application?");
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
                view.NotifyIconText = performer.IsRunning
                    ? TimerFormatter.Format(performer.TimeUntilAction)
                    : Title;
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
                if (performer.IsRunning)
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
                if (performer.IsRunning)
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
            FixedDateControlViewModel.Clear();
            DelayTimeControlViewModel.Clear();

            view.DelayGroupSelected = true;

            SelectedActionType = new ActionTypeItem(ActionType.PowerOff);

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
                    FixedDateControlViewModel.Date = this.configSection.ActionTime.DateTime.Date;
                    FixedDateControlViewModel.Time = this.configSection.ActionTime.DateTime;
                    view.FixedTimeGroupSelected = true;
                    break;

                case ActionTimeType.Delay:
                    DelayTimeControlViewModel.Hours = this.configSection.ActionTime.Hours;
                    DelayTimeControlViewModel.Minutes = this.configSection.ActionTime.Minutes;
                    DelayTimeControlViewModel.Seconds = this.configSection.ActionTime.Seconds;
                    view.DelayGroupSelected = true;
                    break;

                case ActionTimeType.Immediate:
                    view.ImmediateGroupSelected = true;
                    break;
            }

            SelectedActionType = new ActionTypeItem(this.configSection.ActionType.Value);

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
                configSection.ActionTime.DateTime = FixedDateControlViewModel.GetFullTime();
                configSection.ActionTime.Hours = 0;
                configSection.ActionTime.Minutes = 0;
                configSection.ActionTime.Seconds = 0;
            }
            else if (view.DelayGroupSelected)
            {
                configSection.ActionTime.Type = ActionTimeType.Delay;
                configSection.ActionTime.Hours = DelayTimeControlViewModel.Hours;
                configSection.ActionTime.Minutes = DelayTimeControlViewModel.Minutes;
                configSection.ActionTime.Seconds = DelayTimeControlViewModel.Seconds;
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

            configSection.ActionType.Value = SelectedActionType == null ? ActionType.Ring : SelectedActionType.Value;

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
            if (SelectedActionType == null)
            {
                view.ForceActionEnabled = false;
                view.ForceAction = false;
                return;
            }

            switch (SelectedActionType.Value)
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