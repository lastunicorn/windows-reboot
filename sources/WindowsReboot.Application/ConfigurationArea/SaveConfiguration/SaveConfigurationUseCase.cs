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
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ConfigurationArea.SaveConfiguration
{
    internal class SaveConfigurationUseCase : IRequestHandler<SaveConfigurationRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private readonly IConfigStorage configuration;

        public SaveConfigurationUseCase(IUserInterface userInterface, ExecutionTimer executionTimer, ExecutionPlan executionPlan, IConfigStorage configuration)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task Handle(SaveConfigurationRequest request, CancellationToken cancellationToken)
        {
            configuration.ActionTime = executionTimer.ScheduleTime;

            configuration.ActionType = executionPlan.ActionType;
            configuration.ForceClosingPrograms = executionPlan.ForceOption == ForceOption.Yes;

            configuration.Save();

            userInterface.DisplayMessage("The configuration was saved.");

            return Task.CompletedTask;
        }
    }
}