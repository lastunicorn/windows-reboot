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
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal partial class WindowsRebootForm : Form, IWindowsRebootView
    {
        private WindowsRebootPresenter presenter;

        public WindowsRebootPresenter Presenter
        {
            get { return presenter; }
            set
            {
                presenter = value;

                this.Bind(x => x.Text, presenter, x => x.Title, false, DataSourceUpdateMode.Never);

                actionTimeControl1.ViewModel = presenter.ActionTimeControlViewModel;
                actionTypeControl1.ViewModel = presenter.ActionTypeControlViewModel;
                actionControl1.ViewModel = presenter.ActionControlViewModel;
                statusControl1.ViewModel = presenter.StatusControlViewModel;

                goToTrayToolStripMenuItem.Command = presenter.GoToTrayCommand;
                loadDefaultSettingsToolStripMenuItem.Command = presenter.LoadDefaultConfigurationCommand;
                loadInitialSettingsToolStripMenuItem.Command = presenter.LoadConfigurationCommand;
                saveCurrentSettingsToolStripMenuItem.Command = presenter.SaveConfigurationCommand;
                optionsToolStripMenuItem.Command = presenter.OptionsCommand;
                licenseToolStripMenuItem.Command = presenter.LicenseCommand;
                aboutToolStripMenuItem.Command = presenter.AboutCommand;
                exitToolStripMenuItem.Command = presenter.ExitCommand;

                lockComputerToolStripMenuItem.Command = presenter.LockComputerCommand;
                logOffToolStripMenuItem.Command = presenter.LogOffCommand;
                sleepToolStripMenuItem.Command = presenter.SleepCommand;
                hibernateToolStripMenuItem.Command = presenter.HibernateCommand;
                rebootToolStripMenuItem.Command = presenter.RebootCommand;
                shutDownToolStripMenuItem.Command = presenter.ShutDownCommand;
                powerOffToolStripMenuItem.Command = presenter.PowerOffCommand;
                toolStripMenuItem2.Command = presenter.ExitCommand;
            }
        }

        public WindowsRebootForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Presenter.OnFormLoad();
        }
        
        #region Menu Items

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OnNotifyIconShowClicked();
        }

        #endregion

        #region Notify Icon

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Presenter.OnNotifyIconMouseClicked();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            Presenter.OnNotifyIconMouseMove();
        }

        #endregion

        #region IWindowsRebootView Members

        public string NotifyIconText
        {
            set { notifyIcon1.Text = value; }
        }

        public bool NotifyIconVisible
        {
            set { notifyIcon1.Visible = value; }
        }

        #endregion

        private void WindowsRebootForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Presenter.OnFormMinimized();
        }
    }
}
