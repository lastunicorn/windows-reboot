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

using System.Windows.Forms;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public partial class WindowsRebootForm : Form, IWindowsRebootView
    {
        private WindowsRebootViewModel viewModel;

        public WindowsRebootViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;

                this.Bind(x => x.Text, viewModel, x => x.Title, false, DataSourceUpdateMode.Never);

                actionTimeControl1.ViewModel = viewModel.ActionTimeControlViewModel;
                actionTypeControl1.ViewModel = viewModel.ActionTypeControlViewModel;
                actionControl1.ViewModel = viewModel.ActionControlViewModel;
                statusControl1.ViewModel = viewModel.StatusControlViewModel;

                goToTrayToolStripMenuItem.Command = viewModel.GoToTrayCommand;
                loadDefaultSettingsToolStripMenuItem.Command = viewModel.LoadDefaultConfigurationCommand;
                loadInitialSettingsToolStripMenuItem.Command = viewModel.LoadConfigurationCommand;
                saveCurrentSettingsToolStripMenuItem.Command = viewModel.SaveConfigurationCommand;
                optionsToolStripMenuItem.Command = viewModel.OptionsCommand;
                licenseToolStripMenuItem.Command = viewModel.LicenseCommand;
                aboutToolStripMenuItem.Command = viewModel.AboutCommand;
                exitToolStripMenuItem.Command = viewModel.ExitCommand;
            }
        }

        public WindowsRebootForm()
        {
            InitializeComponent();
        }
    }
}