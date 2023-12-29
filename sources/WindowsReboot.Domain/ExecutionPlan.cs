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
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;

namespace DustInTheWind.WindowsReboot.Domain
{
    public class ExecutionPlan
    {
        private static readonly ImmediateSchedule DefaultSchedule = new ImmediateSchedule();
        public static TimeSpan? DefaultWarningTime = TimeSpan.FromSeconds(30);

        private readonly IOperatingSystem operatingSystem;
        private readonly EventBus eventBus;
        private readonly IUserInterface userInterface;

        private readonly InternalExecutionTimer timer;
        private volatile bool isRunning;
        private DateTime startTime;

        private ActionType actionType;
        private ForceOption forceOption;
        private ForceOption lastApplicableForceOption;
        private ISchedule schedule = DefaultSchedule;
        private TimeSpan? warningInterval = DefaultWarningTime;

        public bool IsRunning => isRunning;

        public ISchedule Schedule
        {
            get => schedule;
            set
            {
                if (value == null)
                    schedule = DefaultSchedule;

                schedule = value;

                OnScheduleChangedChanges();
            }
        }

        public TimeSpan? WarningInterval
        {
            get => warningInterval;
            set
            {
                if (isRunning)
                    throw new InvalidOperationException();

                warningInterval = value;

                OnWarningIntervalChanged();
            }
        }

        public TimeSpan TimeUntilAction => timer.ActionTime - DateTime.Now;

        public DateTime ActionTime => timer.ActionTime;

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

        public event EventHandler Warning;

        public event EventHandler Ring;

        public ExecutionPlan(IOperatingSystem operatingSystem, EventBus eventBus, IUserInterface userInterface)
        {
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));

            ActionType = ActionType.Ring;
            ForceOption = ForceOption.NotApplicable;
            lastApplicableForceOption = ForceOption.Yes;

            timer = new InternalExecutionTimer();
            timer.Warning += TimerWarning;
            timer.Ring += TimerRing;
        }

        public void Execute()
        {
            switch (ActionType)
            {
                case ActionType.Ring:
                    userInterface.DisplayNotification();
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

        private void TimerWarning(object sender, EventArgs e)
        {
            OnWarning();
        }

        private void TimerRing(object sender, EventArgs e)
        {
            OnRing();
            isRunning = false;

            OnStopped();
        }

        public void Start()
        {
            startTime = DateTime.Now;
            DateTime? nextRunTime = CalculateNextRunTime(startTime);

            if (nextRunTime == null)
                throw new ActionTimeInThePastException(ActionTime, startTime);

            timer.ActionTime = nextRunTime.Value;
            timer.WarningInterval = warningInterval;
            timer.Start();

            OnStarted();
        }

        private DateTime? CalculateNextRunTime(DateTime dateTime)
        {
            DateTime runTime = Schedule.CalculateTimeFrom(dateTime);

            return runTime < dateTime
                ? null as DateTime?
                : runTime;
        }

        public void Stop()
        {
            timer.Stop();
            isRunning = false;

            OnStopped();
        }

        public void ActivateWarning()
        {
            WarningInterval = DefaultWarningTime;
        }

        public void DeactivateWarning()
        {
            WarningInterval = null;
        }

        protected virtual void OnStarted()
        {
            TimerStartedEvent ev = new TimerStartedEvent
            {
                ActionTime = ActionTime
            };
            eventBus.Publish(ev);
        }

        protected virtual void OnStopped()
        {
            TimerStoppedEvent ev = new TimerStoppedEvent();
            eventBus.Publish(ev);
        }

        protected virtual void OnWarning()
        {
            Warning?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRing()
        {
            Ring?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnWarningIntervalChanged()
        {
            WarningIntervalChangedEvent ev = new WarningIntervalChangedEvent
            {
                Interval = warningInterval
            };

            eventBus.Publish(ev);
        }

        protected virtual void OnScheduleChangedChanges()
        {
            ScheduleChangedEvent ev = new ScheduleChangedEvent(schedule)
            {
                IsAllowedToChange = !isRunning
            };
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

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}