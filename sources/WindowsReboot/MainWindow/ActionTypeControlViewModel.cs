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
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    class ActionTypeControlViewModel : ViewModelBase
    {
        private readonly Timer timer;
        private ActionTypeItem[] actionTypes;
        private ActionTypeItem selectedActionType;
        private bool forceActionEnabled;
        private bool forceActionBackup;
        private bool forceAction;
        private bool displayWarningMessage;

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

                UpdateForceAction();
            }
        }

        public bool ForceAction
        {
            get { return forceAction; }
            set
            {
                forceAction = value;
                OnPropertyChanged("ForceAction");
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
            }
        }

        public ActionTypeControlViewModel(Timer timer)
        {
            if (timer == null) throw new ArgumentNullException("timer");

            this.timer = timer;

            ActionTypes = Enum.GetValues(typeof(TaskType))
                .Cast<TaskType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();

            forceActionBackup = true;

            forceAction = true;
            displayWarningMessage = true;
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
                case TaskType.LogOff:
                case TaskType.Sleep:
                case TaskType.Hibernate:
                case TaskType.Reboot:
                case TaskType.ShutDown:
                case TaskType.PowerOff:
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

        public void Clear()
        {
            SelectedActionType = new ActionTypeItem(TaskType.PowerOff);
            ForceAction = true;
        }
    }
}
