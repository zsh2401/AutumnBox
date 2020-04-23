using AutumnBox.Logging.Internal;
using System;
using System.IO;
using System.Reflection;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 日志系统管理器
    /// </summary>
    public static class LoggingManager
    {
        /// <summary>
        /// 日志站
        /// </summary>
        public static ICoreLogger CoreLogger { get; set; }
        /// <summary>
        /// 日志文件夹
        /// </summary>
        public const string LOG_DIR = "logs";
        static LoggingManager()
        {
            var logFileName = DateTime.Now.ToString("yyy-MM-dd_HH-mm-ss") + ".log";
            var logFile = new FileInfo(Path.Combine(LOG_DIR, logFileName));
            CoreLogger = new FSCoreLogger(logFile);
            CoreLogger.Initialize();
        }
    }
}
