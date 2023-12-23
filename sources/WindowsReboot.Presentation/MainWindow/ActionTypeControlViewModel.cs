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
using System.Threading.Tasks;
using System.Threading;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTypeControlViewModel : ViewModelBase
    {
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private ActionTypeItem[] actionTypes;
        private ActionTypeItem selectedActionType;
        private bool forceActionEnabled;
        private bool forceActionBackup;
        private bool forceAction;
        private bool displayWarningMessage;
        private bool enabled;

        /// <summary>
        /// Sets the available values that can be chose for the action type.
        /// </summary>
        public ActionTypeItem[] ActionTypes
        {
            get => actionTypes;
            set
            {
                actionTypes = value;
                OnPropertyChanged();
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
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionPlan.ActionType = value.Value;
            }
        }

        public bool ForceAction
        {
            get => forceAction;
            set
            {
                forceAction = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionPlan.ApplyForce = value;
            }
        }

        public bool ForceActionEnabled
        {
            get => forceActionEnabled;
            set
            {
                forceActionEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool DisplayActionWarning
        {
            get => displayWarningMessage;
            set
            {
                displayWarningMessage = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    executionTimer.WarningTime = value ? executionTimer.DefaultWarningTime : null;
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        public ActionTypeControlViewModel(ExecutionTimer executionTimer, ExecutionPlan executionPlan, EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));

            ActionTypes = Enum.GetValues(typeof(ActionType))
                .Cast<ActionType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();

            forceActionBackup = true;
            forceAction = executionPlan.ApplyForce;
            displayWarningMessage = executionTimer.WarningTime != null;
            enabled = true;

            UpdateFromBusiness();

            eventBus.Subscribe<WarningTimeChangedEvent>(HandleTimerWarningTimeChangedEvent);
            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);

            executionPlan.ForceChanged += HandleActionForceChanged;
            executionPlan.TypeChanged += HandleActionTypeChanged;
        }

        private Task HandleTimerWarningTimeChangedEvent(WarningTimeChangedEvent ev, CancellationToken cancellationToken)
        {
            RunInInitializeMode(() =>
            {
                DisplayActionWarning = ev.Time != null;
            });
            return Task.CompletedTask;
        }

        private Task HandleTimerStartedEvent(TimerStartedEvent ev, CancellationToken cancellationToken)
        {
            Dispatch(() =>
            {
                Enabled = false;
            });
            return Task.CompletedTask;
        }

        private Task HandleTimerStoppedEvent(TimerStoppedEvent ev, CancellationToken cancellationToken)
        {
            Dispatch(() =>
            {
                Enabled = true;
            });
            return Task.CompletedTask;
        }

        private void HandleActionTypeChanged(object sender, EventArgs eventArgs)
        {
            UpdateFromBusiness();
        }

        private void UpdateFromBusiness()
        {
            RunInInitializeMode(() =>
            {
                SelectedActionType = ActionTypes
                    .First(x => x.Value == executionPlan.ActionType);

                UpdateForceAction();
            });
        }

        private void HandleActionForceChanged(object sender, EventArgs eventArgs)
        {
            RunInInitializeMode(() =>
            {
                ForceAction = executionPlan.ApplyForce;
            });
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