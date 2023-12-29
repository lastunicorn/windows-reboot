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
using DustInTheWind.WindowsReboot.Domain;
using MediatR;
using ScheduleType = DustInTheWind.WindowsReboot.Domain.ScheduleType;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.PresentActionTimeSettings
{
    internal class PresentActionTimeSettingsUseCase : IRequestHandler<PresentActionTimeSettingsRequest, PresentActionTimeSettingsResponse>
    {
        private readonly ExecutionPlan executionPlan;

        public PresentActionTimeSettingsUseCase(ExecutionPlan executionPlan)
        {
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
        }

        public Task<PresentActionTimeSettingsResponse> Handle(PresentActionTimeSettingsRequest request, CancellationToken cancellationToken)
        {
            PresentActionTimeSettingsResponse response = new PresentActionTimeSettingsResponse
            {
                IsAllowedToChange = !executionPlan.IsRunning
            };

            if (executionPlan.Schedule is FixedDateSchedule fixedDateSchedule)
            {
                response.DateTime = fixedDateSchedule.DateTime;
                response.Type = ScheduleType.FixedDate;
            }

            if (executionPlan.Schedule is DailySchedule dailySchedule)
            {
                response.DateTime = DateTime.Now + TimeSpan.FromHours(1);
                response.TimeOfDay = dailySchedule.TimeOfDay;
                response.Type = ScheduleType.Daily;
            }

            if (executionPlan.Schedule is DelaySchedule delaySchedule)
            {
                response.DateTime = DateTime.Now + TimeSpan.FromHours(1);
                response.Hours = delaySchedule.Hours;
                response.Minutes = delaySchedule.Minutes;
                response.Seconds = delaySchedule.Seconds;
                response.Type = ScheduleType.Delay;
            }

            if (executionPlan.Schedule is ImmediateSchedule)
            {
                response.DateTime = DateTime.Now + TimeSpan.FromHours(1);
                response.Type = ScheduleType.Immediate;
            }

            return Task.FromResult(response);
        }
    }
}