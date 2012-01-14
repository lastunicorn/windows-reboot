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

using System;
using System.Runtime.InteropServices;

namespace DustInTheWind.WindowsReboot.Core
{
    /// <summary>
    /// Util class that calls the Windows API function to execute
    /// the lock, log off, sleep, hibernate, reboot, shut down and power off actions.
    /// </summary>
    internal class RebootUtil : IRebootUtil
    {
        #region WinAPI structures

        private struct LUID
        {
            public int UsedPart;
            public int IgnoredForNowHigh32BitPart;
        }

        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public LUID TheLuid;
            public int Attributes;
        }

        #endregion

        #region WinAPI constants

        /// <summary>
        /// Shuts down all processes running in the logon session of the process
        /// that called the ExitWindowsEx function. Then it logs the user off.
        /// </summary>
        private const int EWX_LOGOFF = 0x0000;

        /// <summary>
        /// Shuts down the system to a point at which it is safe to turn off the power.
        /// Needs the SE_SHUTDOWN_NAME privilege.
        /// Specifying this flag will not turn off the power even if the system supports the power-off feature.
        /// (On Windows XP with SP1 and Vista:  If the system supports the power-off feature, specifying this flag turns off the power.)
        /// </summary>
        private const int EWX_SHUTDOWN = 0x0001;

        /// <summary>
        /// Shuts down the system and then restarts it. 
        /// Needs the SE_SHUTDOWN_NAME privilege.
        /// </summary>
        private const int EWX_REBOOT = 0x0002;

        /// <summary>
        /// Shuts down the system and turns off the power. The system must support the power-off feature. 
        /// Needs the SE_SHUTDOWN_NAME privilege.
        /// </summary>
        private const int EWX_POWEROFF = 0x0008;

        /// <summary>
        /// This flag has no effect if terminal services is enabled.
        /// Otherwise, the system does not send the WM_QUERYENDSESSION message.
        /// This can cause applications to lose data.
        /// Therefore, you should only use this flag in an emergency.
        /// </summary>
        private const int EWX_FORCE = 0x0004;

        /// <summary>
        /// Forces processes to terminate if they do not respond to the
        /// WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval.
        /// </summary>
        /// <remarks>
        /// I tried this flag but it doesn't work.
        /// The hanged programs are not closed and the system does not shut down.
        /// Tested on WinXP.
        /// </remarks>
        private const int EWX_FORCEIFHUNG = 0x0010;

        /// <summary>
        /// Other issue.
        /// </summary>
        private const uint SHTDN_REASON_MAJOR_OTHER = 0x00000000;

        /// <summary>
        /// Other issue.
        /// </summary>
        private const uint SHTDN_REASON_MINOR_OTHER = 0x00000000;

        /// <summary>
        /// The shutdown was planned. The system generates a System State Data (SSD) file.
        /// This file contains system state information such as
        /// the processes, threads, memory usage, and configuration.
        /// </summary>
        private const uint SHTDN_REASON_FLAG_PLANNED = 0x80000000;


        private const int TRUE = 1;
        private const int FALSE = 0;


        #endregion

        #region WinAPI functions

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentProcess();

        [DllImport("advapi32.dll")]
        private static extern int OpenProcessToken(int ProcessHandle, int DesiredAccess, out int TokenHandle);

        [DllImport("advapi32.dll")]
        private static extern int LookupPrivilegeValueA(string lpSystemName, string lpName, out LUID lpLuid);

        [DllImport("advapi32.dll")]
        private static extern int AdjustTokenPrivileges(int TokenHandle, int DisableAllPrivileges, ref  TOKEN_PRIVILEGES NewState, int BufferLength, out TOKEN_PRIVILEGES PreviousState, out int ReturnLength);

        /// <summary>
        /// Locks the workstation's display.
        /// </summary>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero. Because the function executes
        /// asynchronously, a nonzero return value indicates that the operation has been initiated.
        /// It does not indicate whether the workstation has been successfully locked.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll")]
        private static extern int LockWorkStation();

        /// <summary>
        /// Logs off the interactive user, shuts down the system, or shuts down and restarts the system.
        /// </summary>
        /// <param name="uFlags">The shutdown type.</param>
        /// <param name="dwReason">
        /// The reason for initiating the shutdown.
        /// On Windows 2000 this parameter is ignored.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero. Because the function executes
        /// asynchronously, a nonzero return value indicates that the shutdown has been initiated.
        /// It does not indicate whether the shutdown will succeed. It is possible that the system,
        /// the user, or another application will abort the shutdown.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll")]
        private static extern int ExitWindowsEx(int uFlags, uint dwReason);

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
        private static extern int SetSuspendState(int Hibernate, int ForceCritical, int DisableWakeEvent);

        #endregion


        #region private static bool IsWinNT()

        /// <summary>
        /// Checks if the system on which this application runs is an NT based system.
        /// </summary>
        /// <returns>true if the system is an NT based; false otherwise.</returns>
        private static bool IsWinNT()
        {
            //int VER_PLATFORM_WIN32_NT = 2;
            return (Environment.OSVersion.Platform == PlatformID.Win32NT);
        }

        #endregion

        #region private static void EnableShutDown()

        /// <summary>
        /// Sets the privilege needed to excute a ShutDown action.
        /// </summary>
        private static void EnableShutDown()
        {
            short TOKEN_ADJUST_PRIVILEGES = 0x20;
            short TOKEN_QUERY = 0x08;
            short SE_PRIVILEGE_ENABLED = 0x02;
            string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            int hdlProcessHandle;
            int hdlTokenHandle;
            LUID tmpLuid;
            TOKEN_PRIVILEGES tkp;
            TOKEN_PRIVILEGES tkpNewButIgnored;
            int lBufferNeeded;

            hdlProcessHandle = GetCurrentProcess();
            if (OpenProcessToken(hdlProcessHandle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hdlTokenHandle) == FALSE)
            {
                throw new WindowsRebootException("Could not obtain the rights to execute the action.");
            }
            LookupPrivilegeValueA(null, SE_SHUTDOWN_NAME, out tmpLuid);
            tkp.PrivilegeCount = 1; // One privilege to set
            tkp.TheLuid = tmpLuid;
            tkp.Attributes = SE_PRIVILEGE_ENABLED;
            if (AdjustTokenPrivileges(hdlTokenHandle, 0, ref tkp, Marshal.SizeOf(typeof(TOKEN_PRIVILEGES)), out tkpNewButIgnored, out lBufferNeeded) == FALSE)
            {
                throw new WindowsRebootException("Could not obtain the rights to execute the action.");
            }
        }

        #endregion


        #region Actions

        /// <summary>
        /// Locks the workstation's display. To unlock the workstation, the user must log in.
        /// </summary>
        public void Lock()
        {
            if (LockWorkStation() == FALSE)
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
            int flags = EWX_LOGOFF;
            uint reason = SHTDN_REASON_MAJOR_OTHER | SHTDN_REASON_MINOR_OTHER | SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= EWX_FORCE;

            if (ExitWindowsEx(flags, reason) == FALSE)
            {
                throw new WindowsRebootException("The LogOff action failed.");
            }
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

            if (SetSuspendState(FALSE, (force ? TRUE : FALSE), FALSE) == FALSE)
            {
                throw new WindowsRebootException("The Sleep action failed.");
            }
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

            if (SetSuspendState(TRUE, (force ? TRUE : FALSE), FALSE) == FALSE)
            {
                throw new WindowsRebootException("The Hibernate action failed.");
            }
        }

        /// <summary>
        /// Restarts the system.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void Reboot(bool force)
        {
            int flags = EWX_REBOOT;
            uint reason = SHTDN_REASON_MAJOR_OTHER | SHTDN_REASON_MINOR_OTHER | SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= EWX_FORCE;

            if (IsWinNT())
                EnableShutDown();

            if (ExitWindowsEx(flags, reason) == FALSE)
            {
                throw new WindowsRebootException("The Reboot action failed.");
            }
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
            int flags = EWX_SHUTDOWN;
            uint reason = SHTDN_REASON_MAJOR_OTHER | SHTDN_REASON_MINOR_OTHER | SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= EWX_FORCE;

            if (IsWinNT())
                EnableShutDown();

            if (ExitWindowsEx(flags, reason) == FALSE)
            {
                throw new WindowsRebootException("The ShutDown action failed.");
            }
        }

        /// <summary>
        /// Shuts down the system and turns off the power.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        public void PowerOff(bool force)
        {
            int flags = EWX_POWEROFF;
            uint reason = SHTDN_REASON_MAJOR_OTHER | SHTDN_REASON_MINOR_OTHER | SHTDN_REASON_FLAG_PLANNED;

            if (force)
                flags |= EWX_FORCE;

            if (IsWinNT())
                EnableShutDown();

            if (ExitWindowsEx(flags, reason) == FALSE)
            {
                throw new WindowsRebootException("The PowerOff action failed.");
            }
        }

        #endregion
    }
}
