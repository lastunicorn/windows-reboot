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
using System.Timers;

namespace DustInTheWind.WindowsReboot.Services
{
    class Ticker100 : IDisposable, ITicker
    {
        private bool isDisposed;
        private readonly Timer timer;
        public event EventHandler Tick;

        public Ticker100()
        {
            timer = new Timer(100);
            timer.Elapsed += HandleTimerElapsed;
            timer.Start();
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            OnTick();
        }

        protected virtual void OnTick()
        {
            EventHandler handler = Tick;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            timer.Dispose();

            isDisposed = true;
        }
    }
}
