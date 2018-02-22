using System.Diagnostics;

namespace AutumnBox.Basic.Executer
{
    public interface IOutputable
    {
        event OutputReceivedEventHandler OutputReceived;
    }
}
