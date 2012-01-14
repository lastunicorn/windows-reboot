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

using System.Configuration;

namespace DustInTheWind.WindowsReboot.Config
{
    /// <summary>
    /// The configuration element that specifies the initial value of the "Force closing programs" check box.
    /// </summary>
    public class ForceClosingProgramsConfigElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the initial value of the "Force closing programs" check box.
        /// If this value is true, WindowsReboot will ask Windows to close all the processes, even if they do not respond.
        /// If this value is false, Windows will display a dialog asking the user to decide if the process should be closed or not.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public bool Value
        {
            get
            {
                return (bool)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
