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
            private bool disposedValue;

            public class LogsCollection : ObservableCollection<ILog>, ILogsCollection { }
            public LogsCollection Logs { get; private set; }
            public ICoreLogger InnerLogger { get; set; }
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
                    Logs.Add(log);
                }
                InnerLogger.Log(log);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        InnerLogger.Dispose();
                        // TODO: 释放托管状态(托管对象)
                    }

                    // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                    // TODO: 将大型字段设置为 null
                    disposedValue = true;
                }
            }

            // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
            ~CoreLoggerProxy()
            {
                // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
                Dispose(disposing: false);
            }

            public void Dispose()
            {
                // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
