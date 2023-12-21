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

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    internal partial class DailyTabContent : UserControl
    {
        public TimeSpan Time
        {
            get { return dateTimePickerDailyTime.Value.TimeOfDay; }
            set
            {
                dateTimePickerDailyTime.Value = DateTime.Today.Add(value);
                OnTimeChanged();
            }
        }

        public event EventHandler TimeChanged;

        public DailyTabContent()
        {
            InitializeComponent();
        }

        protected virtual void OnTimeChanged()
        {
            EventHandler handler = TimeChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void dateTimePickerDailyTime_ValueChanged(object sender, EventArgs e)
        {
            OnTimeChanged();
        }
    }
}