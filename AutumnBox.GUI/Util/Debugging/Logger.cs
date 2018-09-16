using System;

namespace AutumnBox.GUI.Util.Debugging
{
    internal class Logger : ILogger
    {
        protected string TAG { get; set; }

        public Logger(string tag)
        {
            TAG = tag;
        }

        public void Debug(object content)
        {
            LoggingStation.Instance.Log(TAG, "Debug", content.ToString());
        }

        public void Fatal(object content)
        {
            LoggingStation.Instance.Log(TAG, "Fatal", content.ToString());
        }

        public void Info(object content)
        {
            LoggingStation.Instance.Log(TAG, "Info", content.ToString());
        }

        public void Warn(object content)
        {
            LoggingStation.Instance.Log(TAG, "Warn", content.ToString());
        }

        public void Warn(object content, Exception ex)
        {
            LoggingStation.Instance.Log(TAG, "Warn", content + Environment.NewLine + ex);
        }

        public void Warn(Exception ex)
        {
            LoggingStation.Instance.Log(TAG, "Warn", ex.ToString());
        }
    }
}
