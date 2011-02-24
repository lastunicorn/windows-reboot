using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCSharp.Core.Configuration.Tasks;
using MVCSharp.Core.Tasks;
using DustInTheWind.WindowsReboot.Properties;

namespace DustInTheWind.WindowsReboot.UI
{
    internal class OptionsTask : TaskBase
    {
        [InteractionPoint(typeof(OptionsPresenter), true)]
        public const string Options = "Options";

        private Settings settings;
        public Settings Settings
        {
            get { return settings; }
        }

        public override void OnStart(object param)
        {
            object[] parameters = param as object[];

            if (parameters != null && parameters.Length > 0)
            {
                settings = parameters[0] as Settings;
            }

            Navigator.NavigateDirectly(Options);
        }
    }
}
