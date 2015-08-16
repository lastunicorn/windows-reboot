// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
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
using System.Reflection;
using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Core;

namespace DustInTheWind.WindowsReboot.OtherWindows
{
    /// <summary>
    /// Displayes information about the author, the version, etc.
    /// </summary>
    public partial class AboutForm : Form
    {
        private readonly Assembly currentAssembly = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            labelTitle.Text = string.Format("{0} {1}", Application.ProductName, VersionUtil.GetVersionToString());
            labelVersion.Text = VersionUtil.GetVersion().ToString();

            labelAuthor.Text = Application.CompanyName;
            labelDate.Text = new DateTime(2009, 4, 5).ToString("MMMM yyyy");
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
    }
}
