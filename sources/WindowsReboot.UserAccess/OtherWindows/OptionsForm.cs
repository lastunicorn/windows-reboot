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
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core.Config;

namespace DustInTheWind.WindowsReboot.UserAccess.OtherWindows
{
    /// <summary>
    /// Displays some options that the user can set.
    /// </summary>
    internal partial class OptionsForm : Form
    {
        private readonly IWindowsRebootConfiguration configSection;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsForm"/> class with
        /// the configuration object.
        /// </summary>
        /// <param name="configSection">An instance of <see cref="WindowsRebootConfigSection"/> class that contains the values that should be displayed.</param>
        public OptionsForm(IWindowsRebootConfiguration configSection)
        {
            if (configSection == null) throw new ArgumentNullException(nameof(configSection));

            InitializeComponent();

            this.configSection = configSection;
        }

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            checkBoxCloseToTray.Checked = configSection.CloseToTray;
            checkBoxMinimizeToTray.Checked = configSection.MinimizeToTray;
            checkBoxStartTimerAtApplicationStart.Checked = configSection.StartTimerAtApplicationStart;
        }

        private void HandleButtonOkayClick(object sender, EventArgs e)
        {
            configSection.CloseToTray = checkBoxCloseToTray.Checked;
            configSection.MinimizeToTray = checkBoxMinimizeToTray.Checked;
            configSection.StartTimerAtApplicationStart = checkBoxStartTimerAtApplicationStart.Checked;
        }
    }
}
