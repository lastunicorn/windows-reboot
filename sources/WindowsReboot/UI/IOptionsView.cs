using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DustInTheWind.WindowsReboot.UI
{
    internal interface IOptionsView
    {
        bool CloseToTrayChecked { get; set; }
        bool MinimizeToTrayChecked { get; set; }
        bool TimerInitiallyStartedChecked { get; set; }

        bool CloseToTrayEnabled { get; set; }
        bool MinimizeToTrayEnabled { get; set; }
        bool TimerInitiallyStartedEnabled { get; set; }
    }
}
