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
using DustInTheWind.WindowsReboot.Presentation.CommandModel;

namespace DustInTheWind.WindowsReboot.Presentation.CustomControls
{
    public partial class ButtonWithCommand : Button
    {
        private ICommand command;

        public ICommand Command
        {
            get { return command; }
            set
            {
                if (command != null)
                    command.CanExecuteChanged -= HandleCommandCanExecuteChanged;

                command = value;

                if (command != null)
                    command.CanExecuteChanged += HandleCommandCanExecuteChanged;

                Enabled = command == null || command.CanExecute;
            }
        }

        private void HandleCommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            Enabled = command == null || command.CanExecute;
        }

        public ButtonWithCommand()
        {
            InitializeComponent();
        }

        private void ButtonWithCommand_Click(object sender, EventArgs e)
        {
            if (command != null)
                command.Execute();
        }
    }
}