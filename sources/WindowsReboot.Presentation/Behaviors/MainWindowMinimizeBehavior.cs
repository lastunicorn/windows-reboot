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
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Application.MainArea.MinimizeApplication;
using DustInTheWind.WinFormsAdditions;
using MediatR;

namespace DustInTheWind.WindowsReboot.Presentation.Behaviors
{
    public class MainWindowMinimizeBehavior : IFormBehaviour
    {
        private readonly IMediator mediator;
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

        public MainWindowMinimizeBehavior(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        private void Start()
        {
            form.SizeChanged += HandleMainWindowSizeChanged;
        }

        private void Stop()
        {
            form.SizeChanged -= HandleMainWindowSizeChanged;
        }

        private void HandleMainWindowSizeChanged(object sender, EventArgs eventArgs)
        {
            if (form.WindowState != FormWindowState.Minimized)
                return;

            MinimizeApplicationRequest request = new MinimizeApplicationRequest();
            _ = mediator.Send(request);
        }
    }
}