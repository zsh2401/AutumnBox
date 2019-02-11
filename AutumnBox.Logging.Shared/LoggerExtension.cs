using System;
using System.Diagnostics;

namespace AutumnBox.Logging
{
    /// <summary>
    /// ILogger的拓展方法
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// 写出一条Debug级别日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Debug(this ILogger logger, object message)
        {
            logger.Log(nameof(Debug), message);
        }
        /// <summary>
        /// 写出一条带有异常信息的Debug级别日志,秋之盒调试模式时可用
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Debug(this ILogger logger, object message, Exception e)
        {
            logger.Log(nameof(Debug), message, e);
        }
        /// <summary>
        /// 写出一条仅包含异常信息的日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="e"></param>
        public static void Exception(this ILogger logger, Exception e)
        {
            logger.Log(nameof(Exception), e);
        }
        /// <summary>
        /// 写出一条Info级别日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Info(this ILogger logger, object message)
        {
            logger.Log(nameof(Info), message);
        }
        /// <summary>
        /// 写出一条警告级别日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Warn(this ILogger logger, object message)
        {
            logger.Log(nameof(Warn), message);
        }
        /// <summary>
        /// 写出一条带有异常信息的警告级别日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Warn(this ILogger logger, object message, Exception e)
        {
            Trace.WriteLine("wtf!!!");
            logger.Log(nameof(Warn), message, e);
        }
    }
}
