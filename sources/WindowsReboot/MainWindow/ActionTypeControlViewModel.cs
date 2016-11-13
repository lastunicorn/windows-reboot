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
using System.Linq;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.UiCommon;
using Action = DustInTheWind.WindowsReboot.Core.Action;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal class ActionTypeControlViewModel : ViewModelBase
    {
        private readonly Timer timer;
        private readonly Action action;
        private readonly IUserInterface userInterface;
        private ActionTypeItem[] actionTypes;
        private ActionTypeItem selectedActionType;
        private bool forceActionEnabled;
        private bool forceActionBackup;
        private bool forceAction;
        private bool displayWarningMessage;
        private bool enabled;

        private bool updateFromBusiness;

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
                if (selectedActionType == value)
                    return;

                selectedActionType = value;
                OnPropertyChanged("SelectedActionType");

                if (!updateFromBusiness)
                    action.Type = value.Value;
            }
        }

        public bool ForceAction
        {
            get { return forceAction; }
            set
            {
                forceAction = value;
                OnPropertyChanged("ForceAction");

                if (!updateFromBusiness)
                    action.Force = value;
            }
        }

        public bool ForceActionEnabled
        {
            get { return forceActionEnabled; }
            set
            {
                forceActionEnabled = value;
                OnPropertyChanged("ForceActionEnabled");
            }
        }

        public bool DisplayActionWarning
        {
            get { return displayWarningMessage; }
            set
            {
                displayWarningMessage = value;
                OnPropertyChanged("DisplayActionWarning");

                if (!updateFromBusiness)
                    timer.WarningTime = value ? timer.DefaultWarningTime : null;
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public ActionTypeControlViewModel(Timer timer, Action action, IUserInterface userInterface)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (action == null) throw new ArgumentNullException("action");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.timer = timer;
            this.action = action;
            this.userInterface = userInterface;

            ActionTypes = Enum.GetValues(typeof(ActionType))
                .Cast<ActionType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();

            forceActionBackup = true;
            forceAction = action.Force;
            displayWarningMessage = timer.WarningTime != null;

            UpdateFromBusiness();

            timer.WarningTimeChanged += HandleTimerWarningTimeChanged;
            timer.Started += HandleTimerStarted;
            timer.Stoped += HandleTimerStoped;

            action.ForceChanged += HandleActionForceChanged;
            action.TypeChanged += HandleActionTypeChanged;
        }

        private void HandleTimerStarted(object sender, EventArgs e)
        {
            Enabled = false;
        }

        private void HandleTimerStoped(object sender, EventArgs e)
        {
            userInterface.Dispatch(() => { Enabled = true; });
        }

        private void HandleActionTypeChanged(object sender, EventArgs eventArgs)
        {
            UpdateFromBusiness();
        }

        private void UpdateFromBusiness()
        {
            updateFromBusiness = true;
            try
            {
                SelectedActionType = ActionTypes
                    .First(x => x.Value == action.Type);

                UpdateForceAction();
            }
            finally
            {
                updateFromBusiness = false;
            }
        }

        private void HandleActionForceChanged(object sender, EventArgs eventArgs)
        {
            updateFromBusiness = true;
            try
            {
                ForceAction = action.Force;
            }
            finally
            {
                updateFromBusiness = false;
            }
        }

        private void HandleTimerWarningTimeChanged(object sender, EventArgs e)
        {
            updateFromBusiness = true;
            try
            {
                DisplayActionWarning = timer.WarningTime != null;
            }
            finally
            {
                updateFromBusiness = false;
            }
        }

        private void UpdateForceAction()
        {
            if (SelectedActionType == null)
            {
                ForceActionEnabled = false;
                ForceAction = false;
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
                    EnableForceAction();
                    break;

                default:
                    DisableForceAction();
                    break;
            }
        }

        private void DisableForceAction()
        {
            if (ForceActionEnabled)
                forceActionBackup = ForceAction;

            ForceActionEnabled = false;
            ForceAction = false;
        }

        private void EnableForceAction()
        {
            if (!ForceActionEnabled)
                ForceAction = forceActionBackup;

            ForceActionEnabled = true;
        }
    }
}