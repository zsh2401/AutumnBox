using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Debugging
{
    public class LogEventArgs : EventArgs
    {
        public string Tag { get; set; } = "Unknown class";
        public string Text { get; set; } = null;
        public LogLevel Level { get; set; } = LogLevel.Info;
        public LogEventArgs(string text)
        {
            this.Text = text;
        }
    }
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Fatal
    }
    public static class LoggingStation
    {
        public static event EventHandler<LogEventArgs> Logging;
        internal static void RaiseEvent(object senderOrTag, object content, LogLevel level = LogLevel.Info)
        {
            var args = new LogEventArgs(content.ToString())
            {
                Tag = senderOrTag?.ToString() ?? "UnknownClass",
                Level = level,
            };
            Logging?.Invoke(senderOrTag, args);
        }
    }
}
