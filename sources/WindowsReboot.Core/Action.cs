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
        private readonly IRebootUtil rebootUtil;
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

        public Action(Timer timer, IRebootUtil rebootUtil)
        {
            if (timer == null) throw new ArgumentNullException(nameof(timer));

            this.rebootUtil = rebootUtil ?? throw new ArgumentNullException(nameof(rebootUtil));

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
                    rebootUtil.Lock();
                    break;

                case ActionType.LogOff:
                    rebootUtil.LogOff(Force);
                    break;

                case ActionType.Sleep:
                    rebootUtil.Sleep(Force);
                    break;

                case ActionType.Hibernate:
                    rebootUtil.Hibernate(Force);
                    break;

                case ActionType.Reboot:
                    rebootUtil.Reboot(Force);
                    break;

                case ActionType.ShutDown:
                    rebootUtil.ShutDown(Force);
                    break;

                case ActionType.PowerOff:
                    rebootUtil.PowerOff(Force);
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