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

namespace DustInTheWind.WindowsReboot.Presentation
{
    partial class WindowsRebootForm : Form, IWindowsRebootView
    {
        private readonly WindowsRebootPresenter presenter;

        public WindowsRebootForm()
        {
            InitializeComponent();

            UserInterface userInterface = new UserInterface
            {
                MainForm = this
            };

            UiDispatcher uiDispatcher = new UiDispatcher();

            presenter = new WindowsRebootPresenter(this, userInterface, uiDispatcher);

            comboBoxAction.DataSource = presenter.ActionTypes;
            comboBoxAction.Bind(x => x.SelectedItem, presenter, x => x.SelectedActionType, false, DataSourceUpdateMode.OnPropertyChanged);
            
            this.Bind(x => x.Text, presenter, x => x.Title, false, DataSourceUpdateMode.Never);
            labelCurrentTime.Bind(x => x.Text, presenter, x => x.LabelCurrentTime, false, DataSourceUpdateMode.Never);
            labelActionTime.Bind(x => x.Text, presenter, x => x.LabelActionTime, false, DataSourceUpdateMode.Never);
            labelTimer.Bind(x => x.Text, presenter, x => x.LabelTimer, false, DataSourceUpdateMode.Never);

            fixedDateControl1.ViewModel = presenter.FixedDateControlViewModel;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            presenter.OnTimerElapsed();
        }

        private void buttonStartTimer_Click(object sender, EventArgs e)
        {
            presenter.OnStartTimerClicked();
        }

        private void buttonStopTimer_Click(object sender, EventArgs e)
        {
            presenter.OnStopTimerClicked();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            presenter.OnFormLoad();
        }


        #region Menu Items

        private void goToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemGoToTrayClicked();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemExitClicked();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemAboutClicked();
        }

        private void loadInitialSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemLoadInitialSettingsClicked();
        }

        private void saveCurrentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemSaveCurrentSettingsClicked();
        }

        private void loadDefaultSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemLoadDefaultSettingsClicked();
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemLicenseClicked();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconShowClicked();
        }

        private void lockComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconLockComputerClicked();
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconLogOffClicked();
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconSleepClicked();
        }

        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconHibernateClicked();
        }

        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconRebootClicked();
        }

        private void shutDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconShutDownClicked();
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconPowerOffClicked();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            presenter.OnNotifyIconExitClicked();
        }

        #endregion

        #region Notify Icon

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                presenter.OnNotifyIconMouseClicked();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            presenter.OnNotifyIconMouseMove();
        }

        #endregion

        #region IWindowsRebootView Members

        public ActionTypeItem[] ActionTypes
        {
            set
            {
                comboBoxAction.Items.Clear();
                comboBoxAction.Items.AddRange(value);
            }
        }

        public ActionTypeItem ActionType
        {
            get { return (ActionTypeItem)comboBoxAction.SelectedItem; }
            set { comboBoxAction.SelectedItem = value; }
        }

        public bool ForceAction
        {
            get { return checkBoxForceAction.Checked; }
            set { checkBoxForceAction.Checked = value; }
        }

        public bool DisplayActionWarning
        {
            get { return checkBoxDisplayActionWarning.Checked; }
            set { checkBoxDisplayActionWarning.Checked = value; }
        }

        public bool ActionTimeGroupEnabled
        {
            set { groupBoxActionTime.Enabled = value; }
        }

        public bool ActionTypeGroupEnabled
        {
            set { groupBoxActionType.Enabled = value; }
        }

        public bool MenuItem_LoadInitialSettingsEnabled
        {
            set { loadInitialSettingsToolStripMenuItem.Enabled = value; }
        }

        public bool MenuItem_LoadDefaultSettingsEnabled
        {
            set { loadDefaultSettingsToolStripMenuItem.Enabled = value; }
        }

        public bool ActionTypeEnabled
        {
            set { comboBoxAction.Enabled = value; }
        }

        public bool ForceActionEnabled
        {
            set { checkBoxForceAction.Enabled = value; }
        }

        public bool FixedTimeGroupSelected
        {
            get { return tabControlActionTime.SelectedIndex == 0; }
            set { tabControlActionTime.SelectedIndex = 0; }
        }

        public bool DelayGroupSelected
        {
            get { return tabControlActionTime.SelectedIndex == 1; }
            set { tabControlActionTime.SelectedIndex = 1; }
        }

        public bool ImmediateGroupSelected
        {
            get { return tabControlActionTime.SelectedIndex == 2; }
            set { tabControlActionTime.SelectedIndex = 2; }
        }

        public int Hours
        {
            get { return Convert.ToInt32(numericUpDownHours.Value); }
            set { numericUpDownHours.Value = value; }
        }

        public int Minutes
        {
            get { return Convert.ToInt32(numericUpDownMinutes.Value); }
            set { numericUpDownMinutes.Value = value; }
        }

        public int Seconds
        {
            get { return Convert.ToInt32(numericUpDownSeconds.Value); }
            set { numericUpDownSeconds.Value = value; }
        }

        public string NotifyIconText
        {
            set { notifyIcon1.Text = value; }
        }

        public bool NotifyIconVisible
        {
            set { notifyIcon1.Visible = value; }
        }

        #endregion

        private void WindowsRebootForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !presenter.OnFormClosing();
        }

        private void WindowsRebootForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                presenter.OnFormMinimized();
        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.OnActionTypeChanged();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemOptionsClicked();
        }
    }
}
