using AutumnBox.Logging.Internal;
using AutumnBox.Logging.Management;
using System;
using System.Diagnostics;

namespace AutumnBox.Logging
{
    /// <summary>
    /// 静态的Logger类
    /// </summary>
    public static class SLogger
    {
        /// <summary>
        /// 写出一条Debug级别的日志信息,开启秋之盒调试模式才可显示
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        public static void Debug(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), category, message));
        }
        /// <summary>
        /// 写出一条带异常信息的Debug级别的日志信息,开启秋之盒调试模式才可显示
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Debug(object category, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), category, message, e));
        }

        /// <summary>
        /// 写出一条CDebug级别的日志信息,当你的程序集使用非DEBUG编译时,对此函数的调用会被忽略
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        [Conditional("DEBUG")]
        public static void CDebug(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), category, message));
        }

        /// <summary>
        /// 写出一条带异常CDebug级别的日志信息,当你的程序集使用非DEBUG编译时,对此函数的调用会被忽略
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        [Conditional("DEBUG")]
        public static void CDebug(object category, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), category, message, e));
        }
        /// <summary>
        /// 写出一条仅包含异常信息的日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="e"></param>
        public static void Exception(object category, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Exception), category, e));
        }
        /// <summary>
        /// 写出一条Info级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        public static void Info(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Info), category, message));
        }
        /// <summary>
        /// 写出一条警告级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        public static void Warn(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), category, message));
        }
        /// <summary>
        /// 写出一条带有异常信息的警告级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Warn(object category, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), category, message, e));
        }
    }
}
