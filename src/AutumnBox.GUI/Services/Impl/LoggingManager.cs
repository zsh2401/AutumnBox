using AutumnBox.GUI.Services.Impl.Debugging;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILoggingManager))]
    sealed class LoggingManager : ILoggingManager
    {
        public ILoggingStation LoggingStation => loggingStation;
        private readonly ZhangBeiHaiLoggingStation loggingStation = new ZhangBeiHaiLoggingStation();

        public void Initialize()
        {
            loggingStation.Work();
            Logging.Management.LoggingManager.SetLogStation(loggingStation);
        }
    }
}
