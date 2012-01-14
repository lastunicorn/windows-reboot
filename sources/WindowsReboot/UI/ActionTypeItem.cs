// Windows Reboot
// Copyright (C) 2009 Iuga Alexandru
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

namespace DustInTheWind.WindowsReboot
{
    /// <summary>
    /// Class used to associate the action type with a text displayed to the user.
    /// </summary>
    public class ActionTypeItem
    {
        #region Value

        /// <summary>
        /// The type of the action to be executed.
        /// </summary>
        private ActionType value;

        /// <summary>
        /// Gets the type of the action to be executed.
        /// </summary>
        public ActionType Value
        {
            get { return value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTypeItem"/> class with
        /// the type of the action to be executed.
        /// </summary>
        /// <param name="value">The type of the action to be executed.</param>
        public ActionTypeItem(ActionType value)
        {
            this.value = value;
        }

        #endregion

        #region public override string ToString()

        /// <summary>
        /// Returns a string representation of the action type.
        /// </summary>
        /// <returns>A string representation of the action type.</returns>
        public override string ToString()
        {
            switch (value)
            {
                case ActionType.LockWorkstation:
                    return "Lock Computer";

                case ActionType.LogOff:
                    return "Log Off";

                case ActionType.Sleep:
                    return "Sleep";

                case ActionType.Hibernate:
                    return "Hibernate";

                case ActionType.Reboot:
                    return "Reboot";

                case ActionType.ShutDown:
                    return "Shut Down";

                case ActionType.PowerOff:
                    return "Power Off";

                default:
                    return value.ToString();
            }
        }

        #endregion

        #region public override bool Equals(object obj)

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current instance.</param>
        /// <returns>true if the specified <see cref="System.Object"/> is equal to the current instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            ActionTypeItem a = obj as ActionTypeItem;
            if (a == null)
            {
                return false;
            }

            // Return true if the fields match:
            return value == a.value;
        }

        #endregion

        #region public override int GetHashCode()

        /// <summary>
        /// Serves as a hash function for the current instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return (int)value;
        }

        #endregion
    }
}
