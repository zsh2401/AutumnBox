using System.Collections.Generic;

namespace AutumnBox.Logging.Management
{
    public interface ILoggingStation
    {
        IEnumerable<ILog> Logs { get; }
        void Log(ILog log);
    }
}
