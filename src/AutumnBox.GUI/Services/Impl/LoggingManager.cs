using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILoggingManager))]
    sealed class LoggingManager : ILoggingManager
    {
        public ICoreLogger CoreLogger => Logging.Management.LoggingManager.CoreLogger;

        public void Initialize()
        {
            _ = Logging.Management.LoggingManager.CoreLogger;
        }
    }
}
