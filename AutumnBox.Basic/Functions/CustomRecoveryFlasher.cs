using AutumnBox.Basic.Arg;
using AutumnBox.Basic.Other;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    internal class CustomRecoveryFlasher:Function,IThreadFunctionRunner
    {
        public event EventsHandlers.FinishEventHandler FlashFinish;
        public CustomRecoveryFlasher():base(FunctionInitType.Adb) { }
        public void Run(IArgs args) {
            if (FlashFinish == null) { throw new EventNotBoundException(); }
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = "Recovery Flasher Thread";
            mainThread.Start(args);
        }
        private void _Run(object args) {
            FileArgs _args = (FileArgs)args;
            FlashFinish(adb.Execute(_args.deviceID,_args.files[0]));
        }
    }
}
