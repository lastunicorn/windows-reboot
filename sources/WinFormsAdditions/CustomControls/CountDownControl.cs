using System;
using System.Windows.Forms;

namespace DustInTheWind.WinFormsAdditions.CustomControls
{
    public partial class CountDownControl : UserControl
    {
        private TimeSpan? timerTime;
        private DateTime startTime;

        public TimeSpan? TimerTime
        {
            get => timerTime;
            set
            {
                timerTime = value;
                startTime = DateTime.UtcNow;

                UpdateDisplayedValue();
                timer1.Enabled = timerTime != null;
            }
        }

        public CountDownControl()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDisplayedValue();
        }

        private void UpdateDisplayedValue()
        {
            TimeSpan? totalTime = timerTime;

            if (totalTime == null)
            {
                labelTimer.Text = TimerText.Empty.ToString();
            }
            else
            {
                TimeSpan passedTime = DateTime.UtcNow - startTime;
                TimeSpan remainingTime = totalTime.Value - passedTime;

                if (remainingTime < TimeSpan.Zero)
                {
                    timerTime = null;
                    labelTimer.Text = TimerText.Empty.ToString();
                    timer1.Enabled = false;
                }
                else
                {
                    labelTimer.Text = ((TimerText)remainingTime).ToString();
                }
            }

            //labelTimer.Text = totalTime == null
            //    ? TimerText.Empty.ToString()
            //    : ((TimerText)totalTime.Value).ToString();
        }
    }
}