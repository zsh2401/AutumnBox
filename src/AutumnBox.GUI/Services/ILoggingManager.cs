using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Services
{
    interface ILoggingManager
    {
        ICoreLogger CoreLogger { get; }
        void Initialize();
    }
}
