using AutumnBox.Basic.Devices;

namespace AutumnBox.Basic.AdbEnc
{
    internal interface IAdbCommandExecuter {
        OutputData Execute(string command);
        OutputData Execute(string id, string command);
    }
    internal interface IDevicesGetter {
        DevicesHashtable GetDevices();
    }
}
