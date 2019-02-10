using AutumnBox.Logging.Internal;
using System.IO;
using System.Reflection;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggingManager
    {
        /// <summary>
        /// 日志站
        /// </summary>
        public static ILoggingStation LogStation { get; private set; } = new OnlyRecordLoggingStation();
        private static bool _locked = false;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="logStation"></param>
        /// <param name="_lock"></param>
        public static void SetLogStation(ILoggingStation logStation, bool _lock = false)
        {
            if (_locked) return;
            if (logStation.GetType().GetCustomAttribute(typeof(HeritableStationAttribute)) != null)
            {
                foreach (var log in LogStation.Logs)
                {
                    logStation.Log(log);
                }
            }
            LogStation = logStation;
            _locked = _lock;
        }
    }
}
