﻿// Windows Reboot
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

using System.Windows.Forms;

namespace DustInTheWind.WindowsReboot.Presentation.OtherWindows
{
    /// <summary>
    /// Displays some options that the user can set.
    /// </summary>
    public partial class OptionsForm : Form
    {
        public bool CloseToTray
        {
            get => checkBoxCloseToTray.Checked;
            set => checkBoxCloseToTray.Checked = value;
        }

        public bool MinimizeToTray
        {
            get => checkBoxMinimizeToTray.Checked;
            set => checkBoxMinimizeToTray.Checked = value;
        }

        public bool AutoStart
        {
            get => checkBoxStartTimerAtApplicationStart.Checked;
            set => checkBoxStartTimerAtApplicationStart.Checked = value;
        }

        public OptionsForm()
        {
            InitializeComponent();
        }
    }
}