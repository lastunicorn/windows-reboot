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

namespace DustInTheWind.WindowsReboot.Ports.SystemAccess
{
    public class ShutDownFailedException : WindowsRebootException
    {
        private const string DefaultMessage = "The ShutDown action failed.";

        public ShutDownFailedException()
            : base(DefaultMessage)
        {
        }
    }
    public class RebootFailedException : WindowsRebootException
    {
        private const string DefaultMessage = "The Reboot action failed.";

        public RebootFailedException()
            : base(DefaultMessage)
        {
        }
    }
    public class HibernateFailedException : WindowsRebootException
    {
        private const string DefaultMessage = "The Hibernate action failed.";

        public HibernateFailedException()
            : base(DefaultMessage)
        {
        }
    }
    public class SleepFailedException : WindowsRebootException
    {
        private const string DefaultMessage = "The Sleep action failed.";

        public SleepFailedException()
            : base(DefaultMessage)
        {
        }
    }
    public class LogOffFailedException : WindowsRebootException
    {
        private const string DefaultMessage = "The Log Off action failed.";

        public LogOffFailedException()
            : base(DefaultMessage)
        {
        }
    }
    public class LockWorkstationFailedException : WindowsRebootException
    {
        private const string DefaultMessage = "The Loc Workstation action failed.";

        public LockWorkstationFailedException()
            : base(DefaultMessage)
        {
        }
    }
}