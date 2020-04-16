using AutumnBox.Logging.Internal;
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
        static LoggingManager()
        {
            CoreLogger = new FSCoreLogger(new System.IO.FileInfo("test_log.log"));
            CoreLogger.Initialize();
        }
    }
}
