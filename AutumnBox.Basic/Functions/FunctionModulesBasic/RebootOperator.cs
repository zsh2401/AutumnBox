using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    public class RebootOperator : FunctionModule
    {
        enum ExecuterType
        {
            Adb,
            Fastboot,
        }
        private ExecuterType t;
        private RebootArgs args;

        public RebootOperator(RebootArgs rebootArgs)
        {
            this.args = rebootArgs;
            switch (args.nowStatus)
            {
                case DeviceStatus.FASTBOOT:
                    t = ExecuterType.Fastboot;
                    break;
                default:
                    t = ExecuterType.Adb;
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
            OutputData o;
            switch (t) {
                case ExecuterType.Adb:
                    o = Ae(command);
                    break;
                default:
                    o = Fe(command);
                    break;
            }
            return o;
        }
    }
}
