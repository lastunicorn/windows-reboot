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
    internal partial class FixedDateControl : UserControl
    {
        public DateTime Date
        {
            get { return dateTimePickerFixedDate.Value; }
            set
            {
                dateTimePickerFixedDate.Value = value;
                OnDateChanged();
                OnFullTimeChanged();
            }
        }

        public DateTime Time
        {
            get { return dateTimePickerFixedTime.Value; }
            set
            {
                dateTimePickerFixedTime.Value = value;
                OnTimeChanged();
                OnFullTimeChanged();
            }
        }

        public DateTime FullTime
        {
            get
            {
                return Date.Add(Time.TimeOfDay);
            }
            set
            {
                dateTimePickerFixedDate.Value = value.Date;
                dateTimePickerFixedTime.Value = value;
                OnDateChanged();
                OnTimeChanged();
                OnFullTimeChanged();
            }
        }

        public event EventHandler DateChanged;
        public event EventHandler TimeChanged;
        public event EventHandler FullTimeChanged;

        public FixedDateControl()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            DateTime now = DateTime.Now;

            Date = now.Date;
            Time = now;
        }

        protected virtual void OnDateChanged()
        {
            EventHandler handler = DateChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnTimeChanged()
        {
            EventHandler handler = TimeChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnFullTimeChanged()
        {
            EventHandler handler = FullTimeChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void dateTimePickerFixedDate_ValueChanged(object sender, EventArgs e)
        {
            OnDateChanged();
            OnFullTimeChanged();
        }

        private void dateTimePickerFixedTime_ValueChanged(object sender, EventArgs e)
        {
            OnTimeChanged();
            OnFullTimeChanged();
        }
    }
}
