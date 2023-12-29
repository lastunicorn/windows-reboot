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
using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Domain.Scheduling;
using DustInTheWind.WinFormsAdditions;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    internal partial class ActionTimeControl : UserControl
    {
        private ActionTimeControlViewModel viewModel;

        public ActionTimeControlViewModel ViewModel
        {
            get => viewModel;
            set
            {
                if (viewModel != null)
                {
                    DataBindings.Clear();
                    fixedDateControl1.DataBindings.Clear();
                    delayTimeControl1.DataBindings.Clear();
                    dailyControl1.DataBindings.Clear();

                    viewModel.PropertyChanged += HandleViewModelPropertyChanged;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    this.Bind(x => x.Enabled, viewModel, x => x.Enabled, false, DataSourceUpdateMode.Never);

                    fixedDateControl1.Bind(x => x.Date, viewModel, x => x.FixedDate, false, DataSourceUpdateMode.OnPropertyChanged);
                    fixedDateControl1.Bind(x => x.Time, viewModel, x => x.FixedTime, false, DataSourceUpdateMode.OnPropertyChanged);
                    dailyControl1.Bind(x => x.Time, viewModel, x => x.DailyTime, false, DataSourceUpdateMode.OnPropertyChanged);
                    delayTimeControl1.Bind(x => x.Hours, viewModel, x => x.DelayHours, false, DataSourceUpdateMode.OnPropertyChanged);
                    delayTimeControl1.Bind(x => x.Minutes, viewModel, x => x.DelayMinutes, false, DataSourceUpdateMode.OnPropertyChanged);
                    delayTimeControl1.Bind(x => x.Seconds, viewModel, x => x.DelaySeconds, false, DataSourceUpdateMode.OnPropertyChanged);

                    tabControlActionTime.SelectedIndex = ToTabIndex(viewModel.ScheduleType);
                    viewModel.PropertyChanged += HandleViewModelPropertyChanged;
                }
            }
        }

        private void HandleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ScheduleType") 
                tabControlActionTime.SelectedIndex = ToTabIndex(viewModel.ScheduleType);
        }

        public ActionTimeControl()
        {
            InitializeComponent();
        }

        private void tabControlActionTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.ScheduleType = FromTabIndex(tabControlActionTime.SelectedIndex);
        }

        private static int ToTabIndex(ScheduleType scheduleType)
        {
            switch (scheduleType)
            {
                case ScheduleType.FixedDate:
                    return 0;

                case ScheduleType.Daily:
                    return 1;

                case ScheduleType.Delay:
                    return 2;

                case ScheduleType.Immediate:
                    return 3;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ScheduleType FromTabIndex(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    return ScheduleType.FixedDate;

                case 1:
                    return ScheduleType.Daily;

                case 2:
                    return ScheduleType.Delay;

                case 3:
                    return ScheduleType.Immediate;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}