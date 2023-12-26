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

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation
{
    public abstract class CommandBase : ICommand
    {
        private readonly SynchronizationContext synchronizationContext;
        private bool canExecute = true;

        public virtual bool CanExecute
        {
            get => canExecute;
            protected set
            {
                if (canExecute == value)
                    return;

                canExecute = value;
                OnCanExecuteChanged();
            }
        }

        public event EventHandler CanExecuteChanged;

        protected CommandBase()
        {
            synchronizationContext = SynchronizationContext.Current;
        }

        public void Execute()
        {
            try
            {
                DoExecute();
            }
            catch (Exception ex)
            {
                Form mainForm = (Form)Control.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
                MessageBox.Show(mainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected abstract void DoExecute();

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected void Dispatch(Action action)
        {
            synchronizationContext.Post(o => action(), null);
        }
    }
}