using AutumnBox.GUI.Util;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging.Management;
using System;
using System.IO;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILoggingManager))]
    sealed class LoggingManager : ILoggingManager
    {
        public ILogsCollection Logs => Logging.Management.LoggingManager.Logs;
        public void Initialize()
        {
            var logger = new ConsoleLogger(true) + new AsyncFileLogger(OpenLogFileStream());
            Logging.Management.LoggingManager.Use(logger);
        }
        private FileStream OpenLogFileStream()
        {
            //Initlize directory
            var storageDir = this.GetComponent<IStorageManager>().StorageDirectory;
            var logsDir = new DirectoryInfo(Path.Combine(storageDir.FullName, "logs"));
            if (!logsDir.Exists) logsDir.Create();

            //Open file
            var logFileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
            var logFile = new FileInfo(Path.Combine(logsDir.FullName, logFileName));

            return logFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
        }
    }
}
