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

using System;
using System.Configuration;

namespace DustInTheWind.WindowsReboot.Config
{
    /// <summary>
    /// The configuration element that specifies the initial value of the action time.
    /// </summary>
    public class ActionTimeConfigElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the way in which the action time is specified or how it will be calculated.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public ActionTimeType Type
        {
            get { return (ActionTimeType)this["type"]; }
            set { this["type"] = value; }
        }

        /// <summary>
        /// Gets or sets the action time as a specific date time.
        /// This value is used when the <see cref="P:Type"/> is set to FixedDate.
        /// </summary>
        [ConfigurationProperty("dateTime", IsRequired = false, DefaultValue = "1980-06-13 13:00")]
        public DateTime DateTime
        {
            get { return (DateTime)this["dateTime"]; }
            set { this["dateTime"] = value; }
        }

        /// <summary>
        /// Gets or sets the numbers hours to delay the action execution from the timer start time.
        /// This value is used when the <see cref="P:Type"/> is set to Delay.
        /// </summary>
        [ConfigurationProperty("hours", IsRequired = false, DefaultValue = 0)]
        [IntegerValidator(MinValue = 0)]
        public int Hours
        {
            get { return (int)this["hours"]; }
            set { this["hours"] = value; }
        }

        /// <summary>
        /// Gets or sets the numbers minutes to delay the action execution from the timer start time.
        /// This value is used when the <see cref="P:Type"/> is set to Delay.
        /// </summary>
        [ConfigurationProperty("minutes", IsRequired = false, DefaultValue = 0)]
        [IntegerValidator(MinValue = 0)]
        public int Minutes
        {
            get { return (int)this["minutes"]; }
            set { this["minutes"] = value; }
        }

        /// <summary>
        /// Gets or sets the numbers seconds to delay the action execution from the timer start time.
        /// This value is used when the <see cref="P:Type"/> is set to Delay.
        /// </summary>
        [ConfigurationProperty("seconds", IsRequired = false, DefaultValue = 0)]
        [IntegerValidator(MinValue = 0)]
        public int Seconds
        {
            get { return (int)this["seconds"]; }
            set { this["seconds"] = value; }
        }
    }
}
