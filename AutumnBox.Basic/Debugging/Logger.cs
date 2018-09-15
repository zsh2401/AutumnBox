/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:51:24 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.Basic.Debugging
{
    internal class Logger<TSenderClass>
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
        public void Fatal(object content)
        {
            LoggingStation.RaiseEvent(TAG, content, LogLevel.Fatal);
        }
    }
}
