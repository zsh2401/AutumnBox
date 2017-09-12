using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Functions.FunctionArgs;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    public class RebootOperator : FunctionModule
    {
        private RebootArgs args;
        public RebootOperator(RebootArgs rebootArgs) : base(ExecuterInitType.None)
        {
            this.args = rebootArgs;
            switch (args.nowStatus)
            {
                case DeviceStatus.FASTBOOT:
                    MainExecuter = new Fastboot();
                    break;
                default:
                    MainExecuter = new Adb();
                    break;
            }
        }
        protected override OutputData MainMethod()
        {
            string command;

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
            var o = MainExecuter.Execute(DeviceID, command);
            return o;
        }
    }
}
