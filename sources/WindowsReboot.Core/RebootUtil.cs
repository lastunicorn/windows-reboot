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
using System.Runtime.InteropServices;
using DustInTheWind.WindowsReboot.Core.WinApi;

namespace DustInTheWind.WindowsReboot.Core
{
    /// <summary>
    /// Util class that calls the Windows API function to execute
    /// the lock, log off, sleep, hibernate, reboot, shut down and power off actions.
    /// </summary>
    public class RebootUtil : IRebootUtil
    {
        /// <summary>
        /// Checks if the system on which this application runs is an NT based system.
        /// </summary>
        /// <returns>true if the system is an NT based; false otherwise.</returns>
        private static bool IsWinNT()
        {
            //int VER_PLATFORM_WIN32_NT = 2;
            return (Environment.OSVersion.Platform == PlatformID.Win32NT);
        }

        /// <summary>
        /// Sets the privilege needed to excute a ShutDown action.
        /// </summary>
        private static void EnableShutDown()
        {
            const short TOKEN_ADJUST_PRIVILEGES = 0x20;
            const short TOKEN_QUERY = 0x08;
            const short SE_PRIVILEGE_ENABLED = 0x02;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            int hdlTokenHandle;
            LUID tmpLuid;
            TOKEN_PRIVILEGES tkp;
            TOKEN_PRIVILEGES tkpNewButIgnored;
            int lBufferNeeded;

            var hdlProcessHandle = WinApiFunctions.GetCurrentProcess();

            if (WinApiFunctions.OpenProcessToken(hdlProcessHandle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hdlTokenHandle) == WinApiConstants.FALSE)
                throw new WindowsRebootException("Could not obtain the rights to execute the action.");

            WinApiFunctions.LookupPrivilegeValueA(null, SE_SHUTDOWN_NAME, out tmpLuid);
            tkp.PrivilegeCount = 1; // One privilege to set
            tkp.TheLuid = tmpLuid;
            tkp.Attributes = SE_PRIVILEGE_ENABLED;

            if (WinApiFunctions.AdjustTokenPrivileges(hdlTokenHandle, 0, ref tkp, Marshal.SizeOf(typeof(TOKEN_PRIVILEGES)), out tkpNewButIgnored, out lBufferNeeded) == WinApiConstants.FALSE)
                throw new WindowsRebootException("Could not obtain the rights to execute the action.");
        }

        /// <summary>
        /// Locks the workstation's display. To unlock the workstation, the user must log in.
        /// </summary>
        public void Lock()
        {
            if (WinApiFunctions.LockWorkStation() == WinApiConstants.FALSE)
            {
                throw new WindowsRebootException("The LockWorkstation action failed.");
            }
        }

        /// <summary>
        /// Logs off the current user.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void LogOff(bool force)
        {
            int flags = WinApiConstants.EWX_LOGOFF;
            const uint reason = WinApiConstants.SHTDN_REASON_MAJOR_OTHER | WinApiConstants.SHTDN_REASON_MINOR_OTHER | WinApiConstants.SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= WinApiConstants.EWX_FORCE;

            if (WinApiFunctions.ExitWindowsEx(flags, reason) == WinApiConstants.FALSE)
                throw new WindowsRebootException("The LogOff action failed.");
        }

        /// <summary>
        /// Suspends the system by shutting power down and entering in suspend (sleep) state.
        /// </summary>
        /// <param name="force">
        /// <para>Only on Windows Server 2003, Windows XP, and Windows 2000.</para>
        /// <para>
        /// If this parameter is true, the system suspends operation immediately;
        /// if it is false, the system announce each application and requests permission to suspend operation.
        /// </para>
        /// </param>
        public void Sleep(bool force)
        {
            if (IsWinNT())
                EnableShutDown();

            if (WinApiFunctions.SetSuspendState(WinApiConstants.FALSE, (force ? WinApiConstants.TRUE : WinApiConstants.FALSE), WinApiConstants.FALSE) == WinApiConstants.FALSE)
                throw new WindowsRebootException("The Sleep action failed.");
        }

        /// <summary>
        /// Suspends the system by shutting power down and entering in hibernation state.
        /// </summary>
        /// <param name="force">
        /// <para>Only on Windows Server 2003, Windows XP, and Windows 2000.</para>
        /// <para>
        /// If this parameter is true, the system suspends operation immediately;
        /// if it is false, the system announce each application and requests permission to suspend operation.
        /// </para>
        /// </param>
        public void Hibernate(bool force)
        {
            if (IsWinNT())
                EnableShutDown();

            if (WinApiFunctions.SetSuspendState(WinApiConstants.TRUE, (force ? WinApiConstants.TRUE : WinApiConstants.FALSE), WinApiConstants.FALSE) == WinApiConstants.FALSE)
                throw new WindowsRebootException("The Hibernate action failed.");
        }

        /// <summary>
        /// Restarts the system.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void Reboot(bool force)
        {
            int flags = WinApiConstants.EWX_REBOOT;
            const uint reason = WinApiConstants.SHTDN_REASON_MAJOR_OTHER | WinApiConstants.SHTDN_REASON_MINOR_OTHER | WinApiConstants.SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= WinApiConstants.EWX_FORCE;

            if (IsWinNT())
                EnableShutDown();

            if (WinApiFunctions.ExitWindowsEx(flags, reason) == WinApiConstants.FALSE)
                throw new WindowsRebootException("The Reboot action failed.");
        }

        /// <summary>
        /// <para>
        /// Shuts down the system without switching the power off.
        /// </para>
        /// <para>
        /// Note: On Windows XP with SP1 and Vista swithes the power off if the system supports that.
        /// </para>
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void ShutDown(bool force)
        {
            int flags = WinApiConstants.EWX_SHUTDOWN;
            const uint reason = WinApiConstants.SHTDN_REASON_MAJOR_OTHER | WinApiConstants.SHTDN_REASON_MINOR_OTHER | WinApiConstants.SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= WinApiConstants.EWX_FORCE;

            if (IsWinNT())
                EnableShutDown();

            if (WinApiFunctions.ExitWindowsEx(flags, reason) == WinApiConstants.FALSE)
                throw new WindowsRebootException("The ShutDown action failed.");
        }

        /// <summary>
        /// Shuts down the system and turns off the power.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void PowerOff(bool force)
        {
            int flags = WinApiConstants.EWX_POWEROFF;
            const uint reason = WinApiConstants.SHTDN_REASON_MAJOR_OTHER | WinApiConstants.SHTDN_REASON_MINOR_OTHER | WinApiConstants.SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= WinApiConstants.EWX_FORCE;

            if (IsWinNT())
                EnableShutDown();

            if (WinApiFunctions.ExitWindowsEx(flags, reason) == WinApiConstants.FALSE)
                throw new WindowsRebootException("The PowerOff action failed.");
        }
    }
}
