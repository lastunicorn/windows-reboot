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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Services;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    partial class WindowsRebootForm : Form, IWindowsRebootView
    {
        private readonly WindowsRebootPresenter presenter;

        public WindowsRebootForm()
        {
            InitializeComponent();

            UiDispatcher uiDispatcher = new UiDispatcher();

            UserInterface userInterface = new UserInterface(uiDispatcher)
            {
                MainForm = this
            };

            ITicker ticker = new Ticker100();
            Performer performer = new Performer(userInterface, ticker);
            IRebootUtil rebootUtil = new RebootUtil();
            presenter = new WindowsRebootPresenter(this, userInterface, ticker, performer, rebootUtil);

            this.Bind(x => x.Text, presenter, x => x.Title, false, DataSourceUpdateMode.Never);

            fixedDateControl1.ViewModel = presenter.FixedDateControlViewModel;
            statusControl1.ViewModel = presenter.StatusControlViewModel;
            delayTimeControl1.ViewModel = presenter.DelayTimeControlViewModel;
            actionTypeControl1.ViewModel = presenter.ActionTypeControlViewModel;
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OnMenuItemOptionsClicked();
        }
    }
}
