using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    public class RebootOperator : FunctionModule
    {
        enum Executer
        {
            Adb,
            Fastboot,
        }
        private Executer t;
        private RebootArgs args;

        public RebootOperator(RebootArgs rebootArgs)
        {
            this.args = rebootArgs;
            switch (args.nowStatus)
            {
                case DeviceStatus.FASTBOOT:
                    t = Executer.Fastboot;
                    break;
                default:
                    t = Executer.Adb;
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
                case Executer.Adb:
                    o = ae(command);
                    break;
                default:
                    o = fe(command);
                    break;
            }
            return o;
        }
    }
}
