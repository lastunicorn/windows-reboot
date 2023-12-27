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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.PresentActionTimeSettings;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetDailySchedule;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetDelaySchedule;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedDateSchedule;
using DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetSchedule;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTimeControlViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private ScheduleType scheduleType;
        private DateTime fixedDate;
        private TimeSpan fixedTime;
        private int delayHours;
        private int delayMinutes;
        private int delaySeconds;
        private TimeSpan dailyTime;
        private bool enabled;

        public ScheduleType ScheduleType
        {
            get => scheduleType;
            set
            {
                scheduleType = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    IRequest request = CreateSetScheduleRequest();
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
                    IRequest request = CreateSetScheduleRequest();
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
                    IRequest request = CreateSetScheduleRequest();
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
                    IRequest request = CreateSetScheduleRequest();
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
                    IRequest request = CreateSetScheduleRequest();
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
                    IRequest request = CreateSetScheduleRequest();
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
                    IRequest request = CreateSetScheduleRequest();
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

                ScheduleType = response.Type;

                Enabled = response.IsAllowedToChange;
            });
        }

        private Task HandleTimerTimeChangedEvent(ScheduleChangedEvent ev, CancellationToken cancellationToken)
        {
            RunInInitializeMode(() =>
            {
                ScheduleType = ev.Type;

                switch (ev.Type)
                {
                    case ScheduleType.FixedDate:
                        FixedDate = ev.DateTime.Date;
                        FixedTime = ev.DateTime.TimeOfDay;
                        break;

                    case ScheduleType.Daily:
                        DailyTime = ev.TimeOfDay;
                        break;

                    case ScheduleType.Delay:
                        DelayHours = ev.Hours;
                        DelayMinutes = ev.Minutes;
                        DelaySeconds = ev.Seconds;
                        break;

                    case ScheduleType.Immediate:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

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

        private IRequest CreateSetScheduleRequest()
        {
            switch (scheduleType)
            {
                case ScheduleType.FixedDate:
                    return new SetFixedDateScheduleRequest
                    {
                        DateTime = FixedDate
                    };

                case ScheduleType.Daily:
                    return new SetDailyScheduleRequest
                    {
                        TimeOfDay = FixedTime
                    };

                case ScheduleType.Delay:
                    return new SetDelayScheduleRequest
                    {
                        Hours = DelayHours,
                        Minutes = DelayMinutes,
                        Seconds = DelaySeconds
                    };

                case ScheduleType.Immediate:
                    return new SetImmediateScheduleRequest();

                default:
                    throw new Exception("Invalid schedule type.");
            }
        }
    }
}