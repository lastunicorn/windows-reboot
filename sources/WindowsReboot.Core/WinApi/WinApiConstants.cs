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

namespace DustInTheWind.WindowsReboot.Core.WinApi
{
    internal static class WinApiConstants
    {
        /// <summary>
        /// Shuts down all processes running in the logon session of the process
        /// that called the ExitWindowsEx function. Then it logs the user off.
        /// </summary>
        public const int EWX_LOGOFF = 0x0000;

        /// <summary>
        /// Shuts down the system to a point at which it is safe to turn off the power.
        /// Needs the SE_SHUTDOWN_NAME privilege.
        /// Specifying this flag will not turn off the power even if the system supports the power-off feature.
        /// (On Windows XP with SP1 and Vista:  If the system supports the power-off feature, specifying this flag turns off the power.)
        /// </summary>
        public const int EWX_SHUTDOWN = 0x0001;

        /// <summary>
        /// Shuts down the system and then restarts it. 
        /// Needs the SE_SHUTDOWN_NAME privilege.
        /// </summary>
        public const int EWX_REBOOT = 0x0002;

        /// <summary>
        /// Shuts down the system and turns off the power. The system must support the power-off feature. 
        /// Needs the SE_SHUTDOWN_NAME privilege.
        /// </summary>
        public const int EWX_POWEROFF = 0x0008;

        /// <summary>
        /// This flag has no effect if terminal services is enabled.
        /// Otherwise, the system does not send the WM_QUERYENDSESSION message.
        /// This can cause applications to lose data.
        /// Therefore, you should only use this flag in an emergency.
        /// </summary>
        public const int EWX_FORCE = 0x0004;

        /// <summary>
        /// Forces processes to terminate if they do not respond to the
        /// WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval.
        /// </summary>
        /// <remarks>
        /// I tried this flag but it doesn't work.
        /// The hanged programs are not closed and the system does not shut down.
        /// Tested on WinXP.
        /// </remarks>
        public const int EWX_FORCEIFHUNG = 0x0010;

        /// <summary>
        /// Other issue.
        /// </summary>
        public const uint SHTDN_REASON_MAJOR_OTHER = 0x00000000;

        /// <summary>
        /// Other issue.
        /// </summary>
        public const uint SHTDN_REASON_MINOR_OTHER = 0x00000000;

        /// <summary>
        /// The shutdown was planned. The system generates a System State Data (SSD) file.
        /// This file contains system state information such as
        /// the processes, threads, memory usage, and configuration.
        /// </summary>
        public const uint SHTDN_REASON_FLAG_PLANNED = 0x80000000;


        public const int TRUE = 1;
        public const int FALSE = 0;
    }
}