using AutumnBox.GUI.Model;
using AutumnBox.GUI.Properties;
using AutumnBox.Logging.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AutumnBox.GUI.Util.Debugging
{
    [HeritableStation]
    internal class LoggingStation : ILoggingStation, IDisposable
    {
        public const string LOG_FLODER = "..\\logs";
        private const string LOG_FILENAME_FORMAT = "yy_MM_dd__HH_mm_ss";
        public IEnumerable<ILog> Logs => logged;
        public static LoggingStation Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoggingStation();
                }
                return _instance;
            }
        }
        private static LoggingStation _instance;
        public event EventHandler<LogEventArgs> Logging;
        private List<FormatLog> logged;
        private Queue<FormatLog> buffer;
        private FileStream fs;
        private StreamWriter sw;
        private FileInfo LogFile
        {
            get
            {
                if (_logFile == null)
                {
                    if (!Directory.Exists(LOG_FLODER))
                    {
                        Directory.CreateDirectory(LOG_FLODER);
                    }
                    string fileName = DateTime.Now.ToString(LOG_FILENAME_FORMAT) + ".log";
                    string path = Path.Combine(LOG_FLODER, fileName);
                    _logFile = new FileInfo(path);
                }
                return _logFile;
            }
        }
        private FileInfo _logFile;
        private LoggingStation()
        {
            buffer = new Queue<FormatLog>();
            logged = new List<FormatLog>();
        }
        public void Work()
        {
            fs = new FileStream(LogFile.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sw = new StreamWriter(fs)
            {
                AutoFlush = true
            };
            new Thread(Loop)
            {
                Name = "LoggingStation",
                IsBackground = true
            }.Start();
        }
        public void Log(ILog log)
        {
            buffer.Enqueue(new FormatLog(log));
        }
        private void Loop()
        {
            while (true)
            {
                while (buffer.Count > 0)
                {
                    Next();
                }
                Thread.Sleep(500);
            }
        }
        private void Next()
        {
            var log = buffer.Dequeue();
            if (log.Level.ToLower() == "debug" && !Settings.Default.DeveloperMode)
                return;
            try { Logging?.Invoke(this, new LogEventArgs(log)); } catch { }
            string format = log.Formated;
            Console.WriteLine(format);
            sw.WriteLine(format);
            logged.Add(log);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sw?.Dispose();
                    fs?.Dispose();
                }
                sw = null;
                fs = null;
                buffer = null;
                logged = null;

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~LoggingStation() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
