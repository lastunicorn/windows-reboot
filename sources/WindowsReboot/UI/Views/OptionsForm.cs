// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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
using DustInTheWind.WindowsReboot.Config;

namespace DustInTheWind.WindowsReboot.UI.Views
{
    /// <summary>
    /// Displaies some options that the user can set.
    /// </summary>
    public partial class OptionsForm : Form
    {
        private readonly WindowsRebootConfigSection configSection;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsForm"/> class with
        /// the configurationn object.
        /// </summary>
        /// <param name="configSection">An instance of <see cref="WindowsRebootConfigSection"/> class that contains the values that should be displaied.</param>
        public OptionsForm(WindowsRebootConfigSection configSection)
        {
            InitializeComponent();

            if (configSection == null)
                configSection = new WindowsRebootConfigSection();

            this.configSection = configSection;
        }

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            checkBoxCloseToTray.Checked = configSection.CloseToTray.Value;
            checkBoxMinimizeToTray.Checked = configSection.MinimizeToTray.Value;
            checkBoxStartTimerAtApplicationStart.Checked = configSection.StartTimerAtApplicationStart.Value;
        }

        private void HandleButtonOkayClick(object sender, EventArgs e)
        {
            configSection.CloseToTray.Value = checkBoxCloseToTray.Checked;
            configSection.MinimizeToTray.Value = checkBoxMinimizeToTray.Checked;
            configSection.StartTimerAtApplicationStart.Value = checkBoxStartTimerAtApplicationStart.Checked;
        }
    }
}
