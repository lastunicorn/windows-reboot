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

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.PresentActionTimeSettings
{
    internal class PresentActionTimeSettingsUseCase : IRequestHandler<PresentActionTimeSettingsRequest, PresentActionTimeSettingsResponse>
    {
        private readonly ExecutionTimer executionTimer;

        public PresentActionTimeSettingsUseCase(ExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task<PresentActionTimeSettingsResponse> Handle(PresentActionTimeSettingsRequest request, CancellationToken cancellationToken)
        {
            PresentActionTimeSettingsResponse response = new PresentActionTimeSettingsResponse
            {
                DateTime = executionTimer.Schedule.DateTime,
                TimeOfDay = executionTimer.Schedule.TimeOfDay,

                Hours = executionTimer.Schedule.Hours,
                Minutes = executionTimer.Schedule.Minutes,
                Seconds = executionTimer.Schedule.Seconds,

                Type = executionTimer.Schedule.Type,
                IsAllowedToChange = !executionTimer.IsRunning
            };

            return Task.FromResult(response);
        }
    }
}