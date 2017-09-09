using System.Diagnostics;

namespace AutumnBox.Basic.Functions
{
    public interface ICanGetRealTimeOut
    {
        event DataReceivedEventHandler OutputDataReceived;
        event DataReceivedEventHandler ErrorDataReceived;
    }
}
