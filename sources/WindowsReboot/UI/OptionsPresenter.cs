using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCSharp.Core;
using DustInTheWind.WindowsReboot.Properties;

namespace DustInTheWind.WindowsReboot.UI
{
    internal class OptionsPresenter : ControllerBase<OptionsTask, IOptionsView>
    {
        private Settings settings;
        public Settings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                UpdateToView();
            }
        }

        private void ClearView()
        {
            View.CloseToTrayChecked = false;
            View.MinimizeToTrayChecked = false;
            View.TimerInitiallyStartedChecked = false;
        }

        private void EnableView(bool enable)
        {
            View.CloseToTrayEnabled = enable;
            View.MinimizeToTrayEnabled = enable;
            View.TimerInitiallyStartedEnabled = enable;
        }

        private void UpdateToView()
        {
            if (settings == null)
            {
                ClearView();
                EnableView(false);
            }
            else
            {
                View.CloseToTrayChecked = settings.CloseToTray;
                View.MinimizeToTrayChecked = settings.MinimizeToTray;
                View.TimerInitiallyStartedChecked = settings.TimerInitiallyStarted;

                EnableView(true);
            }
        }

        private void UpdateFromView()
        {
            if (settings != null)
            {
                settings.CloseToTray = View.CloseToTrayChecked;
                settings.MinimizeToTray = View.MinimizeToTrayChecked;
                settings.TimerInitiallyStarted = View.TimerInitiallyStartedChecked;
            }
        }

        internal void ViewShown()
        {
            settings = Task.Settings;
            UpdateToView();
        }

        internal void OkayButtonClicked()
        {
            if (settings != null)
            {
                UpdateFromView();
                settings.Save();
            }
        }
    }
}
