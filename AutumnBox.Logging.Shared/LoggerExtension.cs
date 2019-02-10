using System;
using System.Diagnostics;

namespace AutumnBox.Logging
{
    public static class LoggerExtension
    {
        [Conditional("DEBUG")]
        public static void Debug(this ILogger logger, object message)
        {
            logger.Log(nameof(Debug), message);
        }

        [Conditional("DEBUG")]
        public static void Debug(this ILogger logger, object message, Exception e)
        {
            logger.Log(nameof(Debug), message, e);
        }

        public static void Exception(this ILogger logger, Exception e)
        {
            logger.Log(nameof(Exception), e);
        }

        public static void Info(this ILogger logger, object message)
        {
            logger.Log(nameof(Info), message);
        }

        public static void Warn(this ILogger logger, object message)
        {
            logger.Log(nameof(Warn), message);
        }

        public static void Warn(this ILogger logger, object message, Exception e)
        {
            logger.Log(nameof(Warn), message, e);
        }
    }
}
