using AutumnBox.Logging.Management;
using System;

namespace AutumnBox.Logging.Internal
{
    internal class Log : ILog
    {
        public DateTime Time { get; }

        public string Level { get; }

        public string Category { get; }

        public string Message { get; }

        public Log(string level, object category, object message)
        {
            Time = DateTime.Now;
            Level = level ?? "Info";
            Category = category?.ToString() ?? "Unknow";
            Message = message?.ToString();
        }
        public Log(string level, object category, object message, Exception e) : this(level, category, message)
        {
            message += $"{Environment.NewLine}{e.ToString()}";
        }
    }
}
