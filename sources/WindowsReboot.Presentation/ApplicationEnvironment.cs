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
using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WorkersEngine;

namespace DustInTheWind.WindowsReboot.Presentation
{
    public class ApplicationEnvironment
    {
        private readonly ExecutionPlan executionPlan;
        private readonly ExecutionTimer executionTimer;
        private readonly WorkersContainer workersContainer;
        private readonly IConfigStorage configStorage;

        public event CancelEventHandler Closing;
        public event EventHandler CloseRevoked;

        public ApplicationEnvironment(ExecutionPlan executionPlan, ExecutionTimer executionTimer, WorkersContainer workersContainer, IConfigStorage configStorage)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.workersContainer = workersContainer ?? throw new ArgumentNullException(nameof(workersContainer));
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
        }

        public void Initialize()
        {
            executionTimer.Time = configStorage.ActionTime;
            executionPlan.ActionType = configStorage.ActionType;
            executionPlan.ForceOption = configStorage.ForceClosingPrograms
                ? ForceOption.Yes
                : ForceOption.No;

            if (configStorage.StartTimerAtApplicationStart)
                executionTimer.Start();

            workersContainer.Start();
        }

        public void Close()
        {
            CancelEventArgs cancelEventArgs = new CancelEventArgs();
            OnClosing(cancelEventArgs);

            if (cancelEventArgs.Cancel)
                OnCloseRevoked();
            else
                System.Windows.Forms.Application.Exit();
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            Closing?.Invoke(this, e);
        }

        protected virtual void OnCloseRevoked()
        {
            CloseRevoked?.Invoke(this, EventArgs.Empty);
        }
    }
}