using System.Diagnostics;

namespace AutumnBox.Basic.Executer
{
    public interface IOutSender
    {
        event OutputReceivedEventHandler OutputReceived;
    }
}
