using System;
using System.Diagnostics;

namespace AutumnBox.GUI.Util.Debugging
{
    public static class SGLogger<TSender>
    {
        [Conditional("DEBUG")]
        public static void Debug(object content)
        {
            LoggingStation.Instance.Log(typeof(TSender).Name, nameof(Debug), content);
        }
        public static void Info(object content)
        {
            LoggingStation.Instance.Log(typeof(TSender).Name, nameof(Info), content);
        }
        public static void Warn(object content)
        {
            LoggingStation.Instance.Log(typeof(TSender).Name, nameof(Warn), content);
        }
        public static void Warn(object content, Exception ex)
        {
            LoggingStation.Instance.Log(typeof(TSender).Name, nameof(Warn), content + Environment.NewLine + ex);
        }
        public static void Warn(Exception ex)
        {
            LoggingStation.Instance.Log(typeof(TSender).Name, nameof(Warn), ex);
        }
        public static void Fatal(object content)
        {
            LoggingStation.Instance.Log(typeof(TSender).Name, nameof(Fatal), content);
        }
    }
}
