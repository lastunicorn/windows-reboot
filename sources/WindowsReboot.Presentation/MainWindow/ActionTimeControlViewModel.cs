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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.PresentActionTimeSettings;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetDailyTime;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedDate;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedTime;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetHours;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetMinutes;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetScheduleType;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetSeconds;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTimeControlViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private ScheduleTimeType scheduleTimeType;
        private DateTime fixedDate;
        private TimeSpan fixedTime;
        private int delayHours;
        private int delayMinutes;
        private int delaySeconds;
        private TimeSpan dailyTime;
        private bool enabled;

        public ScheduleTimeType ScheduleTimeType
        {
            get => scheduleTimeType;
            set
            {
                scheduleTimeType = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetScheduleTypeRequest request = new SetScheduleTypeRequest
                    {
                        ScheduleType = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public DateTime FixedDate
        {
            get => fixedDate;
            set
            {
                fixedDate = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetFixedDateRequest request = new SetFixedDateRequest
                    {
                        Date = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public TimeSpan FixedTime
        {
            get => fixedTime;
            set
            {
                fixedTime = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetFixedTimeRequest request = new SetFixedTimeRequest
                    {
                        Time = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public int DelayHours
        {
            get => delayHours;
            set
            {
                delayHours = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetHoursRequest request = new SetHoursRequest
                    {
                        Hours = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public int DelayMinutes
        {
            get => delayMinutes;
            set
            {
                delayMinutes = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetMinutesRequest request = new SetMinutesRequest
                    {
                        Minutes = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public int DelaySeconds
        {
            get => delaySeconds;
            set
            {
                delaySeconds = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetSecondsRequest request = new SetSecondsRequest
                    {
                        Seconds = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public TimeSpan DailyTime
        {
            get => dailyTime;
            set
            {
                dailyTime = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    SetDailyTimeRequest request = new SetDailyTimeRequest
                    {
                        Time = value
                    };
                    _ = mediator.Send(request);
                }
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        public ActionTimeControlViewModel(IMediator mediator, EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            eventBus.Subscribe<ScheduleChangedEvent>(HandleTimerTimeChangedEvent);
            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);

            Initialize();
        }

        private async void Initialize()
        {
            PresentActionTimeSettingsRequest request = new PresentActionTimeSettingsRequest();
            PresentActionTimeSettingsResponse response = await mediator.Send(request);

            RunInInitializeMode(() =>
            {
                FixedDate = response.DateTime.Date;
                FixedTime = response.DateTime.TimeOfDay;

                DailyTime = response.TimeOfDay;

                DelayHours = response.Hours;
                DelayMinutes = response.Minutes;
                DelaySeconds = response.Seconds;

                ScheduleTimeType = response.Type;

                Enabled = response.IsAllowedToChange;

            });
        }

        private Task HandleTimerTimeChangedEvent(ScheduleChangedEvent ev, CancellationToken cancellationToken)
        {
            RunInInitializeMode(() =>
            {
                FixedDate = ev.DateTime.Date;
                FixedTime = ev.DateTime.TimeOfDay;

                DailyTime = ev.TimeOfDay;

                DelayHours = ev.Hours;
                DelayMinutes = ev.Minutes;
                DelaySeconds = ev.Seconds;

                ScheduleTimeType = ev.Type;

                Enabled = ev.IsAllowedToChange;
            });

            return Task.CompletedTask;
        }

        private void HandleTimerStartedEvent(TimerStartedEvent ev)
        {
            Dispatch(() =>
            {
                RunInInitializeMode(() => Enabled = false);
            });
        }

        private void HandleTimerStoppedEvent(TimerStoppedEvent ev)
        {
            Dispatch(() =>
            {
                RunInInitializeMode(() => Enabled = true);
            });
        }
    }
}