// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
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

namespace DustInTheWind.WindowsReboot.Domain
{
    public class ExecutionPlan
    {
        private static readonly ImmediateSchedule DefaultSchedule = new ImmediateSchedule();
        public static TimeSpan? DefaultWarningTime = TimeSpan.FromSeconds(30);

        private readonly EventBus eventBus;

        private ActionType actionType;
        private ForceOption forceOption;
        private ForceOption lastApplicableForceOption;
        private ISchedule schedule = DefaultSchedule;
        private TimeSpan? warningInterval = DefaultWarningTime;

        public ISchedule Schedule
        {
            get => schedule;
            set
            {
                if (value == null)
                    schedule = DefaultSchedule;

                schedule = value;

                OnScheduleChanged();
            }
        }

        public TimeSpan? WarningInterval
        {
            get => warningInterval;
            set
            {
                warningInterval = value;

                OnWarningIntervalChanged();
            }
        }

        public ActionType ActionType
        {
            get => actionType;
            set
            {
                bool isNewValueDefined = Enum.IsDefined(typeof(ActionType), value);
                if (!isNewValueDefined)
                    throw new InvalidActionTypeException(value);

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
                    return;

                if (value != ForceOption.NotApplicable)
                    lastApplicableForceOption = value;

                forceOption = value;
                OnForceOptionChanged();
            }
        }

        public ExecutionPlan(EventBus eventBus)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            ActionType = ActionType.Ring;
            ForceOption = ForceOption.NotApplicable;
            lastApplicableForceOption = ForceOption.Yes;
        }

        public void ActivateWarning()
        {
            WarningInterval = DefaultWarningTime;
        }

        public void DeactivateWarning()
        {
            WarningInterval = null;
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

        protected virtual void OnWarningIntervalChanged()
        {
            WarningIntervalChangedEvent ev = new WarningIntervalChangedEvent
            {
                Interval = warningInterval
            };

            eventBus.Publish(ev);
        }

        protected virtual void OnScheduleChanged()
        {
            ScheduleChangedEvent ev = new ScheduleChangedEvent(schedule);
            eventBus.Publish(ev);
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
    }
}