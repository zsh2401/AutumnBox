using System;
using System.Collections.ObjectModel;
using System.IO;

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
        internal static ICoreLogger CoreLogger => proxy;

        /// <summary>
        /// 已记录的日志
        /// </summary>
        public static ILogsCollection Logs => proxy.Logs;

        /// <summary>
        /// 优化已记录日志
        /// </summary>
        public static void OptimizeLogsCollection() { }
        private static readonly CoreLoggerProxy proxy = new CoreLoggerProxy();
        static LoggingManager()
        {
            proxy.InnerLogger = new ConsoleLogger(false);
        }
        /// <summary>
        /// 使用某个日志器
        /// </summary>
        /// <param name="coreLogger"></param>
        public static void Use(ICoreLogger coreLogger)
        {
            if (coreLogger is null)
            {
                throw new ArgumentNullException(nameof(coreLogger));
            }

            proxy.InnerLogger = coreLogger;
        }

        /// <summary>
        /// 核心日志器代理
        /// </summary>
        private class CoreLoggerProxy : ICoreLogger
        {
            public class LogsCollection : ObservableCollection<ILog>, ILogsCollection { }
            public LogsCollection Logs { get; } = new LogsCollection();
            public ICoreLogger InnerLogger { get; set; }
            public void Dispose()
            {
                InnerLogger.Dispose();
            }

            public void Log(ILog log)
            {
                lock (Logs)
                {
                    Logs.Add(log);
                }
                InnerLogger.Log(log);
            }
        }
    }
}
