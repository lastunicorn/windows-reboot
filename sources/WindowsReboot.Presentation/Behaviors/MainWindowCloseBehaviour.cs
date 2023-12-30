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
using System.Diagnostics;
using System.Windows.Forms;
using DustInTheWind.EventBusEngine;
using DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Behaviors
{
    public class MainWindowCloseBehaviour : IFormBehaviour
    {
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

        public MainWindowCloseBehaviour(IMediator mediator, EventBus eventBus)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        private void Start()
        {
            form.FormClosing += HandleMainWindowFormClosing;

            eventBus.Subscribe<ApplicationClosingEvent>(HandleApplicationClosingEvent);
        }

        private void Stop()
        {
            form.FormClosing -= HandleMainWindowFormClosing;

            eventBus.Unsubscribe<ApplicationClosingEvent>(HandleApplicationClosingEvent);
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
                Form mainForm = (Form)Control.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
                MessageBox.Show(mainForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleApplicationClosingEvent(ApplicationClosingEvent ev)
        {
            closingFromBusiness = true;
        }
    }
}