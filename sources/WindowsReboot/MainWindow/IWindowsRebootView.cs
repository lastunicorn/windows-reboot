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

using System.Windows.Forms;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal interface IWindowsRebootView
    {
        bool ActionTimeGroupEnabled { set; }
        bool ActionTypeGroupEnabled { set; }

        bool MenuItem_LoadInitialSettingsEnabled { set; }
        bool MenuItem_LoadDefaultSettingsEnabled { set; }

        bool FixedTimeGroupSelected { get; set; }
        bool DelayGroupSelected { get; set; }
        bool ImmediateGroupSelected { get; set; }

        /// <summary>
        /// Sets the tool tip text displayed by the notify icon.
        /// </summary>
        string NotifyIconText { set; }

        /// <summary>
        /// Sets the visibility of the notify icon.
        /// </summary>
        bool NotifyIconVisible { set; }


        /// <summary>
        /// Closes the form.
        /// </summary>
        void Close();

        /// <summary>
        /// Hides the form without closing it.
        /// </summary>
        void Hide();

        /// <summary>
        /// Shows the form if it is hidden.
        /// </summary>
        void Show();

        FormWindowState WindowState { get; set; }
    }
}
