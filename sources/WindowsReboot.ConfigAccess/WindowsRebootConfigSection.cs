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
    /// Represents the "windowsReboot" configuration section.
    /// </summary>
    public class WindowsRebootConfigSection : ConfigurationSection
    {
        private const string SECTION_NAME = "windowsReboot";

        /// <summary>
        /// Extracts from a <see cref="Configuration"/> object the "windowsReboot" section.
        /// </summary>
        /// <param name="config">The <see cref="Configuration"/> from which to extract the "windowsReboot" section.</param>
        /// <returns>An instance of <see cref="WindowsRebootConfigSection"/> containing the "windowsReboot" section or null if the  <see cref="Configuration"/> object does not contain such a section.</returns>
        public static WindowsRebootConfigSection GetSection(Configuration config)
        {
            return config.GetSection("windowsReboot") as WindowsRebootConfigSection;
        }

        /// <summary>
        /// Extracts from a <see cref="Configuration"/> object the "windowsReboot" section.
        /// If the section does not exist it is automatically created.
        /// </summary>
        /// <param name="config">The <see cref="Configuration"/> from which to extract the "windowsReboot" section.</param>
        /// <returns>An instance of <see cref="WindowsRebootConfigSection"/> containing the "windowsReboot" section.</returns>
        public static WindowsRebootConfigSection GetOrCreateSection(Configuration config)
        {
            WindowsRebootConfigSection section = config.GetSection(SECTION_NAME) as WindowsRebootConfigSection;

            if (section == null)
            {
                section = new WindowsRebootConfigSection();
                config.Sections.Add(SECTION_NAME, section);
            }

            return section;
        }

        /// <summary>
        /// Get the configuration element that specifies the initial value of the action time.
        /// </summary>
        [ConfigurationProperty("actionTime", IsRequired = false)]
        public ActionTimeConfigElement ActionTime => (ActionTimeConfigElement)this["actionTime"] ?? new ActionTimeConfigElement();

        /// <summary>
        /// Gets the configuration element that specifies the initial value of the action type.
        /// The way in which the action time is specified or calculated.
        /// </summary>
        [ConfigurationProperty("actionType", IsRequired = false)]
        public ActionTypeConfigElement ActionType => (ActionTypeConfigElement)this["actionType"] ?? new ActionTypeConfigElement();

        /// <summary>
        /// Gets the configuration element that specifies the initial value of the "Force closing programs" check box.
        /// If this value is true, WindowsReboot will ask Windows to close all the processes, even if they do not respond.
        /// If this value is false, Windows will display a dialog asking the user to decide if the process should be closed or not.
        /// </summary>
        [ConfigurationProperty("forceClosingPrograms", IsRequired = false)]
        public ForceClosingProgramsConfigElement ForceClosingPrograms => (ForceClosingProgramsConfigElement)this["forceClosingPrograms"] ?? new ForceClosingProgramsConfigElement();

        /// <summary>
        /// Gets the configuration element that specifies if the timer should be started
        /// immediately after the application is started.
        /// </summary>
        [ConfigurationProperty("startTimerAtApplicationStart", IsRequired = false)]
        public StartTimerAtApplicationStartConfigElement StartTimerAtApplicationStart => (StartTimerAtApplicationStartConfigElement)this["startTimerAtApplicationStart"] ?? new StartTimerAtApplicationStartConfigElement();

        /// <summary>
        /// Gets the configuration element that specifies if the main form should
        /// minimize to tray icon instead of taskbar when the user clicks the
        /// upper-right minimize button.
        /// </summary>
        [ConfigurationProperty("minimizeToTray", IsRequired = false)]
        public MinimizeToTrayConfigElement MinimizeToTray => (MinimizeToTrayConfigElement)this["minimizeToTray"] ?? new MinimizeToTrayConfigElement();

        /// <summary>
        /// Gets the configuration element that specifies if the main form should
        /// minimize to tray icon instead of closing when the user clicks the
        /// upper-right minimize button.
        /// </summary>
        [ConfigurationProperty("closeToTray", IsRequired = false)]
        public CloseToTrayConfigElement CloseToTray => (CloseToTrayConfigElement)this["closeToTray"] ?? new CloseToTrayConfigElement();
    }
}