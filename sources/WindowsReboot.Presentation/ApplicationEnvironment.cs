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

namespace DustInTheWind.WindowsReboot.Presentation
{
    public class ApplicationEnvironment
    {
        private readonly ExecutionPlan executionPlan;
        private readonly ExecutionTimer executionTimer;
        private readonly WorkerModel.Workers workers;
        private readonly IWindowsRebootConfiguration configuration;

        public event CancelEventHandler Closing;
        public event EventHandler CloseRevoked;

        public ApplicationEnvironment(ExecutionPlan executionPlan, ExecutionTimer executionTimer, WorkerModel.Workers workers, IWindowsRebootConfiguration configuration)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.workers = workers ?? throw new ArgumentNullException(nameof(workers));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Initialize()
        {
            executionTimer.Time = configuration.ActionTime;
            executionPlan.ActionType = configuration.ActionType;
            executionPlan.ApplyForce = configuration.ForceClosingPrograms;

            if (configuration.StartTimerAtApplicationStart)
                executionTimer.Start();

            workers.Start();
        }

        public void Close()
        {
            CancelEventArgs cancelEventArgs = new CancelEventArgs();
            OnClosing(cancelEventArgs);

            if (cancelEventArgs.Cancel)
                OnCloseRevoked();
            else
                Application.Exit();
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