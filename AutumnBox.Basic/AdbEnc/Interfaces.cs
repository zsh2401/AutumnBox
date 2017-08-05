using AutumnBox.Basic.Devices;

namespace AutumnBox.Basic.AdbEnc
{
    internal interface ITools
    {
        DevicesHashtable GetDevices();
    }
    internal interface ICommandExecuter {
        OutputData Execute(string command);
    }
}
