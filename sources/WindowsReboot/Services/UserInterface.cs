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

using System;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using DustInTheWind.WindowsReboot.OtherWindows;
using Action = System.Action;

namespace DustInTheWind.WindowsReboot.Services
{
    internal class UserInterface : IUserInterface
    {
        private readonly IUiDispatcher uiDispatcher;
        public Form MainForm { get; set; }

        public UserInterface(IUiDispatcher uiDispatcher)
        {
            if (uiDispatcher == null) throw new ArgumentNullException("uiDispatcher");

            this.uiDispatcher = uiDispatcher;
        }

        public void Dispatch(Action action)
        {
            uiDispatcher.Dispatch(action);
        }

        public void DisplayAbout()
        {
            using (AboutForm form = new AboutForm())
                form.ShowDialog(MainForm);
        }

        public void DisplayLicense()
        {
            using (LicenseForm form = new LicenseForm())
                form.ShowDialog(MainForm);
        }

        public void DisplayOptions(WindowsRebootConfiguration configuration)
        {
            using (OptionsForm form = new OptionsForm(configuration))
            {
                if (form.ShowDialog(MainForm) == DialogResult.OK)
                    configuration.Save();
            }
        }

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public void DisplayMessage(string message)
        {
            MessageBox.Show(MainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public void DisplayError(string message)
        {
            MessageBox.Show(MainForm, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays the exception in a frendlly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        public void DisplayError(Exception ex)
        {
            MessageBox.Show(MainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool AskToClose(string message)
        {
            return MessageBox.Show(MainForm, message, "Close Windwos Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        public bool Confirm(string message)
        {
            return MessageBox.Show(MainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }
    }
}