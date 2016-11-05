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
using DustInTheWind.WindowsReboot.Commands;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Services;
using DustInTheWind.WindowsReboot.UiCommon;
using Timer = DustInTheWind.WindowsReboot.Core.Timer;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    internal class TrayIconViewModel : ViewModelBase
    {
        private readonly IUserInterface userInterface;
        private readonly Timer timer;
        private readonly string defaultText;
        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public LockComputerCommand LockComputerCommand { get; private set; }
        public LogOffCommand LogOffCommand { get; private set; }
        public SleepCommand SleepCommand { get; private set; }
        public HibernateCommand HibernateCommand { get; private set; }
        public RebootCommand RebootCommand { get; private set; }
        public ShutDownCommand ShutDownCommand { get; private set; }
        public PowerOffCommand PowerOffCommand { get; private set; }

        public TrayIconViewModel(IUserInterface userInterface, IRebootUtil rebootUtil, Timer timer)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (timer == null) throw new ArgumentNullException("timer");

            this.userInterface = userInterface;
            this.timer = timer;

            LockComputerCommand = new LockComputerCommand(userInterface, rebootUtil);
            LogOffCommand = new LogOffCommand(userInterface, rebootUtil);
            SleepCommand = new SleepCommand(userInterface, rebootUtil);
            HibernateCommand = new HibernateCommand(userInterface, rebootUtil);
            RebootCommand = new RebootCommand(userInterface, rebootUtil);
            ShutDownCommand = new ShutDownCommand(userInterface, rebootUtil);
            PowerOffCommand = new PowerOffCommand(userInterface, rebootUtil);

            defaultText = string.Format("{0} {1}", Application.ProductName, VersionUtil.GetVersionToString());

            Text = defaultText;
        }

        public void OnNotifyIconMouseClicked()
        {
            try
            {
                //userInterface.ShowMainWindow();
                //userInterface.HideTrayIcon();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        public void OnNotifyIconMouseMove()
        {
            try
            {
                Text = timer.IsRunning
                    ? TimerFormatter.Format(timer.TimeUntilAction)
                    : defaultText;
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        //#region Notify icon events

        ///// <summary>
        ///// Method called when the mouse is moved over the notify icon.
        ///// </summary>
        //internal void OnNotifyIconMouseMove()
        //{
        //    try
        //    {
        //        view.NotifyIconText = timer.IsRunning
        //            ? TimerFormatter.Format(timer.TimeUntilAction)
        //            : Title;
        //    }
        //    catch (Exception ex)
        //    {
        //        userInterface.DisplayError(ex);
        //    }
        //}

        ///// <summary>
        ///// Method called when the notify icon is clicked with the left mouse button.
        ///// </summary>
        //internal void OnNotifyIconMouseClicked()
        //{
        //    try
        //    {
        //        view.Show();
        //        view.WindowState = FormWindowState.Normal;
        //        view.NotifyIconVisible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        userInterface.DisplayError(ex);
        //    }
        //}

        ///// <summary>
        ///// Method called when the "Show" item of the notify icon menu was clicked.
        ///// </summary>
        //internal void OnNotifyIconShowClicked()
        //{
        //    try
        //    {
        //        view.Show();
        //        view.WindowState = FormWindowState.Normal;
        //        view.NotifyIconVisible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        userInterface.DisplayError(ex);
        //    }
        //}

        //#region internal void OnNotifyIconExitClicked()

        ///// <summary>
        ///// Method called when the "Exit" item of the notify icon menu was clicked.
        ///// </summary>
        //internal void OnNotifyIconExitClicked()
        //{
        //    try
        //    {
        //        exitRequested = true;
        //        view.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        userInterface.DisplayError(ex);
        //    }
        //}

        //#endregion

        //#endregion

        //#region Menu item events

        ///// <summary>
        ///// Method called when the "Go To Tray" item from the "File" menu is clicked.
        ///// </summary>
        //internal void OnMenuItemGoToTrayClicked()
        //{
        //    try
        //    {
        //        view.Hide();
        //        view.NotifyIconVisible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        userInterface.DisplayError(ex);
        //    }
        //}

        ///// <summary>
        ///// Method called when the "Exit" item from the "File" menu is clicked.
        ///// </summary>
        //internal void OnMenuItemExitClicked()
        //{
        //    try
        //    {
        //        exitRequested = true;
        //        view.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        userInterface.DisplayError(ex);
        //    }
        //}

        //#endregion
    }
}
