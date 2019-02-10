using AutumnBox.Logging.Internal;
using AutumnBox.Logging.Management;
using System;
using System.Diagnostics;

namespace AutumnBox.Logging
{
    public static class SLogger<TCategory>
    {
        private static string Category => typeof(TCategory).Name;

        public static void Debug(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), Category, message));
        }

        public static void Debug(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), Category, message, e));
        }

        [Conditional("DEBUG")]
        public static void CDebug(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), Category, message));
        }

        [Conditional("DEBUG")]
        public static void CDebug(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(CDebug), Category, message, e));
        }

        public static void Exception(Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Exception), Category, e));
        }

        public static void Info(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Info), Category, message));
        }

        public static void Warn(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), Category, message));
        }

        public static void Warn(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Warn), Category, message, e));
        }
    }
}
