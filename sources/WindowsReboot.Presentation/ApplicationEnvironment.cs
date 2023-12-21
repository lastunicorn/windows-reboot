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
using DustInTheWind.WindowsReboot.Core.Config;
using Action = DustInTheWind.WindowsReboot.Core.Action;
using Timer = DustInTheWind.WindowsReboot.Core.Timer;

namespace DustInTheWind.WindowsReboot.Presentation
{
    public class ApplicationEnvironment
    {
        private readonly Action action;
        private readonly Timer timer;
        private readonly WorkerModel.Workers workers;
        private readonly WindowsRebootConfiguration configuration;

        public event CancelEventHandler Closing;
        public event EventHandler CloseRevoked;

        public ApplicationEnvironment(Action action, Timer timer, WorkerModel.Workers workers, WindowsRebootConfiguration configuration)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
            this.workers = workers ?? throw new ArgumentNullException(nameof(workers));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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