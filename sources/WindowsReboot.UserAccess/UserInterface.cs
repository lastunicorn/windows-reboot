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
using System.Diagnostics;
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

        public UserInterface(IUiDispatcher uiDispatcher, IConfigStorage configuration)
        {
            this.uiDispatcher = uiDispatcher ?? throw new ArgumentNullException(nameof(uiDispatcher));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void DisplayAbout()
        {
            Form mainForm = GetMainForm();

            using (AboutForm form = new AboutForm())
                form.ShowDialog(mainForm);
        }

        public void DisplayLicense()
        {
            Form mainForm = GetMainForm();

            using (LicenseForm form = new LicenseForm())
                form.ShowDialog(mainForm);
        }

        public void DisplayOptions()
        {
            Form mainForm = GetMainForm();

            using (OptionsForm form = new OptionsForm(configuration))
            {
                if (form.ShowDialog(mainForm) == DialogResult.OK)
                    configuration.Save();
            }
        }

        public void DisplayMessage(string message)
        {
            Form mainForm = GetMainForm();

            uiDispatcher.Dispatch(() =>
            {
                MessageBox.Show(mainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        public void DisplayError(Exception ex)
        {
            Form mainForm = GetMainForm();

            MessageBox.Show(mainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool Confirm(string message)
        {
            Form mainForm = GetMainForm();

            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public void DisplayExecutionWarning(string actionName)
        {
            Form mainForm = GetMainForm();

            uiDispatcher.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds Windows Reboot will perform:\n\n{0}.", actionName);
                MessageBox.Show(mainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        public void DisplayNotification()
        {
            Form mainForm = GetMainForm();

            uiDispatcher.Dispatch(() =>
            {
                const string message = "Ring-ring!";
                MessageBox.Show(mainForm, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        public void CloseUserInterface()
        {
            Application.Exit();
        }

        public bool ConfirmClosingWhileTimerIsRunning()
        {
            Form mainForm = GetMainForm();

            const string message = "The timer is started. Are you sure you want to close the application?";

            DialogResult dialogResult = MessageBox.Show(mainForm, message, "Close Windows Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return dialogResult == DialogResult.Yes;
        }

        public bool ConfirmDirectLock()
        {
            Form mainForm = GetMainForm();

            string message = "Do you want to lock the workstation?";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public bool ConfirmDirectLogOff(string userName)
        {
            Form mainForm = GetMainForm();

            string message = $"Do you want to log off the current user?\nThe current logged in user is '{userName}'";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public bool ConfirmDirectSleep()
        {
            Form mainForm = GetMainForm();

            string message = "Do you want to put the system in 'Stand By' state?";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public bool ConfirmDirectHibernation()
        {
            Form mainForm = GetMainForm();

            string message = "Do you want to put the system in 'Hibernate' state?";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public bool ConfirmDirectReboot()
        {
            Form mainForm = GetMainForm();

            string message = "Do you want to reboot the system?";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public bool ConfirmDirectShutDown()
        {
            Form mainForm = GetMainForm();

            string message = "Do you want to shut down the system?\n\nObs! From WinXP SP1 this command will also power off the system.";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        public bool ConfirmDirectPowerOff()
        {
            Form mainForm = GetMainForm();

            string message = "Do you want to power off the system?\n\nObs! Only if the hardware supports 'Power Off'. Otherwise just a 'Shut Down' will be performed.";
            return MessageBox.Show(mainForm, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        private Form GetMainForm()
        {
            return (Form)Control.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
        }
    }
}