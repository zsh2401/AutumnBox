/*

* ==============================================================================
*
* Filename: BufferedFSCoreLogger
* Description: 
*
* Version: 1.0
* Created: 2020/4/25 17:40:59
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    class BufferedFSCoreLogger : CoreLoggerBase
    {
        private ConcurrentQueue<ILog> threadSafeBuffer = new ConcurrentQueue<ILog>();
        private FileStream fs;
        private StreamWriter sw;

        private bool _cancelled = false;
        protected override void OnInitialize(ICoreLoggerInitializeArgs args)
        {
            base.OnInitialize(args);
            fs = args.LogFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
            sw = new StreamWriter(fs);
            Task.Run(() =>
            {
                Thread.CurrentThread.Name = "BufferedFSCoreLogger Main Thread";
                LoggingLoop();
            });
        }

        protected override void DisposeManagedResource()
        {
            base.DisposeManagedResource();
            _cancelled = true;
            sw.Dispose();
            fs.Dispose();
        }

        protected override void HandleLog(ILog log)
        {
            Task.Run(() =>
            {
                threadSafeBuffer.Enqueue(log);
                ThreadSafeAdd(log);
            });
        }

        private void LoggingLoop()
        {
            int time = 1;
            while (!this._cancelled)
            {
                while (threadSafeBuffer.TryDequeue(out ILog logItem))
                {
                    sw.WriteLine(logItem.ToFormatedString());
                    Writer?.Invoke(logItem.ToFormatedString());
                }
                if (time % 102400 == 0)
                {
                    time = 1;
                    ThreadSafeResizeLogs();
                }
                Thread.Sleep(10);
            }
        }
    }
}
