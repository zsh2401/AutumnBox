using AutumnBox.Basic.Devices;

namespace AutumnBox.Basic.AdbEnc
{
    internal interface ITools
    {
        DevicesHashtable GetDevices();
    }
    internal interface IAdbCommandExecuter {
        OutputData Execute(string command);
        OutputData Execute(string id, string command);
    }
}
