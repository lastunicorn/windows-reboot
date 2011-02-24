// Windows Reboot
// Copyright (C) 2009 Dust in the Wind
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
using MVCSharp.Winforms;
using MVCSharp.Core.Configuration.Views;
using DustInTheWind.WindowsReboot.UI;
using MVCSharp.Winforms.Configuration;

namespace DustInTheWind.WindowsReboot
{
    //[ViewAttribute(typeof(MainTask), MainTask.MainView)]
    [WinformsView(typeof(MainTask), MainTask.MainView)]
    internal partial class WindowsRebootForm : WinFormView, IWindowsRebootView
    {
        private WindowsRebootPresenter Presenter
        {
            get { return Controller as WindowsRebootPresenter; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootForm"/> class.
        /// </summary>
        public WindowsRebootForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Presenter.OnTimerElapsed();
        }

        private void buttonStartTimer_Click(object sender, EventArgs e)
        {
            Presenter.StartTimerClicked();
        }

        private void buttonStopTimer_Click(object sender, EventArgs e)
        {
            Presenter.StopTimerClicked();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Presenter.ViewLoaded();
        }


        #region Menu Items

        private void goToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnMenuItemGoToTrayClicked();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.MenuItemExitClicked();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnMenuItemAboutClicked();
        }

        private void loadInitialSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnMenuItemLoadInitialSettingsClicked();
        }

        private void saveCurrentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnMenuItemSaveCurrentSettingsClicked();
        }

        private void loadDefaultSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnMenuItemLoadDefaultSettingsClicked();
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnMenuItemLicenseClicked();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconShowClicked();
        }

        private void lockComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconLockComputerClicked();
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconLogOffClicked();
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconSleepClicked();
        }

        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconHibernateClicked();
        }

        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconRebootClicked();
        }

        private void shutDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconShutDownClicked();
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconPowerOffClicked();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconExitClicked();
        }

        #endregion

        #region Notify Icon

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Presenter.OnNotifyIconMouseClicked();
            }
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            Presenter.OnNotifyIconMouseMove();
        }

        #endregion

        #region IWindowsRebootView Members

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string LabelCurrentTime
        {
            set { this.labelCurrentTime.Text = value; }
        }

        public string LabelActionTime
        {
            set { this.labelActionTime.Text = value; }
        }

        public string LabelTimer
        {
            get { return this.labelTimer.Text; }
            set { this.labelTimer.Text = value; }
        }

        public ActionTypeItem[] ActionTypes
        {
            set { this.comboBoxAction.Items.Clear(); this.comboBoxAction.Items.AddRange(value); }
        }

        public ActionTypeItem ActionType
        {
            get { return (ActionTypeItem)this.comboBoxAction.SelectedItem; }
            set { this.comboBoxAction.SelectedItem = value; }
        }

        public bool ForceAction
        {
            get { return this.checkBoxForceAction.Checked; }
            set { this.checkBoxForceAction.Checked = value; }
        }

        public bool DisplayActionWarning
        {
            get { return this.checkBoxDisplayActionWarning.Checked; }
            set { this.checkBoxDisplayActionWarning.Checked = value; }
        }

        public bool ActionTimeGroupEnabled
        {
            set { this.groupBoxActionTime.Enabled = value; }
        }

        public bool ActionTypeGroupEnabled
        {
            set { this.groupBoxActionType.Enabled = value; }
        }

        public bool MenuItem_LoadInitialSettingsEnabled
        {
            set { this.loadInitialSettingsToolStripMenuItem.Enabled = value; }
        }

        public bool MenuItem_LoadDefaultSettingsEnabled
        {
            set { this.loadDefaultSettingsToolStripMenuItem.Enabled = value; }
        }

        public bool ActionTypeEnabled
        {
            set { this.comboBoxAction.Enabled = value; }
        }

        public bool ForceActionVisible
        {
            set { this.checkBoxForceAction.Visible = value; }
        }

        public bool FixedTimeGroupSelected
        {
            get { return this.tabControlActionTime.SelectedIndex == 0; }
            set { this.tabControlActionTime.SelectedIndex = 0; }
        }

        public bool DelayGroupSelected
        {
            get { return this.tabControlActionTime.SelectedIndex == 1; }
            set { this.tabControlActionTime.SelectedIndex = 1; }
        }

        public bool ImmediateGroupSelected
        {
            get { return this.tabControlActionTime.SelectedIndex == 2; }
            set { this.tabControlActionTime.SelectedIndex = 2; }
        }

        public int Hours
        {
            get { return Convert.ToInt32(this.numericUpDownHours.Value); }
            set { this.numericUpDownHours.Value = value; }
        }

        public int Minutes
        {
            get { return Convert.ToInt32(this.numericUpDownMinutes.Value); }
            set { this.numericUpDownMinutes.Value = value; }
        }

        public int Seconds
        {
            get { return Convert.ToInt32(this.numericUpDownSeconds.Value); }
            set { this.numericUpDownSeconds.Value = value; }
        }

        public DateTime FixedDate
        {
            get { return this.dateTimePickerFixedDate.Value.Date; }
            set { this.dateTimePickerFixedDate.Value = value; }
        }

        public TimeSpan FixedTime
        {
            get { return this.dateTimePickerFixedTime.Value.TimeOfDay; }
            set { this.dateTimePickerFixedTime.Value = DateTime.Today.AddTicks(value.Ticks); }
        }

        public string NotifyIconText
        {
            set { this.notifyIcon1.Text = value; }
        }

        public bool NotifyIconVisible
        {
            set { this.notifyIcon1.Visible = value; }
        }

        public void DisplayAbout()
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog(this);
            }
        }

        public void DisplayLicense()
        {
            using (LicenseForm form = new LicenseForm())
            {
                form.ShowDialog(this);
            }
        }

        public bool DisplayOptions(WindowsRebootConfigSection configSection)
        {
            using (OptionsForm form = new OptionsForm())
            {
                return (form.ShowDialog(this) == DialogResult.OK);
            }
        }

        #endregion

        #region public void DisplayError(Exception ex)

        private delegate void DisplayErrorDelegate(Exception ex);

        /// <summary>
        /// Displays the exception in a frendlly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        public void DisplayError(Exception ex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisplayErrorDelegate(this.DisplayError), new object[] { ex });
            }
            else
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region public void DisplayErrorMessage(string message)

        private delegate void DisplayErrorMessageDelegate(string message);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public void DisplayErrorMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisplayErrorMessageDelegate(this.DisplayErrorMessage), new object[] { message });
            }
            else
            {
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region public void DisplayMessage(string message)

        private delegate void DisplayMessageDelegate(string message);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public void DisplayMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisplayMessageDelegate(this.DisplayMessage), new object[] { message });
            }
            else
            {
                MessageBox.Show(this, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void WindowsRebootForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Presenter.ViewClosing();
        }

        public bool AskToClose(string message)
        {
            return MessageBox.Show(this, message, "Close Windwos Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        public bool Confirm(string message)
        {
            return MessageBox.Show(this, message, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK;
        }

        private void WindowsRebootForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Presenter.ViewMinimized();
            }

        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.OnActionTypeChanged();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.MenuItemOptionsClicked();
        }
    }
}
