using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Debugging
{
    internal class LoggingStation : IDisposable
    {
        private const string LOG_INFO_FMT = "[{0}][{1}/{2}]: {3}";
        private const string LOG_FLODER = "..\\logs";
        private const string LOG_FILENAME_FORMAT = "yy_MM_dd__HH_mm_ss";
        public static LoggingStation Instance { get; private set; }
        public event EventHandler<LogEventArgs> Logging;
        public string CurrentLogged
        {
            get
            {
                return string.Join(Environment.NewLine, logged);
            }
        }
        private List<string> logged;
        private Queue<string> buffer;
        private FileStream fs;
        private StreamWriter sw;
        public string LogFile { get; set; }
        static LoggingStation()
        {
            Instance = new LoggingStation();
        }
        private LoggingStation()
        {
            buffer = new Queue<string>(25);
            logged = new List<string>();
        }
        public void Work()
        {
            if (LogFile == null)
            {
                LogFile = GetLogFileInfo();
            }
            fs = new FileStream(LogFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sw = new StreamWriter(fs)
            {
                AutoFlush = true
            };
            Task.Run(() =>
            {
                Loop();
            });
        }
        private static string GetLogFileInfo()
        {
            if (!Directory.Exists(LOG_FLODER))
            {
                Directory.CreateDirectory(LOG_FLODER);
            }
            string fileName = DateTime.Now.ToString(LOG_FILENAME_FORMAT) + ".log";
            string path = Path.Combine(LOG_FLODER, fileName);
            return path;
        }
        public void Log(string tag, string prefix, object content)
        {
            string logMessage = string.Format(LOG_INFO_FMT, tag, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"), prefix, content);
            buffer.Enqueue(logMessage);
            Logging?.Invoke(this, new LogEventArgs() { Content = logMessage });
        }
        private void Loop()
        {
            while (true)
            {
                if (buffer.Count == 0) continue;
                var line = buffer.Dequeue();
                TLog(line);
            }
        }
        private void TLog(string text)
        {
            Debugger.Log(3, null, text + Environment.NewLine);
            //Console.WriteLine(text);
            sw.WriteLine(text);
            logged.Add(text);
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
