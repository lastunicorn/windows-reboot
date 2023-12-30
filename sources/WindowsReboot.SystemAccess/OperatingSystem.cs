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
using System.Runtime.InteropServices;
using DustInTheWind.WindowsApi.Powrprof;
using DustInTheWind.WindowsApi.ProcessthreadsapiHeader;
using DustInTheWind.WindowsApi.Securitybaseapi;
using DustInTheWind.WindowsApi.Winbase;
using DustInTheWind.WindowsApi.Winnt;
using DustInTheWind.WindowsApi.Winuser;
using DustInTheWind.WindowsReboot.Ports.SystemAccess;

namespace DustInTheWind.WindowsReboot.SystemAccess
{
    /// <summary>
    /// Util class that calls the Windows API function to execute
    /// the lock, log off, sleep, hibernate, reboot, shut down and power off actions.
    /// </summary>
    public class OperatingSystem : IOperatingSystem
    {
        /// <summary>
        /// Locks the workstation's display. To unlock the workstation, the user must log in again.
        /// </summary>
        public void Lock()
        {
            bool success = Winuser.LockWorkStation();

            if (!success)
                throw new LockWorkstationFailedException();
        }

        /// <summary>
        /// Logs off the current user.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void LogOff(bool force)
        {
            ExitWindowsFlags flags = ExitWindowsFlags.LogOff;

            if (force)
                flags |= ExitWindowsFlags.Force;

            SystemShutdownReason reason = SystemShutdownReason.SHTDN_REASON_MAJOR_OTHER | SystemShutdownReason.SHTDN_REASON_MINOR_OTHER | SystemShutdownReason.SHTDN_REASON_FLAG_PLANNED;

            bool success = Winuser.ExitWindowsEx(flags, reason);

            if (!success)
                throw new LogOffFailedException();
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

            bool success = Powrprof.SetSuspendState(false, force, false);

            if (!success)
                throw new SleepFailedException();
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

            bool success = Powrprof.SetSuspendState(true, force, false);

            if (!success)
                throw new HibernateFailedException();
        }

        /// <summary>
        /// Restarts the system.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void Reboot(bool force)
        {
            if (IsWinNT())
                EnableShutDown();

            ExitWindowsFlags flags = ExitWindowsFlags.Reboot;

            if (force)
                flags |= ExitWindowsFlags.Force;

            SystemShutdownReason reason = SystemShutdownReason.SHTDN_REASON_MAJOR_OTHER | SystemShutdownReason.SHTDN_REASON_MINOR_OTHER | SystemShutdownReason.SHTDN_REASON_FLAG_PLANNED;

            bool success = Winuser.ExitWindowsEx(flags, reason);

            if (!success)
                throw new RebootFailedException();
        }

        /// <summary>
        /// <para>
        /// Shuts down the system without switching the power off.
        /// </para>
        /// <para>
        /// Note: On Windows XP with SP1 and Vista switches the power off if the system supports that.
        /// </para>
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void ShutDown(bool force)
        {
            if (IsWinNT())
                EnableShutDown();

            ExitWindowsFlags flags = ExitWindowsFlags.Shutdown;

            if (force)
                flags |= ExitWindowsFlags.Force;

            SystemShutdownReason reason = SystemShutdownReason.SHTDN_REASON_MAJOR_OTHER | SystemShutdownReason.SHTDN_REASON_MINOR_OTHER | SystemShutdownReason.SHTDN_REASON_FLAG_PLANNED;

            bool success = Winuser.ExitWindowsEx(flags, reason);

            if (!success)
                throw new ShutDownFailedException();
        }

        /// <summary>
        /// Shuts down the system and turns off the power.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void PowerOff(bool force)
        {
            if (IsWinNT())
                EnableShutDown();

            ExitWindowsFlags flags = ExitWindowsFlags.PowerOff;

            if (force)
                flags |= ExitWindowsFlags.Force;

            SystemShutdownReason reason = SystemShutdownReason.SHTDN_REASON_MAJOR_OTHER | SystemShutdownReason.SHTDN_REASON_MINOR_OTHER | SystemShutdownReason.SHTDN_REASON_FLAG_PLANNED;

            bool success = Winuser.ExitWindowsEx(flags, reason);

            if (!success)
                throw new PowerOffFailedException();
        }

        /// <summary>
        /// Checks if the system on which this application runs is an NT based system.
        /// </summary>
        /// <returns>true if the system is an NT based; false otherwise.</returns>
        private static bool IsWinNT()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        /// <summary>
        /// Sets the privilege needed to execute a ShutDown action.
        /// </summary>
        private static void EnableShutDown()
        {
            int tokenHandle = OpenCurrentProcessToken();
            LUID luid = RetrieveLuid();
            AdjustTokenPrivileges(luid, tokenHandle);
        }

        private static int OpenCurrentProcessToken()
        {
            int processHandle = Processthreadsapi.GetCurrentProcess();
            AccessTokens desiredAccess = AccessTokens.TOKEN_ADJUST_PRIVILEGES | AccessTokens.TOKEN_QUERY;

            bool success = Processthreadsapi.OpenProcessToken(processHandle, desiredAccess, out int tokenHandle);

            if (!success)
                throw new ExecutionRightsException();

            return tokenHandle;
        }

        private static LUID RetrieveLuid()
        {
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            LUID luid;

            bool success = Winbase.LookupPrivilegeValueA(null, SE_SHUTDOWN_NAME, out luid);

            if (!success)
                throw new ExecutionRightsException();

            return luid;
        }

        private static void AdjustTokenPrivileges(LUID luid, int tokenHandle)
        {
            const short SE_PRIVILEGE_ENABLED = 0x02;

            TOKEN_PRIVILEGES tokenPrivileges;
            tokenPrivileges.PrivilegeCount = 1; // One privilege to set
            tokenPrivileges.Privileges = new LUID_AND_ATTRIBUTES[1];
            tokenPrivileges.Privileges[0] = new LUID_AND_ATTRIBUTES
            {
                Luid = luid,
                Attributes = SE_PRIVILEGE_ENABLED
            };

            int bufferLength = Marshal.SizeOf(typeof(TOKEN_PRIVILEGES));

            bool success = Securitybaseapi.AdjustTokenPrivileges(tokenHandle, false, ref tokenPrivileges, bufferLength, out _, out _);

            if (!success)
                throw new ExecutionRightsException();
        }
    }
}