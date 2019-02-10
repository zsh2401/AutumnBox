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
            if (category is string)
                Category = category.ToString();
            else if (category != null)
                Category = category.GetType().Name;
            else
                Category = "UnknowClass";
            Message = message?.ToString();
        }
        public Log(string level, object category, object message, Exception e) : this(level, category, message)
        {
            Message += $"{Environment.NewLine}{e.ToString()}";
        }
    }
}
