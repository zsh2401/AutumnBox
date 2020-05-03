using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Services
{
    interface ILoggingManager
    {
        ILogsCollection Logs { get; }
        void Initialize();
    }
}
