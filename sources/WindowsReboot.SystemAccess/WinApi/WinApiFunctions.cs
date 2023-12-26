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

using System.Runtime.InteropServices;

namespace DustInTheWind.WindowsReboot.SystemAccess.WinApi
{
    internal static class WinApiFunctions
    {
        [DllImport("advapi32.dll")]
        public static extern int AdjustTokenPrivileges(int TokenHandle, int DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, out TOKEN_PRIVILEGES PreviousState, out int ReturnLength);

        /// <summary>
        /// Suspends the system by shutting power down. Depending on the Hibernate parameter,
        /// the system either enters a suspend (sleep) state or hibernation (S4).
        /// </summary>
        /// <param name="Hibernate">
        /// If this parameter is TRUE, the system hibernates.
        /// If the parameter is FALSE, the system is suspended.
        /// </param>
        /// <param name="ForceCritical">
        /// <para>
        /// Used only on Windows Server 2003, Windows XP, and Windows 2000.
        /// </para>
        /// <para>
        /// If this parameter is TRUE, the system suspends operation immediately;
        /// if it is FALSE, the system broadcasts a PBT_APMQUERYSUSPEND event to each application
        /// to request permission to suspend operation.
        /// </para>
        /// </param>
        /// <param name="DisableWakeEvent">
        /// If this parameter is TRUE, the system disables all wake events.
        /// If the parameter is FALSE, any system wake events remain enabled.
        /// </param>
        /// <returns>
        /// <para>If the function succeeds, the return value is nonzero.</para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("powrprof.dll")]
        public static extern int SetSuspendState(int Hibernate, int ForceCritical, int DisableWakeEvent);
    }
}