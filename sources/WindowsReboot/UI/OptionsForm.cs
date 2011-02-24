// Windows Reboot
// Copyright (C) 2009 Dust in the Wind
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
using DustInTheWind.WindowsReboot.Config;
using MVCSharp.Winforms;
using DustInTheWind.WindowsReboot.UI;
using DustInTheWind.WindowsReboot.Properties;
using MVCSharp.Core.Configuration.Views;
using MVCSharp.Winforms.Configuration;

namespace DustInTheWind.WindowsReboot
{
    /// <summary>
    /// Displaies some options that the user can set.
    /// </summary>
    //[ViewAttribute(typeof(MainTask), MainTask.Options)]
    [WinformsView(typeof(OptionsTask), OptionsTask.Options, ShowModal = true)]
    internal partial class OptionsForm : WinFormView<OptionsPresenter>, IOptionsView
    {
        //private WindowsRebootConfigSection configSection;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsForm"/> class.
        /// </summary>
        public OptionsForm()
        {
            InitializeComponent();
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="OptionsForm"/> class with
        ///// the configurationn object.
        ///// </summary>
        ///// <param name="configSection">An instance of <see cref="WindowsRebootConfigSection"/> class that contains the values that should be displaied.</param>
        //public OptionsForm(WindowsRebootConfigSection configSection)
        //{
        //    InitializeComponent();

        //    if (configSection == null)
        //        configSection = new WindowsRebootConfigSection();

        //    this.configSection = configSection;
        //}

        #endregion

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            Controller.ViewShown();

            //this.checkBoxCloseToTray.Checked = this.configSection.CloseToTray.Value;
            //this.checkBoxMinimizeToTray.Checked = this.configSection.MinimizeToTray.Value;
            //this.checkBoxStartTimerAtApplicationStart.Checked = this.configSection.StartTimerAtApplicationStart.Value;
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Controller.OkayButtonClicked();

            //this.configSection.CloseToTray.Value = this.checkBoxCloseToTray.Checked;
            //this.configSection.MinimizeToTray.Value = this.checkBoxMinimizeToTray.Checked;
            //this.configSection.StartTimerAtApplicationStart.Value = this.checkBoxStartTimerAtApplicationStart.Checked;
        }

        public bool CloseToTrayChecked
        {
            get { return checkBoxCloseToTray.Checked; }
            set { checkBoxCloseToTray.Checked = value; }
        }

        public bool MinimizeToTrayChecked
        {
            get { return checkBoxMinimizeToTray.Checked; }
            set { checkBoxMinimizeToTray.Checked = value; }
        }

        public bool TimerInitiallyStartedChecked
        {
            get { return checkBoxStartTimerAtApplicationStart.Checked; }
            set { checkBoxStartTimerAtApplicationStart.Checked = value; }
        }

        public bool CloseToTrayEnabled
        {
            get { return checkBoxCloseToTray.Enabled; }
            set { checkBoxCloseToTray.Enabled = value; }
        }

        public bool MinimizeToTrayEnabled
        {
            get { return checkBoxMinimizeToTray.Enabled; }
            set { checkBoxMinimizeToTray.Enabled = value; }
        }

        public bool TimerInitiallyStartedEnabled
        {
            get { return checkBoxStartTimerAtApplicationStart.Enabled; }
            set { checkBoxStartTimerAtApplicationStart.Enabled = value; }
        }
    }
}
