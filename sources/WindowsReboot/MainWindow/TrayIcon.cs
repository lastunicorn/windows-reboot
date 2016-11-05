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

using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal partial class TrayIcon : Component
    {
        private TrayIconViewModel viewModel;

        public TrayIconViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;

                if (viewModel != null)
                {
                    lockComputerToolStripMenuItem.Command = viewModel.LockComputerCommand;
                    logOffToolStripMenuItem.Command = viewModel.LogOffCommand;
                    sleepToolStripMenuItem.Command = viewModel.SleepCommand;
                    hibernateToolStripMenuItem.Command = viewModel.HibernateCommand;
                    rebootToolStripMenuItem.Command = viewModel.RebootCommand;
                    shutDownToolStripMenuItem.Command = viewModel.ShutDownCommand;
                    powerOffToolStripMenuItem.Command = viewModel.PowerOffCommand;

                    //notifyIcon1.Bind(x => x.Text, viewModel, x => x.Text, false, DataSourceUpdateMode.Never);
                }
            }
        }

        public bool Visible
        {
            get { return notifyIcon1.Visible; }
            set { notifyIcon1.Visible = value; }
        }

        public string Text
        {
            get { return notifyIcon1.Text; }
            set { notifyIcon1.Text = value; }
        }

        public TrayIcon()
        {
            InitializeComponent();
        }

        public TrayIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                viewModel.OnNotifyIconMouseClicked();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            viewModel.OnNotifyIconMouseMove();
        }


        //#region Menu Items

        //private void goToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    viewModel.OnMenuItemGoToTrayClicked();
        //}

        //private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    viewModel.OnMenuItemExitClicked();
        //}

        //private void showToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    viewModel.OnNotifyIconShowClicked();
        //}

        //private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    viewModel.OnNotifyIconExitClicked();
        //}

        //#endregion

        //#region Notify Icon

        //private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        //{

        //}

        //private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    viewModel.OnNotifyIconMouseMove();
        //}

        //#endregion
    }
}
