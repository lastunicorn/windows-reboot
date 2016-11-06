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

namespace DustInTheWind.WindowsReboot.Core
{
    public class Action
    {
        private readonly IRebootUtil rebootUtil;
        private TaskType type;
        private bool force;

        public event EventHandler ForceChanged;
        public event EventHandler TypeChanged;
        public event EventHandler NotificationRaised;

        public TaskType Type
        {
            get { return type; }
            set
            {
                if (!Enum.IsDefined(typeof(TaskType), value))
                    throw new ArgumentException("Invalid action type value");

                type = value;
                OnTypeChanged();
            }
        }

        public bool Force
        {
            get { return force; }
            set
            {
                force = value;
                OnForceChanged();
            }
        }

        public Action(Timer timer, IRebootUtil rebootUtil)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (rebootUtil == null) throw new ArgumentNullException("rebootUtil");

            this.rebootUtil = rebootUtil;

            Type = TaskType.Ring;
            Force = true;

            timer.Ring += HandleTimerRing;
        }

        private void HandleTimerRing(object sender, EventArgs eventArgs)
        {
            switch (Type)
            {
                case TaskType.Ring:
                    OnNotificationRaised();
                    break;

                case TaskType.LockWorkstation:
                    rebootUtil.Lock();
                    break;

                case TaskType.LogOff:
                    rebootUtil.LogOff(Force);
                    break;

                case TaskType.Sleep:
                    rebootUtil.Sleep(Force);
                    break;

                case TaskType.Hibernate:
                    rebootUtil.Hibernate(Force);
                    break;

                case TaskType.Reboot:
                    rebootUtil.Reboot(Force);
                    break;

                case TaskType.ShutDown:
                    rebootUtil.ShutDown(Force);
                    break;

                case TaskType.PowerOff:
                    rebootUtil.PowerOff(Force);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnForceChanged()
        {
            EventHandler handler = ForceChanged;

            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnTypeChanged()
        {
            EventHandler handler = TypeChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnNotificationRaised()
        {
            EventHandler handler = NotificationRaised;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}