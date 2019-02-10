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
        public static void Debug(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), category, message));
        }

        public static void Debug(object category, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), category, message, e));
        }

        [Conditional("DEBUG")]
        public static void CDebug(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), category, message));
        }

        [Conditional("DEBUG")]
        public static void CDebug(object category, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), category, message, e));
        }

        public static void Exception(object category, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Exception), category, e));
        }

        public static void Info(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Info), category, message));
        }

        public static void Warn(object category, object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), category, message));
        }

        public static void Warn(object category, object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), category, message, e));
        }
    }
}
