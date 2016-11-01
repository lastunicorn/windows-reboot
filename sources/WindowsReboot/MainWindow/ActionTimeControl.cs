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
                    fixedDateControl1.DataBindings.Clear();
                    delayTimeControl1.DataBindings.Clear();
                    dailyControl1.DataBindings.Clear();

                    viewModel.PropertyChanged += HandleViewModelPropertyChanged;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    fixedDateControl1.Bind(x => x.FullTime, viewModel, x => x.FixedDateTime, false, DataSourceUpdateMode.OnPropertyChanged);
                    delayTimeControl1.Bind(x => x.Hours, viewModel, x => x.DelayHours, false, DataSourceUpdateMode.OnPropertyChanged);
                    delayTimeControl1.Bind(x => x.Minutes, viewModel, x => x.DelayMinutes, false, DataSourceUpdateMode.OnPropertyChanged);
                    delayTimeControl1.Bind(x => x.Seconds, viewModel, x => x.DelaySeconds, false, DataSourceUpdateMode.OnPropertyChanged);
                    dailyControl1.Bind(x => x.Time, viewModel, x => x.DailyTime, false, DataSourceUpdateMode.OnPropertyChanged);

                    viewModel.PropertyChanged += HandleViewModelPropertyChanged;
                }
            }
        }

        private void HandleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TaskTimeType")
            {
                tabControlActionTime.SelectedIndex = ToTabIndex(viewModel.TaskTimeType);
            }
        }

        public ActionTimeControl()
        {
            InitializeComponent();
        }

        private void tabControlActionTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.TaskTimeType = FromTabIndex(tabControlActionTime.SelectedIndex);
        }

        private static int ToTabIndex(TaskTimeType taskTimeType)
        {
            switch (taskTimeType)
            {
                case TaskTimeType.FixedDate:
                    return 0;

                case TaskTimeType.Daily:
                    return 1;

                case TaskTimeType.Delay:
                    return 2;

                case TaskTimeType.Immediate:
                    return 3;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static TaskTimeType FromTabIndex(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    return TaskTimeType.FixedDate;

                case 1:
                    return TaskTimeType.Daily;

                case 2:
                    return TaskTimeType.Delay;

                case 3:
                    return TaskTimeType.Immediate;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}