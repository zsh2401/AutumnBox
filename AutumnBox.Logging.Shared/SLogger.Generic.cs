using AutumnBox.Logging.Internal;
using AutumnBox.Logging.Management;
using System;
using System.Diagnostics;

namespace AutumnBox.Logging
{
    public static class SLogger<TCategory>
    {
        private static string Category => typeof(TCategory).Name;

        [Conditional("DEBUG")]
        public static void Debug(object message)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), Category, message));
        }

        [Conditional("DEBUG")]
        public static void Debug(object message, Exception e)
        {
            LoggingManager.LogStation.Log(new Log(nameof(Debug), Category, message, e));
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
