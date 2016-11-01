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

using System;
using System.Configuration;

namespace DustInTheWind.WindowsReboot.Core.Config
{
    public class WindowsRebootConfiguration
    {
        private readonly Configuration config;
        private readonly WindowsRebootConfigSection configSection;

        public ScheduleTime ActionTime
        {
            get
            {
                return new ScheduleTime
                {
                    Type = configSection.ActionTime.Type,
                    DateTime = configSection.ActionTime.DateTime,
                    TimeOfDay = configSection.ActionTime.DateTime.TimeOfDay,
                    Hours = configSection.ActionTime.Hours,
                    Minutes = configSection.ActionTime.Minutes,
                    Seconds = configSection.ActionTime.Seconds
                };
            }
            set
            {
                configSection.ActionTime.Type = value.Type;
                configSection.ActionTime.DateTime = value.Type == TaskTimeType.Daily ? DateTime.Today.Add(value.TimeOfDay) : value.DateTime;
                configSection.ActionTime.Hours = value.Hours;
                configSection.ActionTime.Minutes = value.Minutes;
                configSection.ActionTime.Seconds = value.Seconds;
            }
        }

        public TaskType ActionType
        {
            get { return configSection.ActionType.Value; }
            set { configSection.ActionType.Value = value; }
        }

        public bool ForceClosingPrograms
        {
            get { return configSection.ForceClosingPrograms.Value; }
            set { configSection.ForceClosingPrograms.Value = value; }
        }

        public bool StartTimerAtApplicationStart
        {
            get { return configSection.StartTimerAtApplicationStart.Value; }
            set { configSection.StartTimerAtApplicationStart.Value = value; }
        }

        public bool CloseToTray
        {
            get { return configSection.CloseToTray.Value; }
            set { configSection.CloseToTray.Value = value; }
        }

        public bool MinimizeToTray
        {
            get { return configSection.MinimizeToTray.Value; }
            set { configSection.MinimizeToTray.Value = value; }
        }

        public WindowsRebootConfiguration()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configSection = WindowsRebootConfigSection.GetOrCreateSection(config);
        }

        public void Save()
        {
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
