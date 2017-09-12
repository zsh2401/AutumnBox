using AutumnBox.Basic.Devices;
using System;
using System.Diagnostics;
using static AutumnBox.Basic.AdbEnc.Events;

namespace AutumnBox.Basic.AdbEnc
{
    internal interface IAdbCommandExecuter:IDisposable {
        event DataReceivedEventHandler OutputDataReceived;
        event DataReceivedEventHandler ErrorDataReceived;
        event ExecuteStartHandler ExecuteStarted;
        Process CmdProcess { get; }
        OutputData Execute(string command);
        OutputData Execute(string id, string command);
    }
    internal interface IDevicesGetter {
        DevicesHashtable GetDevices();
    }
}
