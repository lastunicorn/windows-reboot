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
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTypeControlViewModel : ViewModelBase
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private readonly IUiDispatcher uiDispatcher;
        private ActionTypeItem[] actionTypes;
        private ActionTypeItem selectedActionType;
        private bool forceActionEnabled;
        private bool forceActionBackup;
        private bool forceAction;
        private bool displayWarningMessage;
        private bool enabled;

        private bool updateFromBusiness;

        /// <summary>
        /// Sets the available values that can be chose for the action type.
        /// </summary>
        public ActionTypeItem[] ActionTypes
        {
            get => actionTypes;
            set
            {
                actionTypes = value;
                OnPropertyChanged(nameof(ActionTypes));
            }
        }

        /// <summary>
        /// Gets or sets the action type.
        /// </summary>
        public ActionTypeItem SelectedActionType
        {
            get => selectedActionType;
            set
            {
                if (selectedActionType == value)
                    return;

                selectedActionType = value;
                OnPropertyChanged(nameof(SelectedActionType));

                if (!updateFromBusiness)
                    executionPlan.ActionType = value.Value;
            }
        }

        public bool ForceAction
        {
            get => forceAction;
            set
            {
                forceAction = value;
                OnPropertyChanged(nameof(ForceAction));

                if (!updateFromBusiness)
                    executionPlan.ApplyForce = value;
            }
        }

        public bool ForceActionEnabled
        {
            get => forceActionEnabled;
            set
            {
                forceActionEnabled = value;
                OnPropertyChanged(nameof(ForceActionEnabled));
            }
        }

        public bool DisplayActionWarning
        {
            get => displayWarningMessage;
            set
            {
                displayWarningMessage = value;
                OnPropertyChanged(nameof(DisplayActionWarning));

                if (!updateFromBusiness)
                    executionTimer.WarningTime = value ? executionTimer.DefaultWarningTime : null;
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }

        public ActionTypeControlViewModel(ExecutionTimer executionTimer, ExecutionPlan executionPlan, IUiDispatcher uiDispatcher)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.uiDispatcher = uiDispatcher ?? throw new ArgumentNullException(nameof(uiDispatcher));

            ActionTypes = Enum.GetValues(typeof(ActionType))
                .Cast<ActionType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();

            forceActionBackup = true;
            forceAction = executionPlan.ApplyForce;
            displayWarningMessage = executionTimer.WarningTime != null;

            UpdateFromBusiness();

            executionTimer.WarningTimeChanged += HandleTimerWarningTimeChanged;
            executionTimer.Started += HandleTimerStarted;
            executionTimer.Stopped += HandleTimerStopped;

            executionPlan.ForceChanged += HandleActionForceChanged;
            executionPlan.TypeChanged += HandleActionTypeChanged;
        }

        private void HandleTimerStarted(object sender, EventArgs e)
        {
            Enabled = false;
        }

        private void HandleTimerStopped(object sender, EventArgs e)
        {
            uiDispatcher.Dispatch(() =>
            {
                Enabled = true;
            });
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
                    .First(x => x.Value == executionPlan.ActionType);

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
                ForceAction = executionPlan.ApplyForce;
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
                DisplayActionWarning = executionTimer.WarningTime != null;
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