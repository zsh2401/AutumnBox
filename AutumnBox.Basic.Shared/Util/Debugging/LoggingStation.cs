using System;

namespace AutumnBox.Basic.Util.Debugging
{
    /// <summary>
    /// 日志事件参数
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; } = "Unknown class";
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; } = null;
        /// <summary>
        /// 级别
        /// </summary>
        public LogLevel Level { get; set; } = LogLevel.Info;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text"></param>
        internal LogEventArgs(string text)
        {
            this.Text = text;
        }
    }
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 最低级别
        /// </summary>
        Debug,
        /// <summary>
        /// Info
        /// </summary>
        Info,
        /// <summary>
        /// Warn
        /// </summary>
        Warn,
        /// <summary>
        /// Fatal
        /// </summary>
        Fatal
    }
    /// <summary>
    /// LoggingStation
    /// </summary>
    public static class LoggingStation
    {
        /// <summary>
        /// 当新的有日志时触发
        /// </summary>
        public static event EventHandler<LogEventArgs> Logging;
        internal static void RaiseEvent(object senderOrTag, object content, LogLevel level = LogLevel.Info)
        {
            var args = new LogEventArgs(content?.ToString())
            {
                Tag = senderOrTag?.ToString() ?? "UnknownClass",
                Level = level,
            };
            Logging?.Invoke(senderOrTag, args);
        }
    }
}
