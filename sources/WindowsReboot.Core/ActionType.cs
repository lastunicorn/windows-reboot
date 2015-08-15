// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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

namespace DustInTheWind.WindowsReboot.Core
{
    /// <summary>
    /// Indicates an action that can be executed when the timer elapses.
    /// </summary>
    public enum ActionType
    {
        Ring,

        /// <summary>
        /// Locks the computer.
        /// </summary>
        LockWorkstation,

        /// <summary>
        /// Logs the current user off.
        /// </summary>
        LogOff,

        /// <summary>
        /// Suspends the system by shutting power down and entering in the sleep state.
        /// </summary>
        Sleep,

        /// <summary>
        /// Suspends the system by shutting power down and entering in the hibernation state.
        /// </summary>
        Hibernate,

        /// <summary>
        /// Reboots the machine.
        /// </summary>
        Reboot,

        /// <summary>
        /// Shuts down the machine without turning the power off.
        /// </summary>
        ShutDown,

        /// <summary>
        /// Shuts down the machine and turns off the power.
        /// </summary>
        PowerOff
    }
}
