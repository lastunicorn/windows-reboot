﻿// Windows Reboot
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
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    internal partial class ActionTypeControl : UserControl
    {
        private ActionTypeControlViewModel viewModel;

        public ActionTypeControlViewModel ViewModel
        {
            get => viewModel;
            set
            {
                comboBoxAction.DataSource = null;
                comboBoxAction.DataBindings.Clear();
                checkBoxForceAction.DataBindings.Clear();
                checkBoxDisplayActionWarning.DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                {
                    comboBoxAction.DataSource = viewModel.ActionTypes;
                    comboBoxAction.Bind(x => x.SelectedItem, viewModel, x => x.SelectedActionType, false, DataSourceUpdateMode.OnPropertyChanged);

                    checkBoxForceAction.Bind(x => x.Checked, viewModel, x => x.ForceAction, false, DataSourceUpdateMode.OnPropertyChanged);
                    checkBoxForceAction.Bind(x => x.Enabled, viewModel, x => x.ForceActionEnabled, false, DataSourceUpdateMode.OnPropertyChanged);
                    checkBoxDisplayActionWarning.Bind(x => x.Checked, viewModel, x => x.DisplayActionWarning, false, DataSourceUpdateMode.OnPropertyChanged);

                    this.Bind(x => x.Enabled, viewModel, x => x.Enabled, false, DataSourceUpdateMode.Never);
                }
            }
        }

        public ActionTypeControl()
        {
            InitializeComponent();
        }

        private void comboBoxAction_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // hack: The ComboBox control updates the object bounded to the SelectedItem property only after it looses focus.
            // I need to force the update when user selects an item.
            foreach (Binding dataBinding in comboBoxAction.DataBindings)
                dataBinding.WriteValue();
        }
    }
}