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

namespace DustInTheWind.WindowsReboot.UI.View
{
    /// <summary>
    /// Displayes information about the author, the version, etc.
    /// </summary>
    public partial class AboutForm : Form
    {
        private Assembly currentAssembly = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            this.labelTitle.Text = Application.ProductName + " " + VersionUtil.GetVersionToString();
            this.labelVersion.Text = VersionUtil.GetVersion().ToString();
            
            this.labelAuthor.Text = Application.CompanyName;
            this.labelDate.Text = new DateTime(2009, 4, 5).ToString("MMMM yyyy");
            this.textBoxDescription.Text = this.AssemblyDescription;
        }

        private string AssemblyDescription
        {
            get
            {
                object[] attributes = this.currentAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
