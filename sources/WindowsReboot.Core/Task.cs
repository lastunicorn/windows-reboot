// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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
using DustInTheWind.WindowsReboot.Core.Services;

namespace DustInTheWind.WindowsReboot.Core
{
    public class Task
    {
        private readonly IUserInterface userInterface;
        private readonly ITicker ticker;
        private readonly IRebootUtil rebootUtil;

        /// <summary>
        /// Indicates if the timer was started.
        /// </summary>
        private volatile bool isRunning;

        public TaskType Type { get; set; }
        public TaskTime Time { get; set; }

        public bool ForceAction { get; set; }
        public bool DisplayWarningMessage { get; set; }

        private readonly TimeSpan warningMessageTime = TimeSpan.FromSeconds(30);
        private DateTime taskRunTime;
        public TimeSpan TimeUntilAction { get; private set; }

        public event EventHandler Started;
        public event EventHandler Stoped;
        public event EventHandler<TickEventArgs> Tick;

        /// <summary>
        /// Indicates if the timer was started.
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        public DateTime? ActionTime
        {
            get { return taskRunTime; }
        }

        public Task(IUserInterface userInterface, ITicker ticker, IRebootUtil rebootUtil)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (ticker == null) throw new ArgumentNullException("ticker");
            if (rebootUtil == null) throw new ArgumentNullException("rebootUtil");

            this.userInterface = userInterface;
            this.ticker = ticker;
            this.rebootUtil = rebootUtil;

            ForceAction = true;
            DisplayWarningMessage = true;
        }

        public void Start()
        {
            DateTime now = DateTime.Now;
            DateTime runTime = Time.CalculateTimeFrom(now);

            if (runTime < now)
            {
                string currentTimeString = string.Format("{0} : {1}", now.ToLongDateString(), now.ToLongTimeString());
                string actionTimeString = string.Format("{0} : {1}", runTime.ToLongDateString(), runTime.ToLongTimeString());

                string message = string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString);
                userInterface.DisplayErrorMessage(message);
            }
            else
            {
                taskRunTime = runTime;

                if (DisplayWarningMessage && runTime - now < warningMessageTime)
                    DisplayWarningMessage = false;

                isRunning = true;

                ticker.Tick += HandleTickerTick;

                OnStarted();
            }
        }

        private void HandleTickerTick(object sender, EventArgs eventArgs)
        {
            if (!isRunning)
                return;

            DateTime now = DateTime.Now;

            CalculateRemainingTime(now);
            DisplayWarningIfNeeded(now);
            DoActionIfNeeded(now);
        }

        private void CalculateRemainingTime(DateTime now)
        {
            TimeUntilAction = taskRunTime - now;

            OnTick(new TickEventArgs(TimeUntilAction));
        }

        private void DisplayWarningIfNeeded(DateTime now)
        {
            if (!DisplayWarningMessage || taskRunTime - warningMessageTime > now)
                return;

            DisplayWarningMessage = false;

            userInterface.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds WindowsReboot will perform the action:\n\n{0}.", Type);
                userInterface.DisplayMessage(message);
            });
        }

        private void DoActionIfNeeded(DateTime now)
        {
            if (taskRunTime > now)
                return;

            Stop();
            Run();
        }

        private void Run()
        {
            switch (Type)
            {
                case TaskType.Ring:
                    userInterface.Dispatch(() =>
                    {
                        userInterface.DisplayMessage("Ring-ring!");
                    });
                    break;

                case TaskType.LockWorkstation:
                    rebootUtil.Lock();
                    break;

                case TaskType.LogOff:
                    rebootUtil.LogOff(ForceAction);
                    break;

                case TaskType.Sleep:
                    rebootUtil.Sleep(ForceAction);
                    break;

                case TaskType.Hibernate:
                    rebootUtil.Hibernate(ForceAction);
                    break;

                case TaskType.Reboot:
                    rebootUtil.Reboot(ForceAction);
                    break;

                case TaskType.ShutDown:
                    rebootUtil.ShutDown(ForceAction);
                    break;

                case TaskType.PowerOff:
                    rebootUtil.PowerOff(ForceAction);
                    break;
            }
        }

        public void Stop()
        {
            ticker.Tick -= HandleTickerTick;

            isRunning = false;

            OnStoped();
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnStoped()
        {
            EventHandler handler = Stoped;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnTick(TickEventArgs e)
        {
            EventHandler<TickEventArgs> handler = Tick;

            if (handler != null)
                handler(this, e);
        }
    }
}