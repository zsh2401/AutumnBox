using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.GUI.Util.Debugging
{
    internal class LoggingStation
    {
        public event EventHandler<LogEventArgs> Logging;
        public string CurrentLogged
        {
            get {
                throw new NotImplementedException();
            }
        }
        public static LoggingStation Instance { get; private set; }
        static LoggingStation()
        {
            Instance = new LoggingStation();
        }
        public void Log(string tag, string level, object content, Exception ex = null)
        {

        }
    }
}
