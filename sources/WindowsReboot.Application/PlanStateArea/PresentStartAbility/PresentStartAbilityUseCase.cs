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
using DustInTheWind.WindowsReboot.Ports.WorkerAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.PlanStateArea.PresentStartAbility
{
    internal class PresentStartAbilityUseCase : IRequestHandler<PresentStartAbilityRequest, PresentStartAbilityResponse>
    {
        private readonly IExecutionTimer executionTimer;

        public PresentStartAbilityUseCase(IExecutionTimer executionTimer)
        {
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
        }

        public Task<PresentStartAbilityResponse> Handle(PresentStartAbilityRequest request, CancellationToken cancellationToken)
        {
            PresentStartAbilityResponse response = new PresentStartAbilityResponse
            {
                CanStart = !executionTimer.IsTimerRunning()
            };

            return Task.FromResult(response);
        }
    }
}