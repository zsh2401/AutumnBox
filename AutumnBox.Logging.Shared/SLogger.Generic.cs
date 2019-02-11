using AutumnBox.Logging.Internal;
using AutumnBox.Logging.Management;
using System;
using System.Diagnostics;

namespace AutumnBox.Logging
{
    /// <summary>
    /// 泛型的静态日志器,可通过传入的泛型信息自动设置Category,秋之盒调试模式时可用
    /// </summary>
    /// <typeparam name="TCategory"></typeparam>
    public static class SLogger<TCategory>
    {
        private static string Category => typeof(TCategory).Name;

        /// <summary>
        /// 写出一条Debug级别日志
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), Category, message));
        }

        /// <summary>
        /// 写出一条带有异常信息的Debug级别日志,秋之盒调试模式时可用
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Debug(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), Category, message, e));
        }

        /// <summary>
        /// 写出一条CDebug级别的日志信息,当你的程序集使用非DEBUG编译时,对此函数的调用会被忽略
        /// </summary>
        /// <param name="message"></param>
        [Conditional("DEBUG")]
        public static void CDebug(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), Category, message));
        }
        /// <summary>
        /// 写出一条带异常CDebug级别的日志信息,当你的程序集使用非DEBUG编译时,对此函数的调用会被忽略
        /// </summary>
        /// <param name="message"></param>
        [Conditional("DEBUG")]
        public static void CDebug(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), Category, message, e));
        }
        /// <summary>
        /// 写出一条仅包含异常信息的日志
        /// </summary>
        /// <param name="e"></param>
        public static void Exception(Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Exception), Category, e));
        }
        /// <summary>
        /// 写出一条Info级别日志
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Info), Category, message));
        }
        /// <summary>
        /// 写出一条警告级别日志
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), Category, message));
        }
        /// <summary>
        /// 写出一条带有异常信息的警告级别日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void Warn(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), Category, message, e));
        }
    }
}
