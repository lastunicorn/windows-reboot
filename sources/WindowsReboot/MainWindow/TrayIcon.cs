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
                if (viewModel != null)
                {
                    toolStripMenuItem1.Command = null;
                    lockComputerToolStripMenuItem.Command = null;
                    logOffToolStripMenuItem.Command = null;
                    sleepToolStripMenuItem.Command = null;
                    hibernateToolStripMenuItem.Command = null;
                    rebootToolStripMenuItem.Command = null;
                    shutDownToolStripMenuItem.Command = null;
                    powerOffToolStripMenuItem.Command = null;
                    toolStripMenuItem2.Command = null;

                    viewModel.PropertyChanged -= HandleViewModelPropertyChanged;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    toolStripMenuItem1.Command = viewModel.RestoreMainWindowCommand;
                    lockComputerToolStripMenuItem.Command = viewModel.LockComputerCommand;
                    logOffToolStripMenuItem.Command = viewModel.LogOffCommand;
                    sleepToolStripMenuItem.Command = viewModel.SleepCommand;
                    hibernateToolStripMenuItem.Command = viewModel.HibernateCommand;
                    rebootToolStripMenuItem.Command = viewModel.RebootCommand;
                    shutDownToolStripMenuItem.Command = viewModel.ShutDownCommand;
                    powerOffToolStripMenuItem.Command = viewModel.PowerOffCommand;
                    toolStripMenuItem2.Command = viewModel.ExitCommand;

                    viewModel.PropertyChanged += HandleViewModelPropertyChanged;
                }
            }
        }

        private void HandleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                    notifyIcon1.Text = viewModel.Text;
                    break;

                case "IsVisible":
                    notifyIcon1.Visible = viewModel.IsVisible;
                    break;
            }
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
            if (e.Button == MouseButtons.Left && viewModel != null && viewModel.RestoreMainWindowCommand != null && viewModel.RestoreMainWindowCommand.CanExecute)
                viewModel.RestoreMainWindowCommand.Execute();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            viewModel.OnNotifyIconMouseMove();
        }
    }
}