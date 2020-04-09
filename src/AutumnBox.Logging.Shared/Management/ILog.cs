using System;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 日志数据结构
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 日志时间
        /// </summary>
        DateTime Time { get; }
        /// <summary>
        /// 日志级别
        /// </summary>
        string Level { get; }
        /// <summary>
        /// 日志发送者/类别/标签
        /// </summary>
        string Category { get; }
        /// <summary>
        /// 日志具体信息
        /// </summary>
        string Message { get; }
    }
}
