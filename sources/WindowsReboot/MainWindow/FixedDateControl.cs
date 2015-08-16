using System.Windows.Forms;
using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    partial class FixedDateControl : UserControl
    {
        private FixedDateControlViewModel viewModel;

        public FixedDateControl()
        {
            InitializeComponent();
        }

        public FixedDateControlViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                dateTimePickerFixedDate.DataBindings.Clear();
                dateTimePickerFixedTime.DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                {
                    dateTimePickerFixedDate.Bind(x => x.Value, viewModel, x => x.Date, false, DataSourceUpdateMode.OnPropertyChanged);
                    dateTimePickerFixedTime.Bind(x => x.Value, viewModel, x => x.Time, false, DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }
    }
}
