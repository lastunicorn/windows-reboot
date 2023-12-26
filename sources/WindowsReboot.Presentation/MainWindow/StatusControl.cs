// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
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
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    internal partial class StatusControl : UserControl
    {
        private StatusControlViewModel viewModel;

        public StatusControlViewModel ViewModel
        {
            get => viewModel;
            set
            {
                if (viewModel != null)
                {
                    labelCurrentTime.DataBindings.Clear();
                    labelActionTime.DataBindings.Clear();
                    labelTimer.DataBindings.Clear();
                }

                viewModel = value;

                if (viewModel != null)
                {
                    Binding currentTimeBinding = labelCurrentTime.Bind(x => x.Text, viewModel, x => x.CurrentTime, false, DataSourceUpdateMode.Never);
                    currentTimeBinding.Format += HandleCurrentTimeFormat;

                    Binding actionTimeBinding = labelActionTime.Bind(x => x.Text, viewModel, x => x.ActionTime, false, DataSourceUpdateMode.Never);
                    actionTimeBinding.Format += HandleActionTimeFormat;

                    Binding timerBinding = labelTimer.Bind(x => x.Text, viewModel, x => x.TimerTime, false, DataSourceUpdateMode.Never);
                    timerBinding.Format += HandleTimerFormat;
                }
            }
        }

        public StatusControl()
        {
            InitializeComponent();
        }

        private static void HandleCurrentTimeFormat(object sender, ConvertEventArgs e)
        {
            if (!(e.Value is DateTime) || e.DesiredType != typeof(string))
                return;

            DateTime dateTime = (DateTime)e.Value;

            e.Value = string.Format("{0}  :  {1}", dateTime.ToLongDateString(), dateTime.ToLongTimeString());
        }

        private static void HandleActionTimeFormat(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(string))
                return;

            if (e.Value == null)
            {
                e.Value = string.Empty;
            }
            else
            {
                if (e.Value is DateTime)
                {
                    DateTime dateTime = (DateTime)e.Value;
                    e.Value = string.Format("{0}  :  {1}", dateTime.ToLongDateString(), dateTime.ToLongTimeString());
                }
            }
        }

        private static void HandleTimerFormat(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(string))
                return;

            if (e.Value == null)
            {
                e.Value = TimerText.Empty.ToString();
            }
            else
            {
                if (e.Value is TimeSpan time)
                {
                    e.Value = ((TimerText)time).ToString();
                }
            }
        }
    }
}