using AutumnBox.Logging.Management;
using System;

namespace AutumnBox.GUI.Services.Impl.Debugging
{
    class LogEventArgs : EventArgs
    {
        public ILog Content { get; }
        public LogEventArgs(ILog content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
