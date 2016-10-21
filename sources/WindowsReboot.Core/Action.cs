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
        private readonly Timer timer;
        private readonly IUserInterface userInterface;
        private readonly IRebootUtil rebootUtil;

        public TaskType Type { get; set; }

        public bool Force { get; set; }

        public Action(Timer timer, IUserInterface userInterface, IRebootUtil rebootUtil)
        {
            if (timer == null) throw new ArgumentNullException("timer");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (rebootUtil == null) throw new ArgumentNullException("rebootUtil");

            this.timer = timer;
            this.userInterface = userInterface;
            this.rebootUtil = rebootUtil;

            Force = true;

            this.timer.Ring += HandleTimerRing;
        }

        private void HandleTimerRing(object sender, EventArgs eventArgs)
        {
            Run();
        }

        private void Run()
        {
            switch (Type)
            {
                case TaskType.Ring:
                    userInterface.Dispatch(() =>
                    {
                        userInterface.DisplayMessage("Ring-ring!");
                    });
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
            }
        }
    }
}