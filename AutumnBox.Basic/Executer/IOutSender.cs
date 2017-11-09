using System.Diagnostics;

namespace AutumnBox.Basic.Executer
{
    public interface IOutSender
    {
        event DataReceivedEventHandler OutputDataReceived;
        event DataReceivedEventHandler ErrorDataReceived;
        event OutputReceivedEventHandler OutputReceived;
    }
}
