using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Util;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    public class XiaomiBootloaderRelocker : FunctionModule
    {
        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Fastboot;
        protected override void MainMethod()
        {
            var o = MainExecuter.Execute(DeviceID, " oem lock");
            OnFinish(this, new FinishEventArgs() { OutputData = o});
            Thread.Sleep(3000);
        }
    }
}
