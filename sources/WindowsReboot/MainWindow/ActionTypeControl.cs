using System.Windows.Forms;

namespace DustInTheWind.WindowsReboot.MainWindow
{
    partial class ActionTypeControl : UserControl
    {
        private ActionTypeControlViewModel viewModel;

        public ActionTypeControlViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                comboBoxAction.DataSource = null;
                comboBoxAction.DataBindings.Clear();
                checkBoxForceAction.DataBindings.Clear();
                checkBoxDisplayActionWarning.DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                {
                    comboBoxAction.DataSource = viewModel.ActionTypes;
                    comboBoxAction.Bind(x => x.SelectedItem, viewModel, x => x.SelectedActionType, false, DataSourceUpdateMode.OnPropertyChanged);

                    checkBoxForceAction.Bind(x => x.Checked, viewModel, x => x.ForceAction, false, DataSourceUpdateMode.OnPropertyChanged);
                    checkBoxForceAction.Bind(x => x.Enabled, viewModel, x => x.ForceActionEnabled, false, DataSourceUpdateMode.OnPropertyChanged);
                    checkBoxDisplayActionWarning.Bind(x => x.Checked, viewModel, x => x.DisplayActionWarning, false, DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }

        public ActionTypeControl()
        {
            InitializeComponent();
        }

        private void comboBoxAction_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // hack: the ComboBox control does not write the binded values before the SelectedIndex event is raised. so, i force it to write.
            foreach (Binding dataBinding in comboBoxAction.DataBindings)
                dataBinding.WriteValue();

            viewModel.OnActionTypeChanged();
        }
    }
}
