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
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Core.Config;
using Action = DustInTheWind.WindowsReboot.Core.Action;

namespace DustInTheWind.WindowsReboot
{
    internal class ApplicationEnvironment
    {
        private readonly Action action;
        private readonly Timer timer;
        private readonly WorkerModel.Workers workers;
        private readonly WindowsRebootConfiguration configuration;

        public event CancelEventHandler PrepareToClose;
        public event EventHandler Closing;
        public event EventHandler CloseRevoked;

        public ApplicationEnvironment(Action action, Timer timer, WorkerModel.Workers workers, WindowsRebootConfiguration configuration)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (timer == null) throw new ArgumentNullException("timer");
            if (workers == null) throw new ArgumentNullException("workers");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.action = action;
            this.timer = timer;
            this.workers = workers;
            this.configuration = configuration;
        }

        public void Initialize()
        {
            timer.Time = configuration.ActionTime;
            action.Type = configuration.ActionType;
            action.Force = configuration.ForceClosingPrograms;

            if (configuration.StartTimerAtApplicationStart)
                timer.Start();

            workers.Start();
        }

        public void Close()
        {
            CancelEventArgs cancelEventArgs = new CancelEventArgs();
            OnPrepareToClose(cancelEventArgs);

            if (cancelEventArgs.Cancel)
                OnCloseRevoked();
            else
                OnClosing();
        }

        protected virtual void OnPrepareToClose(CancelEventArgs e)
        {
            CancelEventHandler handler = PrepareToClose;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnClosing()
        {
            EventHandler handler = Closing;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnCloseRevoked()
        {
            EventHandler handler = CloseRevoked;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}