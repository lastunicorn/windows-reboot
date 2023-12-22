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
using DustInTheWind.WindowsReboot.Ports.SystemAccess;

namespace DustInTheWind.WindowsReboot.Core
{
    public class ExecutionPlan
    {
        private readonly IOperatingSystem operatingSystem;
        private ActionType actionType;
        private bool applyForce;

        public event EventHandler ForceChanged;
        public event EventHandler TypeChanged;
        public event EventHandler NotificationRaised;

        public ActionType ActionType
        {
            get => actionType;
            set
            {
                if (!Enum.IsDefined(typeof(ActionType), value))
                    throw new ArgumentException("Invalid action type value");

                actionType = value;
                OnTypeChanged();
            }
        }

        public bool ApplyForce
        {
            get => applyForce;
            set
            {
                applyForce = value;
                OnForceChanged();
            }
        }

        public ExecutionPlan(IOperatingSystem operatingSystem)
        {
            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));

            ActionType = ActionType.Ring;
            ApplyForce = true;
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
                    operatingSystem.LogOff(ApplyForce);
                    break;

                case ActionType.Sleep:
                    operatingSystem.Sleep(ApplyForce);
                    break;

                case ActionType.Hibernate:
                    operatingSystem.Hibernate(ApplyForce);
                    break;

                case ActionType.Reboot:
                    operatingSystem.Reboot(ApplyForce);
                    break;

                case ActionType.ShutDown:
                    operatingSystem.ShutDown(ApplyForce);
                    break;

                case ActionType.PowerOff:
                    operatingSystem.PowerOff(ApplyForce);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnForceChanged()
        {
            ForceChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTypeChanged()
        {
            TypeChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnNotificationRaised()
        {
            NotificationRaised?.Invoke(this, EventArgs.Empty);
        }
    }
}