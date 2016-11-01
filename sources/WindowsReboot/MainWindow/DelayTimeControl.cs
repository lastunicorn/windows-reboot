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

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal partial class DelayTimeControl : UserControl
    {
        public int Hours
        {
            get { return (int)numericUpDownHours.Value; }
            set
            {
                numericUpDownHours.Value = value;
                OnHoursChanged();
            }
        }

        public int Minutes
        {
            get { return (int)numericUpDownMinutes.Value; }
            set
            {
                numericUpDownMinutes.Value = value;
                OnMinutesChanged();
            }
        }
        
        public int Seconds
        {
            get { return (int)numericUpDownSeconds.Value; }
            set
            {
                numericUpDownSeconds.Value = value;
                OnSecondsChanged();
            }
        }

        public event EventHandler HoursChanged;
        public event EventHandler MinutesChanged;
        public event EventHandler SecondsChanged;

        public DelayTimeControl()
        {
            InitializeComponent();
        }
        
        protected virtual void OnHoursChanged()
        {
            EventHandler handler = HoursChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnMinutesChanged()
        {
            EventHandler handler = MinutesChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnSecondsChanged()
        {
            EventHandler handler = SecondsChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void numericUpDownHours_ValueChanged(object sender, EventArgs e)
        {
            OnHoursChanged();
        }

        private void numericUpDownMinutes_ValueChanged(object sender, EventArgs e)
        {
            OnMinutesChanged();
        }

        private void numericUpDownSeconds_ValueChanged(object sender, EventArgs e)
        {
            OnSecondsChanged();
        }
    }
}
