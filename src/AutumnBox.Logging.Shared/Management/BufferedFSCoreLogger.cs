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
    public class BufferedFSCoreLogger : CoreLoggerBase
    {
        private ConcurrentQueue<ILog> buffer = new ConcurrentQueue<ILog>();
        private FileStream fs;
        private StreamWriter sw;
        private Action<string> writer;
        private DateTime programStartTime;
        public override void Initialize(ICoreLoggerInitializeArgs args)
        {
            programStartTime = Process.GetCurrentProcess().StartTime;
            fs = args.LogFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
            sw = new StreamWriter(fs);
            writer = args.Writer;
            Task.Run(() =>
            {
                Thread.CurrentThread.Name = "BufferedFSCoreLogger Main Thread";
                Loop();
            });
        }
        private bool loopCancelled = false;
        private void Loop()
        {
            while (!loopCancelled)
            {
                while (buffer.TryDequeue(out ILog log))
                {
                    var fmtString = GetString(log);
#if! DEBUG
                    writer(fmtString);
#endif
                    sw.WriteLine(fmtString);
                }
                Thread.Sleep(10);//经过测试,性能最优的间隔
            }
        }

        private string GetString(ILog log)
        {
            TimeSpan span = log.Time - programStartTime;
            string timeStr = $"{(int)span.TotalHours}:{span.Minutes}:{span.Seconds}:{span.Milliseconds}";
            return $"[{timeStr}][{log.Category}/{log.Level}]{log.Message}";
        }
        public override void Log(ILog log)
        {
#if DEBUG
            writer(GetString(log));
#endif
            buffer.Enqueue(log);//令人难以置信的是,不使用异步代码反而更快
        }

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
