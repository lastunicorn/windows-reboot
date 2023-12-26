﻿// Windows Reboot
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

using System.Configuration;
using DustInTheWind.WindowsReboot.Domain;

namespace DustInTheWind.WindowsReboot.ConfigAccess
{
    /// <summary>
    /// The configuration element that specifies the initial value of the action type.
    /// </summary>
    public class ActionTypeConfigElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the initial value of the action type.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public ActionType Value
        {
            get => (ActionType)this["value"];
            set => this["value"] = value;
        }
    }
}
