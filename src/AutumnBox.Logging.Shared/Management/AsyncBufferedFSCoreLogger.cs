/*

* ==============================================================================
*
* Filename: BufferedFSCoreLogger
* Description: 
*
* Version: 1.0
* Created: 2020/4/27 1:27:36
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 缓冲的文件系统核心日志器
    /// </summary>
    public class AsyncBufferedFSCoreLogger : CoreLoggerBase
    {
        private ConcurrentQueue<ILog> buffer = new ConcurrentQueue<ILog>();
        private readonly FileStream fs;
        private readonly StreamWriter sw;

        /// <summary>
        /// 指示日志文件的存放文件夹
        /// </summary>
        public static DirectoryInfo LogsDirectory
        {
            get
            {
                _logsDirectory ??= new DirectoryInfo("logs");
                if (!_logsDirectory.Exists)
                {
                    _logsDirectory.Create();
                }
                return _logsDirectory;
            }
        }
        private static DirectoryInfo _logsDirectory;

        /// <summary>
        /// 构建缓冲的记录到文件的核心日志器
        /// </summary>
        public AsyncBufferedFSCoreLogger()
        {
            string logFileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
            FileInfo logFile = new FileInfo(Path.Combine(LogsDirectory.FullName, logFileName));
            fs = logFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
            sw = new StreamWriter(fs);
            Task.Run(() =>
            {
                Thread.CurrentThread.Name = "BufferedFSCoreLogger Main Thread";
                Loop();
            });
        }

        /// <summary>
        /// 指示是否应停止循环
        /// </summary>
        private bool loopCancelled = false;

        /// <summary>
        /// 循环读取内存缓冲区的日志,并写入到文件系统
        /// </summary>
        private void Loop()
        {
            while (!loopCancelled)
            {
                while (buffer.TryDequeue(out ILog log))
                {
                    sw.WriteLine(log.ToFormatedString());
                }
                Thread.Sleep(10);//经过测试,性能最优的间隔
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="log"></param>
        public override void Log(ILog log)
        {
            buffer.Enqueue(log);//令人难以置信的是,不使用异步代码反而更快
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            loopCancelled = true;
            if (disposing)
            {
                sw.Dispose();
                fs.Dispose();
            }
            buffer = null;
            base.Dispose(disposing);
        }
    }
}
