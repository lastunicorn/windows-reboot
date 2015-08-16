using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    class ActionTypeControlViewModel : ViewModelBase
    {
        private readonly Task task;
        private ActionTypeItem[] actionTypes;
        private ActionTypeItem selectedActionType;
        //private bool forceAction;
        private bool forceActionEnabled;
        //private bool displayWarningMessage;

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

        public bool ForceAction
        {
            get { return task.ForceAction; }
            set
            {
                task.ForceAction = value;
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
            get { return task.DisplayWarningMessage; }
            set
            {
                task.DisplayWarningMessage = value;
                OnPropertyChanged("DisplayActionWarning");
            }
        }

        public ActionTypeControlViewModel(Task task)
        {
            if (task == null) throw new ArgumentNullException("task");

            this.task = task;

            ActionTypes = Enum.GetValues(typeof(TaskType))
                .Cast<TaskType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();

            forceActionBackup = true;
        }

        private bool forceActionBackup;

        public void OnActionTypeChanged()
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
