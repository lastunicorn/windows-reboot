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
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WindowsReboot.UserAccess.OtherWindows;

namespace DustInTheWind.WindowsReboot.UserAccess
{

    public class UserInterface : IUserInterface
    {
        private readonly IUiDispatcher uiDispatcher;
        private readonly IConfigStorage configuration;

        public Form MainForm { get; set; }

        public UserInterface(IUiDispatcher uiDispatcher, IConfigStorage configuration)
        {
            this.uiDispatcher = uiDispatcher ?? throw new ArgumentNullException(nameof(uiDispatcher));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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

        public void DisplayOptions()
        {
            using (OptionsForm form = new OptionsForm(configuration))
            {
                if (form.ShowDialog(MainForm) == DialogResult.OK)
                    configuration.Save();
            }
        }

        public void DisplayMessage(string message)
        {
            uiDispatcher.Dispatch(() =>
            {
                MessageBox.Show(MainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        public void DisplayError(Exception ex)
        {
            MessageBox.Show(MainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool Confirm(string message)
        {
            return MessageBox.Show(MainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public void DisplayExecutionWarning(string actionName)
        {
            uiDispatcher.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds Windows Reboot will perform:\n\n{0}.", actionName);
                MessageBox.Show(MainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        public void DisplayNotification()
        {
            uiDispatcher.Dispatch(() =>
            {
                const string message = "Ring-ring!";
                MessageBox.Show(MainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        public void CloseUserInterface()
        {
            Application.Exit();
        }

        public bool ConfirmClosingWhileTimerIsRunning()
        {
            const string message = "The timer is started. Are you sure you want to close the application?";

            DialogResult dialogResult = MessageBox.Show(MainForm, message, "Close Windows Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return dialogResult == DialogResult.Yes;
        }
    }
}