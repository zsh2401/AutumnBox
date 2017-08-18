using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Arg;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    internal class RebootOperator : Function, IThreadFunctionRunner
    {
        public event EventsHandlers.FinishEventHandler RebootFinish;
        public RebootOperator() : base(FunctionInitType.All)
        {
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = "Reboot Operator";
        }
        public void Run(IArgs args)
        {
            mainThread.Start(args);
        }
        private void _Run(object args)
        {
            RebootArgs _args = (RebootArgs)args;
            IAdbCommandExecuter commandExecuter;
            string command;
            switch (_args.nowStatus)
            {
                case DeviceStatus.FASTBOOT:
                    commandExecuter = fastboot;
                    break;
                default:
                    commandExecuter = adb;
                    break;
            }
            if (_args.rebootOption == Arg.RebootOptions.Bootloader)
            {
                command = "reboot-bootloader";
            }
            else if (_args.rebootOption == Arg.RebootOptions.System)
            {
                command = "reboot";
            }
            else if (_args.nowStatus != DeviceStatus.FASTBOOT && _args.rebootOption == Arg.RebootOptions.Recovery)
            {
                command = "reboot recovery";
            }
            else {
                throw new System.Exception();
            }
            RebootFinish?.Invoke(commandExecuter.Execute(_args.deviceID,command));
        }
    }
}
