using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    ///基于文件系统的核心日志器
    /// </summary>
    public class FSCoreLogger : ICoreLogger
    {
        /// <summary>
        /// 获取所有的log
        /// </summary>
        public ObservableCollection<ILog> Logs { get; private set; }

        private readonly ConcurrentQueue<ILog> safeLogQueue;
        private Task writeTask;
        private readonly CancellationTokenSource writeTokenSource;
        private readonly FileInfo logFile;

        /// <summary>
        /// 构建基于文件系统的日志处理器
        /// </summary>
        /// <param name="logFile"></param>
        public FSCoreLogger(FileInfo logFile)
        {
            Logs = new ObservableCollection<ILog>();
            safeLogQueue = new ConcurrentQueue<ILog>();
            writeTokenSource = new CancellationTokenSource();
            this.logFile = logFile ?? throw new ArgumentNullException(nameof(logFile));
        }
        /// <summary>
        /// 初始化并开始工作
        /// </summary>
        public void Initialize()
        {
            if (writeTask?.Status == TaskStatus.Running)
            {
                throw new InvalidOperationException($"{nameof(FSCoreLogger)} is already running");
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
                            Thread.Sleep(100);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(ILog log)
        {
            safeLogQueue.Enqueue(log);
            Logs.Add(log);
            Console.WriteLine(log.ToFormatedString());
        }

        /// <summary>
        /// 析构并释放资源
        /// </summary>
        public void Dispose()
        {
            writeTokenSource.Cancel();
        }
    }
}
