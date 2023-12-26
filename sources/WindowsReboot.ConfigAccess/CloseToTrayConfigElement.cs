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

using System.Configuration;

namespace DustInTheWind.WindowsReboot.ConfigAccess
{
    /// <summary>
    /// The configuration element that specifies if the main form should
    /// minimize to tray icon instead of closing when the user clicks the
    /// upper-right 'X' button.
    /// </summary>
    public class CloseToTrayConfigElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets a value that specifies if the main form should
        /// minimize to tray icon instead of closing when the user clicks the
        /// upper-right 'X' button.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public bool Value
        {
            get => (bool)this["value"];
            set => this["value"] = value;
        }
    }
}
