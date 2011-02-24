using MVCSharp.Core.Configuration.Tasks;
using MVCSharp.Core.Tasks;
using DustInTheWind.WindowsReboot.Properties;

namespace DustInTheWind.WindowsReboot.UI
{
    internal class MainTask : TaskBase
    {
        [InteractionPoint(typeof(WindowsRebootPresenter), true)]
        public const string MainView = "MainView";

        
        //private Customer currentCustomer = Customer.AllCustomers[0];

        //public event EventHandler CurrentCustomerChanged;

        private Settings settings;
        public Settings Settings
        {
            get { return settings; }
        }

        private DuckSettings duckSettings;
        public DuckSettings DuckSettings
        {
            get { return duckSettings; }
        }

        //private WindowsRebootModel model;
        //public WindowsRebootModel Model
        //{
        //    get { return model; }
        //    set { model = value; }
        //}

        public override void OnStart(object param)
        {
            settings = new Settings();
            duckSettings = new DuckSettings();
            Navigator.NavigateDirectly(MainView);
        }
    }
}
