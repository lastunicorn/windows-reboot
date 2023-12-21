using System.Windows.Forms;
using DustInTheWind.WindowsReboot.ConfigAccess;
using DustInTheWind.WindowsReboot.Core;
using DustInTheWind.WindowsReboot.Presentation;
using DustInTheWind.WindowsReboot.Presentation.MainWindow;
using DustInTheWind.WindowsReboot.Presentation.WorkerModel;
using DustInTheWind.WindowsReboot.Setup;
using DustInTheWind.WindowsReboot.UserAccess;
using WindowsReboot.SystemAccess;
using Timer = DustInTheWind.WindowsReboot.Core.Timer;

namespace DustInTheWind.WindowsReboot
{
    internal class WindowsRebootApplication
    {
        private readonly MainWindowCloseBehaviour mainWindowCloseBehaviour;
        private readonly MainWindowStateBehaviour mainWindowStateBehaviour;
        private readonly TrayIcon trayIcon;

        private readonly WindowsRebootForm mainWindow;

        public WindowsRebootApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainWindow = new WindowsRebootForm();
            UiDispatcher uiDispatcher = new UiDispatcher();
            WindowsRebootConfiguration windowsRebootConfiguration = new WindowsRebootConfiguration();

            UserInterface userInterface = new UserInterface(uiDispatcher, windowsRebootConfiguration)
            {
                MainForm = mainWindow
            };

            RebootUtil rebootUtil = new RebootUtil();
            Timer timer = new Timer();
            Action action = new Action(timer, rebootUtil);

            WorkerProvider workerProvider = new WorkerProvider(userInterface, timer, action);
            Workers workers = new Workers(workerProvider);

            ApplicationEnvironment applicationEnvironment = new ApplicationEnvironment(action, timer, workers, windowsRebootConfiguration);
            applicationEnvironment.Initialize();

            mainWindowCloseBehaviour = new MainWindowCloseBehaviour(mainWindow, applicationEnvironment, windowsRebootConfiguration, timer, userInterface);
            mainWindowStateBehaviour = new MainWindowStateBehaviour(mainWindow, userInterface, windowsRebootConfiguration);

            mainWindow.ViewModel = new WindowsRebootViewModel(userInterface, action, timer, windowsRebootConfiguration, applicationEnvironment);

            trayIcon = new TrayIcon
            {
                ViewModel = new TrayIconViewModel(userInterface, rebootUtil, timer, applicationEnvironment)
            };
        }

        public void Run()
        {
            Application.Run(mainWindow);
        }
    }
}