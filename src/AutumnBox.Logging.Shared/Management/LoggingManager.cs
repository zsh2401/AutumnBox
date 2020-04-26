using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            get
            {
                if (_coreLogger == null)
                {
                    Use<BufferedFSCoreLogger>();
                }
                return _coreLogger;
            }
        }
        private static ICoreLogger _coreLogger;

        /// <summary>
        /// 日志文件夹
        /// </summary>
        public static DirectoryInfo DefaultLogsDirectory { get; }

        /// <summary>
        /// 日志文件
        /// </summary>
        private static FileInfo LogFile { get; }

        /// <summary>
        /// 静态构造器,初始化属性
        /// </summary>
        static LoggingManager()
        {
            DefaultLogsDirectory = new DirectoryInfo("logs");
            if (!DefaultLogsDirectory.Exists)
            {
                DefaultLogsDirectory.Create();
            }
            var logFileName = DateTime.Now.ToString("yyy-MM-dd_HH-mm-ss") + ".log";
            LogFile = new FileInfo(Path.Combine(DefaultLogsDirectory.FullName, logFileName));
        }

        /// <summary>
        /// 使用CoreLogger
        /// </summary>
        /// <typeparam name="TCoreLogger"></typeparam>
        public static void Use<TCoreLogger>(bool writeToStdOut = true) where TCoreLogger : ICoreLogger, new()
        {
            static void uselessWriter(string _) { }
            static void consoleWriter(string msg) => Console.WriteLine(msg);
            IEnumerable<ILog> pastLogs = _coreLogger?.Logs?.Any() == true ? _coreLogger.Logs.ToArray() : new ILog[0];
            if (_coreLogger != null)
            {
                _coreLogger.Dispose();
            }
            _coreLogger = new TCoreLogger();
            _coreLogger.Initialize(new CoreLoggerInitializeArgs(LogFile, DefaultLogsDirectory, writeToStdOut ? (Action<string>)consoleWriter : uselessWriter, pastLogs));
        }
    }
}
