using System;

namespace AutumnBox.Basic.Util.Debugging
{
    internal class Logger
    {
        private readonly string TAG = "";

        public Logger(string tag)
        {
            TAG = tag;
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public void Debug(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Debug);
        }

        public void Fatal(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Fatal);
        }

        public void Info(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Info);
        }

        public void Warn(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Warn);
        }

        public void Warn(object content, Exception ex)
        {
            LoggingStation.RaiseEvent(TAG, content + Environment.NewLine + ex, LogLevel.Warn);
        }

        public void Warn(Exception ex)
        {
            LoggingStation.RaiseEvent(TAG, ex, LogLevel.Warn);
        }
    }
}
