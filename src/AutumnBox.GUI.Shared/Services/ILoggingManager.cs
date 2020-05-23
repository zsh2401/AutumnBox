using AutumnBox.Logging.Management;
using System.IO;

namespace AutumnBox.GUI.Services
{
    interface ILoggingManager
    {
        ILogsCollection Logs { get; }
        DirectoryInfo LogsDirectory { get; }
        void Initialize();
    }
}
