﻿// Windows Reboot
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

namespace DustInTheWind.WindowsReboot
{
    internal interface IWindowsRebootView
    {
        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Sets the label that displays the current time.
        /// </summary>
        string LabelCurrentTime { set; }

        /// <summary>
        /// Sets the label that displays the time when the action will tace place.
        /// </summary>
        string LabelActionTime { set; }

        /// <summary>
        /// Gets or sets the label that displays the time remained until the action.
        /// </summary>
        string LabelTimer { get; set; }

        /// <summary>
        /// Sets the available values that can be chosed for the action type.
        /// </summary>
        ActionTypeItem[] ActionTypes { set; }

        /// <summary>
        /// Gets or sets the action type.
        /// </summary>
        ActionTypeItem ActionType { get; set; }

        bool ForceAction { get; set; }
        bool DisplayActionWarning { get; set; }
        bool ActionTimeGroupEnabled { set; }
        bool ActionTypeGroupEnabled { set; }
        bool MenuItem_LoadInitialSettingsEnabled { set; }
        bool MenuItem_LoadDefaultSettingsEnabled { set; }
        bool ForceActionVisible { set; }
        bool FixedTimeGroupSelected { get; set; }
        bool DelayGroupSelected { get; set; }
        bool ImmediateGroupSelected { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }
        int Seconds { get; set; }
        DateTime FixedDate { get; set; }
        TimeSpan FixedTime { get; set; }


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


        /// <summary>
        /// Displays the exception in a frendlly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        void DisplayError(Exception ex);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayErrorMessage(string message);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayMessage(string message);

        /// <summary>
        /// Displays the license window.
        /// </summary>
        void DisplayLicense();

        /// <summary>
        /// Displays the about window.
        /// </summary>
        void DisplayAbout();

        /// <summary>
        /// Displays a message box asking the user to confirm the closing.
        /// </summary>
        /// <param name="message">The message to be displayed in the message box.</param>
        /// <returns>true if the user allows the application to be closed; false otherwise.</returns>
        bool AskToClose(string message);

        bool DisplayOptions(WindowsRebootConfigSection configSection);

        FormWindowState WindowState { get; set; }

        bool Confirm(string p);
    }
}
