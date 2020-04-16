using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    public class FSCoreLogger : ICoreLogger
    {
        public ObservableCollection<ILog> Logs { get; private set; }

        private readonly ConcurrentQueue<ILog> safeLogQueue;
        private Task writeTask;
        private readonly CancellationTokenSource writeTokenSource;
        private readonly FileInfo logFile;

        public FSCoreLogger(FileInfo logFile)
        {
            Logs = new ObservableCollection<ILog>();
            safeLogQueue = new ConcurrentQueue<ILog>();
            writeTokenSource = new CancellationTokenSource();
            this.logFile = logFile ?? throw new ArgumentNullException(nameof(logFile));
        }
        public void Initialize()
        {
            if (writeTask?.Status == TaskStatus.Running)
            {
                throw new InvalidOperationException("FSCoreLogger is already started");
            }
            writeTask = Task.Run(() =>
            {
                using (var fs = logFile.Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        while (!writeTokenSource.IsCancellationRequested)
                        {
                            if (safeLogQueue.Any() && safeLogQueue.TryDequeue(out ILog log))
                            {
                                sw.WriteLine(log.ToFormatedString());
                            }
                        }
                    }
                }
            });
        }

        public void Log(ILog log)
        {
            safeLogQueue.Enqueue(log);
            Logs.Add(log);
            Console.WriteLine(log.ToFormatedString());
        }

        public void Dispose()
        {
            writeTokenSource.Cancel();
        }
    }
}
