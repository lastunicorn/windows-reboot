﻿// Windows Reboot
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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Domain;

namespace DustInTheWind.WindowsReboot.Presentation.OtherWindows
{
    /// <summary>
    /// Displays information about the author, the version, etc.
    /// </summary>
    public partial class AboutForm : Form
    {
        private readonly Assembly currentAssembly = Assembly.GetEntryAssembly();

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            labelTitle.Text = string.Format("{0} {1}", System.Windows.Forms.Application.ProductName, VersionUtil.GetVersionToString());
            labelVersion.Text = VersionUtil.GetVersion().ToString();

            labelAuthor.Text = System.Windows.Forms.Application.CompanyName;
            labelDate.Text = "January 2024";
            textBoxDescription.Text = AssemblyDescription;
        }

        private string AssemblyDescription
        {
            get
            {
                object[] attributes = currentAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                return attributes.Length == 0
                    ? string.Empty
                    : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private void HandleButtonOkayClick(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            if (Owner != null && StartPosition == FormStartPosition.CenterParent && !Modal)
            {
                int x = Owner.Location.X + Owner.Width / 2 - Width / 2;
                int y = Owner.Location.Y + Owner.Height / 2 - Height / 2;

                Location = new Point(x, y);
            }
        }
    }
}