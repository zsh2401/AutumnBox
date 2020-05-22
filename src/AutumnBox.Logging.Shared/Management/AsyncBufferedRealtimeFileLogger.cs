#nullable enable
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
    /// (存在问题,析构时无法将剩下的日志输出)缓冲的文件系统核心日志器
    /// </summary>
    public class AsyncBufferedRealtimeFileLogger : CoreLoggerBase
    {
        private ConcurrentQueue<ILog> buffer = new ConcurrentQueue<ILog>();
        private readonly FileStream fs;
        private readonly StreamWriter sw;

        /// <summary>
        /// 构建 AsyncBufferedFSCoreLogger 的新实例
        /// </summary>
        public AsyncBufferedRealtimeFileLogger(FileStream _fs)
        {
            fs = _fs;
            sw = new StreamWriter(fs)
            {
                AutoFlush = true
            };
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
                while (buffer.TryDequeue(out ILog log))
                {
                    sw.WriteLine(log.ToFormatedString());
                }
                sw.Flush();
                fs.Flush();

                sw.Dispose();
                fs.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
