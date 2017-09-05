using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    public class RebootOperator : FunctionModule
    {
        private RebootArgs args;
        public static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.All;
        public RebootOperator(RebootArgs rebootArgs) : base(RequiredDeviceStatus){
            this.args = rebootArgs;
            var d = this.ToString().Split('.');
            TAG = d[d.Length - 1];
        }
        protected override void MainMethod()
        {
            IAdbCommandExecuter commandExecuter;
            string command;
            switch (args.nowStatus)
            {
                case DeviceStatus.FASTBOOT:
                    commandExecuter = fastboot;
                    break;
                default:
                    commandExecuter = adb;
                    break;
            }
            if (args.rebootOption == RebootOptions.Bootloader)
            {
                command = "reboot-bootloader";
            }
            else if (args.rebootOption == RebootOptions.System)
            {
                command = "reboot";
            }
            else if (args.nowStatus != DeviceStatus.FASTBOOT && args.rebootOption == RebootOptions.Recovery)
            {
                command = "reboot recovery";
            }
            else
            {
                throw new System.Exception();
            }
            var o = commandExecuter.Execute(DeviceID, command);
            OnFinish(this, new FinishEventArgs() { OutputData =o});
        }
    }
}
