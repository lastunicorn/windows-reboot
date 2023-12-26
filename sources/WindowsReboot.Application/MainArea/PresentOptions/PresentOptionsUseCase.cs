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
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.PresentationAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.PresentOptions
{
    internal class PresentOptionsUseCase : IRequestHandler<PresentOptionsRequest>
    {
        private readonly IConfigStorage configStorage;
        private readonly IUserInterface userInterface;

        public PresentOptionsUseCase(IConfigStorage configStorage, IUserInterface userInterface)
        {
            this.configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        public Task Handle(PresentOptionsRequest request, CancellationToken cancellationToken)
        {
            ApplicationOptions modifiedOptions = ShowApplicationOptions();

            if (modifiedOptions != null)
                UpdateConfigStorage(modifiedOptions);

            return Task.CompletedTask;
        }

        private ApplicationOptions ShowApplicationOptions()
        {
            ApplicationOptions initialOptions = new ApplicationOptions
            {
                CloseToTray = configStorage.CloseToTray,
                MinimizeToTray = configStorage.MinimizeToTray,
                AutoStart = configStorage.StartTimerAtApplicationStart
            };

            return userInterface.DisplayOptions(initialOptions);
        }

        private void UpdateConfigStorage(ApplicationOptions modifiedOptions)
        {
            configStorage.CloseToTray = modifiedOptions.CloseToTray;
            configStorage.MinimizeToTray = modifiedOptions.MinimizeToTray;
            configStorage.StartTimerAtApplicationStart = modifiedOptions.AutoStart;

            configStorage.Save();
        }
    }
}