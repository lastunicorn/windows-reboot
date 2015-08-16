using System;
using DustInTheWind.WindowsReboot.Core.Config;

namespace DustInTheWind.WindowsReboot.Core
{
    public interface IUserInterface
    {
        void Dispatch(Action action);
        void DisplayAbout();
        void DisplayLicense();
        bool DisplayOptions(WindowsRebootConfigSection configSection);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayMessage(string message);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayErrorMessage(string message);

        /// <summary>
        /// Displays the exception in a frendlly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        void DisplayError(Exception ex);

        bool AskToClose(string message);
        bool Confirm(string message);
    }
}