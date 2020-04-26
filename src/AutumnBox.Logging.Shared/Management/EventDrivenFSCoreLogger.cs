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
    /// <summary>
    /// 缓冲的FSCoreLogger
    /// </summary>
    public class EventDrivenFSCoreLogger : CoreLoggerBase
    {
        private FileStream fs;
        private StreamWriter sw;
        private Action<string> writer;
        public override void Initialize(ICoreLoggerInitializeArgs args)
        {
            fs = args.LogFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
            sw = new StreamWriter(fs);
            LogsInner.CollectionChanged += LogsInner_CollectionChanged;
        }

        private readonly object writeLock = new object();
        private void LogsInner_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Task.Run(() =>
            {
                lock (writeLock)
                {

                    foreach (ILog item in e.NewItems)
                    {
                        var fmtStr = item.ToFormatedString();
                        writer?.Invoke(fmtStr);
                        sw.WriteLine(fmtStr);
                    }
                }
            });
        }

        public override void Log(ILog log)
        {
            _ = ThreadSafeOperateLogsInnerAsync(() =>
              {
                  LogsInner.Add(log);
              });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sw.Dispose();
                fs.Dispose();
            }
            LogsInner.CollectionChanged -= LogsInner_CollectionChanged;
            base.Dispose(disposing);
        }
    }
}
