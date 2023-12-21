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
    public class Action
    {
        private readonly IOperatingSystem operatingSystem;
        private ActionType type;
        private bool force;

        public event EventHandler ForceChanged;
        public event EventHandler TypeChanged;
        public event EventHandler NotificationRaised;

        public ActionType Type
        {
            get => type;
            set
            {
                if (!Enum.IsDefined(typeof(ActionType), value))
                    throw new ArgumentException("Invalid action type value");

                type = value;
                OnTypeChanged();
            }
        }

        public bool Force
        {
            get => force;
            set
            {
                force = value;
                OnForceChanged();
            }
        }

        public Action(Timer timer, IOperatingSystem operatingSystem)
        {
            if (timer == null) throw new ArgumentNullException(nameof(timer));

            this.operatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));

            Type = ActionType.Ring;
            Force = true;

            timer.Ring += HandleTimerRing;
        }

        private void HandleTimerRing(object sender, EventArgs eventArgs)
        {
            switch (Type)
            {
                case ActionType.Ring:
                    OnNotificationRaised();
                    break;

                case ActionType.LockWorkstation:
                    operatingSystem.Lock();
                    break;

                case ActionType.LogOff:
                    operatingSystem.LogOff(Force);
                    break;

                case ActionType.Sleep:
                    operatingSystem.Sleep(Force);
                    break;

                case ActionType.Hibernate:
                    operatingSystem.Hibernate(Force);
                    break;

                case ActionType.Reboot:
                    operatingSystem.Reboot(Force);
                    break;

                case ActionType.ShutDown:
                    operatingSystem.ShutDown(Force);
                    break;

                case ActionType.PowerOff:
                    operatingSystem.PowerOff(Force);
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