// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
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
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public partial class WindowsRebootForm : FormWithBehaviors
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
                loadDefaultSettingsToolStripMenuItem.Command = viewModel.LoadDefaultPlanCommand;
                loadInitialSettingsToolStripMenuItem.Command = viewModel.LoadThePlanCommand;
                saveCurrentSettingsToolStripMenuItem.Command = viewModel.SaveThePlanCommand;
                optionsToolStripMenuItem.Command = viewModel.OptionsCommand;
                licenseToolStripMenuItem.Command = viewModel.LicenseCommand;
                aboutToolStripMenuItem.Command = viewModel.AboutCommand;
                exitToolStripMenuItem.Command = viewModel.ExitCommand;

                viewModel.PropertyChanged += HandleViewModelPropertyChanged;
            }
        }

        private void HandleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(WindowsRebootViewModel.IsVisible))
                return;

            switch (viewModel.IsVisible)
            {
                case true when !Visible:
                    Show();
                    break;

                case false when Visible:
                    Hide();
                    break;
            }
        }

        public WindowsRebootForm()
        {
            InitializeComponent();
        }
    }
}