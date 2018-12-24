using System;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 日志器
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// TAG
        /// </summary>
        string Tag { get; }
        /// <summary>
        /// 只有在开启秋之盒调试模式时此方法才会有效
        /// </summary>
        /// <param name="msg"></param>
        void Debug(string msg);
        /// <summary>
        /// Info级别
        /// </summary>
        /// <example>
        /// Logger.Info("Hello!");
        /// </example>
        /// <param name="msg"></param>
        void Info(string msg);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);
        /// <summary>
        /// 带有异常的警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Warn(string msg, Exception ex);
        /// <summary>
        /// 致命,慎用
        /// </summary>
        /// <param name="msg"></param>
        void Fatal(string msg);
    }
}
