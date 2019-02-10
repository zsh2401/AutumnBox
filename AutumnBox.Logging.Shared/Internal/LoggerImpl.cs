using System;
using AutumnBox.Logging.Management;

namespace AutumnBox.Logging.Internal
{
    class LoggerImpl : ILogger
    {
        protected virtual string CategoryName { get; }
        public LoggerImpl(string categoryName)
        {
            CategoryName = categoryName ?? throw new System.ArgumentNullException(nameof(categoryName));
        }
        protected LoggerImpl() { }
        public void Log(string level, object message)
        {
            LoggingManager.LogStation.Log(new Log(level, CategoryName, message));
        }

        public void Log(string level, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(level, CategoryName, message, e));
        }
    }
}
