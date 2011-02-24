using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DustInTheWind.WindowsReboot.UI
{
    internal class WindowsRebootModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ActionTimeType actionTimeType;
        public ActionTimeType ActionTimeType
        {
            get { return actionTimeType; }
            set
            {
                actionTimeType = value;
                OnPropertyChanged("ActionTimeType");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        private int hour;
        public int Hour
        {
            get { return hour; }
            set
            {
                hour = value;
                OnPropertyChanged("Hour");
            }
        }

        private int minutes;
        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        private int seconds;
        public int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                OnPropertyChanged("Seconds");
            }
        }

        private ActionType actionType;
        public ActionType ActionType
        {
            get { return actionType; }
            set
            {
                actionType = value;
                OnPropertyChanged("ActionType");
            }
        }

        private bool forceAction;
        public bool ForceAction
        {
            get { return forceAction; }
            set
            {
                forceAction = value;
                OnPropertyChanged("ForceAction");
            }
        }

        private bool displayWarning;
        public bool DisplayWarning
        {
            get { return displayWarning; }
            set
            {
                displayWarning = value;
                OnPropertyChanged("DisplayWarning");
            }
        }

        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        private DateTime? actionTime;
        public DateTime? ActionTime
        {
            get { return actionTime; }
            set
            {
                actionTime = value;
                OnPropertyChanged("ActionTime");
            }
        }

        private TimeSpan? timerValue;
        public TimeSpan? TimerValue
        {
            get { return timerValue; }
            set
            {
                timerValue = value;
                OnPropertyChanged("TimerValue");
            }
        }
    }
}
