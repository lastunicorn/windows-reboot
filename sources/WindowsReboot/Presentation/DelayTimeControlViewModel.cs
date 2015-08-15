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

namespace DustInTheWind.WindowsReboot.Presentation
{
    class DelayTimeControlViewModel : ViewModelBase
    {
        private int hours;
        private int seconds;
        private int minutes;

        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                OnPropertyChanged("Seconds");
            }
        }

        public TimeSpan GetTime()
        {
            return new TimeSpan(Hours, Minutes, Seconds);
        }

        public void Clear()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }
    }
}
