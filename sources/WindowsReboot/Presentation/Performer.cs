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
using DustInTheWind.WindowsReboot.Core;

namespace DustInTheWind.WindowsReboot.Presentation
{
    class Performer
    {
        private readonly UserInterface userInterface;
        private readonly UiDispatcher uiDispatcher;
        private readonly ITicker ticker;

        private readonly IRebootUtil rebootUtil;

        /// <summary>
        /// Indicates if the timer was started.
        /// </summary>
        private volatile bool isRunning;

        private ActionType actionType;

        /// <summary>
        /// The time when the action should be executed.
        /// </summary>
        public DateTime ActionTime { get; private set; }

        public bool DisplayWarningMessage { get; set; }
        
        public bool ForceAction { get; set; }

        private readonly TimeSpan warningMessageTime = TimeSpan.FromSeconds(30);
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
            set { isRunning = value; }
        }

        public Performer(UserInterface userInterface, UiDispatcher uiDispatcher, ITicker ticker)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (uiDispatcher == null) throw new ArgumentNullException("uiDispatcher");
            if (ticker == null) throw new ArgumentNullException("ticker");

            this.userInterface = userInterface;
            this.uiDispatcher = uiDispatcher;
            this.ticker = ticker;

            rebootUtil = new RebootUtil();
        }

        public void Start(DateTime actionTime, ActionType actionType)
        {
            DateTime now = DateTime.Now;

            if (actionTime < now)
            {
                string currentTimeString = string.Format("{0} : {1}", now.ToLongDateString(), now.ToLongTimeString());
                string actionTimeString = string.Format("{0} : {1}", actionTime.ToLongDateString(), actionTime.ToLongTimeString());

                string message = string.Format("The action time already passed.\nPlease specify a time in the future to execute the action.\n\nCurrent time: {0}\nRequested action time: {1}.", currentTimeString, actionTimeString);
                userInterface.DisplayErrorMessage(message);
            }
            else
            {
                ActionTime = actionTime;
                this.actionType = actionType;

                if (DisplayWarningMessage && actionTime - now < warningMessageTime)
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
            TimeUntilAction = ActionTime - now;

            OnTick(new TickEventArgs(TimeUntilAction));
        }

        private void DisplayWarningIfNeeded(DateTime now)
        {
            if (!DisplayWarningMessage || ActionTime - warningMessageTime > now)
                return;

            DisplayWarningMessage = false;

            uiDispatcher.Dispatch(() =>
            {
                string message = string.Format("In 30 seconds WindowsReboot will perform {0} action.", actionType);
                userInterface.DisplayMessage(message);
            });
        }

        private void DoActionIfNeeded(DateTime now)
        {
            if (ActionTime <= now)
            {
                isRunning = false;
                DoAction();
                
                OnStoped();
            }
        }

        private void DoAction()
        {
            switch (actionType)
            {
                case ActionType.Ring:
                    uiDispatcher.Dispatch(() =>
                    {
                        userInterface.DisplayMessage("Ring-ring!");
                    });
                    break;

                case ActionType.LockWorkstation:
                    rebootUtil.Lock();
                    break;

                case ActionType.LogOff:
                    rebootUtil.LogOff(ForceAction);
                    break;

                case ActionType.Sleep:
                    rebootUtil.Sleep(ForceAction);
                    break;

                case ActionType.Hibernate:
                    rebootUtil.Hibernate(ForceAction);
                    break;

                case ActionType.Reboot:
                    rebootUtil.Reboot(ForceAction);
                    break;

                case ActionType.ShutDown:
                    rebootUtil.ShutDown(ForceAction);
                    break;

                case ActionType.PowerOff:
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