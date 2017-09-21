using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public interface IAdbCommandExecuter
    {
        event ProcessStartEventHandler ProcessStarted;
        event DataReceivedEventHandler OutputDataReceived;
        event DataReceivedEventHandler ErrorDataReceived;
        void ExecuteWithDevice(string id, string command, out OutputData o, ExeType type);
        OutputData ExecuteWithDevice(string id, string command, ExeType type);
        void ExecuteWithoutDevice(string command, out OutputData o, ExeType type);
        OutputData ExecuteWithoutDevice(string command, ExeType type);
    }
}
