using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Functions.Event;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    public class XiaomiBootloaderRelocker : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            var o = MainExecuter.Execute(DeviceID, " oem lock");
            return o;
        }
    }
}
