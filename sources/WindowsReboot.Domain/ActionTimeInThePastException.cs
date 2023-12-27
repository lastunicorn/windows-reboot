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

namespace DustInTheWind.WindowsReboot.Domain
{
    public class ActionTimeInThePastException : Exception
    {
        private const string DefaultMessage = "The action time already passed. Specify a time in the future to execute the action. Current time: {0}; Requested action time: {1}.";

        public ActionTimeInThePastException(DateTime actionTime, DateTime currentTime)
            : base(BuildMessage(actionTime, currentTime))
        {
        }

        private static string BuildMessage(DateTime actionTime, DateTime currentTime)
        {
            string currentTimeString = ToString(currentTime);
            string actionTimeString = ToString(actionTime);

            return string.Format(DefaultMessage, currentTimeString, actionTimeString);
        }

        private static string ToString(DateTime dateTime)
        {
            string dateString = dateTime.ToLongDateString();
            string timeString = dateTime.ToLongTimeString();

            return $"{dateString} : {timeString}";
        }
    }
}