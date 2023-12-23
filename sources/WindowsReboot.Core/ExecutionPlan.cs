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
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;

namespace DustInTheWind.WindowsReboot.Core
{
    public class ExecutionPlan
    {
        private readonly IOperatingSystem operatingSystem;
        private readonly EventBus eventBus;
        private ActionType actionType;
        private ForceOption forceOption;
        private ForceOption lastApplicableForceOption;

        public event EventHandler NotificationRaised;

        public ActionType ActionType
        {
            get => actionType;
            set
            {
                if (!Enum.IsDefined(typeof(ActionType), value))
                    throw new ArgumentException("Invalid action type value");

                actionType = value;
                OnActionTypeChanged();

                AdjustForceOption();
            }
        }

        public ForceOption ForceOption
        {
            get => forceOption;
            set
            {
                bool isAllowedToSet = IsAllowedToSet(value);
                if (!isAllowedToSet)
                    throw new ArgumentException($"ForceOption value '{value}' is not allowed for action type '{actionType}'.");

                if (value != ForceOption.NotApplicable)
                    lastApplicableForceOption = value;

                forceOption = value;
                OnForceOptionChanged();
            }
        }

        public ExecutionPlan(IOperatingSystem operatingSystem, EventBus eventBus)
        {
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            ActionType = ActionType.Ring;
            ForceOption = ForceOption.NotApplicable;
            lastApplicableForceOption = ForceOption.Yes;
        }

        public void Execute()
        {
            switch (ActionType)
            {
                case ActionType.Ring:
                    OnNotificationRaised();
                    break;

                case ActionType.LockWorkstation:
                    operatingSystem.Lock();
                    break;

                case ActionType.LogOff:
                    operatingSystem.LogOff(ForceOption == ForceOption.Yes);
                    break;

                case ActionType.Sleep:
                    operatingSystem.Sleep(ForceOption == ForceOption.Yes);
                    break;

                case ActionType.Hibernate:
                    operatingSystem.Hibernate(ForceOption == ForceOption.Yes);
                    break;

                case ActionType.Reboot:
                    operatingSystem.Reboot(ForceOption == ForceOption.Yes);
                    break;

                case ActionType.ShutDown:
                    operatingSystem.ShutDown(ForceOption == ForceOption.Yes);
                    break;

                case ActionType.PowerOff:
                    operatingSystem.PowerOff(ForceOption == ForceOption.Yes);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AdjustForceOption()
        {
            switch (ActionType)
            {
                case ActionType.LogOff:
                case ActionType.Sleep:
                case ActionType.Hibernate:
                case ActionType.Reboot:
                case ActionType.ShutDown:
                case ActionType.PowerOff:
                    if (ForceOption != ForceOption.Yes && ForceOption != ForceOption.No)
                        ForceOption = lastApplicableForceOption;
                    break;

                case ActionType.Ring:
                case ActionType.LockWorkstation:
                default:
                    if (ForceOption != ForceOption.NotApplicable)
                        ForceOption = ForceOption.NotApplicable;
                    break;
            }
        }

        private bool SafeSetForceOption(ForceOption value)
        {
            switch (ActionType)
            {
                case ActionType.LogOff:
                case ActionType.Sleep:
                case ActionType.Hibernate:
                case ActionType.Reboot:
                case ActionType.ShutDown:
                case ActionType.PowerOff:
                    if (value != ForceOption.Yes && value != ForceOption.No)
                    {
                        ForceOption = value;
                        lastApplicableForceOption = value;
                        return true;
                    }
                    break;

                case ActionType.Ring:
                case ActionType.LockWorkstation:
                default:
                    if (value != ForceOption.NotApplicable)
                        ForceOption = ForceOption.NotApplicable;
                    break;
            }

            return false;
        }



        private bool IsAllowedToSet(ForceOption value)
        {
            switch (ActionType)
            {
                case ActionType.LogOff:
                case ActionType.Sleep:
                case ActionType.Hibernate:
                case ActionType.Reboot:
                case ActionType.ShutDown:
                case ActionType.PowerOff:
                    return value == ForceOption.Yes || value == ForceOption.No;

                case ActionType.Ring:
                case ActionType.LockWorkstation:
                default:
                    return value == ForceOption.NotApplicable;
            }
        }

        protected virtual void OnForceOptionChanged()
        {
            ForceOptionChangedEvent ev = new ForceOptionChangedEvent
            {
                ForceOption = ForceOption
            };
            eventBus.Publish(ev);
        }

        protected virtual void OnActionTypeChanged()
        {
            ActionTypeChangedEvent ev = new ActionTypeChangedEvent
            {
                ActionType = ActionType
            };
            eventBus.Publish(ev);
        }

        protected virtual void OnNotificationRaised()
        {
            NotificationRaised?.Invoke(this, EventArgs.Empty);
        }
    }
}