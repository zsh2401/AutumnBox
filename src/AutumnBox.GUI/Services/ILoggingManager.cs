using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Services
{
    interface ILoggingManager
    {
        ILoggingStation LoggingStation { get; }
        void AutoInit();
    }
}
