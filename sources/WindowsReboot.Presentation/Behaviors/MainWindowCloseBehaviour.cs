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
using System.Windows.Forms;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Behaviors
{
    public class MainWindowCloseBehaviour : IFormBehaviour
    {
        private readonly IUserInterface userInterface;
        private readonly IMediator mediator;
        private readonly EventBus eventBus;

        private volatile bool closingFromBusiness;
        private Form form;

        public Form Form
        {
            get => form;
            set
            {
                if (form != null)
                    Stop();

                form = value;

                if (form != null)
                    Start();
            }
        }

        public MainWindowCloseBehaviour(IUserInterface userInterface, IMediator mediator, EventBus eventBus)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        private void Start()
        {
            form.FormClosing += HandleMainWindowFormClosing;

            eventBus.Subscribe<ApplicationClosingEvent>(HandleApplicationClosingEvent);
            eventBus.Subscribe<ApplicationCloseRevokedEvent>(HandleApplicationCloseRevokedEvent);
        }

        private void Stop()
        {
            form.FormClosing -= HandleMainWindowFormClosing;

            eventBus.Unsubscribe<ApplicationClosingEvent>(HandleApplicationClosingEvent);
            eventBus.Unsubscribe<ApplicationCloseRevokedEvent>(HandleApplicationCloseRevokedEvent);
        }

        private void HandleMainWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!closingFromBusiness)
                {
                    e.Cancel = true;

                    CloseApplicationRequest request = new CloseApplicationRequest();
                    mediator.Send(request);
                }
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        private void HandleApplicationClosingEvent(ApplicationClosingEvent ev)
        {
            closingFromBusiness = true;
        }

        private void HandleApplicationCloseRevokedEvent(ApplicationCloseRevokedEvent ev)
        {
            closingFromBusiness = false;
        }
    }
}