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

namespace DustInTheWind.WindowsReboot.Ports.SystemAccess
{
    public interface IRebootUtil
    {
        /// <summary>
        /// Locks the workstation's display. To unlock the workstation, the user must log in.
        /// </summary>
        void Lock();

        /// <summary>
        /// Logs off the current user.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        void LogOff(bool force);

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
        void Sleep(bool force);

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
        void Hibernate(bool force);

        /// <summary>
        /// Restarts the system.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        void Reboot(bool force);

        /// <summary>
        /// <para>
        /// Shuts down the system without switching the power off.
        /// </para>
        /// <para>
        /// Note: On Windows XP with SP1 and Vista switches the power off if the system supports that.
        /// </para>
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        void ShutDown(bool force);

        /// <summary>
        /// Shuts down the system and turns off the power.
        /// </summary>
        /// <param name="force">If true, forces processes to terminate if they do not respond within the timeout interval.</param>
        void PowerOff(bool force);
    }
}
