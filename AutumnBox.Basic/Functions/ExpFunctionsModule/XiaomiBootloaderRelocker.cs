using AutumnBox.Basic.Util;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    public class XiaomiBootloaderRelocker : FunctionModule
    {
        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Fastboot;
        public XiaomiBootloaderRelocker() : base(RequiredDeviceStatus)
        {
            var d = this.ToString().Split('.');
            TAG = d[d.Length - 1];
        }
        protected override void MainMethod()
        {
            var o = fastboot.Execute(DeviceID, " oem lock");
            OnFinish(this, new FinishEventArgs() { OutputData = o});
            Thread.Sleep(3000);
        }
    }
}
