using AutumnBox.Basic.Arg;
using AutumnBox.Basic.DebugTools;
using AutumnBox.Basic.Other;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    internal class CustomRecoveryFlasher:Function,IThreadFunctionRunner
    {
        public string TAG = "CustomRecoveryFlasher";
        public event EventsHandlers.SimpleFinishEventHandler FlashFinish;
        public CustomRecoveryFlasher():base(FunctionInitType.Fastboot) { }
        public void Run(IArgs args) {
            if (FlashFinish == null) { throw new EventNotBoundException(); }
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = "Recovery Flasher Thread";
            mainThread.Start(args);
        }
        private void _Run(object args) {
            FileArgs _args = (FileArgs)args;
            Log.d(TAG,_args.deviceID);
            fastboot.Execute(_args.deviceID, $"flash recovery  \"{_args.files[0]}\"");
            fastboot.Execute(_args.deviceID, $"boot \"{_args.files[0]}\"");
            FlashFinish();
        }
    }
}
