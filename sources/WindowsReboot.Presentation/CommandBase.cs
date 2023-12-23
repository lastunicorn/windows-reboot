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
using System.Threading;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation
{
    public abstract class CommandBase : ICommand
    {
        private readonly SynchronizationContext synchronizationContext;
        protected readonly IUserInterface UserInterface;

        public virtual bool CanExecute => true;

        public event EventHandler CanExecuteChanged;

        protected CommandBase(IUserInterface userInterface)
        {
            UserInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));

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
                UserInterface.DisplayError(ex);
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