using AutumnBox.Basic.Arg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    internal class XiaomiBootloaderRelocker : Function, IThreadFunctionRunner
    {
        public event EventsHandlers.FinishEventHandler RelockFinish;
        private string TAG = "Xiaomi Bootloader Relock";
        public XiaomiBootloaderRelocker() : base(Arg.FunctionInitType.Fastboot)
        {
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = TAG + " Thread";
        }
        public void Run(IArgs args)
        {
            mainThread.Start(args);
        }
        private void _Run(object arg)
        {
            Args _args = (Args)arg;
            RelockFinish?.Invoke(fastboot.Execute(_args.deviceID, " oem lock"));
        }
    }
}
