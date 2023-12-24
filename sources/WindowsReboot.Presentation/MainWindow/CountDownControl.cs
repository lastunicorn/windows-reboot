using System;
using System.Windows.Forms;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public partial class CountDownControl : UserControl
    {
        private TimeSpan? timerTime;

        public TimeSpan? TimerTime
        {
            get => timerTime;
            set
            {
                timerTime = value;

                UpdateDisplayedValue();
                timer1.Enabled = timerTime != null;
            }
        }

        public CountDownControl()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            UpdateDisplayedValue();
        }

        private void UpdateDisplayedValue()
        {
            labelTimer.Text = timerTime == null
                ? TimerText.Empty.ToString()
                : ((TimerText)timerTime.Value).ToString();
        }
    }
}