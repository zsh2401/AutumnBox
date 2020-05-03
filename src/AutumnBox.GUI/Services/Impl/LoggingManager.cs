using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILoggingManager))]
    sealed class LoggingManager : ILoggingManager
    {
        public ILogsCollection Logs => Logging.Management.LoggingManager.Logs;
        public void Initialize()
        {
            Logging.Management.LoggingManager.Use(new ConsoleLogger(true) + new AsyncBufferedFSCoreLogger());
        }
    }
}
