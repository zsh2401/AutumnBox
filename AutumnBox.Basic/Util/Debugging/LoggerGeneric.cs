/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:51:24 (UTC +8:00)
** desc： ...
*************************************************/

using System;

namespace AutumnBox.Basic.Util.Debugging
{
    internal class Logger<TSenderClass> : ILogger
    {
        private readonly string TAG;
        public Logger()
        {
            TAG = typeof(TSenderClass).Name;
        }
        public void Debug(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Debug);
        }
        public void Info(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Info);
        }
        public void Warn(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Warn);
        }
        public void Warn(object content,Exception ex)
        {
            LoggingStation.RaiseEvent(TAG, content + Environment.NewLine + ex, LogLevel.Info);
        }
        public void Warn(Exception ex)
        {
            LoggingStation.RaiseEvent(TAG, ex, LogLevel.Info);
        }
        public void Fatal(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Fatal);
        }
    }
}
