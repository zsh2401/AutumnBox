using AutumnBox.Logging.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutumnBox.Logging.Internal
{
    class SimpleLoggingStation : ILoggingStation
    {
        private const string LOG_TIME_FMT = "yy-MM-dd HH:mm:ss";
        public IEnumerable<ILog> Logs => logs;
        private readonly List<ILog> logs = new List<ILog>();
        public void Log(ILog log)
        {
            Debug.WriteLine(Format(log));
            logs.Add(log);
        }
        private string Format(ILog log)
        {
            return $"[{log.Time.ToString(LOG_TIME_FMT)}][{log.Category}/{log.Level}]:{log.Message}";
        }
    }
}
