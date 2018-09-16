using System;

namespace AutumnBox.GUI.Util.Debugging
{
    public interface ILogger
    {
        void Debug(object content);
        void Info(object content);
        void Warn(object content);
        void Warn(object content, Exception ex);
        void Warn(Exception ex);
        void Fatal(object content);
    }
}
