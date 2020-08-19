using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

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
        public static ICoreLogger CoreLogger
        {
            get => proxy.InnerLogger; set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                proxy.InnerLogger = value;
            }
        }

        /// <summary>
        /// 已记录的日志
        /// </summary>
        public static ILogsCollection Logs => proxy.Logs;

        static readonly CoreLoggerProxy proxy = new CoreLoggerProxy();

        /// <summary>
        /// 使用某个日志器
        /// </summary>
        /// <param name="coreLogger"></param>
        [Obsolete("Use CoreLogger.Setter to instead of.")]
        public static void Use(ICoreLogger coreLogger)
        {
            CoreLogger = coreLogger;
        }

        /// <summary>
        /// 释放所有日志资源，不可恢复
        /// </summary>
        public static void Free()
        {
            proxy.Logs.Clear();
            proxy.Dispose();
        }

        /// <summary>
        /// 核心日志器代理
        /// </summary>
        private class CoreLoggerProxy : ICoreLogger
        {
            public class LogsCollection : ObservableCollection<ILog>, ILogsCollection { }
            public LogsCollection Logs { get; private set; }
            public ICoreLogger InnerLogger { get; set; } = new TraceLogger();
            public CoreLoggerProxy()
            {
                //用这种奇葩方法初始化是为了防止线程问题
                Task.WaitAll(Task.Run(() =>
                {
                    Logs = new LogsCollection();
                }));
            }

            public void Log(ILog log)
            {
                lock (Logs)
                {
                    try
                    {
                        Logs.Add(log);
                    }
                    catch { }
                }
                InnerLogger.Log(log);
            }

            public void Dispose()
            {
                InnerLogger.Dispose();
                InnerLogger = null;
            }
        }
    }
}
