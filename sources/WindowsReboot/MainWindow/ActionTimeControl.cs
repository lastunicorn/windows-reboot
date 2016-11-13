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
using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal partial class ActionTimeControl : UserControl
    {
        private ActionTimeControlViewModel viewModel;

        public ActionTimeControlViewModel ViewModel
        {
            get { return viewModel; }
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

                    tabControlActionTime.SelectedIndex = ToTabIndex(viewModel.ScheduleTimeType);
                    viewModel.PropertyChanged += HandleViewModelPropertyChanged;
                }
            }
        }

        private void HandleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ScheduleTimeType")
            {
                tabControlActionTime.SelectedIndex = ToTabIndex(viewModel.ScheduleTimeType);
            }
        }

        public ActionTimeControl()
        {
            InitializeComponent();
        }

        private void tabControlActionTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.ScheduleTimeType = FromTabIndex(tabControlActionTime.SelectedIndex);
        }

        private static int ToTabIndex(ScheduleTimeType scheduleTimeType)
        {
            switch (scheduleTimeType)
            {
                case ScheduleTimeType.FixedDate:
                    return 0;

                case ScheduleTimeType.Daily:
                    return 1;

                case ScheduleTimeType.Delay:
                    return 2;

                case ScheduleTimeType.Immediate:
                    return 3;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ScheduleTimeType FromTabIndex(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    return ScheduleTimeType.FixedDate;

                case 1:
                    return ScheduleTimeType.Daily;

                case 2:
                    return ScheduleTimeType.Delay;

                case 3:
                    return ScheduleTimeType.Immediate;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}