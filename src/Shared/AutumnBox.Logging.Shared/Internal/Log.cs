using AutumnBox.Logging.Management;
using System;

namespace AutumnBox.Logging.Internal
{
    /// <summary>
    /// 最普通的日志接口实现
    /// </summary>
    public class Log : ILog
    {
        private const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; }

        /// <summary>
        /// 日志发送者/分区
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// 具体信息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 构造一个普通的日志信息对象
        /// </summary>
        /// <param name="level"></param>
        /// <param name="category"></param>
        /// <param name="message"></param>
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

        /// <summary>
        /// 构造一个带异常信息的日志信息对象
        /// </summary>
        /// <param name="level"></param>
        /// <param name="category"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public Log(string level, object category, object message, Exception e) : this(level, category, message)
        {
            Message += $"{Environment.NewLine}{e?.ToString()}";
        }

        /// <summary>
        /// 转化为格式化字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ToFormatedString()
        {
            return $"[{Time.ToString(TIME_FORMAT)}][{Category}/{Level}]:{Message}";
        }

        /// <summary>
        /// 重写ToString(),并返回格式化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToFormatedString();
        }
    }
}
