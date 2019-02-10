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
            Level = level ?? throw new ArgumentNullException(nameof(level));
            Category = category?.ToString() ?? throw new ArgumentNullException(nameof(category));
            Message = message?.ToString() ?? throw new ArgumentNullException(nameof(message));
        }
        public Log(string level, object category, object message, Exception e) : this(level, category, message)
        {
            message += $"{Environment.NewLine}{e.ToString()}";
        }
    }
}
