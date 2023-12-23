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
using System.Linq;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.ActionTypeArea.ConfigureActionType;
using DustInTheWind.WindowsReboot.Application.ActionTypeArea.ConfigureForceOption;
using DustInTheWind.WindowsReboot.Application.ActionTypeArea.ConfigureWarningOption;
using DustInTheWind.WindowsReboot.Application.ActionTypeArea.PresentActionTypeConfiguration;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    public class ActionTypeControlViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private ActionTypeItem[] actionTypes;
        private ActionTypeItem selectedActionType;
        private bool isForceEnabled;
        private bool force;
        private bool isWarningEnabled;
        private bool enabled;

        public ActionTypeItem[] ActionTypes
        {
            get => actionTypes;
            set
            {
                actionTypes = value;
                OnPropertyChanged();
            }
        }

        public ActionTypeItem SelectedActionType
        {
            get => selectedActionType;
            set
            {
                if (selectedActionType == value)
                    return;

                selectedActionType = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    ConfigureActionTypeRequest request = new ConfigureActionTypeRequest
                    {
                        ActionType = value.Value
                    };

                    _ = mediator.Send(request);
                }
            }
        }

        public bool Force
        {
            get => force;
            set
            {
                force = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    ConfigureForceOptionRequest request = new ConfigureForceOptionRequest
                    {
                        ForceOption = value ? ForceOption.Yes : ForceOption.No
                    };

                    _ = mediator.Send(request);
                }
            }
        }

        public bool IsForceEnabled
        {
            get => isForceEnabled;
            set
            {
                isForceEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsWarningEnable
        {
            get => isWarningEnabled;
            set
            {
                isWarningEnabled = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                {
                    ConfigureWarningRequest request = new ConfigureWarningRequest
                    {
                        ActivateWarning = value
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

        public ActionTypeControlViewModel(IMediator mediator, EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            ActionTypes = Enum.GetValues(typeof(ActionType))
                .Cast<ActionType>()
                .Select(x => new ActionTypeItem(x))
                .ToArray();

            eventBus.Subscribe<WarningTimeChangedEvent>(HandleTimerWarningTimeChangedEvent);
            eventBus.Subscribe<TimerStartedEvent>(HandleTimerStartedEvent);
            eventBus.Subscribe<TimerStoppedEvent>(HandleTimerStoppedEvent);

            eventBus.Subscribe<ForceOptionChangedEvent>(HandleForceOptionChangedEvent);
            eventBus.Subscribe<ActionTypeChangedEvent>(HandleActionTypeChangedEvent);

            Initialize();
        }

        private async void Initialize()
        {
            PresentActionTypeConfigurationRequest request = new PresentActionTypeConfigurationRequest();
            PresentActionTypeConfigurationResponse response = await mediator.Send(request);

            RunInInitializeMode(() =>
            {
                SelectedActionType = ActionTypes
                    .First(x => x.Value == response.ActionType);

                IsWarningEnable = response.IsWarningEnabled;
                Force = response.ForceOption == ForceOption.Yes;
                IsForceEnabled = response.ForceOption != ForceOption.NotApplicable && Enum.IsDefined(typeof(ForceOption), response.ForceOption);
                Enabled = true;
            });
        }

        private void HandleTimerWarningTimeChangedEvent(WarningTimeChangedEvent ev)
        {
            Dispatch(() =>
            {
                RunInInitializeMode(() =>
                {
                    IsWarningEnable = ev.Time != null;
                });
            });
        }

        private void HandleTimerStartedEvent(TimerStartedEvent ev)
        {
            Dispatch(() =>
            {
                Enabled = false;
            });
        }

        private void HandleTimerStoppedEvent(TimerStoppedEvent ev)
        {
            Dispatch(() =>
            {
                Enabled = true;
            });
        }

        private void HandleForceOptionChangedEvent(ForceOptionChangedEvent ev)
        {
            RunInInitializeMode(() =>
            {
                Force = ev.ForceOption == ForceOption.Yes;
                IsForceEnabled = ev.ForceOption != ForceOption.NotApplicable && Enum.IsDefined(typeof(ForceOption), ev.ForceOption);
            });
        }

        private void HandleActionTypeChangedEvent(ActionTypeChangedEvent ev)
        {
            RunInInitializeMode(() =>
            {
                SelectedActionType = ActionTypes
                    .First(x => x.Value == ev.ActionType);
            });
        }
    }
}