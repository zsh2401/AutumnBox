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
            SLogger.Debug(TAG, content.ToString());
        }

        public void Fatal(object content)
        {
            SLogger.Fatal(TAG, content.ToString());
        }

        public void Info(object content)
        {
            SLogger.Info(TAG, content);
     
        }

        public void Warn(object content)
        {
            SLogger.Warn(TAG, content);
        }

        public void Warn(object content, Exception ex)
        {
            SLogger.Warn(TAG, content,ex);
        }

        public void Warn(Exception ex)
        {
            SLogger.Warn(TAG, ex);
        }
    }
}
