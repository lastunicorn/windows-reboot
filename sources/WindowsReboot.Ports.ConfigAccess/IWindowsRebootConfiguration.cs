using DustInTheWind.WindowsReboot.Core;

namespace DustInTheWind.WindowsReboot.Ports.ConfigAccess
{
    public interface IWindowsRebootConfiguration
    {
        ScheduleTime ActionTime { get; set; }
        ActionType ActionType { get; set; }
        bool ForceClosingPrograms { get; set; }
        bool StartTimerAtApplicationStart { get; set; }
        bool CloseToTray { get; set; }
        bool MinimizeToTray { get; set; }
        void Save();
    }
}