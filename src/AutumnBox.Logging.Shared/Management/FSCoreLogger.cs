using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
//    /// <summary>
//    ///基于文件系统的核心日志器
//    /// </summary>
//    public class FSCoreLogger : ICoreLogger
//    {
//        private readonly object _writeLock = new object();
//        private FileStream fs;
//        private StreamWriter sw;

//        private Action<string> writer;

//        /// <summary>
//        /// 获取所有的log
//        /// </summary>
//        public ObservableCollection<ILog> Logs { get; private set; } = new ObservableCollection<ILog>();

//        /// <summary>
//        /// 初始化并开始工作
//        /// </summary>
//        public void Initialize(ICoreLoggerInitializeArgs args)
//        {
//            writer = args.Writer;
//            fs = args.LogFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
//            sw = new StreamWriter(fs)
//            {
//                AutoFlush = true
//            };
//        }

//        /// <summary>
//        /// Log内部实现
//        /// </summary>
//        /// <param name="log"></param>
//        private void LogInternal(ILog log)
//        {
//            lock (_writeLock)//同步锁
//            {
//#if !DEBUG
//                  LoggingManager.WriteLine(log.ToFormatedString());
//#endif
//                Logs.Add(log);
//                sw.WriteLine(log.ToFormatedString());
//            }
//        }

//        /// <summary>
//        /// 处理日志
//        /// </summary>
//        /// <param name="log"></param>
//        public void Log(ILog log)
//        {
//#if DEBUG
//            writer?.Invoke(log.ToFormatedString());
//#endif
//            Task.Run(() =>
//            {
//                LogInternal(log);
//            });
//        }

//        #region IDisposable Support
//        private bool disposedValue = false; // 要检测冗余调用

//        /// <summary>
//        /// 内部释放实现
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
//                    sw.Dispose();
//                    fs.Dispose();
//                    // TODO: 释放托管状态(托管对象)。
//                }
//                sw = null;
//                fs = null;
//                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
//                // TODO: 将大型字段设置为 null。

//                disposedValue = true;
//            }
//        }

//        /// <summary>
//        /// 终结器
//        /// </summary>
//        ~FSCoreLogger()
//        {
//            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
//            Dispose(false);
//        }

//        /// <summary>
//        /// 释放器
//        /// </summary>
//        public void Dispose()
//        {
//            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
//            Dispose(true);
//            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
//            GC.SuppressFinalize(this);
//        }
//        #endregion
    //}
}
