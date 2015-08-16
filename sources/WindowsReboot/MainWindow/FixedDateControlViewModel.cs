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
using DustInTheWind.WindowsReboot.Presentation;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    class FixedDateControlViewModel : ViewModelBase
    {
        private DateTime date;
        private DateTime time;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public FixedDateControlViewModel()
        {
            date = DateTime.Today;
            time = DateTime.Now;
        }

        public DateTime GetFullTime()
        {
            return Date.Add(Time.TimeOfDay);
        }

        public void Clear()
        {
            DateTime now = DateTime.Now;

            Date = now.Date;
            Time = now;
        }
    }
}
