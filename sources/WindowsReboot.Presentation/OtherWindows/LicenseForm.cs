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

using System.Windows.Forms;
using DustInTheWind.WindowsReboot.Presentation.Properties;

namespace DustInTheWind.WindowsReboot.Presentation.OtherWindows
{
    /// <summary>
    /// This form displays the license text.
    /// </summary>
    public partial class LicenseForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseForm"/> class.
        /// </summary>
        public LicenseForm()
        {
            InitializeComponent();

            textBoxDescription.Text = Resources.License;
        }
    }
}
